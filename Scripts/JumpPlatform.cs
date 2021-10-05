using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform: MonoBehaviour
{
    GameObject player;
    [SerializeField] float jumpForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlatformDecayCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator PlatformDecayCoroutine()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            player.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            print("bounce!");
        }
    }
}
