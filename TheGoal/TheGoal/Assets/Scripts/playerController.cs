using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public float speed = 1f;

	public GameObject theSun;
	public float sunGravityForce;
	
	public GameObject particleSystemObject;

	private Vector3 sunPosition;
	private Rigidbody2D myRigid;

	private LineRenderer lineRenderer;

	private Vector3 throw_InitialPos;
	private Vector3 throw_EndPos;
	private bool isThrowing;
	private float throw_Power;

	public float throw_PowerMultipler;

	private bool isOrbiting;
	private GameObject orbitBody;
	private float orbitDistance;

	public GameObject Pluto;

	private GameObject master;

	void Awake() {

	}

	// Use this for initialization
	void Start () {
		sunPosition = new Vector3 (0,0,theSun.transform.position.z);
		myRigid = this.gameObject.GetComponent<Rigidbody2D> ();

		if (!myRigid) {
			Debug.Log ("PlayerController: Rigidbody2D not found");
		}

		master = GameObject.FindGameObjectWithTag ("GameController");
		if (!master) {
			Debug.Log ("asteroidController: GameController object not found");
		}

		isThrowing = false;

		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetWidth(0.02f, 0.02f); //thickness of line
		lineRenderer.SetVertexCount(2);

		setOrbit (true, Pluto, 0.3f);
	}
	
	// Update is called once per frame
	void Update () {

		/* Rotate particles out of sun */
		particleSystemObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, sunPosition - transform.position);
		particleSystemObject.transform.Rotate (Vector3.forward * 180);


		//debuger ();

		if (isOrbiting) {
			throwController ();
			//transform.RotateAround(orbitBody.transform.position, Vector3.right, 20 * Time.deltaTime);
		}

		float distanceToSun = (transform.position - theSun.transform.position).magnitude;
		master.GetComponent<gameController> ().updateDistanceToSun (distanceToSun);

		affectGravity (theSun, sunGravityForce);
	}

	void throwController() {

		if (Input.GetMouseButtonDown(0)) {
			isThrowing = true;
			lineRenderer.enabled = true;
		}

		if (Input.GetMouseButtonUp(0)) {
			isThrowing = false;
			lineRenderer.enabled = false;
			throwComet();
		}

		if (isThrowing) {
			throw_InitialPos = transform.position;
			throw_EndPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			//Vector3 negativeX = Vector3.Reflect((throw_EndPos - throw_InitialPos).normalized, Vector3.up);

			//lineRenderer.SetPosition (0, new Vector3 (negativeX.x, negativeX.y, 0));
			lineRenderer.SetPosition (0, new Vector3 (throw_InitialPos.x, throw_InitialPos.y, 0));
			lineRenderer.SetPosition (1, new Vector3 (throw_EndPos.x, throw_EndPos.y, 0));

			transform.rotation = Quaternion.LookRotation(Vector3.forward, throw_EndPos - transform.position);
			transform.Rotate (Vector3.forward * 270.0f);
		}
	}

	void throwComet() {
		throw_Power = (throw_EndPos - throw_InitialPos).magnitude;
		throw_Power = ((throw_Power - 10.0f) * 100) / 4;
		throw_Power *= throw_PowerMultipler;

		if (throw_Power > 6) { 
			throw_Power = 6.0f;
		}

		isOrbiting = false;
		transform.parent = null;
		myRigid.AddForce (transform.right * throw_Power, ForceMode2D.Impulse);

		//Debug.Log ("Throw power: " + throw_Power);
	}

	void affectGravity(GameObject target, float gravityForce) {
		Vector3 targetGravity3D = target.transform.position - transform.position;
		Vector2 targetGravityPoint = new Vector2 (targetGravity3D.x, targetGravity3D.y);
		targetGravityPoint = targetGravityPoint.normalized * gravityForce;

		if (!isOrbiting) {
			myRigid.AddForce (targetGravityPoint, ForceMode2D.Force);
		}
	}

	public void addGravityForce(GameObject target, float gravityForce) {
		affectGravity (target, gravityForce);
		//Debug.Log ("Gravity from: " + target + " : " + target.transform.parent.gameObject + " Scale: " + gravityForce);
	}

	public void setOrbit(bool orb, GameObject celestial, float distance) {
		isOrbiting = orb;
		orbitBody = celestial;
		orbitDistance = distance;

		myRigid.velocity = new Vector2 (0.0f, 0.0f);
		myRigid.angularVelocity = 0.0f;

		transform.parent = orbitBody.transform;
		transform.position = new Vector3(orbitBody.transform.position.x + orbitDistance, orbitBody.transform.position.y);
		//Debug.Log ("Orbiting: true");
	}

	void debuger() {
		/* Debug purposes */
		if (Input.GetKey (KeyCode.Y)) {
			myRigid.AddForce(new Vector2(0.2f,0f), ForceMode2D.Impulse);
		}

		if (Input.GetKey (KeyCode.U)) {
			myRigid.AddForce(new Vector2(0f,0.2f), ForceMode2D.Impulse);
		}

		/* Moving WASD Temporary */
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.position += move * speed * Time.deltaTime;
	}
}
