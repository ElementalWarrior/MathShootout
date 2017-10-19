using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonBehaviour : MonoBehaviour {

	public AudioSource audio;
	public AudioClip applause;

	// Use this for initialization
	void Start () {
		applause = Resources.Load<AudioClip> ("Prize/applause");
		audio = gameObject.AddComponent<AudioSource> ();
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

    public void Intro()
    {
        SceneManager.LoadScene("Intro");
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
        HandleDifficulty(ShootoutControllerBehaviour.Difficulty.Easy, "Intro");
    }

    public void Medium()
    {
        HandleDifficulty(ShootoutControllerBehaviour.Difficulty.Medium, "Intro");

    }

    public void Hard()
    {
        HandleDifficulty(ShootoutControllerBehaviour.Difficulty.Hard, "Intro");

    }
    private void HandleDifficulty(ShootoutControllerBehaviour.Difficulty difficulty, string scene)
    {
        PlayerPrefs.SetInt("Difficulty", (int)difficulty);
        SceneManager.LoadScene(scene);
    }
    public void FinishedShootout(bool won)
    {
        MatchEndBehaviour.WonMatch = won;
        SceneManager.LoadScene("MatchEnd");

		if (won) {
			audio.PlayOneShot (applause);
		}
    }

    public void BackToStandings()
    {
        SceneManager.LoadScene("Standings");
    }

    public void MenuPageAndRefreshPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        MenuPage();
    }

    public void PrizePage()
    {
        SceneManager.LoadScene("Prize");
		audio.PlayOneShot (applause);
    }

    public void MenuPage()
    {
        SceneManager.LoadScene("Menu");
    }
}
