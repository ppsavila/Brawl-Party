﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItRock : IGameMode
{
    GameController aux;
    float timeOfGame;
    GameObject[] hammers = new GameObject[6];
    List<PlayerController> winners = new List<PlayerController>();
    Dictionary<PlayerController, bool> playerMortos = new Dictionary<PlayerController, bool>();
    bool adicionolPoint = false;
    int numwinner = 0;
    float lasthit = 2;
    public GetItRock(GameController gameController, float time)
    {
        aux = gameController;
        timeOfGame = time;
    }
    public void DeathRule(PlayerController player)
    {
        player.ResetarPlayer();
        player.gameObject.SetActive(false);

        playerMortos[player] = true;
        if (VerifyPlayerMortos())
        {
            winners[numwinner] = player;
            numwinner++;
            WinRule();
        }
    }
    void fallRock()
    {

        List<int> posicoes = new List<int>();
        int HammerQuant = Random.Range(1, hammers.Length - 1);
        for (int i = 0; i < HammerQuant; i++)
        {
            int hammer = Random.Range(0, hammers.Length);
            while (posicoes.Contains(hammer))
            {
                hammer = Random.Range(0, hammers.Length);
            }
            posicoes.Add(hammer);
            //TO-DO hammers[hammer].getComponent<Animation>()...

        }
        for (int i = 0; i < posicoes.Count; i++)
        {
            hammers[i].GetComponent<Animator>().SetTrigger("fall");
        }
    }
    public void FinishGame()
    {
        if (!adicionolPoint)
        {
            lasthit -= Time.deltaTime;
            timeOfGame -= Time.deltaTime;
            ShowTime();
            if (timeOfGame <= 0)
            {
                InsertWinners();
                WinRule();
            }
            if (lasthit <= 0)
            {
                fallRock();
                lasthit = 5;
            }
        }
    }
    void InsertWinners()
    {
        int a = 0;
        for (int i = 0; i < aux.playerManager.playersControllers.Count; i++)
        {
            if (playerMortos[aux.playerManager.playersControllers[i]])
            {
                winners[a] = aux.playerManager.playersControllers[i];
                a++;
            }
        }
    }
    public void ShowTime()
    {
        string minute = ((int)(timeOfGame / 60)).ToString("00"); ;
        string seconds = ((int)(timeOfGame % 60)).ToString("00"); ;
        aux.time.text = minute + ":" + seconds;
    }
    bool VerifyPlayerMortos()
    {
        for (int i = 0; i < playerMortos.Count; i++)
        {
            if (playerMortos[GameController.singleton.playerManager.playersControllers[i]] == false)
                return false;
        }
        return true;
    }
    public void MovementRule(Vector3 dir, Transform player, float speed)
    {

        player.transform.position += new Vector3(dir.x, 0, 0) * speed * Time.deltaTime;
        //player.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, -7.5f, 6f),player.position.y,player.position.z);
    }


    public void PointRule(PlayerController player)
    {

    }

    public void RotationRule(Vector3 dir, Transform player)
    {

    }

    public void StartGame()
    {
        InsertPlayerInDates();
        InsertHammersInDates();
    }
    void InsertHammersInDates()
    {
        hammers = GameObject.FindGameObjectsWithTag("Brick");
    }
    void InsertPlayerInDates()
    {
        foreach (PlayerController player in GameController.singleton.playerManager.playersControllers)
        {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
            playerMortos.Add(player, false);
        }
    }
    public void WinRule()
    {
        if (!adicionolPoint)
        {
            for (int i = 0; i < winners.Count; i++)
            {
                GameManager.Instance.pontosGeral[aux.playerManager.playersControllers.IndexOf(winners[i])] += 1;

            }
            aux.FinishGame();
            adicionolPoint = true;
        }
    }

    public void Action(PlayerController player)
    {
        throw new System.NotImplementedException();
    }
}