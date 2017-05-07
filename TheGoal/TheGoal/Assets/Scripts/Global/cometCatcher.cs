using UnityEngine;
using System.Collections;

public class cometCatcher : MonoBehaviour {

	private GameObject player;
	public float orbitDistance;
	public float orbitingSpeed;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		if (!player) {
			Debug.Log ("cometCatcher: Can't find player Game Object");
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,0, orbitingSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "Player") {
			player.GetComponent<playerController>().setOrbit(true, this.gameObject, orbitDistance);
			Debug.Log(this.gameObject);
		} 
	}
	
	void OnTriggerExit2D(Collider2D collider) {
		if(collider.tag == "Player") {
			//player.GetComponent<playerController>().setOrbit(false, this.gameObject);
			//Debug.Log(this.gameObject);
		} 
	}
}
