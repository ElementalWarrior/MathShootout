using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PuckMovementBehaviour : MonoBehaviour {
    DateTime? timeFadeStart = null;
    Boolean _restrictYVelocity = false;
    bool _isMiss = true;
	// Use this for initialization
	void Start () {
	}

    public void Fadeout(bool isMiss = true)
    {
        _isMiss = isMiss;
        timeFadeStart = DateTime.Now;
    }

    //take a color and set the opacity
    private Color SetOpacity(Color c, float opacity)
    {
        Color newColor = c;
        newColor.a = Mathf.Max(0, opacity);
        return newColor;
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
            GetComponent<SpriteRenderer>().color = SetOpacity(GetComponent<SpriteRenderer>().color, (float)(1 - secondsSincefadeStart));

            //the text opacity is not depenedet on the puck sprite, so we need to fade that too
            UnityEngine.UI.Text txt = GetComponentsInChildren<UnityEngine.UI.Text>()[0];
            txt.color = SetOpacity(txt.color, (float)(1 - secondsSincefadeStart));
        }
        //dirty way to remove puck after faded out.
        if (GetComponent<SpriteRenderer>().color.a < 0.001)
        {
            if (_isMiss)
            {
                Log.Submit("PuckMiss", "");
            }
            GameObject.Destroy(gameObject);
        }
        //stop y position if it hits the position of the backboard.
        //and not inside the net
        if (transform.position.y > 1.5)
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
