using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingArrow : MonoBehaviour
{
    public float repeatTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeStateOfObject", 1f, repeatTime);
    }

    private void ChangeStateOfObject()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
