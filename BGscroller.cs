using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGscroller : MonoBehaviour {

	public float ScrollSpeed;
	public float TileSizeZ;

	private Vector3 StartPosition;

	void Start () {
		StartPosition = transform.position;
	}
		
	void Update () {
		float newPosition = Mathf.Repeat (Time.time*ScrollSpeed, TileSizeZ);
		transform.position = StartPosition + Vector3.forward * newPosition;
	}
}
