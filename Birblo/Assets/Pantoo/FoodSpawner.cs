using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{

    public GameObject MurbolModel;
    public GameObject FoodPool;
    [Range(0,100)]
    public float ChanceSpawn=10;
    
    float delta = 0;

    void Start()
    {
        
    }


    private void SpawnFood() 
    {
        float Val = Random.Range(0, 100);

        if (Val < ChanceSpawn)
        {

            if (MurbolModel != null)
            {

                GameObject murbol = Instantiate<GameObject>(MurbolModel);
                float positionx = transform.position.x;
                float positionz = transform.position.z;
                Transform murbolTransform = murbol.transform;
                murbolTransform.position = new Vector3(positionx + Random.Range(-4, 4), 0.7f, positionz + Random.Range(-4, 4));
                murbolTransform.parent = FoodPool.transform;
                

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if (delta > 1) 
        {
            delta = 0;
            SpawnFood();
        }
        
      

    }
}
