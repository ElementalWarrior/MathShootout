using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuckMathBehaviour : MonoBehaviour {

    public int Number;
	// Use this for initialization
	void Start () {
        Number = Random.Range(0, 100);
        GetComponent<Text>().text = Number.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
