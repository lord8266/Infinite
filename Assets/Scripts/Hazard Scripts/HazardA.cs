using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardA : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject[] hazards;
    enum State { activated,deactivated};
    State state;
    private Oscillator[] oscillators;
    [SerializeField]
  
    int active_index;
    
    void Start()
    {
        oscillators = new Oscillator[hazards.Length];
        for (int i=0;i<hazards.Length;i++)
        {
            oscillators[i] = hazards[i].GetComponent<Oscillator>();
        }
        state = State.deactivated;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (state == State.activated)
        {
            float x_pos = transform.position.x;
            if (x_pos < 0)
            {
                Deactivate();
            }
        }
        float factor = oscillators[active_index].factor;
        float light_mag = 1 - Mathf.Abs(factor);
    }

    public void Activate()
    {
        state = State.activated;
        active_index = Random.Range(0, hazards.Length);
        oscillators[active_index].Activate();
    }

    private void Deactivate()
    {
        state = State.deactivated;
        oscillators[active_index].DeActivate();
    }

    public void IncreaseHazardSpeed(float period)
    {
        for (int i=0;i<oscillators.Length;i++)
        {
            oscillators[i].start_period -= period;
        }
    }
}
