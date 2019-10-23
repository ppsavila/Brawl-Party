﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Metralhadora : Arma
{
    public Metralhadora()
    {
        fireRate = 0.1f;
        ammoAmount = 35;
        damage = 4;
        gunSprite = Resources.Load("Armas/Sprites/Pistol") as Sprite;
        prefab = Resources.Load("Armas/Pistol") as GameObject;
        ammunitionPrefab = Resources.Load("Municoes/Projetil") as GameObject;
        ammunitionPrefab.GetComponent<Bullet>().damage = this.damage;
       

    }

    public override void Shoot(Vector3 position, Quaternion rotation,Vector3 Forward, PlayerController player)
    {
        ammoAmount--;
        ammunitionPrefab.GetComponent<Bullet>().transformForward = Forward;
        ammunitionPrefab.GetComponent<Bullet>().player = player;
        Instantiate(ammunitionPrefab, position + player.transform.forward * 1.5f, rotation);

    }
}