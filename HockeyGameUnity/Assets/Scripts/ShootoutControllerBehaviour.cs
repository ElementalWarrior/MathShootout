using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootoutControllerBehaviour : MonoBehaviour {

    public static ShootoutControllerBehaviour Controller { get;  private set; }
    public GameObject Puck;
    public GameObject ArrowSprite;
    private int puckCnt = 0;
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
        puckCnt++;
        newObj.name += puckCnt;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
