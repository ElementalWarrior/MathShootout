using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchEndBehaviour : MonoBehaviour {

    public static bool WonMatch;
    private Color SetAlpha(Color color, float a)
    {
        Color newColor = GameObject.Find("endwin").GetComponent<SpriteRenderer>().color;
        newColor.a = a;
        return newColor;
    }
    // Use this for initialization
    void Start () {

        SpriteRenderer srWin = GameObject.Find("endwin").GetComponent<SpriteRenderer>();
        SpriteRenderer srLose = GameObject.Find("endlose").GetComponent<SpriteRenderer>();

        if (WonMatch)
        {
            srWin.color = SetAlpha(srWin.GetComponent<SpriteRenderer>().color, 1);
            srLose.color = SetAlpha(srWin.GetComponent<SpriteRenderer>().color, 0);
        }
        else
        {
            srWin.color = SetAlpha(srWin.GetComponent<SpriteRenderer>().color, 0);
            srLose.color = SetAlpha(srWin.GetComponent<SpriteRenderer>().color, 1);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
