using UnityEngine;

// This component is created and managed by the SgtTHruster component
[ExecuteInEditMode]
[AddComponentMenu("")]
public class SgtThrusterFlare : MonoBehaviour
{
	public SgtThruster Thruster;
	
	public Vector3 CurrentScale;
	
	[SerializeField]
	private SpriteRenderer spriteRenderer;
	
	private static Material flareMaterial;
	
	// This returns the default shared flare material
	public static Material FlareMaterial
	{
		get
		{
			if (flareMaterial == null)
			{
				flareMaterial = SgtHelper.CreateMaterial(SgtHelper.ShaderNamePrefix + "ThrusterFlare", false);
				flareMaterial.hideFlags = HideFlags.NotEditable | HideFlags.DontSave;
			}
			
			return flareMaterial;
		}
	}
	
	public static SgtThrusterFlare Create(SgtThruster thruster)
	{
		var flare = SgtComponentPool<SgtThrusterFlare>.Pop("Flare", thruster.transform);
		
		flare.Thruster = thruster;
		
		return flare;
	}
	
	public static void Pool(SgtThrusterFlare flare)
	{
		if (flare != null)
		{
			flare.Thruster = null;
			
			SgtComponentPool<SgtThrusterFlare>.Add(flare);
		}
	}
	
	public static void MarkForDestruction(SgtThrusterFlare flare)
	{
		if (flare != null)
		{
			flare.Thruster = null;
			
			flare.gameObject.SetActive(true);
		}
	}
	
	public void SetCurrentObserver(SgtObserver observer, LayerMask mask)
	{
		if (observer != null)
		{
			var observerTransform = observer.transform;
			var targetRotation    = observerTransform.rotation;// * observer.RollQuataternion;
			var origin            = observerTransform.position;
			var target            = transform.position;
			var direction         = target - origin;
			
			// Point flare at observer
			SgtHelper.BeginStealthSet(transform);
			{
				transform.rotation = targetRotation;
			}
			SgtHelper.EndStealthSet();
			
			// Intersect & resize flare
			SgtHelper.BeginStealthSet(spriteRenderer);
			{
				if (Physics.Raycast(origin, direction.normalized, direction.magnitude, mask) == true)
				{
					spriteRenderer.enabled = false;
				}
				else
				{
					spriteRenderer.enabled = true;
				}
			}
			SgtHelper.EndStealthSet();
		}
	}
	
	public void UpdateFlare(Sprite sprite, Vector3 targetScale, float flicker, float dampening)
	{
		// Get or add SpriteRenderer?
		if (spriteRenderer == null)
		{
			spriteRenderer = SgtHelper.GetOrAddComponent<SpriteRenderer>(gameObject);
			
			spriteRenderer.sharedMaterial = FlareMaterial;
		}
		
		// Assign the default material?
		if (spriteRenderer.sharedMaterial == null)
		{
			SgtHelper.BeginStealthSet(spriteRenderer);
			{
				spriteRenderer.sharedMaterial = FlareMaterial;
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