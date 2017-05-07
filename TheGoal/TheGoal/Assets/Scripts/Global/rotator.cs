using UnityEngine;
using System.Collections;

public class rotator : MonoBehaviour {

	public float speed = 1;
	public bool randomizeStartRotation = false;
	public bool directionClockwise = true;
	private float rotation;

	// Use this for initialization
	void Start () {

		/* Randomize rotation */
		if (randomizeStartRotation) {
			Vector3 rndRotation = transform.eulerAngles;
			rndRotation.z = Random.Range (0f, 360f);
			transform.eulerAngles = rndRotation;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		/* Rotate object */
		rotation = speed * Time.deltaTime;

		if (directionClockwise) {
			rotation = rotation * -1;
		}

		transform.Rotate(0,0, rotation);

	}
}
