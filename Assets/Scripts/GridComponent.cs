using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridComponent : MonoBehaviour
{
    public Material transparentMatVarient;
    private Material originalMat;

    // Start is called before the first frame update
    void Start()
    {
        originalMat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFade(float fadeAmt)
    {
        GetComponent<MeshRenderer>().material = fadeAmt < 1f ? transparentMatVarient : originalMat;
        GetComponent<MeshRenderer>().material.SetFloat("_Fade", fadeAmt);
    }
}
