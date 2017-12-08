using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootoutControllerBehaviour : MonoBehaviour {
    
    public static ShootoutControllerBehaviour Controller { get; private set; }
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    public static Difficulty DifficultySetting;
    public GameObject Puck;
    public GameObject ArrowSprite;
    public DateTime StartedAt;
    public DateTime FinishAt;
    public GameObject TimerNumber;
    public int ScoreToBeat;
    public int TimerDebugSkip = 0;

    StandingsBehaviour.Round CurrentRound;

    // Use this for initialization
    void Start ()
    {
        if(PlayerPrefs.GetInt("SaberSounds", 0) == 1)
        {
            GameObject.Find("shoot").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Shootout/saber_sound");
        }
        GameObject.Destroy(GameObject.Find("menu_music"));
        if(PlayerPrefs.HasKey("Difficulty"))
        {
            DifficultySetting = (Difficulty)PlayerPrefs.GetInt("Difficulty");
        } else
        {
            DifficultySetting = Difficulty.Easy;
        }
        if (!PlayerPrefs.HasKey("CurrentRound"))
        {
            CurrentRound = StandingsBehaviour.Round.QuarterFinal;
        }
        else
        {
            CurrentRound = JsonUtility.FromJson<StandingsBehaviour.Round>(PlayerPrefs.GetString("CurrentRound"));
        }

        if(CurrentRound == StandingsBehaviour.Round.QuarterFinal)
        {
            ScoreToBeat = 50;
        } else if(CurrentRound == StandingsBehaviour.Round.SemiFinal)
        {
            ScoreToBeat = 70;
        }
        else if (CurrentRound == StandingsBehaviour.Round.Finals)
        {
            ScoreToBeat = 120;
        }

        Controller = this;
        ArrowSprite = GameObject.Find("ArrowSprite");
        ArrowSprite.SetActive(false);
        Puck = Resources.Load<GameObject>("Shootout/Puck");
        CreateNewPuck();

        GameObject easy = GameObject.Find("Easy");
        GameObject medium = GameObject.Find("Medium");
        GameObject hard = GameObject.Find("Hard");
        easy.SetActive(false);
        medium.SetActive(false);
        hard.SetActive(false);
        int duration;
        switch (DifficultySetting)
        {
            case Difficulty.Medium:
                duration = 75;
                ScoreToBeat = (int)(ScoreToBeat * 1.5);
                medium.SetActive(true);
                break;
            case Difficulty.Hard:
                duration = 60;
                ScoreToBeat = (int)(ScoreToBeat * 2);
                hard.SetActive(true);
                break;
            default:
            case Difficulty.Easy:
                duration = 90;
                easy.SetActive(true);
                break;
        }
        FinishAt = DateTime.Now.AddSeconds(duration);
        Log.Submit("ShootoutDuration", duration);
        Log.Submit("ShootoutScoreToBeat", ScoreToBeat);

        StartedAt = DateTime.Now;
        TimerNumber = GameObject.Find("TimerNumber");
    }

    public void CreateNewPuck()
    {
        GameObject newObj = Instantiate(Puck);
    }
	// Update is called once per frame
	void Update ()
    {
        GameObject.Find("TargetNumber").GetComponent<Text>().text = ScoreToBeat.ToString();
        double timerValue = (FinishAt - DateTime.Now).TotalSeconds - TimerDebugSkip;
        TimerNumber.GetComponent<Text>().text = ((int)timerValue).ToString();

        if(timerValue <= 0)
        {
            FinishShootout();
        }
    }
    public void FinishShootout()
    {
        int currentValue = int.Parse(GameObject.Find("ScoreNumber").GetComponent<Text>().text);
        bool won = currentValue > ScoreToBeat;

        if(won)
        {
            StandingsBehaviour.Round NextRound = ((StandingsBehaviour.Round)((int)CurrentRound + 1));
            PlayerPrefs.SetString("CurrentRound", JsonUtility.ToJson(NextRound));
        }
        Log.Submit("ShootoutOutcome", won ? "Won" : "Lost");
        Log.Submit("ShootoutScore", currentValue);
        GameObject.Find("Main Camera").GetComponent<MenuButtonBehaviour>().FinishedShootout(won);

    }
    public void IncrementScore(int add)
    {
        int currentValue = int.Parse(GameObject.Find("ScoreNumber").GetComponent<Text>().text);
        GameObject.Find("ScoreNumber").GetComponent<Text>().text = (currentValue + add).ToString();
    }
}
