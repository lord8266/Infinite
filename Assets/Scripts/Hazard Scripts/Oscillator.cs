using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update
    enum State {activated,deactivated,moving_away};
    State state = State.deactivated;
    enum type { z,y};
    [SerializeField] float offset;
    public float start_period=4f;
    float period;
    public float factor;
    [SerializeField] type t;
     Vector3 curr_pos;
    [SerializeField]Vector3 start_pos;
    [SerializeField] float[] auxilary_range_y;
    [SerializeField] float[] auxilary_range_z;

    [SerializeField] Light light;
    Vector3 light_angle;
    Vector3 light_pos;
    bool auxilary_state = false;
    [SerializeField] GameObject ufo;
    [SerializeField]  GameObject world;
    private WorldSpeedController wsc;
    private float speed;
    bool accurate_pos = false;
   [SerializeField] Light point_light;
    void Start()
    {
        wsc = world.GetComponent<WorldSpeedController>();


        state = State.deactivated;
    }

    // Update is called once per frame
    void Update()
    {
        speed = wsc.speed;
        if (state==State.activated)
        {
          
            Oscillate();

            if (accurate_pos)
                SetAccuratePos();
            Auxilary_offset();
            ControlLights();

        }
        else if (state==State.moving_away)
        {
            Oscillate();
            CheckPos();
        }

    }
    private void ControlLights()
    {
       
        if (factor>0.9)
        {
            
            light.intensity = 1000;
            Transform tr = light.GetComponent<Transform>();
            Vector3 pos = tr.localPosition;
            Vector3 angle = tr.localEulerAngles;
            if (t==type.y)
            {
                pos.y = auxilary_range_y[0];
                pos.z = curr_pos.z;
                angle.x = 90;
            }
            else if (t==type.z)
            {
                pos.z = auxilary_range_z[1];
                pos.y = curr_pos.y;
                angle.y = 0;
            }
            tr.localPosition = pos;
            tr.localEulerAngles = angle;
        }
        else if (factor<-0.9)
        {
            light.intensity = 1000;
            Transform tr = light.GetComponent<Transform>();
            Vector3 pos = tr.localPosition;
            Vector3 angle = tr.localEulerAngles;
            if (t == type.y)
            {
                pos.y = auxilary_range_y[1];
                pos.z = curr_pos.z;
                angle.x = -90;
            }
            else if (t == type.z)
            {
                pos.z = auxilary_range_z[0];
                pos.y = curr_pos.y;
                angle.y = 180;
            }
            tr.localPosition = pos;
            tr.localEulerAngles = angle;
            
        }
        else
        {
           
            light.intensity = 0;
        }

       
    }
    private void Auxilary_offset()
    {
        if (Mathf.Abs(factor) > 0.9)
        {
           
            if (!auxilary_state)
            {
                    accurate_pos = UnityEngine.Random.Range(0, 1) == 1 ? true : false;
                 SetPeriod();
                SetRandomPos();
            }
            auxilary_state = true;
        }
        else
        {
            auxilary_state = false;
        }
    }

    private void SetPeriod()
    {
       
             Vector3 pos = transform.position;
            float t = pos.x / speed;
            period = t * 4*(2-Math.Abs(factor));
           // Debug.Log(period.ToString() + " " + pos.x.ToString());
         
        period = Mathf.Clamp(period, 2.5f, 6);
        if (pos.x < 16)
        {
            period = 12f;
        }
    }

    private void SetAccuratePos()
    {
        if (transform.position.x>2)
        {
            if (t == type.y)
            {
                
                curr_pos.z = ufo.transform.position.z;
            }
            if (t == type.z)
            {
               
                curr_pos.y = ufo.transform.position.y;
            }
        }
    }

    private void SetRandomPos()
    {
        if (transform.position.x > 2)
        {
            if (t == type.y)
            {
                curr_pos.z = UnityEngine.Random.Range(auxilary_range_z[0], auxilary_range_z[1]);

            }
            if (t == type.z)
            {
                curr_pos.y = UnityEngine.Random.Range(auxilary_range_y[0], auxilary_range_y[1]);

            }
        }
    }

    private void CheckPos()
    {
        if (Mathf.Abs(factor)<0.9)
        {
            period -= 0.2f;
            period = period <= 0 ? 0.2f : period;
        }
        else
        {
            state = State.deactivated;
        }
    }

    private void Oscillate()
    {
        //Debug.Log(state);
        factor = Mathf.Sin(Mathf.PI * 2 * Time.time / period);
        if (t==type.y)
        {
            curr_pos.y = factor * offset;
        }
        else if (t==type.z)
        {
            curr_pos.z = factor * offset;
        }
        transform.localPosition =curr_pos ;
       // Debug.Log(factor*offset);
    }

    public void DeActivate()
    {
       // Debug.Log("deactivate called in oscillator");
        state = State.moving_away;
        ResetLight();
        
    }
    private void ResetLight()
    {
        Transform tr = light.GetComponent<Transform>();
        tr.localEulerAngles = new Vector3(0, 0, 0);
        tr.localPosition = new Vector3(0, 0, 0);
    }
    public void Activate()
    {
      //  Debug.Log("activate called in oscillator");
        curr_pos = start_pos;
    
        state = State.activated;
        SetPeriod();
     
    }
}
