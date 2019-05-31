using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUfo : MonoBehaviour
{
   
    [Range(0, 100)]
    public float speed;

    
    [Range(0, 200)]
    public
    float rot_speed;
    private Vector3 offset_angle =new Vector3(0,0,0);
    private Vector3 offset_postion = new Vector3(0, 0, 0);
    AudioSource audioSource;
    [SerializeField] AudioClip boost_sound;
    public bool boosted = false;
    bool playing_boost = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void AdjustRotationX()
    {
        
        if (offset_angle.x>0)
        {
            offset_angle.x -= Time.deltaTime * rot_speed;
            if (offset_angle.x < 0)
                offset_angle.x = 0;
        }
        if (offset_angle.x < 0)
        {
            offset_angle.x += Time.deltaTime * rot_speed;
            if (offset_angle.x > 0)
                offset_angle.x = 0;
        }
       
    }
    private void AdjustRotationZ()
    {
        if (offset_angle.z > 0)
        {
            offset_angle.z -= Time.deltaTime * rot_speed;
            if (offset_angle.z < 0)
                offset_angle.z = 0;
        }
        if (offset_angle.z < 0)
        {
            offset_angle.z += Time.deltaTime * rot_speed;
            if (offset_angle.z > 0)
                offset_angle.z = 0;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        bool sound_change = false;
      //  Debug.Log(transform.eulerAngles);
       Vector3 stat =  HandleUserInput();
        if (stat.x != 1)
            AdjustRotationX();
        else
            sound_change = true;

        if (stat.z != 1)
            AdjustRotationZ();
        else
            sound_change = true;

        if (boosted)
        {
            if (!playing_boost)
            {
                playing_boost = true;
                audioSource.PlayOneShot(boost_sound);
            }
        }
        else
        {
            playing_boost = false;
        }

        ClampAndSetValues();
    }

    private Vector3 HandleUserInput()
    {
        Vector3 stat = new Vector3(0, 0,0);
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            stat.z = 1;
            offset_postion.y += speed * Time.deltaTime;
            offset_angle.z += Time.deltaTime * rot_speed;
          //  transform.Rotate(new Vector3(0, 0, -Time.deltaTime * rot_speed), Space.World);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            stat.z = 1;
            offset_postion.y -= speed * Time.deltaTime;
            offset_angle.z -= Time.deltaTime * rot_speed;
           // transform.Rotate(new Vector3(0, 0, Time.deltaTime * rot_speed), Space.World);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            stat.x = 1;
            offset_postion.z += speed * Time.deltaTime;
          offset_angle.x += Time.deltaTime * rot_speed;
           // transform.Rotate(new Vector3(-Time.deltaTime * rot_speed,0,0), Space.World);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            stat.x = 1;
            offset_postion.z -= speed * Time.deltaTime;
            offset_angle.x -= Time.deltaTime * rot_speed;
          //  transform.Rotate(new Vector3( Time.deltaTime * rot_speed,0,0), Space.World);
        }
       
       
       
        
        return stat;
    }

    private void ClampAndSetValues()
    {
        offset_angle.z = Mathf.Clamp(offset_angle.z, -45, 45);
        offset_angle.x = Mathf.Clamp(offset_angle.x, -45, 45);
        offset_postion.y = Mathf.Clamp(offset_postion.y, -13, 13);
        offset_postion.z = Mathf.Clamp(offset_postion.z, -13.8f, 13.8f);

        transform.eulerAngles = offset_angle;
        transform.position = offset_postion; ;
        
    }
}
