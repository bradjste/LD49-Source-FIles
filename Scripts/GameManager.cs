using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Player _player;
    [SerializeField] public List<GameObject> elements;
    [SerializeField] public List<GameObject> potions;
    [SerializeField] GameObject _puddlePrefab; 
    [SerializeField] GameObject _bombPotionPrefab;
    [SerializeField] GameObject _jumpPotionPrefab;
    [SerializeField] GameObject _puddlePotionPrefab;
    [SerializeField] GameObject _icePotionPrefab;
    [SerializeField] GameObject _hotTeaPotionPrefab;
    [SerializeField] GameObject _crystalliumElementPrefab;
    [SerializeField] GameObject _etherElementPrefab;
    [SerializeField] GameObject _springsilverElementPrefab;
    [SerializeField] GameObject _hydrogenumElementPrefab;
    [SerializeField] GameObject _myxolidiumElementPrefab;
    public bool gameOver = false;
    Dictionary<string, GameObject> _prefabs;

    [SerializeField] GameObject _storyBox;
    [SerializeField] Text _storyHeader;
    [SerializeField] Text _storyBody;

    [SerializeField] GameObject objective1;
    [SerializeField] GameObject objective2;
    [SerializeField] GameObject objective21;
    [SerializeField] GameObject objective22;
    [SerializeField] GameObject objective3;
    [SerializeField] GameObject objective4;
    [SerializeField] GameObject objective5;
    [SerializeField] GameObject landslide;

    [SerializeField] GameObject _arrow;


    private List<int[]> _reactions = new List<int[]>(){
        new int[] { 1, 2 },
        new int[] { 1, 4 },
        new int[] { 2, 4 },
        new int[] { 3, 4 },
        new int[] { 3, 5 }
    };


    // Start is called before the first frame update
    void Start()
    {
        _prefabs = new Dictionary<string, GameObject>();
        _player = GameObject.Find("Player").GetComponentInChildren<Player>();
        _prefabs.Add("Bomb", _bombPotionPrefab);
        _prefabs.Add("Jump", _jumpPotionPrefab);
        _prefabs.Add("Puddle_Platform", _puddlePotionPrefab);
        _prefabs.Add("Ice", _icePotionPrefab);
        _prefabs.Add("Hot_Tea", _hotTeaPotionPrefab);
        _prefabs.Add("Crystallium", _crystalliumElementPrefab);
        _prefabs.Add("Ether", _etherElementPrefab);
        _prefabs.Add("Springsilver", _springsilverElementPrefab);
        _prefabs.Add("Hydrogenum", _hydrogenumElementPrefab);
        _prefabs.Add("Myxolidium", _myxolidiumElementPrefab);


        //show first story message
        //set arrow heading
        //show first stage things
        showMessage("Objective 1", "Collect the sunflower seed from across the river. (Hint: mix elements to create potions and interact with the environment)");
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator waitForReaction()
    {
        yield return new WaitForSeconds(2);
    }
    public void ComputeReaction(int element_ID1, int element_ID2, Vector3 position)
    {
        print("reacting...");
        int[] elements = { element_ID1, element_ID2 };
        Array.Sort(elements);
        print(elements[0] + " " + elements[1]) ;
        print(_reactions[0]);
        // If the elements combine to make a potion
        for(int i=0; i<_reactions.Count; i++)
        {
            if (_reactions[i].SequenceEqual(elements))
            {
                Instantiate(_puddlePrefab, position, Quaternion.identity);
                StartCoroutine(waitForReaction());
                Instantiate(potions[i], position, Quaternion.identity);
                CreatePotion(elements, position);
                return;
            }
        };
        // If the 2 elements combined do not make a potion, they eventually explode and hurt the player
        
    }

    public void CreatePotion(int[] elements, Vector3 startPosition)
    {
        //Ice potion
        if (elements == _reactions[0])
        {
            Instantiate(potions[0], startPosition, Quaternion.identity);
        }
        //Explosion Potion
        else if (elements == _reactions[1])
        {
            Instantiate(potions[1], startPosition, Quaternion.identity);
        }
        //Healing Tea
        else if (elements == _reactions[2])
        {
            Instantiate(potions[2], startPosition, Quaternion.identity);
        }
        //Skipping Rock Platform
        else if (elements == _reactions[3])
        {
            Instantiate(potions[3], startPosition, Quaternion.identity);
        }
        //Jump Pad
        else if (elements == _reactions[4])
        {
            Instantiate(potions[4], startPosition, Quaternion.identity);
        }

    }

    public GameObject GetPrefabByName(string type)
    {
        return _prefabs[type];
    }

    public void nextStage(int stage)
    {
        //show story message
        //hide previous stage objects
        //show new stage objects
        //update arrow heading

        if(stage == 1)
        {
            objective1.SetActive(false);
            showMessage("Objective 2", "Head to the valley to plant the sunflower seed in the rich soil between the mountains.");
        }
        if(stage == 2)
        {
            objective21.SetActive(true);
            objective22.SetActive(true);
            showMessage("Objective 3", "Burn down the magic tree to collect the magic ashes while you wait for your flower to grow.");
        }
        if(stage == 3)
        {
            objective21.SetActive(false);
            objective22.SetActive(false);
            objective4.SetActive(true);
            landslide.SetActive(true);
            //hide sunflower mound
            //show sunflower
            //show landslide
            showMessage("Objective 4", "Go back to collect your sunflower");
        }
        if(stage == 4)
        {
            objective4.SetActive(false);
            showMessage("Objective 5", "Climb to the top of the mountain to collect the final element to get back home");
        }
        if(stage == 5)
        {
            objective5.SetActive(false);
            showMessage("Go Home!", "Combine the sunflower, ashes, and ice to get back home!");
        }
    }


    private void showMessage(string header, string message)
    {
        //set header
        //set text
        //display ui
        //set timer to remove
        _storyHeader.text = header;
        _storyBody.text = message;
        _storyBox.SetActive(true);
        StartCoroutine(hideMessage());
        
    }

    IEnumerator hideMessage()
    {
        yield return new WaitForSeconds(10);
        _storyBox.SetActive(false);

    }
}