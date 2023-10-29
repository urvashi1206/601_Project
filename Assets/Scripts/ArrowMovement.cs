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
    public float r_timer;
    //private float rotateSpeed = 180f;

    private Ray _ray;
    private RaycastHit _hit;
    private bool coroutineIsFinished = true;
    public GameObject character;
    public GameObject character_head;
    public Rigidbody c_rigidbody;
    bool onrotation;
    float rx, ry, rz;
    public CharacterMovement character_movement;
    Vector3 dir;
    Quaternion r_des;
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;
        character = GameObject.Find("character");
        character_head = GameObject.Find("head");
        c_rigidbody = character.GetComponent<Rigidbody>();
        r_timer = 0;
        onrotation = false;
        rx = 0; ry = 0; rz = 0;
        r_des = scene.transform.rotation;
        character_movement = GameObject.FindObjectOfType<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onrotation)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _ray = m_Camera.ScreenPointToRay(Input.mousePosition);

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
                            //character facing fall side
                            dir = new Vector3(0, 0, angle);
                            character.transform.LookAt(character.transform.position + dir);
                            character_head.transform.LookAt(character_head.transform.position + dir);
                            c_rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                            //level rotation
                            r_des = Quaternion.Euler(angle, 0, 0) * scene.transform.rotation;
                            onrotation = true;
                            character_movement.onrotation = true;
                            rx = angle; ry = 0; rz = 0;
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
                            c_rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                            r_des = Quaternion.Euler(0, angle, 0) * scene.transform.rotation;
                            onrotation = false;
                            character_movement.onrotation = true;
                            rx = 0; ry = angle; rz = 0;
                        }
                        else if (gameObject.CompareTag("Z"))
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
                            //character facing fall side
                            dir = new Vector3(-angle, 0, 0);
                            character.transform.LookAt(character.transform.position + dir);
                            character_head.transform.LookAt(character_head.transform.position + dir);
                            c_rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                            //level rotation
                            r_des = Quaternion.Euler(0, 0, angle) * scene.transform.rotation;
                            onrotation = true;
                            character_movement.onrotation = true;
                            rx = 0; ry = 0; rz = angle;
                        }
                    }
                }
            }
        }
        else
        {
            if(r_timer > 1)
            {
                r_timer = 0;
                onrotation = false;
                character_movement.onrotation = false;
                scene.transform.rotation = r_des;
                c_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                c_rigidbody.velocity = Vector3.zero;
            }
            else
            {
                r_timer += Time.deltaTime;
                scene.transform.rotation = Quaternion.Euler(rx * Time.deltaTime, ry * Time.deltaTime, rz * Time.deltaTime) * scene.transform.rotation;
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