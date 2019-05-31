using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHazardHandle : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] GameObject[] hazardA_objects;
    HazardA[] hazardA;
   [SerializeField] GameObject powerup_object;
    Boost_PowerUp powerup;
    void Start()
    {
        hazardA = new HazardA[hazardA_objects.Length];
        for (int i=0;i<hazardA_objects.Length;i++)
        {
            hazardA[i] = hazardA_objects[i].GetComponent<HazardA>();
        }
        
        powerup = powerup_object.GetComponent<Boost_PowerUp>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        int i1= Random.Range(0,5);
     //   int i2 = Random.Range(0, hazardA.Length);
      //  while(i2==i1)
      //  {
         //   i2 = Random.Range(0, hazardA.Length);
      //  }
       foreach(HazardA hazard in hazardA)
        {
            hazard.Activate();
        }
        if ( i1==1)
        {
            powerup.Activate();
       }
    }

    public void IncreaseHazardSpeed(float period)
    {
        foreach( HazardA hazard in hazardA)
        {
            hazard.IncreaseHazardSpeed(period);
        }
    }
}
