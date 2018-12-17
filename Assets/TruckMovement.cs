using UnityEngine;
using System.Collections;

public class TruckMovement : MonoBehaviour {
	public GameObject _Player;
	// Use this for initialization
	void Start () {
		_Player = GameObject.Find("Player");

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Vector3.Distance (transform.position, _Player.transform.position) >= 5)
			transform.Translate (0.1f, 0, 0);
		else {
			transform.GetChild(0).gameObject.GetComponent<EnemyAI>()._IsAlert= true;
			gameObject.GetComponent<TruckMovement> ().enabled = false;
		
		}
	}
}
