using UnityEngine;
using System.Collections;

public class Bank : MonoBehaviour
{

	public float doubleTapDelay = 0.2f;
	public float barrelRollDuration = 1.0f;
	public Transform ship;

	private float time = float.MaxValue;
	public bool buttonDown = false;
	private bool inBarrelRoll = false;
	public int count;

	float bankAxis;
	ShipMovement shipMovement;

	void Start(){
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {
			shipMovement = player.GetComponent <ShipMovement> ();
		}
	}
	// Update is called once per frame
	void Update ()
	{
		if(!inBarrelRoll)
		{
			bankAxis = Input.GetAxis("Bank");
			float moveHorizontal = Input.GetAxis ("Horizontal");
			
			//Single axis method (For windows/keyboard)
			if (bankAxis != 0.0f){
				Vector3 newRotationEuler = ship.transform.rotation.eulerAngles;
				newRotationEuler.z = - 90*bankAxis;
				//lateral speed boost
				newRotationEuler.y = 60*moveHorizontal;
				shipMovement.bankSpeed = 30f;
				Quaternion newQuat = Quaternion.identity;
				newQuat.eulerAngles = newRotationEuler;
				transform.rotation = newQuat;
			}
			//What we want:
			//We need a timer.  If you hit the button (Axis switches from 0 to non-0) the timer starts,
			//it resets when you go from 0 to non-zero again, and does a barrel roll if you do this within the time limit
			
			if(bankAxis == 0.0f)
			{
				buttonDown = false;
				shipMovement.bankSpeed =20f;
			}
			//We are not at 0!
			else if(buttonDown == false)
			{	
				buttonDown = true;
			


				if(time < doubleTapDelay)
				{	
					if(bankAxis < 0.0f)
					{StartCoroutine("BarrelRollLeft");}
					else if(bankAxis > 0.0f)
					{StartCoroutine("BarrelRollRight");}

				}
				time = 0.0f;
			}
			
			time += Time.deltaTime;
		}
	}
	
	IEnumerator BarrelRollLeft()
	{
		inBarrelRoll = true;
		float t = 0.0f;
		
		Vector3 initialRotation = transform.rotation.eulerAngles;
		
		Vector3 goalRotation = initialRotation;
		goalRotation.z += 180.0f;
		
		Vector3 currentRotation = initialRotation;
		
		while(t < barrelRollDuration/2.0f)
		{
			currentRotation.z = Mathf.Lerp(initialRotation.z,goalRotation.z,t/(barrelRollDuration/2.0f));
			transform.rotation = Quaternion.Euler(currentRotation);
			t += Time.deltaTime;
			yield return null;
		}
		
		t = 0;
		
		initialRotation = transform.rotation.eulerAngles;
		goalRotation = initialRotation;
		goalRotation.z += 180.0f;
		
		while(t < barrelRollDuration/2.0f)
		{
			currentRotation.z = Mathf.Lerp(initialRotation.z,goalRotation.z,t/(barrelRollDuration/2.0f));
			transform.rotation = Quaternion.Euler(currentRotation);
			t += Time.deltaTime;
			yield return null;
		}
		
		inBarrelRoll = false;
		bankAxis = 0.0f;
	}

	IEnumerator BarrelRollRight()
	{
		inBarrelRoll = true;
		float t = 0.0f;
		
		Vector3 initialRotation = transform.rotation.eulerAngles;
		
		Vector3 goalRotation = initialRotation;
		goalRotation.z -= 180.0f;
		
		Vector3 currentRotation = initialRotation;
		
		while(t < barrelRollDuration/2.0f)
		{
			currentRotation.z = Mathf.Lerp(initialRotation.z,goalRotation.z,t/(barrelRollDuration/2.0f));
			transform.rotation = Quaternion.Euler(currentRotation);
			t += Time.deltaTime;
			yield return null;
		}
		
		t = 0;
		
		initialRotation = transform.rotation.eulerAngles;
		goalRotation = initialRotation;
		goalRotation.z -= 180.0f;
		
		while(t < barrelRollDuration/2.0f)
		{
			currentRotation.z = Mathf.Lerp(initialRotation.z,goalRotation.z,t/(barrelRollDuration/2.0f));
			transform.rotation = Quaternion.Euler(currentRotation);
			t += Time.deltaTime;
			yield return null;
		}
		
		inBarrelRoll = false;
		bankAxis = 0.0f;
	}
}
