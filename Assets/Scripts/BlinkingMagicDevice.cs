using UnityEngine;

public class BlinkingMagicDevice : MonoBehaviour
{
    public Color starColor = Color.blue;
    public Color endColor = Color.red;
    [Range(0, 10)]
    public float speed = 1;

    Renderer ren;
    private void Awake()
    {
        ren = GetComponent<Renderer>();
    }

    private void Update()
    {
        ren.material.color = Color.Lerp(starColor, endColor, Mathf.PingPong(Time.time * speed, 1));
    }
}
