using UnityEngine;
using System.Collections;

public class DamageByContact : MonoBehaviour
{

	public int damage;
	public int damagePerShot;

	protected GameController gameController;
	protected PlayerHealth playerHealth;
	protected EnemyHealth enemyHealth;
	

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

		enemyHealth = GetComponent <EnemyHealth>();

		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
		if (playerHealth == null)
		{
			Debug.Log ("Cannot find 'PlayerHealth' script");
		}
		if (enemyHealth == null)
		{
			Debug.Log ("Cannot find 'EnemyHealth' script");
		}
	}

	void OnTriggerEnter (Collider other)
	{


		if (other.tag == "Projectile")
		{	
			Debug.Log ("Hit");
			enemyHealth.TakeDamage(damagePerShot);


		}
		if (other.tag == "Obstacle")
		{	
			Debug.Log ("Smashed");
			enemyHealth.TakeDamage(damage);
			
			
		}


		if (other.tag == "Player") {
			playerHealth.TakeDamage (damage);
		} 


		else {
				return;
		}
		

	}
}