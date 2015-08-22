using UnityEngine;
using System.Collections;

public class EllipticalPath : MonoBehaviour {
	
	public Vector3 center;
	public float r1;
	public float r2;
	public float radPerSecond;
	public float alpha;
	
	public float angle;
	
	// Use this for initialization
	void Start () {
		//alpha = Mathf.PI * (1f - .25f * (Mathf.Sqrt (2f * Mathf.Pow (r1 / r2 + 1f, 3f))));


	
		
	}
	
	// Update is called once per frame
	void Update () {
		while (angle <= alpha) 
		{
			angle += radPerSecond * Time.deltaTime;
		
			// Calculates position with parametric form, explanation:
			// http://en.wikipedia.org/wiki/Ellipse#Parametric_form_in_canonical_position
			float x = r1 * Mathf.Cos (angle);
			float y = r2 * Mathf.Sin (angle);
		
			transform.position = center + new Vector3 (x, 0, y); //starting point?

			OnDrawGizmosSelected();
		}
	}

	void OnDrawGizmosSelected(){

		Gizmos.DrawLine(Vector3.zero, transform.position);
	}
}