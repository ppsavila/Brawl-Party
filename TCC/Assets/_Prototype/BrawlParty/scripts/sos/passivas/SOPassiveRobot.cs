﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Passivas", menuName = "Passivas/PassivaRobo")]
public class SOPassiveRobot : SOPassive
{
    Arma _armaAtual;
    public float finishedShildTime;
    PlayerController _player;

    public override void AtivarPassiva(PlayerController player)
    {
        this._player = player;
        if(player.actualArma != null && CheckCD())
        {
            if (_armaAtual != player.actualArma)
            {
                _armaAtual = player.actualArma;
                player.AtivarEscudo(Mathf.FloorToInt(player.player.hp * 0.20f));
                inCD = true;
            }
        }
    }
    public override bool CheckCD()
    {
        if (inCD == true)
        {
            actualtimeCD -= Time.deltaTime;
            if (actualtimeCD <= finishedShildTime)
                _player.DesativarEscudo(Mathf.FloorToInt(_player.player.hp * 0.20f));
            if (actualtimeCD <= 0)
            {
                actualtimeCD = timeCD;
                inCD = false;
                return true;
            }
            return false;
        }
        else
        {
            actualtimeCD = timeCD;
            return true;
        }
    }
}