using UnityEngine;

// This component is created and managed by the SgtTHruster component
[ExecuteInEditMode]
[AddComponentMenu("")]
public class SgtThrusterFlame : MonoBehaviour
{
	public SgtThruster Thruster;
	
	public Vector3 CurrentScale;
	
	[SerializeField]
	private SpriteRenderer spriteRenderer;
	
	private static Material flameMaterial;
	
	// This returns the default shared flame material
	public static Material FlameMaterial
	{
		get
		{
			if (flameMaterial == null)
			{
				flameMaterial = SgtHelper.CreateMaterial(SgtHelper.ShaderNamePrefix + "ThrusterFlame", false);
				flameMaterial.hideFlags = HideFlags.NotEditable | HideFlags.DontSave;
			}
			
			return flameMaterial;
		}
	}
	
	public static SgtThrusterFlame Create(SgtThruster thruster)
	{
		var flame = SgtComponentPool<SgtThrusterFlame>.Pop("Flame", thruster.transform);
		
		flame.Thruster = thruster;
		
		return flame;
	}
	
	public static void Pool(SgtThrusterFlame flame)
	{
		if (flame != null)
		{
			flame.Thruster = null;
			
			SgtComponentPool<SgtThrusterFlame>.Add(flame);
		}
	}
	
	public static void MarkForDestruction(SgtThrusterFlame flame)
	{
		if (flame != null)
		{
			flame.Thruster = null;
			
			flame.gameObject.SetActive(true);
		}
	}
	
	public void SetCurrentObserver(SgtObserver observer)
	{
		if (Thruster != null && observer != null)
		{
			var thrusterTransform = Thruster.transform;
			var pointDir          = thrusterTransform.InverseTransformPoint(observer.transform.position);
			var roll              = Mathf.Atan2(pointDir.y, pointDir.x) * Mathf.Rad2Deg;
			var rotation          = thrusterTransform.rotation * Quaternion.Euler(roll, 270.0f, 0.0f);
			
			// Rotate flame to observer
			SgtHelper.BeginStealthSet(transform);
			{
				transform.rotation = rotation;
			}
			SgtHelper.EndStealthSet();
		}
	}
	
	public void UpdateFlame(Sprite sprite, Vector3 targetScale, float flicker, float dampening)
	{
		// Get or add SpriteRenderer?
		if (spriteRenderer == null)
		{
			spriteRenderer = SgtHelper.GetOrAddComponent<SpriteRenderer>(gameObject);
			
			spriteRenderer.sharedMaterial = FlameMaterial;
		}
		
		// Assign the default material?
		if (spriteRenderer.sharedMaterial == null)
		{
			SgtHelper.BeginStealthSet(spriteRenderer);
			{
				spriteRenderer.sharedMaterial = FlameMaterial;
			}
			SgtHelper.EndStealthSet();
		}
		
		// Assign the current sprite?
		if (spriteRenderer.sprite != sprite)
		{
			spriteRenderer.sprite = sprite;
		}
		
		// Transition to scale?
		if (Application.isPlaying == true)
		{
			CurrentScale = SgtHelper.Dampen3(CurrentScale, targetScale, dampening, Time.deltaTime, 0.1f);
			
			transform.localScale = CurrentScale * Random.Range(1.0f, 1.0f - flicker);
		}
		// Set scale instantly?
		else if (transform.localScale != targetScale)
		{
			CurrentScale = targetScale;
			
			transform.localScale = targetScale;
		}
	}
	
	protected virtual void Update()
	{
		if (Thruster == null)
		{
			Pool(this);
		}
	}
}