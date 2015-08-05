using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth;
    public int currentHealth;


	public GameObject healthBar;
    public Slider healthSlider;
	public GameObject shieldBar;
	public Slider shieldSlider;
    public Image damageImage;  
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);



    public bool isDead;
    bool damaged;
	GameObject player; 

	public GameObject hitExplosion;
	public GameObject playerExplosion;
	GameController gameController;
	Transform playerTranform;

	ShipMovement shipMovement;
	Transform playerTransform;

    void Start ()
    {	
		isDead = false;
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
        
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null)
		{
			shipMovement = player.GetComponent <ShipMovement>();
			playerTransform = player.GetComponent <Transform>();
	
		}

		shieldSlider.value = startingHealth;
		currentHealth = startingHealth;
		healthSlider.value = currentHealth;	
		isDead = false;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
			Instantiate(hitExplosion, transform.position, transform.rotation);
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

		if(currentHealth > startingHealth) {
			currentHealth = startingHealth;
			healthSlider.value = currentHealth;	
		}
		if(currentHealth <= 0) {
			currentHealth = 0;
			Death();
		}
    }


    public void TakeDamage (int amount)
    {
        damaged = true;
		if (shieldSlider.value >= 0) 
		{
			shieldSlider.value -= amount;	
			//reset shield recharge delay count
		}

		if (shieldSlider.value <= 0) 
		{
			currentHealth -= amount;
			healthSlider.value = currentHealth;			
		}

		if(currentHealth <= 0 && !isDead)
		{	
			isDead = true;
			healthBar.SetActive(false);
            Death ();
        }
    }

	public void TakeHeal (int amount)
	{
			currentHealth += amount;
			healthSlider.value = currentHealth;			
			Debug.Log ("Healz");
	}

	/*void ShieldRecharge ()
	 {
		if (time > recharge delay && shieldSlider.value <= startingHealth)
		{
			start incrementing shield hp 
		}
	 }*/

    void Death ()
    {
		if (isDead == true) 
		{	
			shipMovement.enabled = false;
			gameController.GameOver ();

			Instantiate (playerExplosion, playerTransform.position, playerTransform.rotation);
			gameObject.SetActive(false);
			Debug.Log ("Player Dead");
		}

		
    }


	void OnTriggerExit (Collider other) 
	{
		if (other.tag == "Finish Line" ) {		
			gameController.Finish();
			Debug.Log ("FinishLne called");
		}
	}
}
