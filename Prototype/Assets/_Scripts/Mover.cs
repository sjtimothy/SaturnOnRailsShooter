using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
	public float xspeed;
	public float yspeed;
	public float zspeed;



	void Start ()
	{

		GetComponent<Rigidbody>().velocity = (transform.right * xspeed + transform.up * yspeed + transform.forward * zspeed);
	}
	
}
	


