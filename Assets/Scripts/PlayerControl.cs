using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public enum Animation_State
{
	Shooting,
	idle,
	reload,
	dead
}
public class PlayerControl : MonoBehaviour {
	public Image CrossHair;
	float lookX;
	float lookY;
	public float sensitivity = 2;
	public GameObject _cubeForOffset;
	public Animator _Animator;
	public float sensitivityTouch;
	/// <summary>
	/// Touch
	/// </summary>
	private int _TouchFingerId;
	public Rect[] _touchRect;
	Vector2 _lastTouch = Vector2.zero;

	/// <summary>
	/// Inverse Kinematics 
	/// </summary>
	public bool ikActive = true;
	float _IkWeight =1.0f;
	public Transform	_lookObj;
	/// <summary>
	/// Enum Declaration for animtion states transition
	/// </summary>
	public static Animation_State _State;

	public static bool _IsDead;
	/// <summary>
	/// Muzzle and Partical Effects
	/// </summary>
	public GameObject muzzleEffect;

	/// <summary>
	/// Health System
	/// </summary>
	public healthSystem Health;
	public float DamageAttack;


	bool startShooting;
	bool shootOnce;
	// Use this for initialization
	void Start () {
		_State = Animation_State.idle;
		_IsDead = false;



		for (int i=0; i<_touchRect.Length; i++) {
			_TouchFingerId = -1;
			_touchRect [i] = new Rect (_touchRect [i].x * Screen.width, _touchRect [i].y * Screen.height, _touchRect [i].width * Screen.width, _touchRect [i].height * Screen.height);
		}
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		MouseControl();
	
		#else

		UpdateTouch ();
		if (Health.health > 0) {
		if(startShooting)
		{
		Fire ();
		if(!shootOnce)
		{
		    Invoke("FireRayCast",0.5f);
		    shootOnce=true;
		}
		}
		else
		{
		   Idle ();
		}
		}
		else
		{
	     	Dead();
		}
		#endif
	}
	void MouseControl()
	{
		lookX = Input.GetAxis ("Mouse X");
		lookY = Input.GetAxis ("Mouse Y");
		
		CrossHair.rectTransform.position += new Vector3 (lookX*sensitivity, lookY*sensitivity, 0);
		CrossHair.rectTransform.position = new Vector3 (Mathf.Clamp (CrossHair.rectTransform.position.x, 0, Screen.width), Mathf.Clamp (CrossHair.rectTransform.position.y,0, Screen.height), 0.0f);
	
		if(((CrossHair.rectTransform.position.x>0)&&(CrossHair.rectTransform.position.x<Screen.width))&&((CrossHair.rectTransform.position.y>0)&&(CrossHair.rectTransform.position.y<Screen.height)))
			_cubeForOffset.transform.position += new Vector3(lookX*sensitivityTouch,lookY*sensitivityTouch,0);


		if (Health.health > 0) {
			if (Input.GetKey (KeyCode.Mouse0)) {
				Fire ();
				if(!shootOnce)
				{
					Invoke("FireRayCast",0.5f);
					shootOnce=true;
				}
			} else if (Input.GetKeyUp (KeyCode.Mouse0)) 
				Idle ();
		} else {
			Dead();
		}

	}
	void OnGUIs()
	{
		if (GUI.Button (new Rect(_touchRect[0].x,_touchRect[0].y,_touchRect[0].width,_touchRect[0].height), "")) {
			
		}
	}

	public void OnFireButtonEnter()
	{
		startShooting = true;

	}
	public void OnFireButtonExit()
	{
		startShooting = false;

	}


	void FireRayCast()
	{
		shootOnce = false;
		RaycastHit _Hit;
		Ray _CrossHairRay = Camera.main.ScreenPointToRay (CrossHair.rectTransform.position);
		if(Physics.Raycast(_CrossHairRay,out _Hit,100.0f))
		{
			
			

			if(_Hit.collider.tag.Contains("Enemy"))
			{
				_Hit.collider.gameObject.GetComponent<healthSystem>().ApplyDamage(DamageAttack,_Hit.point,_Hit.collider.tag);
			}
			else if(_Hit.collider.tag.Contains("Metal"))
			{
				if(_Hit.collider.gameObject.GetComponent<healthSystem>())
				_Hit.collider.gameObject.GetComponent<healthSystem>().ApplyDamage(DamageAttack,_Hit.point,_Hit.collider.tag);
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
	}
	void Dead()
	{
		_State = Animation_State.dead;
		_Animator.SetBool ("Fire",false);
		_Animator.SetBool ("Idle",false);
		_Animator.SetBool ("Reload",false);
		_Animator.SetBool ("Dead",true);

		muzzleEffect.SetActive (false);

		Invoke ("ConfirmedDead", 3.0f);
	}
	void ConfirmedDead()
	{
		
		_IsDead	 = true;
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
		muzzleEffect.SetActive (false);
	}


	

	public void OnAnimatorIK()
	{
		//if the IK is active, set the position and rotation directly to the goal. 
		if(ikActive) {
			
			// Set the look target position, if one has been assigned
			if(_lookObj != null) {
				_Animator.SetLookAtWeight (_IkWeight,1f,1f);
				_Animator.SetLookAtPosition (_lookObj.position);
			}    
			
			
			
		}
		
		//if the IK is not active, set the position and rotation of the hand and head back to the original position
		else {          
			
			_Animator.SetLookAtWeight(0,0,0);
		}
	}

	void UpdateTouch ()
	{
		
		Touch[] touches = Input.touches;
		
		for (int i=0; i<touches.Length; i++) {
			Touch t = touches [i];
			Vector2 vec = t.position;
			vec.y = Screen.height - vec.y;
			
			for (int j=0; j<_touchRect.Length; j++) {
				if (_touchRect [j].Contains (t.position)) {
				
					if (t.phase == TouchPhase.Began) {
						if (_TouchFingerId == -1) {
							_lastTouch = t.position; 
							_TouchFingerId = t.fingerId;
						}
					} else if (_TouchFingerId == t.fingerId && t.phase == TouchPhase.Moved) {
						Vector2 current = t.position;
						Vector2 dir = current - _lastTouch;
						_lastTouch = current;
						
						
						
						lookX = dir.x;
						lookY = dir.y;
						
						
						
						CrossHair.rectTransform.position += new Vector3 (lookX, lookY, 0);

						CrossHair.rectTransform.position = new Vector3 (Mathf.Clamp (CrossHair.rectTransform.position.x, 0, Screen.width), Mathf.Clamp (CrossHair.rectTransform.position.y,0, Screen.height), 0.0f);
						if(((CrossHair.rectTransform.position.x>0)&&(CrossHair.rectTransform.position.x<Screen.width))&&((CrossHair.rectTransform.position.y>0)&&(CrossHair.rectTransform.position.y<Screen.height)))
						_cubeForOffset.transform.position += new Vector3(lookX*(sensitivityTouch/2),lookY*(sensitivityTouch/2),0);
			
					} 
				
				}
				
				if (_TouchFingerId == t.fingerId && (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)) {
					_lastTouch = Vector2.zero;
					_TouchFingerId = -1;
					//          lookX = 0;
					//          lookY = 0;
				}
				
				
				
			}
			
			
		}
		if (Health.health <= 0)
			Dead ();
	}
}
