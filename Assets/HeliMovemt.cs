using UnityEngine;
using System.Collections;

public class HeliMovemt : MonoBehaviour {
	public GameObject _Player;
	// Use this for initialization
	void Start () {
		_Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Vector3.Distance (transform.position, _Player.transform.position) >= 11)
			transform.Translate (0, 0, -0.3f);
		else {
			HeliAI._HeliStates= Heli_States.Alert;
			HeliAI.startShooting= true;
			gameObject.GetComponent<HeliMovemt>().enabled=false;
		}
	}
}
