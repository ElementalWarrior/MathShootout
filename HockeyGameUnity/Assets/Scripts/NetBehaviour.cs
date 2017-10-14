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
        GameObject collider = collision.gameObject;
        if(collision.gameObject.tag == "Puck")
        {
            collider.GetComponent<PuckMovementBehaviour>().Fadeout();
            collider.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            int puckValue = collider.GetComponentInChildren<PuckMathBehaviour>().Number;
            if (name == "HitboxNetEven" && puckValue % 2 == 0)
            {
                ShootoutControllerBehaviour.Controller.IncrementScore(5);
            } else if (name == "HitboxNetOdd" && puckValue % 2 == 1)
            {
                ShootoutControllerBehaviour.Controller.IncrementScore(5);
            }
        }
    }
}
