using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;
	public int burstCount;
	int startWait = 1;

	void Start ()
	{
		StartCoroutine (FireBurst ());

	}



	IEnumerator FireBurst ()
	{
		yield return new WaitForSeconds (startWait);
		Debug.Log ("Start LOOP");
		while (true)
		{
			for (int i = 0; i < burstCount; i++)
			{
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				GetComponent<AudioSource>().Play();
				yield return new WaitForSeconds (fireRate);
				Debug.Log ("Burst Fired");
			}
			yield return new WaitForSeconds (delay);

		
//			if (isDead)
//			{
//				break;
//			}
		}
	}
		

}
