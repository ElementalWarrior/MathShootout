using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehaviour : MonoBehaviour {
    public static bool PuckActive = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
