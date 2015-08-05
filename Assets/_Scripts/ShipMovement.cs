using UnityEngine;

public class ShipMovement : MonoBehaviour
{
	public static ShipMovement shipMovement;
	public Vector3 velocity;
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
		screenHeight = 2*Camera.main.orthographicSize+5f;
		screenWidth = (screenHeight)*Camera.main.aspect;


	}

	void FixedUpdate ()
	{
		Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(0, 0, 0), (Time.fixedDeltaTime ));
		this.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		//TODO: movement in z-direction (boost/brake)

		//Screen Clamp

		Vector3 shipPos = new Vector3
			(
				Mathf.Clamp (ShipMovement.shipMovement.transform.localPosition.x,-.5f*screenWidth , .5f*screenWidth), 
				Mathf.Clamp (ShipMovement.shipMovement.transform.localPosition.y, -.4f*screenHeight, .4f*screenHeight),
				ShipMovement.shipMovement.transform.localPosition.z
				);
		ShipMovement.shipMovement.transform.localPosition = shipPos;

		//Rigidbody stuff
		Rigidbody rb = GetComponent<Rigidbody> ();
		direction = new Vector3 (moveHorizontal, moveVertical, 0);
		velocity = direction * speed;
		rb.velocity = velocity;
		rb.rotation = Quaternion.Euler (rb.velocity.y * -ytilt, rb.velocity.x * ztilt, rb.velocity.x * -xtilt);

	
	}

	//TODO: movement in z-direction (boost/brake)


}
