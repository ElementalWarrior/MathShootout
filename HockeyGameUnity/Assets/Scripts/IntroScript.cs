using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {

	public Sprite[] cutscenes;
	public Sprite[] primary_masks;
    public SpriteRenderer current_image;
    public SpriteRenderer primary_mask;
	public Button next;
	public int loc = 0;

	void Start () {
		cutscenes = Resources.LoadAll<Sprite> ("Intro/Cutscene");
		primary_masks = Resources.LoadAll<Sprite> ("Intro/primary_mask");
        current_image = GameObject.Find("Cutscene1").GetComponent<SpriteRenderer> ();
		primary_mask = GameObject.Find("primary_mask").GetComponent<SpriteRenderer> ();

        SpriteRenderer secondary_mask = GameObject.Find("secondary_mask").GetComponent<SpriteRenderer>();
        string strPrimColour = PlayerPrefs.GetString("PrimaryColour");
        string strSecColour = PlayerPrefs.GetString("SecondaryColour");
        Color primary_colour = Color.white;
        Color secondary_colour = Color.white;
        if (!string.IsNullOrEmpty(strPrimColour))
        {
            primary_colour = JsonUtility.FromJson<Color>(strPrimColour);
        } if(!string.IsNullOrEmpty(strSecColour)) { 
            secondary_colour = JsonUtility.FromJson<Color>(strSecColour);
        }
        primary_mask.color = primary_colour;
        secondary_mask.color = secondary_colour;
    }

	public void NextButton() {
		/* Loop through all intro scenes */
		if (cutscenes.Length > (loc + 1)) {
			loc++;

            Vector2 prevSize = new Vector2(primary_masks[loc - 1].rect.width, primary_masks[loc - 1].rect.height);
            Vector2 currSize = new Vector2(primary_masks[loc].rect.width, primary_masks[loc].rect.height);

            GameObject gamePrimaryMask = GameObject.Find("primary_mask");
            //Vector2 sizeDiff = currSize - prevSize;
            //sizeDiff /= 100;
            //sizeDiff.x *= gamePrimaryMask.transform.lossyScale.x;
            //sizeDiff.y *= gamePrimaryMask.transform.localScale.y;
            //gamePrimaryMask.transform.position += (Vector3)sizeDiff;
        }

		/* If intro is finished, load game */
		if (cutscenes.Length == (loc + 1)) {
			SceneManager.LoadScene ("Shootout");
		}
	}

	void Update () {
		current_image.sprite = cutscenes [loc];
		primary_mask.sprite = primary_masks [loc];
    }
}
