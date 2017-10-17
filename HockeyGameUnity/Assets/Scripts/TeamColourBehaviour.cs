using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TeamColourBehaviour : MonoBehaviour {

    public Dictionary<string, string> Colours = new Dictionary<string, string>() {
        { "Green", "0D974700" },
        { "Aqua", "4AADD100" },
        { "Red", "BD141800" },
        { "Brown", "C16F0C00" },
        { "Yellow", "EFE63400" },
        { "Purple", "472C8100" },
        { "Black", "08080C00" },
        { "Blue", "1022A500" },
        //new KeyValuePair<string, string>("Green", "0D974700")
    };
    public Color[] ChosenColours = new Color[2];
    public bool[] ColoursAreSet = new bool[2];
    public bool ChoosingPrimary = true;
    public GameObject PrimaryIndicator;
    public GameObject SecondaryIndicator;
    // Use this for initialization
    void Start()
    {
        PrimaryIndicator = GameObject.Find("choosingprimary");
        SecondaryIndicator = GameObject.Find("choosingsecondary");

        Button buttonScript = GameObject.Find("Continue").GetComponent<Button>();
        buttonScript.interactable = false;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (ColoursAreSet[0])
        {
            GameObject.Find("PrimaryColour").GetComponent<SpriteRenderer>().color = ChosenColours[0];
        }

        if (ColoursAreSet[1]) { 
            GameObject.Find("SecondaryColour").GetComponent<SpriteRenderer>().color = ChosenColours[1];
        }

        PrimaryIndicator.SetActive(ChoosingPrimary);
        SecondaryIndicator.SetActive(!ChoosingPrimary);

        Button buttonScript = GameObject.Find("Continue").GetComponent<Button>();
        Image buttonImage = GameObject.Find("Continue").GetComponent<Image>();

        if (ColoursAreSet[0] && ColoursAreSet[1])
        {
            buttonScript.interactable = true;
        }
        //buttonImage.color = buttonScript.interactable ? buttonScript.colors.normalColor : buttonScript.colors.disabledColor;
	}

    public void SetColour()
    {
        Color colour = GameObject.Find(EventSystem.current.currentSelectedGameObject.name).GetComponent<Image>().color;
        int index = ChoosingPrimary ? 0 : 1;
        ChosenColours[index] = colour;//Colours[colour];
        ColoursAreSet[index] = true;

        //swap to the other colour
        ChoosingPrimary = !ChoosingPrimary;
    }
}
