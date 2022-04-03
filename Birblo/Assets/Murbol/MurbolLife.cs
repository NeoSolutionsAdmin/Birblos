using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurbolLife : MonoBehaviour
{
    // Start is called before the first frame update

    public int MurbolLifespan = 3000;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MurbolLifespan -= 1;
        if (MurbolLifespan <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
