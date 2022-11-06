using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Vector3 spawnArea;

    Transform my_transform;
    Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        my_transform = transform;
        InvokeRepeating("Spawn", 0.0f, 3.0f);
    }

    public void Spawn()
    {
        int index = Random.Range(0, enemyPrefabs.Length); //minInclusive, maxExclusive
        int loops = 0;
        do
        {
            if(loops >= 50) { break; }

            spawnPosition.x = Random.Range(-spawnArea.x / 2.0f, spawnArea.x / 2.0f);
            spawnPosition.y = Random.Range(-spawnArea.y / 2.0f, spawnArea.y / 2.0f);
            spawnPosition.z = Random.Range(-spawnArea.z / 2.0f, spawnArea.z / 2.0f);
            //Debug.Log(spawnPosition);
            if (CanSpawnAtPosition(spawnPosition)) break;
            loops++;
        } while (!CanSpawnAtPosition(spawnPosition));
        
        Enemy enemy = Instantiate(enemyPrefabs[index], my_transform.localPosition + spawnPosition, Quaternion.identity, my_transform);
        enemy.gameObject.SetActive(true);
        
    }

    public bool CanSpawnAtPosition(Vector3 spawnPos)
    {
        Debug.Log("Repositioning");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 100.0f);
        for(int i = 0; i < colliders.Length; i++)
        {
            Vector3 centerPoint = colliders[i].bounds.center;
            float width = colliders[i].bounds.extents.x;
            float height = colliders[i].bounds.extents.y;
            float depth = colliders[i].bounds.extents.z;

            float leftExtent = centerPoint.x - width;
            float rightExtent = centerPoint.x + width;
            float lowerExtent = centerPoint.y - height;
            float upperExtent = centerPoint.y + height;
            float innerExtent = centerPoint.z - depth;
            float outerExtent = centerPoint.z + depth;

            //Debug.Log(new Vector4(leftExtent, rightExtent, lowerExtent, upperExtent));
            if(spawnPos.x >= leftExtent && spawnPos.x <= rightExtent &&
                spawnPos.y >= lowerExtent && spawnPos.y <= upperExtent &&
                spawnPos.z >= innerExtent && spawnPos.z <= outerExtent)
            {
                return false;
            }
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
}
