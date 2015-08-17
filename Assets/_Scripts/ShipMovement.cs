using UnityEngine;

public class ShipMovement : MonoBehaviour
{
	public static ShipMovement shipMovement;
	public Transform bankingShip;
	public Rigidbody rb;
	PlayerHealth playerHealth;
	float yScreenEdge;
	float xScreenEdge;
	public float bankSpeed;
	public int xtilt;
	public int ytilt;
	public int ztilt; 

	private Vector3 direction;

	public float screenWidth;
	public float screenHeight;
		
		/// <summary>
		/// Decelerates the ship and raises on y-axis. 
		/// </summary>
	public void Ascend()
	{
		bool pastTopScreenEdge = transform.position.y > yScreenEdge;
		if (!pastTopScreenEdge)
		{
			//transform.Translate(0, .5f, 0);
		bankingShip.localRotation = Quaternion.Slerp(bankingShip.localRotation, Quaternion.Euler(rb.velocity.y * -ytilt, 0, 0), (Time.fixedDeltaTime * bankSpeed));
		}
	}
	
	/// <summary>
	/// Steers ship to the left.
	/// </summary>
	public void BankLeft()
	{
		bool pastLeftScreenEdge = transform.position.x <= -xScreenEdge;
		if (!pastLeftScreenEdge)
		{
			//transform.Translate(-.5f, 0, 0);
		bankingShip.localRotation = Quaternion.Slerp(bankingShip.localRotation, Quaternion.Euler(0, rb.velocity.x * ztilt, rb.velocity.x * -xtilt), (Time.fixedDeltaTime * bankSpeed));
		}
	}
	
	/// <summary>
	/// Steers ship to the right.
	/// </summary>
	public void BankRight()
	{
		bool pastRightScreenEdge = transform.position.x >= xScreenEdge;
		if (!pastRightScreenEdge)
		{
			//transform.Translate(.5f, 0, 0);
		bankingShip.localRotation = Quaternion.Slerp(bankingShip.localRotation, Quaternion.Euler(0, rb.velocity.x * ztilt, rb.velocity.x * -xtilt), (Time.fixedDeltaTime * bankSpeed));
		}
	}
		
		/// <summary>
		/// Accelerates ship and lowers on y-axis.
		/// </summary>
	public void Dive()
	{
		bool pastBottomScreenEdge = transform.position.y <= -yScreenEdge;

		if (!pastBottomScreenEdge)
		{
			//transform.Translate(0, -.5f, 0);
		bankingShip.localRotation = Quaternion.Slerp(bankingShip.localRotation, Quaternion.Euler(rb.velocity.y * -ytilt, 0, 0), (Time.fixedDeltaTime * bankSpeed));
		}
	}
		
	void FixedUpdate()
	{
		if (this.playerHealth.isDead)
		{
			return;
		}
		//Screen Clamp
		
		Vector3 shipPos = new Vector3
			(
				Mathf.Clamp (ShipMovement.shipMovement.transform.localPosition.x, -xScreenEdge, xScreenEdge), 
				Mathf.Clamp (ShipMovement.shipMovement.transform.localPosition.y, -yScreenEdge, yScreenEdge),
				Mathf.Clamp (ShipMovement.shipMovement.transform.localPosition.z, 5f, 15f)
			);
		ShipMovement.shipMovement.transform.localPosition = shipPos;

		// normalize ship rotation
		this.bankingShip.localRotation = Quaternion.Slerp(this.bankingShip.localRotation, Quaternion.Euler(0, 0, 0), (Time.fixedDeltaTime * bankSpeed));
		this.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		direction = new Vector3 (moveHorizontal, moveVertical, 0);
		Vector3 velocity = direction * bankSpeed;
		rb.velocity = velocity;

		if (moveVertical < 0)
		{
			this.Dive();
		}
		else if (moveVertical > 0)
		{
			this.Ascend();
		}
			
		if (moveHorizontal < 0)
		{
			this.BankLeft();
		}
		else if (moveHorizontal > 0)
		{
			this.BankRight();
		}
	}
		
	void RotateShip(float speed, Vector3 targetDirection)
	{
		Vector3 newDirection = Vector3.RotateTowards(bankingShip.forward, targetDirection, Time.fixedDeltaTime * speed, 0);
		bankingShip.rotation = Quaternion.LookRotation(newDirection);
	}
		
	void Start()
	{
		shipMovement = this;
		playerHealth = GetComponentInChildren<PlayerHealth>();

		//screen clamp
		screenHeight = 2*Camera.main.orthographicSize+5f;
		screenWidth = (screenHeight)*Camera.main.aspect;
		yScreenEdge = .4f * screenHeight;
		xScreenEdge = .5f * screenWidth;


	}
}

//	//TODO: movement in z-direction (boost/brake)
//
//
//}
