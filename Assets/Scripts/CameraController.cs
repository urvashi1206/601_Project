using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject characterObj;

    private List<GameObject> hitObjs;

    // Start is called before the first frame update
    void Start()
    {
        hitObjs = new();
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> objsThisFrame = new();
        Vector3 toPlayer = characterObj.transform.position - transform.position;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, toPlayer.normalized, toPlayer.magnitude, 1 << 3); // Geometry layer
        foreach (RaycastHit hit in hits) 
        {
            if (hit.transform.TryGetComponent(out GridComponent c))
            {
                objsThisFrame.Add(hit.transform.gameObject);

                // if the hit wasn't in the existing list (meaning it's a new hit)
                if (!hitObjs.Remove(hit.transform.gameObject))
                    c.SetFade(0.2f);
            }
        }

        // All remaining objs in list were not hit; set them back to opaque material
        foreach (GameObject obj in hitObjs)
            obj.GetComponent<GridComponent>().SetFade(1f);

        hitObjs = objsThisFrame;
    }
}
