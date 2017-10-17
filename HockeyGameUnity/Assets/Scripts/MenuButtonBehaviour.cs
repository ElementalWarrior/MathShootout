using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        if(String.IsNullOrEmpty(PlayerPrefs.GetString("TeamName")))
        {
            SceneManager.LoadScene("TeamName");
        } else
        {
            SceneManager.LoadScene("Standings");
        }
    }

    public void ChooseName()
    {
        Debug.Log(GameObject.Find("InputField").GetComponent<InputField>().text);
        PlayerPrefs.SetString("TeamName", GameObject.Find("InputField").GetComponent<InputField>().text);
        SceneManager.LoadScene("Difficulty");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Easy()
    {
        ShootoutControllerBehaviour.DifficultySetting = ShootoutControllerBehaviour.Difficulty.Easy;
        SceneManager.LoadScene("Shootout");
    }

    public void Medium()
    {
        ShootoutControllerBehaviour.DifficultySetting = ShootoutControllerBehaviour.Difficulty.Medium;
        SceneManager.LoadScene("Shootout");

    }

    public void Hard()
    {
        ShootoutControllerBehaviour.DifficultySetting = ShootoutControllerBehaviour.Difficulty.Hard;
        SceneManager.LoadScene("Shootout");

    }
}
