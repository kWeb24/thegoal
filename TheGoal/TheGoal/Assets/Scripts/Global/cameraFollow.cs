using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	/* TODO
	 * Fix cam shaking drag
	 */

	public float dampTime = 0.15f;
	public Transform target;

	public float cameraZoomDampSpeed = 1.0f;
	public float cameraOriginalSize = 5.0f;
	public float cameraMaxSize = 10.0f;

	private Vector3 velocity = Vector3.zero;
	private bool cameraZoom = false;
	private Camera mainCam;

	private float currentCameraSize;

	Vector3 originalCameraPosition;

	public float force = 0.02f;
	public float quake = 2.0f;
	float shakeAmt = 0;

	void Awake() {
		mainCam = this.gameObject.GetComponent<Camera> ();
		originalCameraPosition = mainCam.transform.position;
	}

	void Start() {
		currentCameraSize = mainCam.orthographicSize;
	}

	// Update is called once per frame
	void Update () 
	{
		if (target)
		{
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}

		if (Input.GetKey (KeyCode.LeftShift)) {
			cameraZoom = true;
		} else {
			cameraZoom = false;
		}

		/* if (Input.GetKey (KeyCode.T)) { Shake(); } */

		zoomCamera ();		
	}

	void zoomCamera() {
		currentCameraSize = mainCam.orthographicSize;

		if (cameraZoom == true) {
			if (currentCameraSize < cameraMaxSize) {
				mainCam.orthographicSize += cameraZoomDampSpeed * Time.deltaTime;
			}
		} else {
			if (mainCam.orthographicSize > cameraOriginalSize) {
				mainCam.orthographicSize -= cameraZoomDampSpeed * Time.deltaTime;
			}
		}
	}

	public void Shake() {
		shakeAmt = force;
		InvokeRepeating("CameraShake", 0, .01f);
		Invoke("StopShaking", 0.3f);
	}

	void CameraShake()
	{
		if(shakeAmt>0) 
		{
			float quakeAmt = Random.value*shakeAmt*quake - shakeAmt;
			Vector3 pp = mainCam.transform.position;
			pp.y+= quakeAmt; // can also add to x and/or z
			pp.x+= quakeAmt;
			mainCam.transform.position = pp;
		}
	}
	
	void StopShaking()
	{
		CancelInvoke("CameraShake");
		mainCam.transform.position = originalCameraPosition;
	}
}
