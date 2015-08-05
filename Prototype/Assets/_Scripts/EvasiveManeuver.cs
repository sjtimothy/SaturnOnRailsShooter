using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour
{
	public Boundary boundary;
	public float tilt;
	public float dodge;
	public float smoothing;
	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;

	private float currentSpeed;
	private float targetManeuver;

	Rigidbody rb;

	public float torque;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		currentSpeed = rb.velocity.z;
		StartCoroutine(Evade());

	}
	
	IEnumerator Evade ()
	{
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));
		while (true)
		{
			targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
		}
	}
	
	void FixedUpdate ()
	{
		float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, smoothing * Time.deltaTime);
		rb.velocity = new Vector3 (newManeuver, newManeuver, currentSpeed);
		rb.position = new Vector3
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
			Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax), 
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);
		
		rb.rotation = Quaternion.Euler (0, 0, rb.velocity.x * -tilt);
		if (gameObject.tag == "Scout") {
			//rotate -follow player
		}
	}
}
