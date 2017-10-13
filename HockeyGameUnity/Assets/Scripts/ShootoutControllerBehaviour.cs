using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Instantiate(Puck);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
