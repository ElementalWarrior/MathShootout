using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PuckMovementBehaviour : MonoBehaviour {
    DateTime? timeFadeStart = null;
    Boolean _restrictYVelocity = false;
	// Use this for initialization
	void Start () {
	}

    public void Fadeout()
    {
        timeFadeStart = DateTime.Now;
    }
	
	// Update is called once per frame
	void Update () {
        
        //fadeout if hit backboard or past the outer edges of the nets
        if((transform.position.y > 1 || Mathf.Abs(transform.position.x) > 8.5) && timeFadeStart == null)
        {
            Fadeout();
        }

        if (timeFadeStart != null)
        {
            double secondsSincefadeStart = (DateTime.Now - timeFadeStart.Value).TotalSeconds;
            Color newColor = GetComponent<SpriteRenderer>().color;
            newColor.a = Mathf.Max(0, (float)(1 - secondsSincefadeStart));
            GetComponent<SpriteRenderer>().color = newColor;
        }
        //dirty way to remove puck after faded out.
        if (GetComponent<SpriteRenderer>().color.a < 0.001)
        {
            GameObject.Destroy(gameObject);
        }
        //stop y position if it hits the position of the backboard.
        //and not inside the net
        if (Mathf.Abs(transform.position.x) < 2.5 && transform.position.y > 1.5)
        {
            _restrictYVelocity = true;
        }
        if(_restrictYVelocity)
        {
            Vector3 newPos = transform.position;
            newPos.y = Mathf.Min(1.5f, newPos.y);
            transform.position = newPos;
        }
	}
}
