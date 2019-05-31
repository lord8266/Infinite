using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStart : MonoBehaviour
{
    
    public float speed = 30;
    [SerializeField] GameObject block1;
    [SerializeField] GameObject block2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(speed);
        transform.position = new Vector3(Mathf.Min(block1.transform.position.x, block2.transform.position.x),0,0);
    }
}
