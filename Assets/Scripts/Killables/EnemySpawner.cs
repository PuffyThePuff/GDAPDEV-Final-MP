using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Properties")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Vector2 spawnBorder;
    [SerializeField] private Vector2 spawnPositionOffset;

    [Header("Spawn conditions")]
    [SerializeField] private int maxNumSpawn;
    [SerializeField] private int spawnDelay;

    private int currNumSpawn;
    [Header("Gizmos")]
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private Crosshair crosshair;

    Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        crosshair = Crosshair.Instance;
        currNumSpawn = 0;
        InvokeRepeating("Spawn", spawnDelay, spawnDelay);
    }

    public void Spawn()
    {
        int index = Random.Range(0, enemyPrefabs.Length); //minInclusive, maxExclusive
        int loops = 0;
        
        do
        {
            if (loops >= 50) { break; }

            float aspectRatio = Utils.AspectRatio();
            //Determine spawn area
            float minAreaX = (canvasRectTransform.localPosition.x + (spawnPositionOffset.x * aspectRatio)) - (canvasRectTransform.rect.width * canvasRectTransform.localScale.x / 2.0f) + (spawnBorder.x * aspectRatio);
            float maxAreaX = (canvasRectTransform.localPosition.x + (spawnPositionOffset.x * aspectRatio)) + (canvasRectTransform.rect.width * canvasRectTransform.localScale.x / 2.0f) - (spawnBorder.x * aspectRatio);
            float minAreaY = (canvasRectTransform.localPosition.y + (spawnPositionOffset.y * aspectRatio)) - (canvasRectTransform.rect.height * canvasRectTransform.localScale.y / 2.0f) + (spawnBorder.y * aspectRatio);
            float maxAreaY = (canvasRectTransform.localPosition.y + (spawnPositionOffset.y * aspectRatio)) + (canvasRectTransform.rect.height * canvasRectTransform.localScale.y / 2.0f) - (spawnBorder.y * aspectRatio);
            float minAreaZ = canvasRectTransform.localPosition.z + 1.0f;
            float maxAreaZ = canvasRectTransform.localPosition.z + 2.0f;
            

            //Randomly set the spawn position
            spawnPosition.x = Random.Range(minAreaX, maxAreaX);
            spawnPosition.y = Random.Range(minAreaY, maxAreaY);
            spawnPosition.z = Random.Range(minAreaZ, maxAreaZ);

            //Just for extra percausion
            if (CanSpawnAtPosition(spawnPosition)) {
                GameObject enemy = Instantiate(enemyPrefabs[index], spawnPosition, Quaternion.identity);
                enemy.SetActive(true);
                currNumSpawn++;
                if (currNumSpawn >= maxNumSpawn) CancelInvoke("Spawn");
                break;
            }

            loops++;

        } while (!CanSpawnAtPosition(spawnPosition)); //Loop till no colliders are not intersecting  
    }

    public bool CanSpawnAtPosition(Vector3 spawnPos)
    {
        //Debug.Log("Repositioning");
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
        float aspectRatio = Utils.AspectRatio();
        Gizmos.color = Color.green;
        float minAreaX = (canvasRectTransform.localPosition.x + (spawnPositionOffset.x * aspectRatio)) - (canvasRectTransform.rect.width * canvasRectTransform.localScale.x / 2.0f) + spawnBorder.x * aspectRatio;
        float maxAreaX = (canvasRectTransform.localPosition.x + (spawnPositionOffset.x * aspectRatio)) + (canvasRectTransform.rect.width * canvasRectTransform.localScale.x / 2.0f) - spawnBorder.x * aspectRatio;
        float minAreaY = (canvasRectTransform.localPosition.y + (spawnPositionOffset.y * aspectRatio)) - (canvasRectTransform.rect.height * canvasRectTransform.localScale.y / 2.0f) + spawnBorder.y * aspectRatio;
        float maxAreaY = (canvasRectTransform.localPosition.y + (spawnPositionOffset.y * aspectRatio)) + (canvasRectTransform.rect.height * canvasRectTransform.localScale.y / 2.0f) - spawnBorder.y * aspectRatio;
        float minAreaZ = canvasRectTransform.localPosition.z + 1.0f;
        float maxAreaZ = canvasRectTransform.localPosition.z + 2.0f;

        //Debug.Log(new Vector4(minAreaX, maxAreaX, minAreaY, maxAreaY));
        Vector3 from = new Vector3(maxAreaX, maxAreaY, maxAreaZ);
        Vector3 to = new Vector3(minAreaX, minAreaY, minAreaZ);
        Gizmos.DrawLine(from, to);
        //Gizmos.DrawWireCube(transform.position * aspectRatio, spawnArea * aspectRatio);
    }
}
