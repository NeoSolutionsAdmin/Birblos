using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyStats : MonoBehaviour
{
    // Start is called before the first frame update
    public float Direction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Direction = this.transform.rotation.eulerAngles.y;
    }
}
