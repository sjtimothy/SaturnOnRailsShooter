using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);



    public bool isDead;
    bool damaged;
	GameObject player; 


	public GameObject playerExplosion;
	GameController gameController;
	Transform playerTranform;

//	PlayerController playerController;


    void Start ()
    {	
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
        
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null)
		{
			playerTranform = GetComponent <Transform>();
			Debug.Log ("Player Found");
	
		}

        currentHealth = startingHealth;
		isDead = false;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;
	
        currentHealth = currentHealth - amount;

        healthSlider.value = currentHealth;
		

        if(currentHealth <= 0 && !isDead)
		{	
			isDead = true;
            Death ();
        }
    }


    void Death ()
    {
		if (isDead == true) 
		{

			gameController.GameOver ();

			Instantiate (playerExplosion, playerTransform.position, playerTransform.rotation);
			GameObject.Destroy (player);
		}

		
    }


    public void RestartLevel ()
    {
        Application.LoadLevel (Application.loadedLevel);
    }
}
