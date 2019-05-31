using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpeedController : MonoBehaviour
{
    
    Move move;
    public float speed;
    BlockHazardHandle hazard_handle;
    void Start()
    {
        move = GetComponent<Move>();
        Debug.Log(this.gameObject);
        Debug.Log(move);
        hazard_handle = GetComponent<BlockHazardHandle>();
    }

 
    void Update()
    {
        IncreaseOverallSpeed();
    }

    public void IncreaseHazardSpeed(float period)
    {
        hazard_handle.IncreaseHazardSpeed(period);
       
    }

    void IncreaseOverallSpeed()
    {
        move.speed = speed;
    }
    
}
