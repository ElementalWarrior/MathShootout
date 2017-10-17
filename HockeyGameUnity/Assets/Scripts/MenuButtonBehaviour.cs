using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
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
