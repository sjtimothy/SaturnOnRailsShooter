public class EnemyMovement : CameraMovement
{

	public static EnemyMovement enemyMovement = null;
	EnemyHealth enemyHealth;

	
	
	void Start()
	{	
		enemyMovement = this;
		enemyHealth = GetComponentInChildren<EnemyHealth>();
		
		waypoints[0].LookAt(transform.position);
		for(int x = 1; x < waypoints.Count; x++)
		{
			waypoints[x].LookAt(waypoints[x-1].transform.position);
		}
	}

	void FixedUpdate()
	{
		// make the camera move
		Move ();
	}
}
