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
        if(!PlayerPrefs.HasKey("TeamName"))
        {
            SceneManager.LoadScene("TeamName");
        } else if(!PlayerPrefs.HasKey("PrimaryColour"))
        {
            SceneManager.LoadScene("TeamColours");
        } else if(!PlayerPrefs.HasKey("Difficulty"))
        {
            SceneManager.LoadScene("Difficulty");
        } else 
        {
            SceneManager.LoadScene("Standings");
        }
    }

    public void ChooseName()
    {
        PlayerPrefs.SetString("TeamName", GameObject.Find("InputField").GetComponent<InputField>().text);
        SceneManager.LoadScene("TeamColours");
    }

    public void ChooseColours()
    {
        Color[] colours = GameObject.Find("Main Camera").GetComponent<TeamColourBehaviour>().ChosenColours;
        PlayerPrefs.SetString("PrimaryColour", JsonUtility.ToJson(colours[0]));
        PlayerPrefs.SetString("SecondaryColour", JsonUtility.ToJson(colours[1]));
        SceneManager.LoadScene("Difficulty");
    }

    public void PlayNextMatch()
    {
        SceneManager.LoadScene("Shootout");
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
        LoadShootout(ShootoutControllerBehaviour.Difficulty.Easy);
    }

    public void Medium()
    {
        LoadShootout(ShootoutControllerBehaviour.Difficulty.Medium);

    }

    public void Hard()
    {
        LoadShootout(ShootoutControllerBehaviour.Difficulty.Hard);

    }
    private void LoadShootout(ShootoutControllerBehaviour.Difficulty difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", (int)difficulty);
        SceneManager.LoadScene("Standings");
    }
    public void FinishedShootout(bool won)
    {
        MatchEndBehaviour.WonMatch = won;
        SceneManager.LoadScene("MatchEnd");
    }

    public void BackToStandings()
    {
        SceneManager.LoadScene("Standings");
    }

    public void PrizePage()
    {
        SceneManager.LoadScene("Prize");
    }

    public void MenuPage()
    {
        SceneManager.LoadScene("Menu");
    }
}
