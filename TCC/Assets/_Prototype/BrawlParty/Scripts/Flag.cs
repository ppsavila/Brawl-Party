﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{


    private void OnTriggerExit(Collider other)  
    {
        if(other.tag == "Player")
        {
            GameController.singleton.gameMode.PointRule(other.GetComponent<PlayerController>());
            Destroy(this.gameObject);
        }
    }

}
