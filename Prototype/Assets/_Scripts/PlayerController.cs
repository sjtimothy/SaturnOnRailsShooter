using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, yMin, yMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour
{

	public float speed;
	public float xtilt;
	public float ytilt;
	public float ztilt;
	public Boundary boundary;
	public float envSpeed;

	public GameObject shot;
	public Transform shotSpawnL, shotSpawnR;
	public float fireRate;
	 
	private float nextFire;

	Vector3 movement;
	float time = 0;
	int incrementTime = 1;

	void Update ()
	{
		Shoot ();
		time+=Time.deltaTime; while (time>incrementTime) 
		{
			time-=incrementTime; 
			ShiftBoundary (envSpeed);
		}
	}

	void Shoot ()
	{	
		
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{	
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawnL.position, shotSpawnL.rotation);
			Instantiate(shot, shotSpawnR.position, shotSpawnR.rotation);
			GetComponent<AudioSource>().Play ();
		}

		
	}
	void FixedUpdate ()
	{	

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		//no movement in z-direction (add boost/brake capability?)


		movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			Mathf.Clamp (GetComponent<Rigidbody>().position.y, boundary.yMin, boundary.yMax) 
			
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (GetComponent<Rigidbody>().velocity.y * -ytilt, GetComponent<Rigidbody>().velocity.x * ztilt, GetComponent<Rigidbody>().velocity.x * -xtilt);

	}

	void ShiftBoundary (float speed)
	{
		boundary.xMin += speed;
		boundary.xMax += speed;
	}

}
