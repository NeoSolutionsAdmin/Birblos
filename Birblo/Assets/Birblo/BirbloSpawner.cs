using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbloSpawner : MonoBehaviour
{

    public int BirbloMinPopulation;
    public GameObject BirbloModel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int population = GameObject.FindGameObjectsWithTag("Birblo").Length;
        if (population < BirbloMinPopulation) 
        {
            GameObject NewBirblo = Instantiate(BirbloModel);
            NewBirblo.transform.position = new Vector3(0, 1.2f, 0);
            transform.Rotate(Vector3.up * Random.Range(-180, 180));
        }

    }
}
