using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSensor : MonoBehaviour
{

    public bool HIT;
    public RaycastHit[] HITS;
    public float Distance = 0;
    public bool EAT;
    public float Direction;
    public float MaxDistanceSensor = 5f;
    [Range(0.001f,3f)]
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f,0.8f,0f);
        Vector3 direction = transform.TransformDirection(Vector3.forward) *MaxDistanceSensor;
        Gizmos.DrawRay(this.transform.position, direction);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up, speed);
        EAT = false;
        HIT = false;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * MaxDistanceSensor;
        Debug.DrawRay(this.transform.position, direction);
        Ray R = new Ray(this.transform.position, direction);
        HITS = Physics.RaycastAll(R,5f);
        if (HITS != null && HITS.Length > 0) 
        {
            foreach (RaycastHit h in HITS) {
                if (h.transform.tag == "Food")
                {
                    HIT = true;
                    Distance = Vector3.Distance(h.transform.position, transform.position);
                    Distance = Distance<0f?Distance*-1:Distance;
                    Direction = this.transform.rotation.eulerAngles.y;
                    if (Distance < 1f)
                    {
                        EAT = true;
                        h.collider.enabled = false;
                        Destroy(h.collider.gameObject);
                       

                    }
                }
            }
        }



    }
}
