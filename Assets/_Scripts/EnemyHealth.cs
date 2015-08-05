using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth;
    public int currentHealth;
	  
    CapsuleCollider capsuleCollider;
//  bool isDead;


	public int scoreValue = 0;
	public GameObject dropItem;
	public GameObject enemyExplosion;
	public GameObject hitExplosion;

	Transform enemyTransform;
	Shooting enemyController;
	GameController gameController;
	float dropCalc;


    void Start ()
    {
        //capsuleCollider = GetComponent <CapsuleCollider> ();

		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}

		if (gameObject.tag == "Enemy") {
			GameObject enemy = gameObject;
			if (enemy != null) {
			enemyController = enemy.GetComponent <Shooting> ();
								
			
			}
		}
		enemyTransform = gameObject.GetComponent <Transform> ();
        currentHealth = startingHealth;
		//isDead = false;
    }


    void Update ()
    {
		if (currentHealth <= 0) {
				currentHealth = 0;
				Death ();
		}
	}
	
	
	public void TakeDamage (int amount)
    {

		currentHealth +=- amount;

		//Instantiate(hitExplosion, transform.position, transform.rotation);
			   

        if(currentHealth <= 0)
        {	
//			isDead = true;
            Death ();
        }
    }


    void Death ()
    {

		gameController.AddScore(scoreValue);
	//	Instantiate (enemyExplosion, enemyTransform.position, enemyTransform.rotation);
//		if (gameObject.tag == "Enemy") {
//			dropCalc = Random.value;
//			if (dropCalc <= .3f){
//				Instantiate (dropItem, enemyTransform.position, enemyTransform.rotation);
//		
//			}
//		}
		Destroy (gameObject);
	}
	

}
