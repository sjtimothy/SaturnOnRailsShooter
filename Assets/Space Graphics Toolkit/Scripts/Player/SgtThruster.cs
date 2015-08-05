using UnityEngine;
using System.Collections.Generic;

// This component allows you to create simple thrusters that can apply forces to Rigidbodies based on their position. You can also use sprites to change the graphics
[ExecuteInEditMode]
[AddComponentMenu(SgtHelper.ComponentMenuPrefix + "Thruster")]
public class SgtThruster : MonoBehaviour
{
	// Static list of all currently enabled thrusters in the scene
	public static List<SgtThruster> AllThrusters = new List<SgtThruster>();
	
	// How active is this thruster? 0 for off, 1 for max power, -1 for max reverse, etc
	public float Throttle;
	
	// How quickly the scale will move to the target value when the throttle value is changed
	public float Dampening = 10.0f;
	
	// This sets how much the flame & flare scale will randomly change
	[SgtRangeAttribute(0.0f, 1.0f)]
	public float Flicker = 0.1f;
	
	// The rigidbody you want to apply the thruster forces to
	public Rigidbody Rigidbody;
	
	// The type of force we want to apply to the Rigidbody
	public SgtForceType ForceType = SgtForceType.AddForceAtPosition;
	
	// The force mode used when ading force to the Rigidbody
	public ForceMode ForceMode = ForceMode.Acceleration;
	
	// The maximum amount of force applied to the rigidbody (when the throttle is -1 or 1)
	public float ForceMagnitude = 1.0f;
	
	// This allows you to set the sprite used by the thruster flame
	public Sprite FlameSprite;
	
	// The scale of the thruster flame when the throttle is at 1
	public Vector2 FlameScale = Vector2.one;
	
	// This allows you to set the sprite used by the thruster flare
	public Sprite FlareSprite;
	
	// The scale of the thruster flame when the throttle is at 1
	public Vector2 FlareScale = Vector2.one;
	
	// This allows you to set which layers the flare will get occluded by
	public LayerMask FlareMask = -5;
	
	[SerializeField]
	private SgtThrusterFlame flame;
	
	[SerializeField]
	private SgtThrusterFlare flare;
	
	// Create a child GameObject with a thruster attached
	public static SgtThruster CreateThruster(Transform parent = null)
	{
		return CreateThruster(parent, Vector3.zero, Quaternion.identity, Vector3.one);
	}
	
	public static SgtThruster CreateThruster(Transform parent, Vector3 localPosition, Quaternion localRotation, Vector3 localScale)
	{
		var gameObject = SgtHelper.CreateGameObject("Thruster", parent, localPosition, localRotation, localScale);
		var thruster   = gameObject.AddComponent<SgtThruster>();
		
		return thruster;
	}
	
	public void SetCurrentObserver(SgtObserver observer)
	{
		if (flame != null)
		{
			flame.SetCurrentObserver(observer);
		}
		
		if (flare != null)
		{
			flare.SetCurrentObserver(observer, FlareMask);
		}
	}
	
	protected virtual void OnEnable()
	{
		AllThrusters.Add(this);
		
		if (flame != null) flame.gameObject.SetActive(true);
		if (flare != null) flare.gameObject.SetActive(true);
	}
	
	protected virtual void OnDisable()
	{
		AllThrusters.Remove(this);
		
		if (flame != null) flame.gameObject.SetActive(false);
		if (flare != null) flare.gameObject.SetActive(false);
	}
	
	protected virtual void OnDestroy()
	{
		SgtThrusterFlame.MarkForDestruction(flame);
		SgtThrusterFlare.MarkForDestruction(flare);
	}
	
	protected virtual void FixedUpdate()
	{
#if UNITY_EDITOR
		if (Application.isPlaying == false)
		{
			return;
		}
#endif
		// Apply thruster force to rigidbody
		if (Rigidbody != null)
		{
			var force = transform.forward * ForceMagnitude * Throttle * Time.fixedDeltaTime;
			
			switch (ForceType)
			{
				case SgtForceType.AddForce: Rigidbody.AddForce(force, ForceMode); break;
				case SgtForceType.AddForceAtPosition: Rigidbody.AddForceAtPosition(force, transform.position, ForceMode); break;
			}
		}
	}
	
	protected virtual void LateUpdate()
	{
		if (flame == null) flame = SgtThrusterFlame.Create(this);
		if (flare == null) flare = SgtThrusterFlare.Create(this);
		
		flame.UpdateFlame(FlameSprite, FlameScale * Throttle, Flicker, Dampening);
		flare.UpdateFlare(FlareSprite, FlareScale * Throttle, Flicker, Dampening);
	}
	
#if UNITY_EDITOR
	protected virtual void OnDrawGizmosSelected()
	{
		var a = transform.position;
		var b = transform.position + transform.forward * ForceMagnitude;
		
		Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
		Gizmos.DrawLine(a, b);
		
		Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		Gizmos.DrawLine(a, a + (b - a) * Throttle);
	}
#endif
	
#if UNITY_EDITOR
	[UnityEditor.MenuItem(SgtHelper.GameObjectMenuPrefix + "Thruster", false, 10)]
	public static void CreateThrusterMenuItem()
	{
		var parent   = UnityEditor.Selection.activeGameObject;
		var thruster = CreateThruster(parent != null ? parent.transform : null);
		
		SgtHelper.SelectAndPing(thruster);
	}
#endif
}