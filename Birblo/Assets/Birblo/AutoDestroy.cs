using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 50f || transform.position.x < -50f || transform.position.z > 50f || transform.position.z < -50) 
        {
            transform.position = new Vector3(0f, transform.position.y, 0f);
        }
    }
}
