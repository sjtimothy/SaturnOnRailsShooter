using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
	public static CameraMovement cameraMovement = null;
	
	public List<Transform> waypoints;

	public float speed;
	public bool fightingBoss;
	public float screenWidth;
	public float screenHeight;

	PlayerHealth playerHealth;

	
	public Vector3 GetMovementVector()
	{
		Vector3 moveVector = Vector3.zero;
		if (waypoints.Count != 0)
			moveVector = Vector3.Normalize(transform.position - waypoints [0].transform.position);
		return moveVector;

	}
	
	public void NextWaypoint(bool loops)
	{	Debug.Log ("Waypoint reached");
		if(loops)
		{
			waypoints.Add(waypoints[0]);
		}
		waypoints.RemoveAt(0);
		Debug.Log ("To Next Waypoint ");


	}
	
	void Start()
	{	
		cameraMovement = this;
		screenHeight = 2*Camera.main.orthographicSize;
		screenWidth = screenHeight*Camera.main.aspect;
		playerHealth = GetComponentInChildren<PlayerHealth>();
		
		waypoints[0].LookAt(transform.position);
		for(int x = 1; x < waypoints.Count; x++)
		{
			waypoints[x].LookAt(waypoints[x-1].transform.position);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		// make the camera move
		if (waypoints.Count != 0 && !playerHealth.isDead)
		{
			if(!fightingBoss)
			{
				float step = speed * Time.deltaTime;
				Vector3 moveVector = Vector3.Normalize(transform.position - waypoints [0].transform.position);
				transform.position = transform.position - moveVector * step;


			}
			
			Quaternion originalRotation = transform.rotation;
			Vector3 targetDirection = waypoints [0].position - transform.position;
			Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * .01f, 0);
			transform.rotation = Quaternion.LookRotation(newDirection);
			ShipMovement.shipMovement.RotateShip(speed, newDirection);


			//Lock 	ship in camera frame
			Vector3 shipPos = new Vector3
				(
					Mathf.Clamp (ShipMovement.shipMovement.transform.localPosition.x,-.4f*screenWidth , .4f*screenWidth), 
					Mathf.Clamp (ShipMovement.shipMovement.transform.localPosition.y, -.4f*screenHeight, .4f*screenHeight),
					Mathf.Clamp(ShipMovement.shipMovement.transform.localPosition.z, 10, 10)
				);
			ShipMovement.shipMovement.transform.localPosition = shipPos;
		}		
	}
}
