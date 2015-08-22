using UnityEngine;
using System.Collections;

public class DamageByContact : MonoBehaviour
{

	public int damage;

	protected GameController gameController;
	protected PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	

	void Start ()
	{	

		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}

		GameObject playerHealthObject = GameObject.FindGameObjectWithTag ("Player");
		if (playerHealthObject != null)
		{
			playerHealth = playerHealthObject.GetComponent <PlayerHealth>();
		}



		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
		if (playerHealth == null)
		{
			Debug.Log ("Cannot find 'PlayerHealth' script");
		}

	}

	void OnTriggerExit (Collider other)
	{
		
		if (other.tag == "Enemy")
		{	
			Debug.Log ("hit");
			enemyHealth = other.gameObject.GetComponent <EnemyHealth>();
			if (enemyHealth == null)
			{
				Debug.Log ("Cannot find 'EnemyHealth' script");
			}
			enemyHealth.TakeDamage(damage);
			
			
		}




		else {
				return;
		}
		

	}
}