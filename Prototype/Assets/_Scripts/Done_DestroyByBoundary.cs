using UnityEngine;
using System.Collections;



public class Done_DestroyByBoundary : MonoBehaviour
{	
	protected PlayerHealth playerHealth;

	void Start ()
	{
		GameObject playerHealthObject = GameObject.FindGameObjectWithTag ("Player");
		if (playerHealthObject != null)
		{
			playerHealth = playerHealthObject.GetComponent <PlayerHealth>();
		}
	}

	void OnTriggerExit (Collider other) 
	{
			if (other.tag == "Enemy" || other.tag == "Shootable") {
				Destroy (other.gameObject);
			} 
			
			if (other.tag == "Player") {
				playerHealth.TakeDamage(1000);
			} 

			else{
				//gameObject.SetActive(false);
				return;
			}
	}
}