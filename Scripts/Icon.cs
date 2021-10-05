using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    [SerializeField] GameObject _unselectedScrim;
    [SerializeField] GameObject _equippedBackdrop;
    [SerializeField] Text _count;
    [SerializeField] public string _type;
    [SerializeField] GameObject _player;
    public bool active = false;
    [SerializeField] GameObject _activeIcon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Unselect()
    {
        _unselectedScrim.SetActive(true);
    }

    public void Select()
    {
        _unselectedScrim.SetActive(false);
    }

    public void Equip()
    {
        _equippedBackdrop.SetActive(true);
        _player.GetComponent<Player>().SetSelection(_type);
    }

    public void Unequip()
    {
        _equippedBackdrop.SetActive(false);
    }

    public void Learn()
    {
        active = true;
        _activeIcon.SetActive(true);
    }

    public void Unlearn()
    {
        active = false;
        _activeIcon.SetActive(false);
        Unequip();
    }

    public void ChangeCount(int count)
    {
        _count.text = count + "";
    }
}
