using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{

	[SerializeField]private List<Killable> activeEnemyList;
	[SerializeField]private List<Killable> deactiveEnemyList;

	[SerializeField] private Killable[] killablePrefabs;

	private void Start()
	{
		for(int i = 0; i < killablePrefabs.Length; i++)
		{
			Killable temp = Instantiate(killablePrefabs[i], new Vector3(9999.0f, 9999.0f, 9999.0f), Quaternion.identity);
			deactiveEnemyList.Add(temp);
		}
	}

	public void AddEnemyPool(Killable obj)
	{
		deactiveEnemyList.Add(obj);
	}

	public Killable GetEnemyFromPool()
	{
		foreach (Killable enemy in deactiveEnemyList)
		{
			Debug.Log("Grabbed enemy from pool");
			enemy.setMaxHP(PlayerDataManager.instance.level);
			enemy.initialize();
			activeEnemyList.Add(enemy);
			deactiveEnemyList.Remove(enemy);
			return enemy;
		}
		int index = Random.Range(0, killablePrefabs.Length);
		Killable temp = Instantiate(killablePrefabs[index]);
		activeEnemyList.Add(temp);
		//Debug.Log($"Created to pool {killablePrefabs[index]}");
		return temp;
	}

	public void killActivePool()
	{
		foreach(Killable enemy in activeEnemyList)
		{
			setUnused(enemy);
		}
	}

	public void setUnused(Killable killable)
	{
		activeEnemyList.Remove(killable);
		Debug.Log("Adding to unused pool");
		killable.transform.position = new Vector3(9999.0f, 9999.0f, 9999.0f);
		deactiveEnemyList.Add(killable);
	}

	public void ClearAllEnemyPools()
	{
		deactiveEnemyList.Clear();
		activeEnemyList.Clear();
	}

}
