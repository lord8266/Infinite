using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCam : MonoBehaviour
{
    float distance = 7.62f;
    Vector3 ufo_pos = new Vector3(0, 0, 0);
    [SerializeField]
    GameObject ufo;

    void Start()
    {

    }

    void Update()
    {
        float atan_y = 180 / Mathf.PI * Mathf.Atan(ufo.transform.position.y / distance);
        float atan_z = 180 / Mathf.PI * Mathf.Atan(ufo.transform.position.z / distance);
        Vector3 ini = transform.eulerAngles;
        // Debug.Log(new Vector2(atan_y,atan_z));
        transform.eulerAngles = new Vector3(ini.x,-atan_z, atan_y);
    }
}
