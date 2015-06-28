using UnityEngine;

public class ShipMovement : MonoBehaviour
{
	public static ShipMovement shipMovement;
	public Transform ship;
	public float speed;
	public int xtilt;
	public int ytilt;
	public int ztilt; 

	private Vector3 direction;

	public float screenWidth;
	public float screenHeight;


	void Start ()
	{	
		shipMovement = this;
		screenHeight = 2*Camera.main.orthographicSize;
		screenWidth = screenHeight*Camera.main.aspect;

	}

	void FixedUpdate ()
	{
		
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		//TODO: movement in z-direction (boost/brake)

		Rigidbody rb = GetComponent<Rigidbody> ();
//		Vector3 centerScreen = (Camera.main.WorldToScreenPoint (transform.position));
		direction = new Vector3 (moveHorizontal, moveVertical, 0.0f);
		rb.velocity = direction * speed;
		rb.rotation = Quaternion.Euler (rb.velocity.y * -ytilt, rb.velocity.x * ztilt, rb.velocity.x * -xtilt);
//		rb.position = new Vector3
//			(
//				Mathf.Clamp (transform.position.x,-.4f*screenWidth , .4f*screenWidth), 
//				Mathf.Clamp (transform.position.y, -.4f*screenHeight, .4f*screenHeight),
//				Mathf.Clamp(transform.position.z, 10, 10)
//			);
	
	}

	public void RotateShip(float speed, Vector3 targetDirection)
	{
		Vector3 newDirection = Vector3.RotateTowards(ship.forward, targetDirection, Time.fixedDeltaTime * speed, 0);
		ship.rotation = Quaternion.LookRotation(newDirection);
	}


}
