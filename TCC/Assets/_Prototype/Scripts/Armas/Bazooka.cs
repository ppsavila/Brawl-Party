﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bazooka : Arma
{
    public Bazooka()
    {
        fireRate = 1f;
        ammoAmount = 1;
        damage = 100;
        gunSprite = Resources.Load("Armas/Sprites/Shotgun") as Sprite;
        prefab = Resources.Load("Armas/Shotgun") as GameObject;
        ammunitionPrefab = Resources.Load("Municoes/ProjetilBazuca") as GameObject;
        ammunitionPrefab.GetComponent<BulletBazuca>().damage = this.damage;
       

    }

    public override void Shoot(Vector3 position, Quaternion rotation,Vector3 Forward, PlayerController player)
    {
        ammoAmount--;
        ammunitionPrefab.GetComponent<BulletBazuca>().transformForward = Forward;
        ammunitionPrefab.GetComponent<BulletBazuca>().player = player;
        GameObject ob = Instantiate(ammunitionPrefab, position + player.transform.forward * 2f, rotation);
        ob.transform.localScale = new Vector3(1.4f,1.4f,1.4f);
    }
}