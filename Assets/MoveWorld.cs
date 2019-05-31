using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorld : MonoBehaviour
{
    // Start is called before the first frame update
  [SerializeField]  GameObject g1;
   [SerializeField] GameObject g2;
    bool current;
    public float speed;
    BlockHazardHandle hazard1;
    BlockHazardHandle hazard2;
    void Start()
    {
        hazard1 = g1.GetComponent<BlockHazardHandle>();
        hazard2 = g2.GetComponent<BlockHazardHandle>();
    }

    // Update is called once per frame
    void Update()
    {

        Transform t = g1.transform;
        float x = g1.transform.position.x;
        if (x < -200)
        {
            //  Debug.Log("here");
            t.Translate(new Vector3(400, 0, 0), Space.World);
            hazard1.Activate();
        }
        t.Translate(-new Vector3(speed * Time.deltaTime, 0, 0), Space.World);

        t = g2.transform;
        x = g2.transform.position.x;
        if (x < -200)
        {
            //  Debug.Log("here");
            t.Translate(new Vector3(400, 0, 0), Space.World);
            hazard2.Activate();
        }
        t.Translate(-new Vector3(speed * Time.deltaTime, 0, 0), Space.World);
    }
}

