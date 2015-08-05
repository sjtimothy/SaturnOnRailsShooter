using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
[AddComponentMenu(SgtHelper.ComponentMenuPrefix + "Skysphere")]
public class SgtSkysphere : MonoBehaviour
{
	public static List<SgtSkysphere> AllSkyspheres = new List<SgtSkysphere>();
	
	public List<Mesh> Meshes = new List<Mesh>();
	
	public Color Color = Color.white;
	
	public float Brightness = 1.0f;
	
	public SgtRenderQueue RenderQueue = SgtRenderQueue.Transparent;
	
	public int RenderQueueOffset;
	
	public Texture MainTex;
	
	public bool FollowObservers;
	
	[System.NonSerialized]
	private Material material;
	
	[SerializeField]
	private List<SgtSkysphereModel> models = new List<SgtSkysphereModel>();
	
	private static Vector3 tempPosition;
	
	public void ObserverPreCull(SgtObserver observer)
	{
		if (FollowObservers == true)
		{
			tempPosition = transform.position;
			
			SgtHelper.BeginStealthSet(transform);
			{
				transform.position = observer.transform.position;
			}
			SgtHelper.EndStealthSet();
		}
	}
	
	public void ObserverPostRender(SgtObserver observer)
	{
		if (FollowObservers == true)
		{
			SgtHelper.BeginStealthSet(transform);
			{
				transform.position = tempPosition;
			}
			SgtHelper.EndStealthSet();
		}
	}
	
	public void UpdateState()
	{
		UpdateMaterial();
		UpdateModels();
	}
	
	public static SgtSkysphere CreateSkysphere(Transform parent = null)
	{
		return CreateSkysphere(parent, Vector3.zero, Quaternion.identity, Vector3.one);
	}
	
	public static SgtSkysphere CreateSkysphere(Transform parent, Vector3 localPosition, Quaternion localRotation, Vector3 localScale)
	{
		var gameObject = SgtHelper.CreateGameObject("Skysphere", parent, localPosition, localRotation, localScale);
		var skysphere  = gameObject.AddComponent<SgtSkysphere>();
		
		return skysphere;
	}
	
#if UNITY_EDITOR
	[UnityEditor.MenuItem(SgtHelper.GameObjectMenuPrefix + "Skysphere", false, 10)]
	public static void CreateSkysphereMenuItem()
	{
		var skysphere = CreateSkysphere(null);
		
		SgtHelper.SelectAndPing(skysphere);
	}
#endif
	
	protected virtual void OnEnable()
	{
#if UNITY_EDITOR
		if (AllSkyspheres.Count == 0)
		{
			SgtHelper.RepaintAll();
		}
#endif
		AllSkyspheres.Add(this);
		
		for (var i = models.Count - 1; i >= 0; i--)
		{
			var model = models[i];
			
			if (model != null)
			{
				model.gameObject.SetActive(true);
			}
		}
	}
	
	protected virtual void OnDisable()
	{
		AllSkyspheres.Remove(this);
		
		for (var i = models.Count - 1; i >= 0; i--)
		{
			var model = models[i];
			
			if (model != null)
			{
				model.gameObject.SetActive(false);
			}
		}
	}
	
	protected virtual void OnDestroy()
	{
		SgtHelper.Destroy(material);
		
		for (var i = models.Count - 1; i >= 0; i--)
		{
			SgtSkysphereModel.MarkForDestruction(models[i]);
		}
		
		models.Clear();
	}
	
	protected virtual void Update()
	{
		UpdateState();
	}
	
	private void UpdateMaterial()
	{
		if (material == null) material = SgtHelper.CreateTempMaterial(SgtHelper.ShaderNamePrefix + "Skysphere");
		
		var color       = SgtHelper.Brighten(Color, Brightness);
		var renderQueue = (int)RenderQueue + RenderQueueOffset;
		
		material.renderQueue = renderQueue;
		material.SetTexture("_MainTex", MainTex);
		material.SetColor("_Color", color);
	}
	
	private void UpdateModels()
	{
		models.RemoveAll(m => m == null);
		
		if (Meshes.Count != models.Count)
		{
			SgtHelper.ResizeArrayTo(ref models, Meshes.Count, i => SgtSkysphereModel.Create(this), m => SgtSkysphereModel.Pool(m));
		}
		
		for (var i = Meshes.Count - 1; i >= 0; i--)
		{
			models[i].ManualUpdate(Meshes[i], material);
		}
	}
}