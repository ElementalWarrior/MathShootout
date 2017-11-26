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
	IEnumerator Start () {
        Log.Submit("SceneStarted", SceneManager.GetActiveScene().name);
		applause = Resources.Load<AudioClip> ("Prize/applause");
		audio = gameObject.AddComponent<AudioSource> ();
        if (!PlayerPrefs.HasKey("session_id"))
        {
            PlayerPrefs.SetString("session_id", Guid.NewGuid().ToString());
        }
        if (!PlayerPrefs.HasKey("session_start"))
        {
            PlayerPrefs.SetString("session_start", DateTime.Now.ToString());
        }

		/* User does not have location services on */
		if (!Input.location.isEnabledByUser) {
			//			location_on = false;
			//			yield break;
		}

		/* User does have location service on */
		Input.location.Start();

		int start_time = 0;

		/* Wait up to 20 seconds for location services to initialize */
		while ((Input.location.status == LocationServiceStatus.Initializing) && (start_time <= 20)) {
			yield return new WaitForSeconds(1);
			start_time++;
		}
	}
    private void OnDestroy()
    {
        Log.Submit("SceneEnded", SceneManager.GetActiveScene().name);
    }
    static DateTime? lastHeartbeatLog = null;
    IEnumerator Heartbeat()
    {
        if(lastHeartbeatLog == null || (DateTime.Now - lastHeartbeatLog.Value).TotalSeconds > 30)
        {
            lastHeartbeatLog = DateTime.Now;
            Log.Submit("Heartbeat", "");
        }
        yield return null;
    }
    // Update is called once per frame
    void Update () {
        StartCoroutine(Heartbeat());
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
		PlayerPrefs.DeleteAll();
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
		PlayerPrefs.DeleteAll();
    }

    public void MenuPage()
    {
        SceneManager.LoadScene("Menu");
    }
}
