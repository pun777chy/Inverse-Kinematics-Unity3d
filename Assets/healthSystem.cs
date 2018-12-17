using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class healthSystem : MonoBehaviour {
	public  float health;
	public Slider _HealthSlider;
	// Use this for initialization
	void Start () {
	
	}

	public void ApplyDamage(float damage,Vector3 EffectArea,string tag)
	{
		health -= damage;
		if (health <= 0) {
			health=0;
		}
		_HealthSlider.value = health;
		if (tag.Contains ("Player") | tag.Contains ("Enemy")) {
			ReferenceContainer._BloodEffect.gameObject.SetActive (true);
			ReferenceContainer._BloodEffect.gameObject.transform.position = EffectArea;
			Invoke ("SwitchOffEffect", 0.6f);
		} else 	if (tag.Contains ("Metal")){
			ReferenceContainer._MetalEffect.gameObject.SetActive (true);
			ReferenceContainer._MetalEffect.gameObject.transform.position = EffectArea;
			Invoke ("SwitchOffEffectMetal", 0.6f);
		}
	}
	void SwitchOffEffect()
	{
		ReferenceContainer._BloodEffect.gameObject.SetActive (false);
	}
	void SwitchOffEffectMetal()
	{
		ReferenceContainer._MetalEffect.gameObject.SetActive (false);
	}
}
