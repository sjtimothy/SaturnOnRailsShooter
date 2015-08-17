using UnityEngine;
using System.Collections;



public class DestroyByBoundary : MonoBehaviour
{	
	//protected PlayerHealth playerHealth;

//	void Start ()
//	{
//		GameObject playerHealthObject = GameObject.FindGameObjectWithTag ("Player");
//		if (playerHealthObject != null)
//		{
//			playerHealth = playerHealthObject.GetComponent <PlayerHealth>();
//		}
//	}

	void OnTriggerExit (Collider other) 
	{
			if (other.tag == TagsAndEnums.enemy || other.tag == TagsAndEnums.projectile) {
				Destroy (other.gameObject);
			} 
			
		if (other.tag == TagsAndEnums.player) {
				return;
				//playerHealth.TakeDamage(1000);
			} 

			else{
				//gameObject.SetActive(false);
				return;
			}
	}
}