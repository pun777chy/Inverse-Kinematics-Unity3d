using UnityEngine;
using System.Collections;
public enum Heli_States{
	AtEase,
	Alert,
	Dead
}
public class HeliAI : MonoBehaviour {
	public GameObject _Player;
	public static Heli_States _HeliStates;
	public static bool startShooting;
	bool IsDead;
	public float AttackDamage;
	public GameObject Explosion;
	public GameObject muzzleEffect;
	public healthSystem _Health;
	// Use this for initialization
	void Start () {
		_HeliStates = Heli_States.AtEase;
		_Player = GameObject.Find("Player");
		transform.LookAt(_Player.transform);

	}
	void LateUpdate()
	{
		if (!IsDead) {
			if (_Health.health > 0) {
				transform.LookAt (_Player.transform.position);
				if (startShooting) {
					Debug.DrawRay (transform.position, transform.forward * 100, Color.red);
					muzzleEffect.SetActive (true);
					Invoke ("Fire", 1);
					startShooting = false;
				}
			} else {
				_HeliStates = Heli_States.Dead;
				IsDead = true;

				Dead ();
			}
		} else {
			transform.Rotate (2.6f, 0.6f, 5.6f);
		}
	}
	void Fire()
	{
		RaycastHit _hit;

		if (Physics.Raycast (transform.position, transform.forward * 100.0f, out _hit)) {
		if(_hit.collider.gameObject==_Player)
			{
				_Player.GetComponent<healthSystem>().ApplyDamage(AttackDamage,_hit.point,_Player.tag);
			}
		}
		startShooting = true;
	}
	void Dead()
	{
		Explosion.SetActive (true);
		gameObject.GetComponent<Rigidbody> ().AddForce (transform.up * 20); 
		gameObject.GetComponent<Rigidbody> ().useGravity = true;
		Destroy (transform.parent.gameObject, 2);
	}

}
