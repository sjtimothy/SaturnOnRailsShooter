using UnityEngine;

// This component allows you to snap a GameObject to the surface of an SgtTerrain
[ExecuteInEditMode]
[AddComponentMenu(SgtHelper.ComponentMenuPrefix + "Snap To Terrain")]
public class SgtSnapToTerrain : MonoBehaviour
{
	// This allows you to set which terrain this GameObject will be snapped to
	public SgtTerrain Terrain;
	
	// Enable this if you want the position to be snapped
	public bool SnapPosition = true;
	
	// This allows you to set how far from the surface this GameObject will be snapped
	public float SnapOffset;
	
	// Enable this if you want the rotation to be snapped
	public bool SnapRotation = true;
	
	// This allows you to set how far apart the right/left height samples will be. Increasing this can make the rotations smoother
	public float SnapRightDistance = 0.1f;
	
	// This allows you to set how far apart the forward/back height samples will be. Increasing this can make the rotations smoother
	public float SnapForwardDistance = 0.1f;
	
	// This static method will move the transform down to the surface of the terrain
	public static void SnapTransformPosition(SgtTerrain terrain, Transform transform, float offset = 0.0f)
	{
		if (terrain != null && transform != null)
		{
			var oldPosition = transform.position;
			var newPosition = terrain.GetSurfacePositionWorld(oldPosition, offset);
			
			if (oldPosition != newPosition)
			{
				transform.position = newPosition;
			}
		}
	}
	
	// This static method will rotate the transform to the surface of the terrain below
	public static void SnapTransformRotation(SgtTerrain terrain, Transform transform, float rightDistance = 1.0f, float forwardDistance = 1.0f)
	{
		if (terrain != null && transform != null)
		{
			var newNormal = default(Vector3);
			
			// Rotate to surface normal?
			if (rightDistance != 0.0f && forwardDistance != 0.0f)
			{
				var worldRight   = transform.right   * rightDistance;
				var worldForward = transform.forward * forwardDistance;
				
				newNormal = terrain.GetSurfaceNormalWorld(transform.position, worldRight, worldForward);
			}
			// Rotate to planet center?
			else
			{
				newNormal = terrain.GetSurfaceNormalWorld(transform.position);
			}
			
			var oldRotation = transform.rotation;
			var newRotation = Quaternion.FromToRotation(transform.up, newNormal) * oldRotation;
			
			if (oldRotation != newRotation)
			{
				transform.rotation = newRotation;
			}
		}
	}
	
	public void UpdateSnap()
	{
		// Snap the position?
		if (SnapPosition == true)
		{
			SnapTransformPosition(Terrain, transform, SnapOffset);
		}
		
		// Snap the rotation?
		if (SnapRotation == true)
		{
			SnapTransformRotation(Terrain, transform, SnapRightDistance, SnapForwardDistance);
		}
	}
	
	protected virtual void Update()
	{
		UpdateSnap();
	}
}