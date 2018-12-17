using UnityEngine;
using System.Collections;

public class MenuUI : MonoBehaviour {
	public GameObject FireButton;

	// Use this for initialization
	void Start () {
		#if UNITY_EDITOR
		FireButton.SetActive(false);
		#else
		FireButton.SetActive(true);
		
		#endif
	}

}
