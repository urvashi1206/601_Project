using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    // Start is called before the first frame update
    private Ray _ray;
    private RaycastHit _hit;
    private Camera m_Camera;
    public bool setDevice;
    void Start()
    {
        m_Camera = Camera.main;
        setDevice = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (setDevice)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _ray = m_Camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _hit, 1000f))
                {
                    if (_hit.transform == transform)
                    {
                        if (gameObject.CompareTag("MagicDevice"))
                        {
                            GameObject.FindWithTag("Dialogue").GetComponent<Dialogue>().setArrow = true;
                            GameObject.FindGameObjectWithTag("MagicDevice").SetActive(false);
                        }
                    }
                }
            }
        }
    }

    public void ClickObject()
    {
        
    }
}
