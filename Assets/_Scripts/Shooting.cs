using UnityEngine;
using System.Collections;

[System.Serializable]
public class Shooting : MonoBehaviour
	{	
		public GameObject shot;
		public Transform ship;
		public Transform shotSpawnL, shotSpawnR;
		public float fireRate;

		private float time = 0;
		private int incrementTime = 1;
		private float nextFire;
		void Update ()
		{
			Shoot ();
			time+=Time.deltaTime; while (time>incrementTime) 
			{
				time-=incrementTime; 
			}
		}

	
		
		void Shoot ()
		{	
			
			if (Input.GetButton("Fire1") && Time.time > nextFire) 
			{	
				nextFire = Time.time + fireRate;
				Instantiate(shot, shotSpawnL.position, ship.rotation);
				Instantiate(shot, shotSpawnR.position, ship.rotation);
		
			}
			
			
		}
	}


