using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

	public GameObject Shot;
	public Transform shotSpawn;
	public float FireRate;
	public float delay;

	private AudioSource sound;

	void Start () {
		sound = GetComponent<AudioSource> ();
		InvokeRepeating ("Fire", delay, FireRate);
	}

	void Fire ()
	{
		Instantiate (Shot, shotSpawn.position, shotSpawn.rotation);
		sound.Play ();
	}
}
