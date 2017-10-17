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

    // Use this for initialization
    void Start ()
    {
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
        switch (DifficultySetting)
        {
            case Difficulty.Medium:
                medium.SetActive(true);
                FinishAt = DateTime.Now.AddSeconds(75);
                break;
            case Difficulty.Hard:
                hard.SetActive(true);
                FinishAt = DateTime.Now.AddSeconds(60);
                break;
            default:
            case Difficulty.Easy:
                easy.SetActive(true);
                FinishAt = DateTime.Now.AddSeconds(90);
                break;
        }

        StartedAt = DateTime.Now;
        TimerNumber = GameObject.Find("TimerNumber");
    }

    public void CreateNewPuck()
    {
        GameObject newObj = Instantiate(Puck);
    }
	// Update is called once per frame
	void Update () {
        double timerValue = (FinishAt - DateTime.Now).TotalSeconds;
        TimerNumber.GetComponent<Text>().text = ((int)timerValue).ToString();

        if(timerValue <= 0)
        {
            FinishShootout();
        }
    }
    public void FinishShootout()
    {

    }
    public void IncrementScore(int add)
    {
        int currentValue = int.Parse(GameObject.Find("ScoreNumber").GetComponent<Text>().text);
        GameObject.Find("ScoreNumber").GetComponent<Text>().text = (currentValue + add).ToString();
    }
}
