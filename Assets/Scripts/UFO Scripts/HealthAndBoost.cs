using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndBoost : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject[] particle_objects;
    ParticleSystem[] particles;
    [SerializeField] GameObject world;
    MoveUfo ufo;
    WorldSpeedController wsc;
    bool[] stop_buffer =new bool[2] { false, false };
    int hits = 0;
    float previous=0f;
    float current=0f;
    bool boost_enabled = true;
    bool boosted = false;
    AudioSource source;
    [SerializeField] AudioClip collide_sound;
    [SerializeField] AudioClip powerup_sound;
    [SerializeField] AudioClip pickup_sound;

    enum State { powerup,user};
    State state;
    void Start()
    {
        state = State.user;
        source = GetComponent<AudioSource>();
        ufo = GetComponent<MoveUfo>();
        particles = new ParticleSystem[particle_objects.Length];
        for (int i=0;i<particle_objects.Length;i++)
        {
            particles[i] = particle_objects[i].GetComponent<ParticleSystem>();
            particles[i].Stop();
        }
        wsc = world.GetComponent<WorldSpeedController>();

    }

    // Update is called once per frame
    void Update()
    {
        ufo.boosted = boosted;
        current = Time.time;
        if (state == State.user)
        {
            CheckUserBoost();
        }
        else
        {
            CheckPowerUp();

        }
        

    }

    private void CheckPowerUp()
    {
        if ((current-previous)>=5f)
        {
            state = State.user;
            boosted = false;
            boost_enabled = false;
            wsc.speed /= 3;
            ufo.speed /= 1.75f;
            Invoke("UnlockBoost", 1f);
            StopBoostEffect();
        }
        else
        {
            PlayBoostEffect();
        }
    }

    private void CheckUserBoost()
    {
        HandleSpace();
          if (boosted)
        {
            if ((current - previous) > 0.5f)
            {
                boosted = false;
                boost_enabled = false;
                Invoke("UnlockBoost", 0.5f);
                wsc.speed /= 2;
                ufo.speed /= 1.5f;
                
            }


            PlayBoostEffect();
        }
        else
        {
            StopBoostEffect();
        }

    }

    private void HandleSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (boost_enabled && (!boosted))
            {
                previous = current;
                wsc.speed *= 2;
                ufo.speed *= 1.5f;
                boosted = true;

            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (boosted&&(state==State.user))
            {
                wsc.speed /= 2;
                ufo.speed /= 1.5f;
                boost_enabled = false;
                boosted = false;
                Invoke("UnlockBoost", 0.5f);
            }

        }
    }
    private void UnlockBoost()
    {
        boost_enabled = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "PowerUp")
        {
            particles[0].Play();
            stop_buffer[0] = true;
            EnablePowerUp();
            Invoke("StopParticle", 1f);
            source.PlayOneShot(pickup_sound);
        }
        else
        {
            if (state == State.user)
            {
                source.PlayOneShot(collide_sound);
                wsc.speed = 50;
                ufo.speed = 10f;
                boosted = false;



                particle_objects[1].transform.position = collision.transform.position;
                particles[1].Play();
                stop_buffer[1] = true;
                Invoke("StopParticle", 1f);

            }
        }
    }

    private void EnablePowerUp()
    {
        if (state == State.user)
        {
            if (boosted)
            {
                wsc.speed /= 2;
                ufo.speed /= 1.5f;
                boosted = false;

            }
         
                wsc.speed *= 3;
                ufo.speed *= 1.75f;

        
            state = State.powerup;
           
        }
        else
        {
            
        }
        previous = current;
        source.PlayOneShot(powerup_sound);
    }

    void StopParticle()
    {
        if (stop_buffer[0])
        {
            particles[0].Stop();
            stop_buffer[0] = false;
        }
        if (stop_buffer[1])
        {
            stop_buffer[1] = false;
            particles[1].Stop();
        }

    }

    private void PlayBoostEffect()
    {
        if (!particles[2].isPlaying)
        {
            particles[2].Play();
            particles[3].Play();
        }
    }

    private void StopBoostEffect()
    {
        if (particles[2].isPlaying)
        {
            particles[2].Stop();
            particles[3].Stop();
        }
    }
}
