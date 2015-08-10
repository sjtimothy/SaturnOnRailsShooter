using UnityEngine;

public class ShipMovement : MonoBehaviour
{
	public static ShipMovement shipMovement;
	public Transform bankingShip;
	public Rigidbody rb;
	PlayerHealth playerHealth;
	
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
			bool pastTopScreenEdge = Camera.main.WorldToScreenPoint(transform.position).y > Screen.height;
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
			bool pastLeftScreenEdge = Camera.main.WorldToScreenPoint(transform.position).x <= 0;
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
			bool pastRightScreenEdge = Camera.main.WorldToScreenPoint(transform.position).x >= Screen.width;
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
			bool pastBottomScreenEdge = Camera.main.WorldToScreenPoint(transform.position).y <= 0;
			
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
		}
	}


//	void Start ()
//	{	
//		shipMovement = this;
//		screenHeight = 2*Camera.main.orthographicSize+5f;
//		screenWidth = (screenHeight)*Camera.main.aspect;
//
//
//	}
//
//	void FixedUpdate ()
//	{
//		transform.localRotation = Quaternion.identity;
//		this.GetComponentInChildren<Rigidbody> ().velocity = Vector3.zero;
//		float moveHorizontal = Input.GetAxis ("Horizontal");
//		float moveVertical = Input.GetAxis ("Vertical");
//		//TODO: movement in z-direction (boost/brake)
//
//		//Screen Clamp
//
//		Vector3 shipPos = new Vector3
//			(
//				Mathf.Clamp (ShipMovement.shipMovement.transform.localPosition.x, -.5f * screenWidth, .5f * screenWidth), 
//				Mathf.Clamp (ShipMovement.shipMovement.transform.localPosition.y, -.4f * screenHeight, .4f * screenHeight),
//				Mathf.Clamp (ShipMovement.shipMovement.transform.localPosition.z, 5f, 15f)
//		);
//		ShipMovement.shipMovement.transform.localPosition = shipPos;
//
//		//Rigidbody stuff
//		Rigidbody rb = GetComponent<Rigidbody> ();
//		direction = new Vector3 (moveHorizontal, moveVertical, 0);
//		velocity = direction * speed;
//		rb.velocity = velocity;
//		//bankingShip.rotation = Quaternion.Euler (rb.velocity.y * -ytilt, rb.velocity.x * ztilt, rb.velocity.x * -xtilt);
//
//
//	}
//
//	public void RotateShip(Vector3 targetDirection)
//	{	
//		Debug.Log ("rotatecalled");
//		Rigidbody rb = GetComponent<Rigidbody> ();
//		Vector3 newDirection= targetDirection;
//
//		velocity2 = newDirection * speed;
//		rb.velocity = velocity2;
//	//	Debug.Log ("print velocity");
//	//	bankingShip.rotation = Quaternion.Euler (rb.velocity.y * -ytilt, rb.velocity.x * ztilt, rb.velocity.x * -xtilt);
//	}
//	//TODO: movement in z-direction (boost/brake)
//
//
//}
