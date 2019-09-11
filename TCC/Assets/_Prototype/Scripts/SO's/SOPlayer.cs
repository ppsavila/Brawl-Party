﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu (fileName = "Player",menuName ="Jogadores")]
public class SOPlayer : ScriptableObject
{
   public string nome;
   public int hp;
   public int actualHP;
   public float speed;
   public GameObject prefab;
   public Animator stateAnimation;
   public Passive passiva;
  
}
