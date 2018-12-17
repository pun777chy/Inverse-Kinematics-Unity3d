using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {
	public GameObject[] Trucks;
	[SerializeField]
	private GameObject Tuck1,Tuck2;
	[SerializeField]
	private Transform TransformTuck1=null,TransformTuck2=null;
	// Use this for initialization
	void Start () {
		Tuck1 = GameObject.Instantiate(Trucks[0],TransformTuck1.position,TransformTuck1.rotation) as GameObject;
		Tuck2 = GameObject.Instantiate(Trucks[1],TransformTuck2.position,TransformTuck2.rotation) as GameObject;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (!Tuck1) {
			Tuck1 = GameObject.Instantiate (Trucks [0], TransformTuck1.position, TransformTuck1.rotation) as GameObject;

		}
		if (!Tuck2) {
			Tuck2 = GameObject.Instantiate (Trucks [1], TransformTuck2.position, TransformTuck2.rotation) as GameObject;
		}
	}

}
