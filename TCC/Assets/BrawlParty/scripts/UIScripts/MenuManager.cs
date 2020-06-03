﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using OnlyLemons.BrawlParty.UI;


public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuInicial;
    [SerializeField]
    private GameObject menuInicialButton;
    [SerializeField]
    private GameObject creditos;
    [SerializeField]
    private GameObject config;
    [SerializeField]
    private GameObject gameConfig;
    [SerializeField]
    private GameObject selectMode;
    [SerializeField]
    private GameObject firstScene;
    [SerializeField]
    private GameObject exitPopup;
    [SerializeField]
    private EventSystem es;

    public string ActiveScene { get; set; }

    private AnimationManager AnimManager => _animManager;

    [Header("Animation Refferences")]
    [SerializeField]
    private AnimationManager _animManager = null;


    public void Abrir(GameObject obj)
    {
        obj.SetActive(true);
        menuInicial.SetActive(false);
    }

    public void SetSelectedObj(GameObject obj)
    {
        es.SetSelectedGameObject(obj);
    }

    private void Update()
    {
        if(firstScene.activeSelf && Input.anyKey)
        {
            AnimManager.FirstToHome();
            es.SetSelectedGameObject(menuInicialButton);
        }
    }

    public void OnReturn()
    {
        switch (ActiveScene) 
        {
            case ("Credits") :
                AnimManager.CreditsToMenu();
                break;

            case ("Settings"):
                AnimManager.SettingsToMenu();
                config.SetActive(false);
                menuInicial.SetActive(true);
                break;

            case ("Single") :
                Debug.Log("SingleToHome");
                break;
            case ("Exit"):
                AnimManager.DontExit();
                break;

            default :
                Debug.Log("Vix");
                break;
        }

        //menuInicial.SetActive(true);
        es.SetSelectedGameObject(menuInicialButton);
        //creditos.SetActive(false);
        //config.SetActive(false);
        //firstScene.SetActive(false);
        //gameConfig.SetActive(false);
        //selectMode.SetActive(false);
    }

}
