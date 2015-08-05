using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
	public static CameraMovement cameraMovement = null;
	
	public List<Transform> waypoints;

	public float speed;
	public bool fightingBoss;
	PlayerHealth playerHealth;

	
	public Vector3 GetMovementVector()
	{
		Vector3 moveVector = Vector3.zero;
		if (waypoints.Count != 0)
			moveVector = Vector3.Normalize(transform.position - waypoints [0].transform.position);
		return moveVector;

	}
	
	public void NextWaypoint(bool loops)
	{
		if(loops)
		{
			waypoints.Add(waypoints[0]);
		}
		waypoints.RemoveAt(0);

	}
	
	// Use this for initialization
	void Start()
	{
		cameraMovement = this;
		playerHealth = GetComponentInChildren<PlayerHealth>();
		
//		waypoints[0].LookAt(transform.position);
//		for(int x = 1; x < waypoints.Count; x++)
//		{
//			waypoints[x].LookAt(waypoints[x-1].transform.position);
//		}
	}
	
	// Update is called once per frame
	void Update()
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
			Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * .2f, 0);
			transform.rotation = Quaternion.LookRotation(newDirection);

		}
		
		
		// Camera follow distance
		Vector3 shipPos = ShipMovement.shipMovement.transform.localPosition;
		shipPos.z = 35;
		shipPos.y = -.9f;
		ShipMovement.shipMovement.transform.localPosition = shipPos;
	}
}