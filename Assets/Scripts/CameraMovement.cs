using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 pos = Vector3.zero;
    private float timer;
    GameObject character;
    [HideInInspector] public Vector3 vs, v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, ve;
    [HideInInspector] public Quaternion q0, q1, q2, q3, q4, q5, q6, q7, q8, q9;
    void Start()
    {
        character = GameObject.Find("character");
        pos = character.transform.position;
        transform.LookAt(pos);
        timer = 0;
        vs = transform.position + new Vector3(0, 0, 13.2f);
        v0 = transform.position;
        v1 = transform.position + new Vector3(0, 0, -13.2f);
        v2 = transform.position + new Vector3(0, 0, -26.4f);
        v3 = transform.position + new Vector3(5f, 0, -26.4f);
        v4 = transform.position + new Vector3(12.8f, 0, -26.4f);
        v5 = transform.position + new Vector3(17.6f, 0, -26.4f);
        v6 = transform.position + new Vector3(17.6f, 0, -29f);
        ve = transform.position + new Vector3(17.6f, 0, -35f);
    }
    void catmull_rom(Vector3 vs, Vector3 v0, Vector3 v1, Vector3 v2, float t)
    {
        Vector3 c0 = v0,
                c1 = -vs / 2 + v1 / 2,
                c2 = vs - 5 * v0 / 2 + 2 * v1 - v2 / 2,
                c3 = -vs / 2 + 3 * v0 / 2 - 3 * v1 / 2 + v2 / 2;
        transform.position = c0 + c1 * t + c2 * Mathf.Pow(t, 2) + c3 * Mathf.Pow(t, 3);
    }
    void Bezier_deCasteljau_s(Vector3 v0, Vector3 v1, Vector3 v2, float t)
    {
        Vector3 a0 = 2 * v1 - v2,
                b1 = v0 / 2 + v1 - v2 / 2;
        Vector3 R0 = Vector3.Lerp(v0, a0, t),
                R1 = Vector3.Lerp(a0, b1, t),
                R2 = Vector3.Lerp(b1, v1, t);
        Vector3 r0 = Vector3.Lerp(R0, R1, t),
                r1 = Vector3.Lerp(R1, R2, t);
        transform.position = Vector3.Lerp(r0, r1, t);
    }
    void Bezier_deCasteljau(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, float t)
    {
        Vector3 a1 = -v0 / 2 + v1 + v2 / 2,
                b2 = v1 / 2 + v2 - v3 / 2;
        Vector3 R0 = Vector3.Lerp(v1, a1, t),
                R1 = Vector3.Lerp(a1, b2, t),
                R2 = Vector3.Lerp(b2, v2, t);
        Vector3 r0 = Vector3.Lerp(R0, R1, t),
                r1 = Vector3.Lerp(R1, R2, t);
        transform.position = Vector3.Lerp(r0, r1, t);
    }
    void Bezier_deCasteljau_e(Vector3 v0, Vector3 v1, Vector3 v2, float t)
    {
        Vector3 a0 = -v0 / 2 + v1 + v2 / 2,
                b1 = 2 * v2 - v1;
        Vector3 R0 = Vector3.Lerp(v1, a0, t),
                R1 = Vector3.Lerp(a0, b1, t),
                R2 = Vector3.Lerp(b1, v2, t);
        Vector3 r0 = Vector3.Lerp(R0, R1, t),
                r1 = Vector3.Lerp(R1, R2, t);
        transform.position = Vector3.Lerp(r0, r1, t);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= 4)
        {
            //transform.position += new Vector3(0, 0, -26.4f * Time.deltaTime / 7);
            catmull_rom(vs, v0, v1, v2, timer / 4);
            pos = character.transform.position;
            transform.LookAt(pos);
        }
        else if (timer <= 8)
        {
            catmull_rom(v0, v1, v2, v3, (timer - 4) / 4);
            pos = character.transform.position;
            transform.LookAt(pos);
        }
        else if (timer <= 10)
        {
            catmull_rom(v1, v2, v3, v4, (timer - 8) / 2);
            pos = character.transform.position;
            transform.LookAt(pos);
        }
        else if (timer <= 13)
        {
            catmull_rom(v2, v3, v4, v5, (timer - 10) / 3);
            pos = character.transform.position;
            transform.LookAt(pos);
        }
        else if (timer <= 16)
        {
            //transform.rotation = Quaternion.Euler(0, 90 * Time.deltaTime, 0) * transform.rotation;
            catmull_rom(v3, v4, v5, v6, (timer - 13) / 3);
            pos = character.transform.position;
            transform.LookAt(pos);
        }
        else if (timer <= 17)
        {
            //transform.position += new Vector3(0, 0, -26.4f * Time.deltaTime / 7);
            catmull_rom(v4, v5, v6, ve, (timer - 16) / 1);
            pos = character.transform.position;
            transform.LookAt(pos);
        }
    }
}
