using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
	public Text AmmoTxt;
	public Text HealthTxt;
	public GameObject IceProjectile;
	public AudioClip clipAmb; 
	public AudioClip clipAmb2; 
	public AudioClip clipAmb3;
	public AudioClip clipAmb4;
	public AudioClip clipShoot;
	
	private AudioSource audioAmb;
	private AudioSource audioAmb2;
	private AudioSource audioShoot;

	public GameObject GameController;

	GameObject GunEnd;

	bool shooting = false;
	int Ammo = 1;
	int health = 10;
	float AmmoRegen = 2f;
	float SavedTime, TouchTime = 0;
	bool Swapped2 = false, Swapped3 = false, Swapped4 = false;
	// Use this for initialization
	void Awake () {
		//AmmoTxt = GameObject.FindGameObjectWithTag ("AmmoTxt");
		GunEnd = GameObject.FindGameObjectWithTag ("GunEnd");
		SavedTime = Time.time;

		audioAmb = AddAudio(clipAmb, true, true, 0.2f);

//		audioAmb = AddAudio(clipAmb, true, false, 0.2f);
		audioAmb2 = AddAudio(clipAmb2, true, true, 0.2f); // always
//		audioAmb3 = AddAudio(clipAmb3, true, false, 0.2f);
//		audioAmb4 = AddAudio(clipAmb4, true, false, 0.2f);

		audioShoot = AddAudio(clipShoot, false, false, 0.2f);
		audioAmb2.Play ();

	}
	public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) { 
		AudioSource newAudio = this.gameObject.AddComponent <AudioSource> ();
		newAudio.clip = clip; 
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol; 
		return newAudio; 
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Enemy")) {
			other.gameObject.GetComponent<EnemyScript>().InvokeRepeating("DealDamage", 1, 1);
		}

	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("Enemy")) {
			other.gameObject.GetComponent<EnemyScript>().CancelInvoke("DealDamage");
		}
	}

	
	// Update is called once per frame
	void Update () {

		if (Time.time - SavedTime >= AmmoRegen && Ammo < 10) {
			SavedTime = Time.time;
			Ammo+=1;
		}
		Controls ();
		UpdateUI ();

		AudioUpdate ();
	}
	void playAudio(){

	}
	void AudioUpdate(){
		if (health >= 7) {
			//Nothing?
		} else if (health < 7 && health >= 4 && Swapped2 == false) {
			//Debug.Log("fuck");
			Swapped2 = true;
			audioAmb2.loop = false;
			audioAmb2.clip = clipAmb3;
			audioAmb2.Play();
			audioAmb2.loop = true;

		}else if (health < 4 && health >= 1 && Swapped3 == false) {
			//Debug.Log("fuck");
			Swapped3 = true;
			audioAmb2.loop = false;
			audioAmb2.clip = clipAmb4;
			audioAmb2.Play();
			audioAmb2.loop = true;
			
		}else if (health < 1 && Swapped4 == false) {
			//Debug.Log("fuck");
			Swapped4 = true;
			audioAmb2.loop = false;
			audioAmb2.clip = clipAmb;
			audioAmb2.Play();
			audioAmb2.loop = true;
			
		}

	}

	void Controls() {
		
		if (Input.GetKeyDown(KeyCode.Mouse0) && shooting == false && Ammo >= 1) {
			audioShoot.Play();
			Shoot ();
			
		}
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			shooting = false;
		}
	}

	void UpdateUI() {
		string ammotemp = "Ammo: ";
		string healthtemp = "";
		for (int i = 1; i <= Ammo; i++) {
			ammotemp = ammotemp + "█";
		}
		for (int i = 1; i <= health; i++) {
			healthtemp = healthtemp + "█";
		}
		AmmoTxt.text= ammotemp;
		HealthTxt.text= healthtemp;

	}
	void Shoot(){

		Ammo -= 1;
		//GameObject iceProj = new GameObject ();
		GameObject iceProj = (GameObject)Instantiate (IceProjectile, GunEnd.gameObject.transform.position, transform.rotation);
		// clone.rigidbody.AddForce(clone.transform.forward * shootForce);

		iceProj.transform.rotation = this.GetComponentInChildren<Transform> ().transform.rotation;
		iceProj.GetComponent<Rigidbody> ().AddForce (iceProj.transform.forward * 500f); //Ice balls are slower
		//iceProj.GetComponent<BulletScript>().type = "Ice";

	}

	public void DoDamage(int dmg) {
		health -= dmg;
		GameScript gs = GameController.gameObject.GetComponent<GameScript> ();
		if(health <= 0 && !gs.IsOver()){
			
			gs.EndGame();
		}
		
	}

}
