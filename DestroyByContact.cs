using System.Collections;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject Explosion;
	public GameObject PlayerExplosion;

	public int scoreValue;
	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		} else if (gameControllerObject == null) {
			Debug.Log("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundary" || other.tag =="Enemy") 
		{
			return;
		}

		if (Explosion != null) {
			Instantiate (Explosion, transform.position, transform.rotation);
		}

		if (other.tag == "Player") {
			Instantiate (PlayerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}

		gameController.AddScore (scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
