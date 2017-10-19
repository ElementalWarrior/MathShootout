using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject menu_music = GameObject.Find("menu_music");
        if(menu_music == null)
        {
            menu_music = Instantiate(Resources.Load<GameObject>("menu_music"));
            menu_music.name = "menu_music";
        }
        if(menu_music != null)
        {
            menu_music.transform.parent = null;
            DontDestroyOnLoad(menu_music);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
