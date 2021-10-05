using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> _elementPrefabs;
    [SerializeField] int _random;
    bool _canSpawn = false;
    

    // Start is called before the first frame update
    void Start()
    {
        _random = Random.Range(0, _elementPrefabs.Count);
        GameObject _newElement = Instantiate(_elementPrefabs[_random], transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount < 1 && _canSpawn)
        {
            _canSpawn = false;
            StartCoroutine(spawnNewElement());
        }
    }

    IEnumerator spawnNewElement()
    {
        yield return new WaitForSeconds(3);
        _random = Random.Range(0, _elementPrefabs.Count);
        GameObject _newElement = Instantiate(_elementPrefabs[_random], transform);
        _newElement.transform.parent = this.transform;
        _canSpawn = true;
    }
}
