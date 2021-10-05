using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10f, _rotateSpeed = 10f;
    [SerializeField] int _rediumCount, _yellowgenCount = 0;
    [SerializeField] float _rediumDecayTime, _yellowgenDecayTime = 0;
    [SerializeField] string _selectedElement = "";
    [SerializeField] int _selectedElementID;
    [SerializeField] string _selectedFlask;
    [SerializeField] int _selectedFlaskID;
    [SerializeField] Text _rediumText, _yellowgenText;
    [SerializeField] GameObject _flaskPrefab;
    [SerializeField] GameObject _thrownElementPrefab;
    bool _canThrowFlask = true;
    [SerializeField] float _flaskCooldown = 0.5f;
    [SerializeField] float _jumpForce = 10f;
    bool _canJump = true;
    [SerializeField] int _health = 100;
    [SerializeField] bool _playerAlive = true;
    InventoryManager _inventoryManager;
    [SerializeField] GameManager _gameManager;
    [SerializeField] UIManager _uiManager;
    GameObject _selectedPrefab;
    string _selectedName;
    Vector3 _lastPosition;
    Quaternion _lastRotation;

    [SerializeField] GameObject objective1;
    [SerializeField] GameObject objective2;
    [SerializeField] GameObject objective3;
    [SerializeField] GameObject objective4;
    [SerializeField] GameObject objective5;
    [SerializeField] GameObject landslide;

    [SerializeField] GameObject _flask1;
    [SerializeField] GameObject _flask2;


    void Start()
    {
        _inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        _gameManager = _gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {

        if (transform.position.y < -10f)
        {
            transform.position = _lastPosition;
            transform.rotation = _lastRotation;
            Damage(10);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _canJump = false;
        }

        if (Input.GetMouseButtonDown(0) && _canThrowFlask && _playerAlive)
        {
            ThrowEquippedItem();
        }
        if (Input.GetMouseButtonDown(1) && _canThrowFlask && _playerAlive)
        {
            ThrowEquippedItem2();
        }
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * verticalInput * _moveSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, horizontalInput, 0) * _rotateSpeed, Space.Self);
    }

    public void PickUpElement(string type, int ID)
    {
        if (_selectedElement == "")
        {
            _selectedElement = type;
            _selectedElementID = ID;
        }
        _inventoryManager.UpdateInventory("Element", type, 1);
        StartCoroutine(ElementDecayRoutine(type));
    }

    public void PickUpPotion(string type)
    {
        if (_selectedFlask == "")
        {
            _selectedFlask = type;
        }
        _inventoryManager.UpdateInventory("Potion", type, 1);
    }

    public void ThrowElement()
    {
        GameObject clone;
        clone = Instantiate(_selectedPrefab, transform.position + new Vector3(0, 2.5f, 0), transform.rotation);
        ThrownElement thrown = clone.gameObject.GetComponent<ThrownElement>();
        thrown.setElementID(_selectedElementID);
        _canThrowFlask = false;
        StartCoroutine(FlaskCooldownRoutine());
    }

    void ThrowEquippedItem()
    {
        
        Instantiate(_flask1, transform.position, transform.rotation);
        
    }
    void ThrowEquippedItem2()
    {

        Instantiate(_flask2, transform.position, transform.rotation);

    }

    public void ThrowFlask()
    {

        Instantiate(_flaskPrefab, transform.position + new Vector3(0, 2.5f, 0), transform.rotation);
        _canThrowFlask = false;
        StartCoroutine(FlaskCooldownRoutine());
    }

    IEnumerator ElementDecayRoutine(string type)
    {
        switch (type)
        {
            case "Redium":
                yield return new WaitForSeconds(_rediumDecayTime);
                _rediumCount--;
                Debug.Log("Redium count: " + _rediumCount);
                _rediumText.text = "Redium: " + _rediumCount;
                break;
            case "Yellowgen":
                yield return new WaitForSeconds(_yellowgenDecayTime);
                _yellowgenCount--;
                Debug.Log("Yellowgen count: " + _yellowgenCount);
                _yellowgenText.text = "Yellowgen: " + _yellowgenCount;
                break;
        }
    }

    IEnumerator FlaskCooldownRoutine()
    {
        yield return new WaitForSeconds(_flaskCooldown);
        _canThrowFlask = true;
    }

    IEnumerator JumpCooldownRoutine()
    {
        yield return new WaitForSeconds(1);
        _canJump = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground" || collision.transform.tag == "Puddle" || collision.transform.tag == "Ice")
        {
            _canJump = true;
        }
        else if (collision.transform.tag == "Water")
        {
            transform.position = _lastPosition;
            transform.rotation = _lastRotation;
            Damage(10);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Objective1")
        {
            _gameManager.nextStage(1);
        }
        if (collision.transform.tag == "Objective2")
        {
            _gameManager.nextStage(2);
        }
        if (collision.transform.tag == "Objective3")
        {
            _gameManager.nextStage(3);
        }
        if (collision.transform.tag == "Objective4")
        {
            _gameManager.nextStage(4);
        }
        if (collision.transform.tag == "Objective5")
        {
            _gameManager.nextStage(5);
        }
    }
  

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            _lastPosition = gameObject.transform.position;
            _lastRotation = gameObject.transform.rotation;
        }
    }

    public void Damage(int amount)
    {
        _health -= amount;
        if (_health <= 0)
        {
            _playerAlive = false;
        }
    }

    public string GetSelectedElement()
    {
        return _selectedElement;
    }

    public void SetSelection(string type)
    {
        Debug.Log(type);
        _selectedPrefab = _gameManager.GetPrefabByName(type);
        _selectedName = type;
    }

    public void ClearSelection()
    {
        //_selectedPrefab = null;
        //_selectedName = null;
    }
}
