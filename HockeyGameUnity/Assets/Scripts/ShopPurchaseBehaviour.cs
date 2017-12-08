using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPurchaseBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseUp()
    {
        if(name == "purchased_striker")
        {
            PurchaseStriker();
        } else if(name == "purchased_puck")
        {
            PurchasePuck();
        }
    }

    public void PurchaseStriker()
    {
        if (!StandingsBehaviour.ShopOpen)
        {
            return;
        }
        Debug.Log("Hit Striker");
        if (PlayerPrefs.GetInt("DonCherryMode", 0) != 1)
        {
            if (PlayerPrefs.GetInt("Points", 0) >= 1000)
            {
                PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points", 0) - 1000);
                PlayerPrefs.SetInt("DontCherryMode", 1);
                Color col = Color.white;
                col.a = 1;
                GameObject.Find("purchased_striker").GetComponent<SpriteRenderer>().color = col;
            }
        }
    }

    public void PurchasePuck()
    {
        if (!StandingsBehaviour.ShopOpen)
        {
            return;
        }
        if (PlayerPrefs.GetInt("CanadaPuck", 0) != 1)
        {
            if (PlayerPrefs.GetInt("Points", 0) >= 500 || true)
            {
                PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points", 0) - 500);
                PlayerPrefs.SetInt("CanadaPuck", 1);
                Color col = Color.white;
                col.a = 1;
                GameObject.Find("purchased_puck").GetComponent<SpriteRenderer>().color = col;
            }
        }

    }
}
