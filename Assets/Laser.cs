using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Laser : Projectile {
	
	public override void Intercept(Vector3 moveVector, float relativeSpeed)
	{
		StartCoroutine(InterceptCoroutine(moveVector, relativeSpeed));
	}
	
	IEnumerator InterceptCoroutine(Vector3 moveVector, float relativeSpeed)
	{
		float lastRun = Time.time;
		Vector3 origin = transform.position;
		while (TagsAndEnums.GetSqrDistance(origin, transform.position) < selfDestructRange*selfDestructRange && !hitObject)
		{	
			float step = (speed+relativeSpeed) * (Time.time - lastRun);
			// update the position
			transform.position = new Vector3(transform.position.x - moveVector.x * step,
			                                 transform.position.y - moveVector.y * step,
			                                 transform.position.z - moveVector.z * step);
			
			lastRun = Time.time;
			yield return new WaitForEndOfFrame();
		}
		armed = false;
		hitObject = false;
		
		BackInThePool();
	}
}