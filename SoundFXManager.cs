using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour {

	private AudioSource audio;	
	
	void Start(){
		audio = GetComponent<AudioSource>();
	}
	
	public void PlayExplosion(float delay){
		audio.PlayDelayed(delay);	
	}
	
}
