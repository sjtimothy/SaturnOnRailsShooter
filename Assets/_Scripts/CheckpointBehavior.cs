using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehavior : MonoBehaviour
{
	//public List<EnemyHealth> bosses;
	public bool bossFight;
	//public BossHealthBar bossHealthBar;
	public bool cameraWaypoint;
	public bool loops = false;
	//public List<EnemyHealth> destroyOnVictory;
	public string dialogue;
	//public List<CheckpointActivatedMovement> enemiesToTrigger;
	public bool loadScene;
	public int sceneToLoad;
	public string speaker;
	public bool triggerEnemies;
	
	private void OnTriggerEnter(Collider col)
	{	

		if (col.tag != "Player")
		{
			return;
		}
		
//		if (!string.IsNullOrEmpty(this.dialogue))
//		{
//			var inGameDialogue = InGameDialogue.Instance;
//			if (InGameDialogue.DialogueCoroutine != null)
//			{
//				inGameDialogue.StopCoroutine(InGameDialogue.DialogueCoroutine);
//			}
//			InGameDialogue.DialogueCoroutine = inGameDialogue.SayDialogue(this.speaker, this.dialogue);
//			inGameDialogue.StartCoroutine(InGameDialogue.DialogueCoroutine);
//		}

/*		if (this.loadScene)
		{
			SceneLoader.LoadCutscene(this.sceneToLoad);
			this.loadScene = false;
		}
*/		
		if (this.cameraWaypoint)
		{	

			CameraMovement.cameraMovement.NextWaypoint(loops);
			this.cameraWaypoint = false;

		}
		
/*		if (this.triggerEnemies)
		{
			foreach (var enemy in this.enemiesToTrigger)
			{
				enemy.Trigger();
			}
			this.triggerEnemies = false;
		}
		
		if (this.bossFight)
		{
			if(!loops)
				CameraMovement.cameraMovement.fightingBoss = true;
			this.bossFight = false;
			this.StartCoroutine(this.TestForVictory());
		}
*/	}

	private void OnTriggerExit()
	{
		if(loops)
			this.cameraWaypoint = true;
	}
	
/*	private IEnumerator TestForVictory()
	{
		this.bossHealthBar.SwoopIn();
		
		var sumMaxHealth = 0;
		foreach (var boss in this.bosses)
			sumMaxHealth += boss.startingHealth;
		
		var lastHeath = 0;
		while (this.bosses.Count > 0)
		{
			this.bosses.RemoveAll(enemyHealth => enemyHealth == null);
			
			var health = 0;
			foreach (var boss in this.bosses)
				health += boss.Health;
			
			if(lastHeath != health)
			{
				this.bossHealthBar.UpdateHealthBar(health, sumMaxHealth);
				lastHeath = health;
			}
			
			yield return new WaitForEndOfFrame();
		}
		
		CameraMovement.cameraMovement.fightingBoss = false;
		
		yield return new WaitForSeconds(.4f);
		
		foreach (var enemyHealth in this.destroyOnVictory)
			enemyHealth.Death();
		
		this.bossHealthBar.SwoopOut();
	}
	*/
}
