using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    private Ray _ray;
    private RaycastHit _hit;
    private Camera m_Camera;
    public GameObject scene;
    private float timer = 0;
    private bool onrotation = false;
    private Quaternion r_des;
    public CharacterMovement CM;
    public GameObject character;
    public Rigidbody c_rigidbody;
    void Start()
    {
        m_Camera = Camera.main;
        CM = GameObject.FindWithTag("Player").GetComponent<CharacterMovement>();
        character = GameObject.Find("character");
        c_rigidbody = character.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, 1000f))
            {
                if (_hit.transform == transform)
                {
                    if (gameObject.CompareTag("rotate"))
                    {
                        GetComponent<AudioSource>().Play();
                        Destroy(GameObject.FindWithTag("Dialogue").GetComponent<Dialogue>().Arrow2);
                        GameObject.FindWithTag("Dialogue").GetComponent<Dialogue>().ArrowClick = true;
                        onrotation = true;
                        r_des = Quaternion.Euler(-90, 0, 0) * scene.transform.rotation;
                    }
                }
            }
        }
        if (onrotation)
        {
            if (timer > (1/0.7))
            {
                timer = 0;
                onrotation = false;
                scene.transform.rotation = r_des;
                c_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                c_rigidbody.velocity = Vector3.zero;
            }
            else
            {
                timer += Time.deltaTime;
                scene.transform.rotation = Quaternion.Euler(-90f * Time.deltaTime * 0.7f, 0 * Time.deltaTime * 0.7f, 0 * Time.deltaTime * 0.7f) * scene.transform.rotation;
            }
        }
    }
}
