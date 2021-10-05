using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] UIManager _uiManager;
    [SerializeField] List<string> _potionTypes;
    [SerializeField] List<string> _elementTypes;
    Dictionary<string, Dictionary<string,int>> _inventory;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = InitializeInventory();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateInventory(string type, string subtype, int amount)
    {

        int currentCount = _inventory[type][subtype];
        if (currentCount >= 0)
        {
            _inventory[type][subtype] = currentCount + amount;
        }
        else
        {
            _inventory[type][subtype] = 0;
        }
        _uiManager.UpdateItem(type, subtype, _inventory[type][subtype]);
        
    }

    Dictionary<string, Dictionary<string, int>> InitializeInventory()
    {
        Dictionary<string, Dictionary<string, int>> typeList = new Dictionary<string, Dictionary<string, int>>();
        typeList["Potion"] = new Dictionary<string, int>();
        foreach (string type in _potionTypes)
        {
            typeList["Potion"][type] = 0;
        }
        typeList["Element"] = new Dictionary<string, int>();
        foreach (string type in _elementTypes)
        {
            typeList["Element"][type] = 0;
        }
        return typeList;
    }
}
