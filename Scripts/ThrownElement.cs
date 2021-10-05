using UnityEngine;
using System.Linq;

namespace Assets.Scripts
{
    public class ThrownElement : Element
    {
        Rigidbody _rigidBody;
        [SerializeField] float _force = 10;
        [SerializeField] GameObject _puddlePrefab;
        Element reactionPartner;

        float _startY;

        // Use this for initialization
        void Start()
        {
            _startY = transform.position.y;
            _rigidBody = GetComponent<Rigidbody>();
            _rigidBody.AddForce((transform.forward + Vector3.up) * _force, ForceMode.Impulse);

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            print(other);
            if (other.tag == "Ground")
            {
                GameObject puddle = Instantiate(_puddlePrefab, transform.position, transform.rotation);
                puddle.GetComponent<Puddle>().tag = "Element";
                puddle.GetComponent<Puddle>().SetElementID(_elementID);
                Destroy(gameObject);
            }
            else if (other.tag == "Element")
            {
                reactionPartner = other.GetComponent<Element>();
                _gameManager.ComputeReaction(_elementID, reactionPartner._elementID, transform.position);
                Destroy(gameObject);
                Destroy(other);
            }
        }
    }
}