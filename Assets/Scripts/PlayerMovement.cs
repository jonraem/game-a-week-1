using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed;
	private float maxSpeed = 5f;
	public GameObject sparks;
	private Vector3 input;
	private Vector3 spawn;
	private Vector3 currentPosition;
	public GameManager manager;

	// Use this for initialization
	void Start () {
		spawn = transform.position;
		manager = manager.GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		if (rigidbody.velocity.magnitude < maxSpeed)
		{
			rigidbody.AddForce (input * moveSpeed);
		}
		InvisibleWalls ();
	}

	void InvisibleWalls()
	{
		currentPosition = transform.position;
		if (currentPosition.x < -6.5f)
		{
			transform.position = new Vector3(-6.5f, currentPosition.y, currentPosition.z);
		}
		if (currentPosition.x > 6.5f)
		{
			transform.position = new Vector3(6.5f, currentPosition.y, currentPosition.z);
		}
		
		if (currentPosition.z < -2.5f)
		{
			transform.position = new Vector3 (currentPosition.x, currentPosition.y, -2.5f);
		}
		if (currentPosition.z > 7.0f)
		{
			transform.position = new Vector3 (currentPosition.x, currentPosition.y, 7.0f);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "Enemy")
		{
			Die();
		}
	}

	void Die()
	{
		Instantiate (sparks, transform.position, Quaternion.identity);
		transform.position = spawn;
	}

	void OnTriggerEnter(Collider other) {
		if (other.transform.tag == "Goal") {
			manager.CompleteLevel ();
		}
	}
}
