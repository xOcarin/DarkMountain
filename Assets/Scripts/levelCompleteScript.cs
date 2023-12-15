using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class levelCompleteScript : MonoBehaviour
{
    public TextMeshProUGUI time;
    public TextMeshProUGUI health;
    public TextMeshProUGUI rank;

    public CinemachineFreeLook freeLookCamera;
    
    public GameObject ui;
    
    
    void Start()
    {
        time = GameObject.Find("time").GetComponent<TextMeshProUGUI>();
        health = GameObject.Find("health").GetComponent<TextMeshProUGUI>();
        rank = GameObject.Find("rank").GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        float timeNum = playerAudioHandler.time; 
        float healthNum = HealthScript.playerHealth; 
        string rankFin;

        float timeScore;
        float healthScore = 0;
        float rankScore;
        
        time.text = "Time: " + timeNum;
        health.text = "Health: " + healthNum + "/3";

        switch (healthNum)
        {
            case 1:
                healthScore = 20;
                break;
            case 2:
                healthScore = 40;
                break;
            case 3:
                healthScore = 60;
                break;
        }
        
        if (timeNum <= 60)
        {
            timeScore = 100;
        }
        else if (timeNum >= 61 && timeNum <= 80)
        {
            timeScore = 80;
        }
        else if (timeNum >= 81 && timeNum <= 100)
        {
            timeScore = 60;
        }
        else if (timeNum >= 101 && timeNum <= 120)
        {
            timeScore = 40;
        }
        else if (timeNum >= 121 && timeNum <= 140)
        {
            timeScore = 20;
        }
        else
        {
            timeScore = 0;
        }

        rankScore = timeScore + healthScore;

        switch (rankScore)
        {
            case 160:
                rankFin = "S";
                break;
            case 140:
                rankFin = "A";
                break;
            case 120:
                rankFin = "B";
                break;
            case 100:
                rankFin = "C";
                break;
            case 80:
                rankFin = "D";
                break;
            default:
                rankFin = "F";
                break;
        }


        if (RescueeAudioScript.levelOver)
        {
            ui.SetActive(false);
            freeLookCamera.enabled = false;
            if (Input.GetButton("Jump"))
            {
                HealthScript.playerHealth = 3;
                SceneManager.LoadScene("level2");
            }
        }
        
        
        
        
        rank.text = "Rank: " + rankFin;



    }
}
