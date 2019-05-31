using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpeedController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]  GameObject[] blocks;
    [SerializeField]  GameObject start_block;
    MoveStart start_blockmove;
    BlockSpeedController[] speed_controllers;
    
    [Range(0, 360)] public float speed;
    [SerializeField] float speed_increase;
    [SerializeField] float period_decrease =0.05f;
    [SerializeField] float start_period = 4f;
    private Oscillator[] oscillators;
    enum State { boosted,normal};
    State state;
    bool start =false;

    [SerializeField] MoveWorld mv;
    void Start()
    {  
        oscillators = GetComponentsInChildren<Oscillator>();
        Debug.Log(oscillators.Length);
        state = State.normal;
        speed_controllers = new BlockSpeedController[blocks.Length];
        for (int i=0;i<blocks.Length;i++)
        {
            speed_controllers[i] = blocks[i].GetComponent<BlockSpeedController>();
        }
        start_blockmove = start_block.GetComponent<MoveStart>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.normal)
        {
            if (!start)
            {
                start = true;
                Invoke("IncreaseHazardSpeed", 1.5f);
            }
            UpdateOverallSpeed();
        }
       
    }

    private void UpdateOverallSpeed()
    {
        speed += (Time.deltaTime * 8.75f);
       // foreach(BlockSpeedController sc in speed_controllers)
     //   {
           // sc.speed = speed;
        //   }
        mv.speed = speed;
        start_blockmove.speed = speed;
    }

    private void IncreaseHazardSpeed()
    {
        start_period -= period_decrease;
        foreach (Oscillator osc in oscillators)
        {
            osc.start_period = start_period;
        }
        Invoke("IncreaseHazardSpeed", 1.5f);
    }
}
