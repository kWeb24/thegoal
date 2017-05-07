using UnityEngine;
using System.Collections;

public class asteroidController : MonoBehaviour {

	private float rotationSpeed;
	private GameObject master;

	private float mass;

	// Use this for initialization
	void Start () {
		rotationSpeed = Random.Range (20, 80);
		master = GameObject.FindGameObjectWithTag ("GameController");
		if (!master) {
			Debug.Log ("asteroidController: GameController object not found");
		}
		mass = Random.Range (100, 1069); /* Szymcio */
	}
	
	// Update is called once per frame
	void Update () {
		float rotation;
		rotation = rotationSpeed * Time.deltaTime;
		transform.Rotate(0,0, rotation);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			//Debug.Log("Destroying: " + this.gameObject);
			master.GetComponent<gameController>().addMass(mass);
			Destroy(this.gameObject);
		}
	}
}
