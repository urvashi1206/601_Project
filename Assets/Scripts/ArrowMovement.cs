using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _ray=m_Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, 1000f))
            {
                if (_hit.transform == transform)
                {
                    if (gameObject.CompareTag("X")) 
                    {
                        scene.transform.rotation = Quaternion.Euler(angle, 0, 0) * scene.transform.rotation;
                        //scene.transform.rotation *= Quaternion.AngleAxis(angle, Vector3.right);
                    }
                    else if (gameObject.CompareTag("Y"))
                    {
                        scene.transform.rotation = Quaternion.Euler(0, angle, 0) * scene.transform.rotation;
                        //scene.transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
                    }
                    else if(gameObject.CompareTag("Z"))
                    {
                        scene.transform.rotation = Quaternion.Euler(0, 0, angle) * scene.transform.rotation;
                        //scene.transform.rotation *= Quaternion.AngleAxis(angle, Vector3.forward);
                    }
                }
            }
        }
    }
}
