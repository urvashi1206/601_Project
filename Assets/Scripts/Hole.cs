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
    private float speed = 8f;
    //public float r_timer; 
    //private Animator anim;
    void Start()
    {
        //anim = GetComponent<Animator>();
        //r_timer = 0;
        //Debug.Log(transform.position.z);
        //Debug.Log(Time.time);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time <= 5)
        {
            //Debug.Log(transform.position.z);
            //transform.Translate(Vector3.forward * speed * Time.deltaTime );
            transform.position += new Vector3(0, 0, -26.4f * Time.deltaTime / 5);
        }
        else if (Time.time <= 6)
        {
            transform.rotation = Quaternion.Euler(0, -90 * Time.deltaTime, 0) * transform.rotation;
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
        else if(Time.time <= 10) 
        {
            transform.position += new Vector3(15 * Time.deltaTime / 4, 0, 0);
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
