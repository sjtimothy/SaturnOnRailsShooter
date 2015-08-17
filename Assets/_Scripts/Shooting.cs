using UnityEngine;
using System.Collections;

[System.Serializable]
public class Shooting : MonoBehaviour
	{	
		//public TagsAndEnums.ProjectileType projectileType;
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
				//PrefabAccessor.GetProjectile(projectileType, transform.root.tag,  shotSpawnL.position);
				//PrefabAccessor.GetProjectile(projectileType, transform.root.tag,  shotSpawnR.position);
				Instantiate(shot, shotSpawnL.position, ship.rotation);
				Instantiate(shot, shotSpawnR.position, ship.rotation);
		
			}
			
			
		}
	}


