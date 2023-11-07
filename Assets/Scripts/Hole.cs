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
    private float timer;
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= 7)
        {
            transform.position += new Vector3(0, 0, -26.4f * Time.deltaTime / 7);
        }
        else if (timer <= 8)
        {
            transform.rotation = Quaternion.Euler(0, -90 * Time.deltaTime, 0) * transform.rotation;
        }
        else if (timer <= 15)
        {
            transform.position += new Vector3(17.6f * Time.deltaTime / 7, 0, 0);
        }
        else if (timer <= 16)
        {
            transform.rotation = Quaternion.Euler(0, 90 * Time.deltaTime, 0) * transform.rotation;
        }
        else if (timer <= 17)
        {
            transform.position += new Vector3(0, 0, -26.4f * Time.deltaTime / 7);
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
