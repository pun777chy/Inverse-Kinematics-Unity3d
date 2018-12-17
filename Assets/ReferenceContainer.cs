using UnityEngine;
using System.Collections;

public class ReferenceContainer : MonoBehaviour {
	[SerializeField]
	private GameObject _BloodEffectRefernce;
	[SerializeField]
	private GameObject _MetalEffectRefernce;

	public static GameObject _BloodEffect=null;
	public static GameObject _MetalEffect=null;
	// Use this for initialization
	void Start () {
		_BloodEffect = _BloodEffectRefernce;
		_MetalEffect = _MetalEffectRefernce;
	}

}
