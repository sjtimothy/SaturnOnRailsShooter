using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{

	public static EnemyMovement enemyMovement = null;
	EnemyHealth enemyHealth;
	public int speed;
	public List<Transform> waypoints;
	
	void Start()
	{	
		enemyMovement = this;
		enemyHealth = GetComponentInChildren<EnemyHealth>();
		
		waypoints[0].LookAt(transform.position);
		for(int x = 1; x < waypoints.Count; x++)
		{
			waypoints[x].LookAt(waypoints[x-1].transform.position);
		}
	}

	void FixedUpdate()
	{
		float step = speed * Time.deltaTime;
		Vector3 moveVector = Vector3.Normalize(transform.position - waypoints [0].transform.position);
		transform.position = transform.position - moveVector * step;
	
		
		Quaternion originalRotation = transform.rotation;
		Vector3 targetDirection = waypoints [0].position - transform.position;
		Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * .2f, 0);
		transform.rotation = Quaternion.LookRotation(newDirection);
	}
}
