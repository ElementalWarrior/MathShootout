using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {

	public Sprite[] cutscenes;
	public Image current_image;
	public Button next;
	public int loc = 0;

	void Start () {
		cutscenes = Resources.LoadAll<Sprite> ("Intro");
	}

	public void NextButton() {
		/* Loop through all intro scenes */
		if (cutscenes.Length > (loc + 1)) {
			loc++;
		}

		/* If intro is finished, load game */
		if (cutscenes.Length == (loc + 1)) {
			SceneManager.LoadScene ("Shootout");
		}
	}

	void Update () {
		current_image.sprite = cutscenes [loc];
	}
}
