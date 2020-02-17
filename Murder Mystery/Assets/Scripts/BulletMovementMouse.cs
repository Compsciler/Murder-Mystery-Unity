using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovementMouse : MonoBehaviour {

    // private Vector2 direction = new Vector2(Input.mousePosition.x, Input.mousePosition.y);  // normalized?
    Ray bulletDirection = Camera.main.ScreenPointToRay(Input.mousePosition);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// transform.position = 
	}
}
