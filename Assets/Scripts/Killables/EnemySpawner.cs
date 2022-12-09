using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyPool pool;
    [SerializeField] private Killable[] killablePrefabs;
    [SerializeField] private Killable boss;
    [SerializeField] private int requiredToKillForBoss;

    [Header("Spawn area")]
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] private bool onlyEdge;

    [Range(0.0f, 1.0f)][SerializeField] private float minX = 0.0f;
    [Range(0.0f, 1.0f)][SerializeField] private float maxX = 1.0f;
    [Range(0.0f, 1.0f)][SerializeField] private float minY = 1.0f;
    [Range(0.0f, 1.0f)][SerializeField] private float maxY = 1.0f;

    CameraHandler camHandler;
    Transform m_transform;
    Vector2 spawnPosition = new Vector2();
#if false
    
    //[SerializeField] private Enemy[] enemyPrefabs;
    //[SerializeField] private Enemy2[] enemy2Prefabs;
    [Header("Reference Canvas")]
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private Vector2 spawnBorder;
    //[SerializeField] private Vector2 spawnArea;

    [SerializeField] private Crosshair crosshair;
    Transform my_transform;
    Vector3 spawnPosition;
    // Start is called before the first frame update
#endif
    void Start()
    {
        m_transform = transform;
        camHandler = CameraHandler.instance;
		pool = FindObjectOfType<EnemyPool>();


        InvokeRepeating("Spawn", 0.0f, 2.0f);
    }

    public void Spawn()
    {
#if false
        int index = Random.Range(0, killablePrefabs.Length); //minInclusive, maxExclusive
        //int index2 = Random.Range(0, enemy2Prefabs.Length);
        int loops = 0;
        do
        {
            if (loops >= 50) { break; }

            float minAreaX = canvasRectTransform.position.x - (((canvasRectTransform.rect.width / 2.0f) * canvasRectTransform.localScale.x) + (spawnBorder.x * Utils.CalculateAspectRatio()));
            float maxAreaX = canvasRectTransform.position.x + (((canvasRectTransform.rect.width / 2.0f) * canvasRectTransform.localScale.x) + (spawnBorder.x * Utils.CalculateAspectRatio()));
            float minAreaY = canvasRectTransform.position.y - (((canvasRectTransform.rect.height / 2.0f) * canvasRectTransform.localScale.y) + (spawnBorder.y * Utils.CalculateAspectRatio()));
            float maxAreaY = canvasRectTransform.position.y + (((canvasRectTransform.rect.height / 2.0f) * canvasRectTransform.localScale.y) + (spawnBorder.y * Utils.CalculateAspectRatio()));
            float minAreaZ = 1.0f;
            float maxAreaZ = 1.0f;

            spawnPosition.x = Random.Range(minAreaX, maxAreaX);
            spawnPosition.y = Random.Range(minAreaY, maxAreaY);
            spawnPosition.z = Random.Range(minAreaZ, maxAreaZ);

            //Debug.Log(spawnPosition.ToString());
            if (CanSpawnAtPosition(canvasRectTransform.localPosition + spawnPosition)) break;
            loops++;
        } while (!CanSpawnAtPosition(canvasRectTransform.localPosition + spawnPosition));
        
        //Enemy enemy = Instantiate(enemyPrefabs[index], my_transform.position + spawnPosition, Quaternion.identity, my_transform);
        //Enemy2 enemy2 = Instantiate(enemy2Prefabs[index2], my_transform.position + spawnPosition + new Vector3(Random.Range(-5,5), Random.Range(-5, 5), Random.Range(-5, 5)), Quaternion.identity, my_transform);
        //Killable killable = Instantiate(killablePrefabs[index], canvasRectTransform.localPosition + spawnPosition, Quaternion.identity);
		Killable killable = pool.GetEnemyFromPool();
		killable.transform.SetPositionAndRotation(canvasRectTransform.localPosition + spawnPosition, Quaternion.identity);
        //Debug.Log((canvasRectTransform.localPosition + spawnPosition).ToString());
        killable.gameObject.SetActive(true);
        //enemy.gameObject.SetActive(true);
        //enemy2.gameObject.SetActive(true);
#endif
        if(ScoreManager.Singleton.score >= requiredToKillForBoss)
        {
            if (boss.isDead) return;

            boss.gameObject.SetActive(true);
            return;
        }

        float aspectRatio = camHandler.AspectRatio();
        float x = m_transform.position.x + Random.Range(-spawnArea.x * minX * aspectRatio, spawnArea.x * maxX * aspectRatio);
        float y = m_transform.position.y + Random.Range(-spawnArea.y * minY * aspectRatio , spawnArea.y * maxY * aspectRatio);


        if (onlyEdge)
        {
            int indexToEdge = Random.Range(1, 3);
            //Debug.Log($"{indexToEdge} || ({x}, {y})");
            switch (indexToEdge)
            {
                case 1:
                    x = -spawnArea.x * minX * aspectRatio;
                    break;
                case 2:
                    x = spawnArea.x * maxX * aspectRatio;
                    break;
                case 3:
                    y = spawnArea.y * maxY * aspectRatio;
                    break;
#if false
                case 4:
                    y = -spawnArea.y * minY * aspectRatio;
                    break;
#endif
            }
        }

        spawnPosition.x = x;
        spawnPosition.y = y;

        Killable killable = pool.GetEnemyFromPool();
        killable.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
        //Debug.Log((canvasRectTransform.localPosition + spawnPosition).ToString());
        killable.gameObject.SetActive(true);
    }

#if false
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
#endif

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float aspect = Camera.main.aspect;
        Gizmos.DrawWireCube(transform.position, spawnArea * aspect);
    }
}
