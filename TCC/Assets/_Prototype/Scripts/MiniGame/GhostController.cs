﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    NavMeshAgent thisAgent;
    void Start()
    {
        thisAgent = GetComponent<NavMeshAgent>();
    }
    public void FollowPlayer(PlayerController player)
    {
        this.thisAgent.Move(player.transform.position);
    }

   
}