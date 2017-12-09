using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupBehaviour : MonoBehaviour {
    public static bool PuckActive = false;
    public static int ScoreMultiplier = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject.Find("TotalPoints").GetComponent<Text>().text = PlayerPrefs.GetInt("Points", 0).ToString();
	}

    public void OnMouseUp()
    {
        if(name == "powerup_puck")
        {
            PowerupPuck();
        }
        if (name == "powerup_time")
        {
            PowerupTime();
        }
        if (name == "powerup_double")
        {
            PowerupDouble();
        }
    }

    private void PowerupPuck()
    {
        if(PuckActive)
        {
            return;
        }
        if (PlayerPrefs.GetInt("Points", 0) >= 100)
        {
            PuckActive = true;
            PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points", 0) - 100);
        }
    }
    private void PowerupTime()
    {
        if (PlayerPrefs.GetInt("Points", 0) >= 200)
        {
            GameObject.Find("Main Camera").GetComponent<ShootoutControllerBehaviour>().FinishAt = GameObject.Find("Main Camera").GetComponent<ShootoutControllerBehaviour>().FinishAt.Add(TimeSpan.FromSeconds(20));
            PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points", 0) - 200);
        }
    }
    private void PowerupDouble()
    {
        if (PlayerPrefs.GetInt("Points", 0) >= 300)
        {
            ScoreMultiplier*=2;
            PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points", 0) - 300);
        }
    }
}
