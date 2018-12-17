using UnityEngine;
using System.Collections;

public class TyreRoation : MonoBehaviour {
	public GameObject _Player;
	// Use this for initialization
	void Start () {
		_Player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(0,0,-0.5f);
	}	
}
