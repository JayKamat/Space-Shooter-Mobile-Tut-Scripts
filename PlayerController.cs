using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private AudioSource sound;

	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	private Quaternion calibrateQuaternion;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		sound = GetComponent<AudioSource> ();

		//CalibrateAccelerometer ();
	}

	void Update()
	{
		if (CrossPlatformInputManager.GetButton("Fire") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			//GameObject shotClone = 
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation); //as GameObject;
			sound.Play();
		}
	}

	void CalibrateAccelerometer () {
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrateQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	Vector3 FixAcceleration (Vector3 acceleration) {
		Vector3 fixedAcceleration = calibrateQuaternion * acceleration;
		return fixedAcceleration;
	}
	
	// FixedUpdate called beore every physics step
	void FixedUpdate () 
	{
		//WASD Controls
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveVertical = Input.GetAxis ("Vertical");

		//Tilt Controls
		//Vector3 Rawacceleration = Input.acceleration;
		//Vector3 acceleration = FixAcceleration (Rawacceleration);

		float moveHorizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		float moveVertical = CrossPlatformInputManager.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement*speed;

		rb.position = new Vector3 
			(
				Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax) ,
				0.0f,
				Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
			);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
