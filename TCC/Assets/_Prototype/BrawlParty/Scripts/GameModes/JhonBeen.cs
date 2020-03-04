﻿using UnityEngine;
using System.Collections.Generic;


public class JhonBeen : IGameMode
{
    class Stun
    {
        public bool canMove;
        public float timeInStun;
    }
    public class PositionsLR
    {
        public Vector3 left;
        public Vector3 right;
    }
    GameController aux;
    float timeOfGame;
    float timeToSpawn = 0;
    GameObject _bird = Resources.Load("Mecanicas/Bird") as GameObject;
    List<PlayerController> winners = new List<PlayerController>();
    GameObject[] cameras;
    Dictionary<PlayerController, Stun> canMove = new Dictionary<PlayerController, Stun>();
    Dictionary<PlayerController, bool> playerMortos = new Dictionary<PlayerController, bool>();
    Dictionary<PlayerController, PositionsLR> playerPosition = new Dictionary<PlayerController, PositionsLR>();
    bool adicionolPoint = false;

    int vencedor;
    public JhonBeen(GameController gameController, float time)
    {
        timeOfGame = time;
        aux = gameController;
    }
    public void HitRule(PlayerController player)
    {
        player.GetComponent<ParticlePlayer>().Play(1f); // tempo da particula de stun aqui
        canMove[player].canMove = false;
        canMove[player].timeInStun = 1;
    }
    void removePlayersInStun()
    {
        foreach (PlayerController player in GameController.singleton.playerManager.playersControllers)
        {
            if (!canMove[player].canMove)
            {
                canMove[player].timeInStun -= Time.deltaTime;
                if (canMove[player].timeInStun <= 0)
                    canMove[player].canMove = true;
            }
        }
    }

    public void Update()
    {
        if (!adicionolPoint)
        {
            UpdatePositionCamera();
            timeOfGame -= Time.deltaTime;
            ShowTime();
            removePlayersInStun();
            if (timeOfGame <= 0)
            {
                InsertWinners();
                WinRule();
            }

        }
    }

    private void UpdatePositionCamera()
    {
        for (int i = 0; i < aux.playerManager.playersControllers.Count; i++)
        {
            if(aux.playerManager.playersControllers[i] != null)
            {
                if (aux.playerManager.playersControllers[i].transform.position.y > 0)
                    cameras[i].transform.position =  Vector3.Lerp(cameras[i].transform.position, new Vector3(aux.playerManager.playersControllers[i].transform.localPosition.x, aux.playerManager.playersControllers[i].transform.localPosition.y + 5, aux.playerManager.playersControllers[i].transform.localPosition.z - zoomCam), Time.deltaTime*2);
                    else
                    cameras[i].transform.position =  Vector3.Lerp(cameras[i].transform.position, new Vector3(aux.playerManager.playersControllers[i].transform.localPosition.x, aux.playerManager.playersControllers[i].transform.localPosition.y + 5, aux.playerManager.playersControllers[i].transform.localPosition.z - zoomCam), Time.deltaTime*2);
            }
        }
    }

