using UnityEngine;

public class Rocket : MonoBehaviour{

	[Tooltip("Initial thrust of the rocket")]
	public float thrust;

	[Tooltip("Thust multiplier")]
	public float addMoreThrust;	

	[Tooltip("Time until rocket explodes")]
	public float destroyDelay = 3.0f;
   		
	private bool rocketLaunched = false;

    private AudioManager audioManager;
    
	private Rigidbody rb;
	
	private Quaternion myQuaternion;
	
	private Vector3 targetPos = Vector3.zero;
	
	private GameManager gameManager;
	
	private Player player;


    private void Awake(){

        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("rocketTakeoff");
    }

    void Start(){

        player = FindObjectOfType<Player>();
        player.SetRocketActive(true);
        myQuaternion = new Quaternion();
		rb = GetComponent<Rigidbody>();
		gameManager = FindObjectOfType<GameManager>();
        GameObject.Destroy(gameObject, destroyDelay);       
    }

    public void LaunchRocket(Vector3 target){

		rocketLaunched = true;
		targetPos = target;    
    }	
		
	void Update (){	
        
		//Propell rocket when rocket is launched
		if(rocketLaunched){ 
            
			//Trajectory angle
			myQuaternion.SetFromToRotation(transform.position, targetPos);
			transform.rotation = myQuaternion * transform.rotation;
		}
	}
	
	//Higher! Faster !!
	void FixedUpdate(){

        if (rocketLaunched){
            rb.AddForce(targetPos * thrust * Time.deltaTime);
            thrust *= addMoreThrust;
        }
        else{
            return;
        }
	}

	void OnCollisionEnter(Collision collision){

        //!!! IMPORTANT : CLASS HAS TO BE CORRECTED DEPENDING THE VERSION !!!!
		Target2P target = collision.gameObject.GetComponent<Target2P>();
				
		if(target.IsTarget()){
			gameManager.AddLife();
            //audioManager.Play("rocketExplode");
        }else{

            audioManager.Play("wrongHit");
            gameManager.SubtractLife();					
		}
        Destroy(gameObject);
    }
}
