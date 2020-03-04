﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public int velocidadeDaBala = 3;
    public Vector3 transformForward;
    public PlayerController player;
    AudioSource source;
    public AudioClip audio;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transformForward* 800f * velocidadeDaBala, ForceMode.Acceleration);
        source = GetComponent<AudioSource>();
        source.clip = audio;
        Destroy(this.gameObject,5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        source.Play();
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log(player);
           
            other.GetComponent<PlayerController>().ReceiveDamage(damage,player);
      
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Obstaculo")
        {
            Destroy(this.gameObject);
        }

    }
}