using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool correctNet = false;
        GameObject collider = collision.gameObject;
        if(collision.gameObject.tag == "Puck")
        {
            collider.GetComponent<PuckMovementBehaviour>().Fadeout(false);
            collider.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            int puckValue = collider.GetComponentInChildren<PuckMathBehaviour>().Number;
            if (tag == "NetEven" )
            {
                if(puckValue % 2 == 0)
                {


                    //set points
                    int pts = 5;
                    ShootoutControllerBehaviour.Controller.IncrementScore(pts);
                    PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points", 0) + pts);
                    correctNet = true;
                } else
                {
                    ShootoutControllerBehaviour.Controller.IncrementScore(-2);
                }
            } else if (tag == "NetOdd")
            {
                if (puckValue % 2 == 1)
                {
                    ShootoutControllerBehaviour.Controller.IncrementScore(5);
                    correctNet = true;
                } else
                {
                    ShootoutControllerBehaviour.Controller.IncrementScore(-2);
                }
            }
        }
        if(correctNet)
        {
            //play sound
            GameObject.Find("goal_sound").GetComponent<AudioSource>().Play();
        } else
        {

            //play sound
            GameObject.Find("buzzer").GetComponent<AudioSource>().Play();
        }
        Log.Submit("NetCollision", correctNet);
    }
}
