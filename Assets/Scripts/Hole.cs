using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 8.0f;
    //private Animator anim;
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z >= -22)
        {
            //Debug.Log(transform.position.z);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.EulerAngles(0, 90 * Time.deltaTime, 0) * transform.rotation;
            //anim.ResetTrigger("OnReachingPosition");
            /*else
            {
                if (transform.position.z >= -24)
                {
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector3.forward * 0 * Time.deltaTime);
                }
            }*/
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlanetoTouch")
        {
            GameObject.Find("RocksAboveTheHole").GetComponent<Rigidbody>().useGravity = true;
            Destroy(collision.gameObject);
        }
    }
}
