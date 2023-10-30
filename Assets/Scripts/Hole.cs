using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour
{
    // Start is called before the first frame update
    //private float speed = 8f;
    public ParticleSystem particleSystem;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time <= 5)
        {
            transform.position += new Vector3(0, 0, -26.4f * Time.deltaTime / 5);
        }
        else if (Time.time <= 6)
        {
            transform.rotation = Quaternion.Euler(0, -90 * Time.deltaTime, 0) * transform.rotation;
        }
        else if (Time.time <= 10)
        {
            transform.position += new Vector3(18 * Time.deltaTime / 4, 0, 0);
        }
        else if (Time.time <= 11)
        {
            transform.rotation = Quaternion.Euler(0, 90 * Time.deltaTime, 0) * transform.rotation;
        }
        else if (Time.time <= 17)
        {
            transform.position += new Vector3(0, 0, -26.6f * Time.deltaTime / 6);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlanetoTouch")
        {
            particleSystem.Play();
            GameObject.Find("RocksAboveTheHole").GetComponent<Rigidbody>().useGravity = true;
            Destroy(collision.gameObject);
        }
    }
}
