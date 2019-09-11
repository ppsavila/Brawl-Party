﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteInstancias : MonoBehaviour
{
    public GameObject[] go;
<<<<<<< Updated upstream
    public int velocidadeQueda = 10;
=======
    public GameObject cxPadrao;
    public int velocidadeQueda = 3;
>>>>>>> Stashed changes

    GameObject goAtual;
    Vector3 pontoRef;
    bool podeContinuar;
    float timer;
    public int tempoRespawn = 5;
    void Start()
    {
        goAtual = null;
        podeContinuar = true;
        timer = 0;
        pontoRef = Vector3.zero;
    }

    void Update()
    {
        Instanciar();
        ControlarQueda();
    }

    void InstanciarRandom()
    {
        int x = Random.Range(-15, 16);
        int z = Random.Range(-15, 16);
        pontoRef = new Vector3(x, 20, z);

        if (goAtual == null)
        {
            //goAtual = Instantiate(go[Random.Range(0, go.Length)], pontoRef, Quaternion.identity);
            goAtual = Instantiate(cxPadrao, pontoRef, Quaternion.identity);
        }
    }

    void InstaciarCaixa()
    {

    }

    void Instanciar()
    {
        timer += Time.deltaTime;
        if (timer >= tempoRespawn)
        {
            timer = 0;
            InstanciarRandom();
        }
    }

    public GameObject InstanciarArma(int b)
   {
       int rnd = Random.Range(0,5);
       switch(rnd)
       {
            case 0:
                go[b].GetComponent<ArmaController>().actualArma = new Pistol() ;
                break;
            case 1:
             go[b].GetComponent<ArmaController>().actualArma = new Pistol();
                break;
  
            case 2:
             go[b].GetComponent<ArmaController>().actualArma = new Shotgun();
                break;
  
            case 3:
             go[b].GetComponent<ArmaController>().actualArma = new Shotgun();
                break;
  
            case 4:
             go[b].GetComponent<ArmaController>().actualArma = new Pistol();
                break;

            default:
                break;
       }
       return go[b];
   }

    void ControlarQueda()
    {
        if(goAtual != null)
        {
            Vector3 valor = goAtual.transform.position;
            if (goAtual.transform.position.y > 0.5)
            {
                goAtual.transform.position = new Vector3(valor.x, valor.y -= Time.deltaTime*velocidadeQueda, valor.z);
                goAtual.transform.Rotate(0,Mathf.PingPong(Time.time, 2)-1,0);
                
            }
            else
            {
                int b = Random.Range(0, go.Length);
                if(b != 3)
                    Instantiate(go[b], goAtual.transform.position, Quaternion.identity);
                else
                    Instantiate(InstanciarArma(b), goAtual.transform.position, Quaternion.identity);
                    
                Destroy(goAtual);
                goAtual = null;
            }
        }
    }

}
