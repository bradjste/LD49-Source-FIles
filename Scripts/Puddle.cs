using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    [SerializeField] float _shrink = 1;
    [SerializeField] int _elementID;
    float _percentSize = 100;



    void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale = new Vector3(transform.localScale.x - (_shrink * Time.deltaTime), transform.localScale.y, transform.localScale.z - (_shrink * Time.deltaTime));

        if (transform.localScale.x < 0.01)
        {
            Destroy(gameObject);
        }
    }

    public void SetSize(float size)
    {
        _percentSize = size;
    }

    public void SetElementID(int ID)
    {
        _elementID = ID;
    }
}
