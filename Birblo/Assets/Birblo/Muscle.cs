using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muscle : MonoBehaviour
{

    public GameObject Body;
    [Range(0f,100f)]
    public float Turnforce;
    [Range(0f, 100f)]
    public float ForwardForce;

    public bool Left;
    public bool Right;
    public bool Fwd;
    public bool Bck;

    private Transform Birblo;


    // Start is called before the first frame update
    void Start()
    {
        Birblo = Body.transform;
    }

    private void FixedUpdate()
    {
        if (Left) { Birblo.Rotate(Vector3.up *  Time.deltaTime * -Turnforce, Space.Self); }
        if (Right) { Birblo.Rotate(Vector3.up * Time.deltaTime  * Turnforce, Space.Self); }
        if (Fwd) { Birblo.Translate(Vector3.forward * Time.deltaTime *  ForwardForce, Space.Self); }
        if (Bck) { Birblo.Translate(Vector3.back * Time.deltaTime *  ForwardForce, Space.Self); }

        Left = false;
        Right = false;
        Fwd = false;
        Bck = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
