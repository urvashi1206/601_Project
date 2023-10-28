using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    float timer = 0;
    public GameObject head;
    public GameObject EndSpot;
    void Start()
    {
        head = GameObject.Find("head");
        EndSpot = GameObject.Find("EndingSpot");
        //character facing endpoint
        Vector3 dir = EndSpot.transform.position;
        transform.LookAt(new Vector3(dir.x, transform.position.y, dir.z));
        head.transform.LookAt(dir);
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
                //character stand up
                transform.position += new Vector3(0, 1, 0);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                //character facing endpoint
                Vector3 dir = EndSpot.transform.position;
                transform.LookAt(new Vector3(dir.x, transform.position.y, dir.z));
                head.transform.LookAt(dir);
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<BoxCollider>().enabled = true;
            }
            timer = 0;
        }
    }
}
