using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour
{
    Rigidbody _rigidBody;
    bool _canSkip = true;
    int _skipCount = 0;
    float _floor;
    bool _active = false;
    [SerializeField] int _potionID;
    [SerializeField] float _force = 10;
    [SerializeField] int _maxSkips = 8;
    [SerializeField] GameObject _puddlePrefab;
    [SerializeField] GameObject _jumpPlatformPrefab;
    [SerializeField] float explosionRadius = 5.0f;
    [SerializeField] float explosionPower = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.AddForce((transform.forward + Vector3.up) * _force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < _floor && _canSkip && _skipCount < _maxSkips && _active && _potionID == 3)
        {
            Skip();
        }
        else if (transform.position.y < -5 || _skipCount == _maxSkips)
        {
            Destroy(gameObject);
        }
    }

    void Skip()
    {
        Vector3 position = new Vector3(transform.position.x, _floor, transform.position.z);
        GameObject puddle = Instantiate(_puddlePrefab, position, transform.rotation);
        puddle.GetComponent<Puddle>().SetSize(100 - _skipCount * 17);
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.AddForce((transform.forward + Vector3.up) * _force, ForceMode.Impulse);
        _canSkip = false;
        StartCoroutine(SkipTimerRoutine());
        _skipCount++;
    }

    public void Explode()
    {
        print("i'm exploding");
        ParticleSystem exp = GetComponent<ParticleSystem>();
        exp.Play();
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionPower, transform.position, explosionRadius, 3.0F);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground" && !_active && _potionID == 3)
        {
            _active = true;
            _floor = transform.position.y;
            Skip();
        } else if(other.tag == "Ground")
        {
            MeshRenderer puddleRenderer;
            GameObject puddle;
            switch (_potionID)
            {
                case 0:
                    puddle = Instantiate(_puddlePrefab, transform.position, Quaternion.identity);
                    puddleRenderer = puddle.GetComponent<MeshRenderer>();
                    puddleRenderer.material.SetColor("_Color", Color.blue);
                    break;
                case 1:
                    Explode();
                    break;
                case 2:
                    puddle = Instantiate(_puddlePrefab, transform.position, Quaternion.identity);
                    puddleRenderer = puddle.GetComponent<MeshRenderer>();
                    puddleRenderer.material.SetColor("_Color", Color.green);

                    break;
                case 3:
                    Skip();
                    //puddle = Instantiate(_puddlePrefab, transform.position, Quaternion.identity);
                    //puddleRenderer = puddle.GetComponent<MeshRenderer>();
                    //puddleRenderer.material.SetColor("_Color", Color.magenta);

                    break;
                case 4:
                    Instantiate(_jumpPlatformPrefab, transform.position, Quaternion.identity);
                    break;
                default: break;
            }
        }
    }

    IEnumerator SkipTimerRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        _canSkip = true;
    }

    public void SetFloor(float floor)
    {
        _floor = floor;
    }
}
