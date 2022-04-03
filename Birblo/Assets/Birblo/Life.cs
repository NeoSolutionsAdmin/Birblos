using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    // Start is called before the first frame update
    public long MyLife = 1000;
    public bool Ethernal = false;
    void Start()
    {
        
    }

    public void AddLife(long pLife) 
    {
        MyLife += pLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (Ethernal==false) MyLife -= 1;
        if (MyLife < 0) 
        {
            Destroy(transform.parent.gameObject);
        }

    }
}
