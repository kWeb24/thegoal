using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

	private Object[] asteroids;

	public float belt1_min_x = 8.35f;
	public float belt1_max_x = 10.10f;

	public float belt2_min_x = 28.0f;
	public float belt2_max_x = 35.0f;

	public int belt1_count = 50;
	public int belt2_count = 100;

	public float rotationSpeed = 20;

	private bool isGenerated;

	/* Texts */
	public Text TXT_mass;
	public Text TXT_status;
	public Text TXT_stability;
	/* Points */
	private float mass;
	private float sunDistance;
	public float massLooseRate;
	private float nextMassLoose;

	// Use this for initialization
	void Start () {

		mass = 10000.0f;
		isGenerated = false;

		asteroids = new Object[10];
		for (int i = 0; i <= 9; i++) {
			asteroids[i] = Resources.Load("AsteroidPrefabs/asteroid" + (i+1).ToString());
		}

		/* 1st belt */
		for (int i = 0; i <= belt1_count; i++) {
			generateSeed();

			float range_x = Random.Range(belt1_min_x, belt1_max_x);
			int asteroid = Random.Range(0,9);

			randomRotation();
			Vector3 position = new Vector3(range_x, 0, 0);
			GameObject instance = Instantiate (asteroids[asteroid], position, transform.rotation) as GameObject;
			instance.transform.parent = gameObject.transform;
		}

		/* 2nd belt */
		for (int i = 0; i <= belt2_count; i++) {
			generateSeed();
			
			float range_x = Random.Range(belt2_min_x, belt2_max_x);
			int asteroid = Random.Range(0,9);

			/* Rotate spawner */
			randomRotation();

			Vector3 position = new Vector3(range_x, 0, 0);
			GameObject instance = Instantiate (asteroids[asteroid], position, transform.rotation) as GameObject;
			instance.transform.parent = gameObject.transform;
		}

		isGenerated = true;

	}
	
	// Update is called once per frame
	void Update () {
	
		if (isGenerated) {
			/* Rotate object */
			float rotation;
			rotation = rotationSpeed * Time.deltaTime;
			transform.Rotate(0,0, rotation);
		}

		removeMass ();
		updateUI ();
		//Debug.Log (mass);
		//Debug.Log (sunDistance);

		if (mass > 40000) {
			Application.LoadLevel (3);
		}

		if (mass <= 0) {
			Application.LoadLevel (4);
		}
	}

	void randomRotation() {
		Vector3 rndRotation = transform.eulerAngles;
		rndRotation.z = Random.Range (0f, 360f);
		transform.eulerAngles = rndRotation;
	}

	void generateSeed() {
		//Random.seed = (int)System.DateTime.Now.Ticks;

		//Debug.Log ((int)System.DateTime.Now.Ticks);
		//Debug.Log ((int)System.DateTime.Now.Ticks);
	}

	void removeMass() {
		if(Time.time > nextMassLoose) {
			nextMassLoose = Time.time + massLooseRate;
			mass -= ((50 - sunDistance) * 0.1f);
		}
	}

	void updateUI() {
		TXT_mass.text = mass.ToString ("F2") + " teratons";

		if (mass <= 0) {
			TXT_status.text = "You've become a cloud of lost hope";
			TXT_stability.text = "";
		} else if (mass <= 5000) {
			TXT_status.text = "You're only miserable asteroid";
			TXT_stability.text = "";
		} else if (mass <= 15000) {
			TXT_status.text = "You're a comet! Really nice one!";
			TXT_stability.text = "";
		} else if ((mass <= 25000) && (mass < 35000)) {
			TXT_status.text = "Good! You're an dwarf planet. Just like Pluto!"; 
			TXT_stability.text = "";
		} else if (mass >= 35000) {
			TXT_status.text = "Great! You become a planet. You change the solar system and reached your new year goal!";
			TXT_stability.text = "Now estabilish your mass. Reach 40k terratons!";
		}
	}

	public void addMass(float amn) {
		mass += amn;
	}

	public void updateDistanceToSun(float amn) {
		sunDistance = amn;
		//Debug.Log ("Topnienie: " + ((50 - amn) * 2));
	}



}