    float zoomCam = 14f;
    void CancelarCameras()
    {
        
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].GetComponent<Camera>().enabled = false;
        }

        for (int i = 0; i < aux.playerManager.playersControllers.Count; i++)
        {
            cameras[i].GetComponent<Camera>().enabled = true;
            cameras[i].GetComponent<Camera>().fieldOfView = 28.41141f;
        }

        //teste

        int qtdPlayersJogando = aux.playerManager.playersControllers.Count;
        
        
        switch (qtdPlayersJogando)
        {
            case 1:
                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
                zoomCam = 30;
                break;
            case 2:
                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
                cameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
                zoomCam = 25;
                break;
            case 3:
                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 0.333f, 1);
                cameras[1].GetComponent<Camera>().rect = new Rect(0.333f, 0, 0.334f, 1);
                cameras[2].GetComponent<Camera>().rect = new Rect(0.667f, 0, 0.333f, 1);
                zoomCam = 16;
                break;
            case 4:
                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 0.25f, 1);
                cameras[1].GetComponent<Camera>().rect = new Rect(0.25f, 0, 0.25f, 1);
                cameras[2].GetComponent<Camera>().rect = new Rect(0.50f, 0, 0.25f, 1);
                cameras[3].GetComponent<Camera>().rect = new Rect(0.75f, 0, 0.25f, 1);
                zoomCam = 14;
                break;
        }
        UpdatePositionCamera();
        //fimteste
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
    void goDownPlayers()
    {
        foreach (PlayerController player in GameController.singleton.playerManager.playersControllers)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 3 * Time.deltaTime, player.transform.position.z);
        }
    }
    public void MovementRule(Vector3 dir, Transform player, float speed)
    {
        
        if (canMove[player.gameObject.GetComponent<PlayerController>()].canMove && !player.gameObject.GetComponent<PlayerController>().travar)
        {
            if (dir.x > 0f)
            {
                player.rotation = Quaternion.Lerp(Quaternion.LookRotation(Vector3.left), Quaternion.identity, Time.deltaTime);
                player.position = new Vector3(playerPosition[player.gameObject.GetComponent<PlayerController>()].right.x, player.transform.position.y, player.transform.position.z);
            }
            if (dir.x < 0f)
            {
                player.rotation = Quaternion.Lerp(Quaternion.LookRotation(Vector3.right), Quaternion.identity, Time.deltaTime);
                player.position = new Vector3(playerPosition[player.gameObject.GetComponent<PlayerController>()].left.x, player.transform.position.y, player.transform.position.z);
            }

            else 
            player.position = player.position;
           
        }
    }

    int vencedorPlaca = 0;
    public void PointRule(PlayerController player)
    {
        //winners.Add(player);
        //numwinner++;
        //WinRule();

        player.transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(Vector3.right), Quaternion.identity, Time.deltaTime);

        GameManager.Instance.pontosGeral[aux.playerManager.playersControllers.IndexOf(player)] += vencedor;
        player.travar = true;
        vencedor--;

        GameObject.Instantiate(player.vencedor[vencedorPlaca], new Vector3(player.transform.position.x,player.transform.position.y + 10, player.transform.position.z), Quaternion.identity, player.gameObject.transform);
        vencedorPlaca++;

        if (vencedor <= 0)
            WinRule();
    }

    public void RotationRule(Vector3 dir, Transform player)
    {

    }

    public void StartGame()
    {
        InsertPlayerInDates();
        GameController.singleton.uIManager.SumirTudo();
      //  UpdatePositionCamera();
        CancelarCameras();
        vencedor = aux.playerManager.playersControllers.Count - 1;

        for (int i = 0; i < aux.playerManager.playersControllers.Count; i++)
        {
            if(aux.playerManager.playersControllers[i] != null)
            {
                    cameras[i].transform.position =  new Vector3(aux.playerManager.playersControllers[i].transform.localPosition.x, aux.playerManager.playersControllers[i].transform.localPosition.y + 5, aux.playerManager.playersControllers[i].transform.localPosition.z - zoomCam);
            }
        }

    }
    void spawnBirds()
    {
        int QuantidadeBomb = Random.Range(10, 18);
        for (int i = 0; i < GameController.singleton.playerManager.playersControllers.Count; i++)
        {
            List<Vector3> posicoesInstance = new List<Vector3>();
            for (int a = 0; a < QuantidadeBomb; a++)
            {
                int side = Random.Range(0, 2);
                Vector3 position;
                if (side == 0)
                {
                    position = new Vector3(aux.tileManager.bases[i].position.x, Random.Range(-15, 17.8f), aux.tileManager.bases[i].position.z);
                    //GameObject.Instantiate(_bird, new Vector3(aux.tileManager.bases[i].x, aux.tileManager.bases[i].y + Random.Range(-19, 17.8f), aux.tileManager.bases[i].z), Quaternion.identity);
                }
                else
                {
                    position = new Vector3(aux.tileManager.bases[i].position.x + 2, Random.Range(-15, 17.8f), aux.tileManager.bases[i].position.z);
                    //GameObject.Instantiate(_bird, new Vector3(aux.tileManager.bases[i].x + 2, aux.tileManager.bases[i].y + Random.Range(-19, 17.8f), aux.tileManager.bases[i].z), Quaternion.identity);
                }
                while (posicoesInstance.Contains(position) && CanInstance(posicoesInstance))
                {
                    side = Random.Range(0, 2);
                    if (side == 0)
                    {
                        position = new Vector3(aux.tileManager.bases[i].position.x, Random.Range(-15, 17.8f), aux.tileManager.bases[i].position.z);
                        //GameObject.Instantiate(_bird, new Vector3(aux.tileManager.bases[i].x, aux.tileManager.bases[i].y + Random.Range(-19, 17.8f), aux.tileManager.bases[i].z), Quaternion.identity);
                    }
                    else
                    {
                        position = new Vector3(aux.tileManager.bases[i].position.x + 2, Random.Range(-15, 17.8f), aux.tileManager.bases[i].position.z);
                        //GameObject.Instantiate(_bird, new Vector3(aux.tileManager.bases[i].x + 2, aux.tileManager.bases[i].y + Random.Range(-19, 17.8f), aux.tileManager.bases[i].z), Quaternion.identity);
                    }
                }
                GameObject.Instantiate(_bird, position, Quaternion.identity);
            }
        }

    }
    bool CanInstance(List<Vector3> posicoes)
    {
        foreach (Vector3 pos in posicoes)
        {
            for (int i = 0; i < posicoes.Count; i++)
            {
                if (Vector3.Distance(pos, posicoes[i]) < 2f)
                {
                    return false;
                }
            }

        }
        return true;
    }
    void InsertPlayerInDates()
    {
        cameras = new GameObject[GameController.singleton.playerManager.playersControllers.Count];
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i] = GameObject.Find("Camera_P"+(i+1));
            //Debug.Log(cameras[i].gameObject.name);
            
        }
        for (int i = 0; i < GameController.singleton.playerManager.playersControllers.Count; i++)
        {


            playerMortos.Add(GameController.singleton.playerManager.playersControllers[i], false);
            PositionsLR auxLR = new PositionsLR();
            playerPosition.Add(GameController.singleton.playerManager.playersControllers[i], auxLR);
            playerPosition[GameController.singleton.playerManager.playersControllers[i]].left = GameController.singleton.playerManager.playersControllers[i].transform.position;
            playerPosition[GameController.singleton.playerManager.playersControllers[i]].right = new Vector3( GameController.singleton.playerManager.playersControllers[i].transform.position.x + 2f,  GameController.singleton.playerManager.playersControllers[i].transform.position.y,  GameController.singleton.playerManager.playersControllers[i].transform.position.z);
            Stun auxStun = new Stun();
            auxStun.canMove = true;
            auxStun.timeInStun = 0;
            canMove.Add(GameController.singleton.playerManager.playersControllers[i], auxStun);
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
        if (canMove[player.gameObject.GetComponent<PlayerController>()].canMove && aux.comecou && !adicionolPoint && !player.travar)
        {
            player.anim.SetTrigger("Climb");
            player.transform.position += new Vector3(0f, 1f, 0f);
        }
    }

    void SubidaSuave()
    {
       
    }
}