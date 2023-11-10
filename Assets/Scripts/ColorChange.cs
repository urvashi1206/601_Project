using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Color originalColor;
    public Color hoverColor;
    bool mouseOver = false;

    // Start is called before the first frame update
    void Start()
    {
        //_Colour, _SpecColor, _Emission, _ReflectColor
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        mouseOver = true;
        GetComponent<Renderer>().material.SetColor("_Color", hoverColor);
    }

    private void OnMouseExit()
    {
        mouseOver = false;
        GetComponent<Renderer>().material.SetColor("_Color", originalColor);
    }
}
