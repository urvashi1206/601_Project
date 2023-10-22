using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowMovement : MonoBehaviour
{
    private Camera m_Camera;
    public float angle;

    public GameObject scene;
    //private float rotateSpeed = 180f;

    private Ray _ray;
    private RaycastHit _hit;
    private bool coroutineIsFinished = true;
    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;
        character = GameObject.Find("character");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            _ray =m_Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, 1000f))
            {
                if (_hit.transform == transform)
                {
                    if (gameObject.CompareTag("X")) 
                    {
                        if (coroutineIsFinished)
                        {
                            StartCoroutine("DisableXButtonsBoxCollider");
                            StartCoroutine("DisableYButtonsBoxCollider");
                            StartCoroutine("DisableZButtonsBoxCollider");
                        }
                        else
                        {
                            //If the coroutine is still going on we stop it
                            StopCoroutine("DisableXButtonsBoxCollider");
                            StopCoroutine("DisableYButtonsBoxCollider");
                            StopCoroutine("DisableZButtonsBoxCollider");
                        }
                        Vector3 dir = new Vector3(0, 0, angle);
                        character.transform.LookAt(character.transform.position + dir);
                        scene.transform.rotation = Quaternion.Euler(angle, 0, 0) * scene.transform.rotation;
                        //Debug.Log(character.transform.position + dir);
                        //Debug.Log(character.transform.position);
                    }
                    else if (gameObject.CompareTag("Y"))
                    {
                        if (coroutineIsFinished)
                        {
                            StartCoroutine("DisableXButtonsBoxCollider");
                            StartCoroutine("DisableYButtonsBoxCollider");
                            StartCoroutine("DisableZButtonsBoxCollider");
                        }
                        else
                        {
                            //If the coroutine is still going on we stop it
                            StopCoroutine("DisableXButtonsBoxCollider");
                            StopCoroutine("DisableYButtonsBoxCollider");
                            StopCoroutine("DisableZButtonsBoxCollider");
                        }
                        scene.transform.rotation = Quaternion.Euler(0, angle, 0) * scene.transform.rotation;
                    }
                    else if(gameObject.CompareTag("Z"))
                    {
                        if (coroutineIsFinished)
                        {
                            StartCoroutine("DisableXButtonsBoxCollider");
                            StartCoroutine("DisableYButtonsBoxCollider");
                            StartCoroutine("DisableZButtonsBoxCollider");
                        }
                        else
                        {
                            //If the coroutine is still going on we stop it
                            StopCoroutine("DisableXButtonsBoxCollider");
                            StopCoroutine("DisableYButtonsBoxCollider");
                            StopCoroutine("DisableZButtonsBoxCollider");
                        }
                        Vector3 dir = new Vector3(-angle, 0, 0);
                        character.transform.LookAt(character.transform.position + dir);
                        scene.transform.rotation = Quaternion.Euler(0, 0, angle) * scene.transform.rotation;
                        //Debug.Log(character.transform.position + dir);
                        //Debug.Log(character.transform.position);
                    }
                }
            }
        }
    }

    private IEnumerator DisableXButtonsBoxCollider()
    {
        coroutineIsFinished = false;
        GameObject[] xObjects = GameObject.FindGameObjectsWithTag("X");
        foreach (GameObject xObject in xObjects)
        {
            //Debug.Log("False");
            xObject.GetComponent<BoxCollider>().enabled = false;
        }

        yield return new WaitForSeconds(4.0f);

        foreach (GameObject xObject in xObjects)
        {
            //Debug.Log("True");
            xObject.GetComponent<BoxCollider>().enabled = true;
        }
        coroutineIsFinished = true;
    }

    private IEnumerator DisableYButtonsBoxCollider()
    {
        coroutineIsFinished = false;
        GameObject[] yObjects = GameObject.FindGameObjectsWithTag("Y");
        foreach (GameObject yObject in yObjects)
        {
            //Debug.Log("False");
            yObject.GetComponent<BoxCollider>().enabled = false;
        }

        yield return new WaitForSeconds(4.0f);

        foreach (GameObject yObject in yObjects)
        {
            //Debug.Log("True");
            yObject.GetComponent<BoxCollider>().enabled = true;
        }
        coroutineIsFinished = true;
    }

    private IEnumerator DisableZButtonsBoxCollider()
    {
        coroutineIsFinished = false;
        GameObject[] zObjects = GameObject.FindGameObjectsWithTag("Z");
        foreach (GameObject zObject in zObjects)
        {
            //Debug.Log("False");
            zObject.GetComponent<BoxCollider>().enabled = false;
        }

        yield return new WaitForSeconds(4.0f);

        foreach (GameObject zObject in zObjects)
        {
            //Debug.Log("True");
            zObject.GetComponent<BoxCollider>().enabled = true;
        }
        coroutineIsFinished = true;
    }
}