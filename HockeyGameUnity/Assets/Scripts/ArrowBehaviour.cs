using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(transform.position.y - mousePoint.y, transform.position.x - mousePoint.x) * Mathf.Rad2Deg + 90);

        float x = mousePoint.x - transform.position.x;
        float y = mousePoint.y - transform.position.y;
        float scale = Mathf.Min(2.5f, Mathf.Sqrt(x*x + y*y)) / 5f; // in world units not pixels
        transform.localScale = new Vector3(scale, scale, 1);


    }
}
