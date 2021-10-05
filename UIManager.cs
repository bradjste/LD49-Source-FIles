using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] bool _potionMode = false;
    [SerializeField] List<Sprite> _sprites = new List<Sprite>();
    [SerializeField] GameObject _parentCanvas;
    List<Dictionary<string, string>> _potionTypes = new List<Dictionary<string, string>>();
    List<Dictionary<string, string>> _elementTypes = new List<Dictionary<string, string>>();
    [SerializeField] GameObject _elementCrystalliumIcon;
    [SerializeField] GameObject _elementEtherIcon;
    [SerializeField] GameObject _elementSpringsilverIcon;
    [SerializeField] GameObject _elementHydrogenumIcon;
    [SerializeField] GameObject _elementMyxolidiumIcon;
    [SerializeField] GameObject _potionJumpIcon;
    [SerializeField] GameObject _potionPuddlePlatformIcon;
    [SerializeField] GameObject _potionBombIcon;
    [SerializeField] GameObject _potionHotTeaIcon;
    [SerializeField] GameObject _potionIceIcon;
    List<Icon> _activeElementIcons = new List<Icon>();
    List<Icon> _activePotionIcons = new List<Icon>();
    Icon _equippedPotion;
    Icon _equippedElement;
    Dictionary<string, GameObject> _icons = new Dictionary<string, GameObject>();
    Icon _selectedIcon;
    int _elementIndex;
    int _potionIndex;
    [SerializeField] GameObject _player;
    // Use this for initialization
    void Start()
    {
        _icons.Add("Crystallium", _elementCrystalliumIcon);
        _icons.Add("Ether", _elementEtherIcon);
        _icons.Add("Springsilver", _elementSpringsilverIcon);
        _icons.Add("Hydrogenum", _elementHydrogenumIcon);
        _icons.Add("Myxolidium", _elementMyxolidiumIcon);
        _icons.Add("Jump", _potionJumpIcon);
        _icons.Add("Bomb", _potionBombIcon);
        _icons.Add("Puddle_Platform", _potionPuddlePlatformIcon);
        _icons.Add("Hot_Tea", _potionHotTeaIcon);
        _icons.Add("Ice", _potionIceIcon);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _potionMode = !_potionMode;
            UpdateMode();
            UnequipAllIcons();
            if (_potionMode)
            {
                if (_equippedPotion)
                {
                    _equippedPotion.Equip();
                }
            } else
            {
                if (_equippedElement)
                {
                    _equippedElement.Equip();
                }
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_potionMode)
            {
                if (_potionIndex == 0)
                {
                    _potionIndex = _activePotionIcons.Count-1;
                }
                else
                {
                    _potionIndex--;
                }
                _equippedPotion = _activePotionIcons[_potionIndex];
                UnequipAllIcons();
                _equippedPotion.Equip();
            } 
            else
            {
                if (_elementIndex == 0)
                {
                    _elementIndex = _activeElementIcons.Count-1;
                }
                else
                {
                    _elementIndex--;
                }
                _equippedElement = _activeElementIcons[_elementIndex];
                UnequipAllIcons();
                _equippedElement.Equip();
            }  
        } 
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (_potionMode)
            {
                if (_potionIndex == _activePotionIcons.Count-1)
                {
                    _potionIndex = 0;
                }
                else
                {
                    _potionIndex++;
                }
                _equippedPotion = _activePotionIcons[_potionIndex];
                UnequipAllIcons();
                _equippedPotion.Equip();
            }
            else
            {
                if (_elementIndex == _activeElementIcons.Count - 1)
                {
                    _elementIndex = 0;
                }
                else
                {
                    _elementIndex++;
                }
                _equippedElement = _activeElementIcons[_elementIndex];
                UnequipAllIcons();
                _equippedElement.Equip();
            }
        }
    }

    void UpdateMode()
    {
        if (_potionMode)
        {
            _elementCrystalliumIcon.GetComponent<Icon>().Unselect();
            _elementEtherIcon.GetComponent<Icon>().Unselect();
            _elementSpringsilverIcon.GetComponent<Icon>().Unselect();
            _elementHydrogenumIcon.GetComponent<Icon>().Unselect();
            _elementMyxolidiumIcon.GetComponent<Icon>().Unselect();
            _potionJumpIcon.GetComponent<Icon>().Select();
            _potionPuddlePlatformIcon.GetComponent<Icon>().Select();
            _potionBombIcon.GetComponent<Icon>().Select();
            _potionHotTeaIcon.GetComponent<Icon>().Select();
            _potionIceIcon.GetComponent<Icon>().Select();
        }
        else
        {
            _elementCrystalliumIcon.GetComponent<Icon>().Select();
            _elementEtherIcon.GetComponent<Icon>().Select();
            _elementSpringsilverIcon.GetComponent<Icon>().Select();
            _elementHydrogenumIcon.GetComponent<Icon>().Select();
            _elementMyxolidiumIcon.GetComponent<Icon>().Select();
            _potionJumpIcon.GetComponent<Icon>().Unselect();
            _potionPuddlePlatformIcon.GetComponent<Icon>().Unselect();
            _potionBombIcon.GetComponent<Icon>().Unselect();
            _potionHotTeaIcon.GetComponent<Icon>().Unselect();
            _potionIceIcon.GetComponent<Icon>().Unselect();
        }
    }

    public bool GetPotionMode()
    {
        return _potionMode;
    }

    void UnequipAllIcons()
    {
        foreach (Icon iconLoop in _activePotionIcons)
        {
            iconLoop.Unequip();
        }
        foreach (Icon iconLoop in _activeElementIcons)
        {
            iconLoop.Unequip();
        }
    }

    public void UpdateItem(string type, string name, int count)
    {
        Icon icon = GetIconByName(name).GetComponent<Icon>();
        if (!icon.active)
        {
            icon.Learn();
        }
        icon.ChangeCount(count);
        if (type == "Potion" && count > 0)
        {
            _potionMode = true;
            _equippedPotion = icon;
            _activePotionIcons.Add(icon);
        }
        else if (type == "Element" && count > 0)
        {
            _potionMode = false;
            _equippedElement = icon;
            _activeElementIcons.Add(icon);
        }

        if (count != 0)
        {
            UpdateMode();
            UnequipAllIcons();
            icon.Equip();
        }
        else
        {
            icon.Unlearn();
            if (type == "Element")
            {
                _activeElementIcons.Remove(icon);
            } else
            {
                _activePotionIcons.Remove(icon);
            }
            
            if (_potionMode)
            {
                if (_activePotionIcons.Count > 0)
                {
                    _player.GetComponent<Player>().SetSelection(icon._type);
                } else
                {
                    _player.GetComponent<Player>().ClearSelection();
                    icon.Unlearn();
                    _activePotionIcons.Remove(icon);
                }
                
            } 
            else
            
            {
                if (_activeElementIcons.Count > 0)
                {
                    _player.GetComponent<Player>().SetSelection(icon._type);
                }
                else
                {
                    _player.GetComponent<Player>().ClearSelection();
                    icon.Unlearn();
                    _activeElementIcons.Remove(icon);
                }

            }
            
            
        }
        
    }

    public void UpdateIconCount(string name, int count)
    {
        GetIconByName(name).GetComponent<Icon>().ChangeCount(count);
    }

    GameObject GetIconByName(string name)
    {
        return _icons[name];
    }
}