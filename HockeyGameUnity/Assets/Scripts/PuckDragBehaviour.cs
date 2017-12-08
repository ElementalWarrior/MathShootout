using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckDragBehaviour : MonoBehaviour {

	public AudioSource audio;
	public AudioClip shoot;

    // Use this for initialization
	void Start () {
        transform.position = new Vector3(0, -3.39f);

		shoot = Resources.Load<AudioClip> ("Shootout/shoot");
        if (PlayerPrefs.GetInt("SaberSounds", 0) == 1)
        {
            shoot = Resources.Load<AudioClip>("Shootout/saber_sound");
        }
        audio = gameObject.AddComponent<AudioSource> ();
	}
    private bool _dragging = false;
	// Update is called once per frame
	void Update ()
    {
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        BoxCollider2D collider = this.GetComponent<BoxCollider2D>();
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();

        //using half a second has passed to make sure that two pucks don't fire when letting go of the previous puck.
        if (Input.GetMouseButtonDown(0)) {
            
            if(collider.OverlapPoint(mousePoint))
            {
                _dragging = true;
            }
        } else if(_dragging && Input.GetMouseButtonUp(0)) //fired puck
        {
            _dragging = false;

			audio.PlayOneShot (shoot);

            Vector2 direction = mousePoint - (Vector2)transform.position;
            direction.Normalize();

            float x = mousePoint.x - transform.position.x;
            float y = mousePoint.y - transform.position.y;
            float scale = Mathf.Min(2.5f, Mathf.Sqrt(x * x + y * y)) / 2.5f; // in world units not pixels

            rigid.velocity = direction * scale * 12;

            ShootoutControllerBehaviour.Controller.CreateNewPuck();
        }

        ShootoutControllerBehaviour.Controller.ArrowSprite.SetActive(_dragging);
	}
}
