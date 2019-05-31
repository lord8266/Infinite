using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    
    public float speed = 30;
    BlockHazardHandle hazard;
    // Start is called before the first frame update
    void Start()
    {
        hazard = GetComponent<BlockHazardHandle>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(speed);
        float x = transform.position.x;
        if (x<-200)
        {
          //  Debug.Log("here");
            transform.Translate(new Vector3(400,0,0), Space.World);
            hazard.Activate();
        }
        transform.Translate(-new Vector3(speed * Time.deltaTime,0,0), Space.World);
    }
}
