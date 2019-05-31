using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePowerUp : MonoBehaviour
{
    Vector3 axis =new Vector3(0,0,1);
    public float rot_speed;
    enum State { activated,deactivated};
    State state;
    // Start is called before the first frame update
    void Start()
    {
        state = State.deactivated;
    }

    // Update is called once per frame
    void Update()
    {
        if (state==State.activated)
        {
            ChangeAxis();
            transform.RotateAround(transform.position, axis, rot_speed * Time.deltaTime);
        }
    }

    public void Activate()
    {
        state = State.activated;

    }
    public void DeActivate()
    {
        state = State.deactivated;

    }

        private void ChangeAxis()
    {
        axis.x = UnityEngine.Random.Range(0f, 1f);
        axis.y = UnityEngine.Random.Range(0f, 1f);
        axis.z = UnityEngine.Random.Range(0f, 1f);
    }
}
