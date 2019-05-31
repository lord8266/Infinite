using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class Boost_PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 startpos;
    Vector3 currpos;
    RotatePowerUp powerup;
    enum State { activated,deactivated};
    State state;
    void Start()
    {
        state = State.deactivated;
        startpos = transform.localPosition;
        currpos = startpos;
        powerup = GetComponentInChildren<RotatePowerUp>();
        
      
    }

    // Update is called once per frame
    void Update()
    {
        if (state==State.activated)
        {
            float x = transform.position.x;
            if (x<0)
            {
                DeActivate();
            }
        }
        transform.localPosition = currpos;
    }

    public void Activate()
    {
        currpos.y = UnityEngine.Random.Range(-7f, 7f);
        currpos.z = UnityEngine.Random.Range(-8f, 8f);
        state = State.activated;
        powerup.Activate();
    }

    public void DeActivate()
    {
        currpos = startpos;
        state = State.deactivated;
        powerup.DeActivate();
    }

}
