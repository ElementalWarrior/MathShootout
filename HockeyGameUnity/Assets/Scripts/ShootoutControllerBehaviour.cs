using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootoutControllerBehaviour : MonoBehaviour {

    public static ShootoutControllerBehaviour Controller { get;  private set; }
    public GameObject Puck;
    public GameObject ArrowSprite;
	// Use this for initialization
	void Start () {
        Controller = this;
        ArrowSprite = GameObject.Find("ArrowSprite");
        ArrowSprite.SetActive(false);
        Puck = Resources.Load<GameObject>("Shootout/Puck");
        CreateNewPuck();
	}

    public void CreateNewPuck()
    {
        GameObject newObj = Instantiate(Puck);
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void IncrementScore(int add)
    {
        int currentValue = int.Parse(GameObject.Find("ScoreNumber").GetComponent<Text>().text);
        GameObject.Find("ScoreNumber").GetComponent<Text>().text = (currentValue + add).ToString();
    }
}
