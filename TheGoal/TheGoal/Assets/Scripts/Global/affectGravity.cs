using UnityEngine;
using System.Collections;

public class affectGravity : MonoBehaviour {

	private GameObject target;
	private GameObject affector;
	private bool isAffected;

	public float gravityForce;

	// Use this for initialization
	void Start () {
		isAffected = false;
		target = this.gameObject;
		affector = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (isAffected);
		if (isAffected) {
			affector.GetComponent<playerController>().addGravityForce(target, gravityForce);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "cometGravityTrigger") {
			isAffected = true;
		} 
	}

	void OnTriggerExit2D(Collider2D collider) {
		if(collider.tag == "cometGravityTrigger") {
			isAffected = false;
		} 
	}

}
