using UnityEngine;
using System.Collections;


public class PickupByContact : DamageByContact
{		
		public int heal;
		
	
		
		void OnTriggerEnter (Collider other)
		{		
			
			if (other.tag == "Player")
			{
				playerHealth.TakeHeal(heal);
				//item pickup anim
				//Instantiate (itemGrabbed, gameObject.transform.position, gameObject.transform.rotation);
				//audio.Play();
				gameObject.SetActive(false);

				
			//	Debug.Log ("You got an item!");
			}

		}
}


