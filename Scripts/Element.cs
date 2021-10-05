using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    [SerializeField] string _type = "None";
    [SerializeField] public int _elementID = 0;
    InventoryManager _inventoryManager;
    // Start is called before the first frame update
    public GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().PickUpElement(_type, _elementID);
        }
        Destroy(gameObject);
    }

    private void Update()
    {

    }

    public int getElementID()
    {
        return _elementID;
    }

    public void setElementID(int ID)
    {
        _elementID = ID;
    }
}
