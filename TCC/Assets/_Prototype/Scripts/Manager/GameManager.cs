﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject playerInputPrefab;
    public IGameMode gameMode;
    public GameController gameController;
    public float TimeInGame = 120;
    public GameModes newGameMode;
    public List<GameObject> playersPanels = new List<GameObject>();
    public Vector3 lastPainel;

    #region LevelInteract
    public int nextLevel;
    #endregion  
    private void OnEnable()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }      
    }


    private void FixedUpdate()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 4: // Menu do Personagem
                foreach (GameObject player in playersPanels)
                {
                    player.transform.GetChild(0).gameObject.SetActive(false);
                    player.transform.GetChild(1).gameObject.SetActive(true);
                }

                    transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    if (playersPanels.Count > 0 && !playersPanels.Find(x => x.GetComponentInChildren<PlayerSelect>().isConfirmed == false))
                    {
                          SceneManager.LoadScene(5); // provisorio
                    }


                break;

            case 7:  //cenas de jogo no caso essa é a primeira 
                foreach(GameObject i in GameObject.FindGameObjectsWithTag("Player")) //Correção temporária pra bug
                {
                    i.transform.parent = this.transform;
                    if (i.transform.position.x > 40 || i.transform.position.y > 40 || i.transform.position.z > 40)
                        i.transform.position = new Vector3(0, 0, 0);
                }
                break;

            default:
                foreach (GameObject player in playersPanels)
                {
                    player.transform.GetChild(1).gameObject.SetActive(false);
                    player.transform.GetChild(0).gameObject.SetActive(true);
                }
                transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                break;
        }
    }



    public void TryGetGameController()
    {
        if(GameObject.FindObjectOfType<GameController>() != null)
        {
            gameController = GameObject.FindObjectOfType<GameController>();
            foreach (GameObject playerComplete in playersPanels)
            {
                if (!gameController.playerManager.Players.Exists(x => x == playerComplete.transform.GetChild(0).GetComponent<PlayerController>()))
                gameController.playerManager.Players.Add(playerComplete.transform.GetChild(0).GetComponent<PlayerController>());
               // gameController.playerManager.Players.Add(playerComplete.transform.GetChild(0).GetComponent<PlayerController>());
            }
           

            newScene(newGameMode);
        }

    }
    public void newScene(GameModes game)
    {
        if (game == GameModes.CaptureTheFlag)
        {
            gameController.gameMode =  new CaptureTheFlag(gameController, TimeInGame);
        }
        else if (game == GameModes.FreeForAll)
        {
            gameController.gameMode = new FreeForAll(gameController, TimeInGame);
        }
    }


}
