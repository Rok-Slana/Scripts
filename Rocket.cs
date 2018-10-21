using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
	[Tooltip("Initial thrust of the rocket")]
	public float thrust;
	[Tooltip("Thust multiplier")]
	public float addMoreThrust;	
	[Tooltip("Time until rocket explodes")]
	public float destroyDelay = 3.0f;
	public float takeOffSoundDelay = 0.5f;
	[Tooltip("Time needed to reignite the rocket after hiting a target(only for positive hits)")]
	public float relaunchTime = 0.5f;	
	[Tooltip("Insert ExplodeCollider object here")]
	//ExplodeCollider is a second collider following the rocket on another layer
	//Intended for manualy exploding the rocket midflight. See ExplodeCollider class
	public ExplodeCollider eCollider; 
	[Tooltip("Insert ParticleSystem explosion here")]
	public GameObject explosionObject;
	[Tooltip("Insert audio for wrong hits")]
	public AudioSource wrongHitAudio;
	[Tooltip("Insert SoundFXManager object here")]
	public SoundFXManager soundManager;
		
	private bool rocketLaunched = false;
	private bool oneHit = false;
	private bool timer = false;
	
	private Rigidbody rb;
	
	private Quaternion myQuaternion;
	
	private Vector3 targetPos = Vector3.zero;
	
	private AudioSource audio;
	
	private GameManager gameManager;

	private Transform eccTransform;
	
	private ExplodeCollider eColliderClone;
	
	private Player player;
	
	void Start(){
		
		myQuaternion = new Quaternion();
		rb = GetComponent<Rigidbody>();
		audio = GetComponent<AudioSource>();
		//soundManager = FindObjectOfType<SoundFXManager>(); <------- ZAKAJ NE DELUJE IN JE POTREBNO JAVNO IZPOSTAVIT VARIABILO??
		gameManager = FindObjectOfType<GameManager>();
		eCollider = FindObjectOfType<ExplodeCollider>();
	}
		
	public void LaunchRocket(Vector3 target){
		rocketLaunched = true;
		eColliderClone = Instantiate(eCollider, this.transform.position, Quaternion.identity);
		eColliderClone.InitializeCollider(destroyDelay);
		targetPos = target;
		player = FindObjectOfType<Player>();
		player.SetRocketActive(true);
		//play explosion sound after destroyDelay time
		soundManager.PlayExplosion(destroyDelay);
		Invoke("PlayTakeOffSound", takeOffSoundDelay);
        //Destroy rocket after destroyDelay time
        GameObject.Destroy(gameObject, destroyDelay);
	}	
		
	void Update () {
		
		//Propell rocket when rocket is launched
		if(rocketLaunched){	
			//Trajectory angle
			myQuaternion.SetFromToRotation(transform.position, targetPos);
			transform.rotation = myQuaternion * transform.rotation;
			//Handle the position of the eCollider
			eColliderClone.transform.position = this.transform.position;
		}
		//If rocket has a positive hit it gets deactivated and activated after relaunchTimer time
		if(timer){
			relaunchTime-=Time.deltaTime;
			if(relaunchTime <= 0.0f){
				timer = false;
				rocketLaunched = true;
			}
		}
	}
	
	//Higher! Faster !!
	void FixedUpdate(){
		if(rocketLaunched){
			rb.AddForce(targetPos * thrust * Time.deltaTime);
			thrust*=addMoreThrust;
		}
	}
	
	void PlayTakeOffSound(){
			audio.Play();	
	}
	
	void OnCollisionEnter(Collision collision){
		
		try{
            /*
            timer = true;
            gameManager.AddLife();
            GameObject explosionObjectClone = Instantiate(explosionObject, this.transform.position, this.transform.rotation);
            ParticleSystem explosionClone = explosionObjectClone.GetComponentInChildren<ParticleSystem>();
            AudioSource explosionSound = explosionObjectClone.GetComponent<AudioSource>();
            explosionClone.Play();
            explosionSound.Play();
            rocketLaunched = false;
            Destroy(explosionObjectClone, 5.0f);
            Destroy(explosionClone, 5.0f);
            */
            if(!oneHit){
			    
                //!!! IMPORTANT : CLASS HAS TO BE CORRECTED DEPENDING NO VESION !!!!
				Target2P target = collision.gameObject.GetComponent<Target2P>();
				
				if(target.IsTarget() && rocketLaunched){
					timer = true;
					gameManager.AddLife();
					GameObject explosionObjectClone = Instantiate(explosionObject,this.transform.position, this.transform.rotation);
					ParticleSystem explosionClone = explosionObjectClone.GetComponentInChildren<ParticleSystem>();
					AudioSource explosionSound = explosionObjectClone.GetComponent<AudioSource>();
					explosionClone.Play();
					explosionSound.Play();
					//rocketLaunched = false;
					Destroy(explosionObjectClone, 5.0f);
					Destroy(explosionClone, 5.0f);
					}
				else{					
					//rocketLaunched = false;
					AudioSource whSound = wrongHitAudio.GetComponent<AudioSource>();
					whSound.Play();
					gameManager.SubtractLife();
					
				}
                //oneHit = true;
                oneHit = false;
            }

        }
        catch (Exception e){
			//Debug.LogException(e,this);
		}
        Debug.Log("??");
        //GameObject.Destroy(gameObject);
    }
}
