using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterTeamNameScript : MonoBehaviour {

	public static string team_name;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetString ("TeamName", GameObject.Find ("PathToTheGameObject").GetComponent<InputField> ().text);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
