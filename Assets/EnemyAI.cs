using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public GameObject _Player;
	public Animator _Animator;
	/// <summary>
	/// Muzzle and Partical Effects
	/// </summary>
	public GameObject muzzleEffect;
	/// <summary>
	/// Enum Declaration for animtion states transition
	/// </summary>
	public Animation_State _State;
	/// <summary>
	/// IsAlert
	/// </summary>
	public bool _IsAlert; 
	public bool _FireOnce; 
	/// <summary>
	/// Health
	/// </summary>
	public healthSystem _Health;

	public GameObject Explosion;
	public float AttackDamage;

	[SerializeField]
	 private float RandomReloadTime;
	// Use this for initialization
	void Start () {
		RandomReloadTime = Random.Range (2.20f,2.30f);
		_Player = GameObject.Find("Player");
		transform.LookAt (_Player.transform.position);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!_IsAlert) {
			transform.LookAt (_Player.transform.position);
		} else {
			if(_Health.health>0){
				if(!_FireOnce)
			    {
			     _FireOnce=true;
			     Fire();
			    }
			}else
			{
				Dead();
			}

		}
	}
	void Fire()
	{
		_State = Animation_State.Shooting;
		_Animator.SetBool ("Fire",true);
		_Animator.SetBool ("Idle",false);
		_Animator.SetBool ("Reload",false);
		_Animator.SetBool ("Dead",false);
		muzzleEffect.SetActive (true);

		RayCastForHealthDamage ();
		Invoke ("Reload", RandomReloadTime);


	}
	void RayCastForHealthDamage()
	{
	
		RaycastHit _hit;

		if (Physics.Raycast (new Vector3(transform.position.x,transform.position.y+1.0f,transform.position.z), transform.forward * 1000.0f, out _hit)) {

			if(_hit.collider.gameObject==_Player)
			{
				_Player.GetComponent<healthSystem>().ApplyDamage(AttackDamage,_hit.point,_Player.tag);
			}
		}
	}
	void Dead()
	{
		_State = Animation_State.dead;
		_Animator.SetBool ("Fire",false);
		_Animator.SetBool ("Idle",false);
		_Animator.SetBool ("Reload",false);
		_Animator.SetBool ("Dead",true);
		muzzleEffect.SetActive (false);

		Invoke ("ExplosionDestroy", 2.0f);
	
		Destroy (transform.root.gameObject, 2.5f);

	}

	void Idle()
	{
		_State = Animation_State.idle;
		_Animator.SetBool ("Fire",false);
		_Animator.SetBool ("Idle",true);
		_Animator.SetBool ("Reload",false);
		_Animator.SetBool ("Dead",false);
		muzzleEffect.SetActive (false);
	}
	void Reload()
	{
		_State = Animation_State.reload;
		_Animator.SetBool ("Fire",false);
		_Animator.SetBool ("Idle",false);
		_Animator.SetBool ("Reload",true);
		_Animator.SetBool ("Dead",false);
	
		Invoke ("Fire", RandomReloadTime);
		muzzleEffect.SetActive (false);
	}
	void ExplosionDestroy()
	{
		transform.root.gameObject.GetComponent<Rigidbody> ().AddForce (transform.root.up * 10);
		Explosion.SetActive (true);
	}
}
