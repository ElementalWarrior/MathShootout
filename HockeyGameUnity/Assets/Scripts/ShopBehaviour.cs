using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("Points").GetComponent<Text>().text = "Points: " + PlayerPrefs.GetInt("Points").ToString();
        if(PlayerPrefs.GetInt("CanadaPuck", 0) == 1)
        {
            GameObject.Find("purchased_puck").GetComponent<SpriteRenderer>().color = Color.white;
        }
        if(PlayerPrefs.GetInt("DonCherryMode", 0) == 1)
        {
            GameObject.Find("purchased_striker").GetComponent<SpriteRenderer>().color = Color.white;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
