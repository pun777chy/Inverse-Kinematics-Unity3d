using UnityEngine;
using System.Collections;

public class FanRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(0,10,0);
	}
}
