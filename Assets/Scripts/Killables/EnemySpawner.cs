using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Killable[] killablePrefabs;
    //[SerializeField] private Enemy[] enemyPrefabs;
    //[SerializeField] private Enemy2[] enemy2Prefabs;
    [Header("Reference Canvas")]
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private Vector2 spawnArea;

    Crosshair crosshair;
    Transform my_transform;
    Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        crosshair = Crosshair.Instance;
        my_transform = transform;
        InvokeRepeating("Spawn", 0.0f, 2.0f);
    }

    public void Spawn()
    {
        int index = Random.Range(0, killablePrefabs.Length); //minInclusive, maxExclusive
        //int index2 = Random.Range(0, enemy2Prefabs.Length);
        int loops = 0;
        do
        {
            if (loops >= 50) { break; }

            float minAreaX = canvasRectTransform.position.x - ((canvasRectTransform.rect.width / 2.0f) + crosshair.Border.x + (spawnArea.x * crosshair.aspectRatio) * canvasRectTransform.localScale.x);// + crosshair.Border.x + (spawnArea.x * crosshair.aspectRatio));
            float maxAreaX = canvasRectTransform.position.x + ((canvasRectTransform.rect.width / 2.0f) - crosshair.Border.x - (spawnArea.x * crosshair.aspectRatio) * canvasRectTransform.localScale.x);// - crosshair.Border.x - (spawnArea.x * crosshair.aspectRatio));
            float minAreaY = canvasRectTransform.position.y - ((canvasRectTransform.rect.height / 2.0f) + crosshair.Border.y + (spawnArea.y * crosshair.aspectRatio) * canvasRectTransform.localScale.y);// + crosshair.Border.y + (spawnArea.y * crosshair.aspectRatio));
            float maxAreaY = canvasRectTransform.position.y + ((canvasRectTransform.rect.height / 2.0f) - crosshair.Border.y - (spawnArea.y * crosshair.aspectRatio) * canvasRectTransform.localScale.y);// - crosshair.Border.y - (spawnArea.y * crosshair.aspectRatio));
            float minAreaZ = canvasRectTransform.position.z - 1.0f;
            float maxAreaZ = canvasRectTransform.position.z - 2.0f;

            spawnPosition.x = Random.Range(minAreaX, maxAreaX);
            spawnPosition.y = Random.Range(minAreaY, maxAreaY);
            spawnPosition.z = Random.Range(minAreaZ, maxAreaZ);

            if (CanSpawnAtPosition(my_transform.position + spawnPosition)) break;
            loops++;
        } while (!CanSpawnAtPosition(my_transform.position + spawnPosition));
        
        //Enemy enemy = Instantiate(enemyPrefabs[index], my_transform.position + spawnPosition, Quaternion.identity, my_transform);
        //Enemy2 enemy2 = Instantiate(enemy2Prefabs[index2], my_transform.position + spawnPosition + new Vector3(Random.Range(-5,5), Random.Range(-5, 5), Random.Range(-5, 5)), Quaternion.identity, my_transform);
        Killable killable = Instantiate(killablePrefabs[index], my_transform.position + spawnPosition, Quaternion.identity, my_transform);
        killable.gameObject.SetActive(true);
        //enemy.gameObject.SetActive(true);
        //enemy2.gameObject.SetActive(true);
        
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
}
