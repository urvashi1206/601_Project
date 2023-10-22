using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    float timer = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Rigidbody>().velocity.magnitude < 0.001)
        {
            timer += Time.deltaTime;
            //UnityEngine.Debug.Log(timer);
        }
        else
        {
            timer = 0;
            //UnityEngine.Debug.Log("0");
        }
        if(timer > 1)
        {
            if (Vector3.Dot(transform.up, Vector3.up) < 0.99)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<BoxCollider>().enabled = false;
                transform.position += new Vector3(0, 1, 0);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                GetComponent<BoxCollider>().enabled = true;
                GetComponent<Rigidbody>().isKinematic = false;
            }
            timer = 0;
        }
    }
}
