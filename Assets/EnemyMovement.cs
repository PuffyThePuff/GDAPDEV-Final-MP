using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public enum EnemyMovementType { Idle, Patrol, Chase }
public class EnemyMovement : MonoBehaviour
{
    [Header("Initial Movement Type")]
    [SerializeField] private EnemyMovementType enemyMovementType;
    public EnemyMovementType EnemyMovementType
    {
        set
        {
            enemyMovementType = value;
            OnMovementChanged();
        }
    }

    public Transform target { get; private set; }

    [Header("Chasing")]
    [SerializeField] private float chaseSpeed;

    [Header("Patrolling")]
    [Range(0.0f, 1.0f)][SerializeField] private float minX = 0.0f;
    [Range(0.0f, 1.0f)][SerializeField] private float maxX = 1.0f;
    [Range(0.0f, 1.0f)][SerializeField] private float minY = 1.0f;
    [Range(0.0f, 1.0f)][SerializeField] private float maxY = 1.0f;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float waitTime;

    private float speed;
    private Vector2 spriteBounds = new Vector2();
    //Destinations
    private Vector2 screenBounds = new Vector2();
    private Vector2 destination = new Vector2();

    private Transform m_transform;
    private WaitForSeconds waitforSeconds;

    // Start is called before the first frame update
    void Start()
    {
        //Get Components
        Collider2D collider = GetComponent<Collider2D>();
        screenBounds.x = collider.bounds.extents.x;
        screenBounds.y = collider.bounds.extents.y;

        //Transform
        m_transform = transform;

        //Less garbage
        waitforSeconds = new WaitForSeconds(waitTime);

        //Screenbounds
        CameraHandler c = CameraHandler.instance;
        screenBounds = c.CalculateScreenToWorldView();
        Debug.Log($"EnemyMovement: {screenBounds}");

        //Set Target
        if(FindObjectOfType<PlayerShip>() != null)
            target = FindObjectOfType<PlayerShip>().transform;

        //Initialize
        OnMovementChanged();
    }

    private void OnMovementChanged()
    {
        StopAllCoroutines();
        if (enemyMovementType == EnemyMovementType.Patrol)
        {
            StartCoroutine("Move");
            speed = patrolSpeed;
        }
        else if (enemyMovementType == EnemyMovementType.Chase)
        {
            SetDestinationToTarget();
            speed = chaseSpeed;
        }
        else
        {
            speed = 0;
        }
    }

    private void LateUpdate()
    {
        if (enemyMovementType == EnemyMovementType.Idle) return;

        float step = speed * Time.deltaTime;

        if (enemyMovementType == EnemyMovementType.Chase)
        {
            step = chaseSpeed * Time.deltaTime;
            SetDestinationToTarget();
        }

        m_transform.position = Vector3.MoveTowards(m_transform.position, destination, step);
    }

    private void SetPatrolDestination()
    {
        //float x = m_transform.position.x + Random.Range(-border.x, border.x);
        //float y = m_transform.position.y + Random.Range(-border.y, border.y);
        float x = Random.Range(-screenBounds.x * minX + spriteBounds.x, screenBounds.x * maxX - spriteBounds.x);
        float y = Random.Range(-screenBounds.y * minY + spriteBounds.y, screenBounds.y * maxY - spriteBounds.y);

        //x = Mathf.Clamp(x, -screenBounds.x, screenBounds.x);
        //y = Mathf.Clamp(y, -screenBounds.y, screenBounds.y);

        //Debug.Log($"{x}, {y} || {screenBounds}");
        destination.x = x;
        destination.y = y;
    }

    private void SetDestination(Vector2 destination)
    {
        this.destination = destination;
    }

    private void SetDestinationToTarget()
    {
        if (target == null) { EnemyMovementType = EnemyMovementType.Idle; return; };

        destination = target.position;
    }

    IEnumerator Move()
    {
        Debug.Log("Moving");
        while (gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(m_transform.position, destination) <= 0.1f)
            {
                SetPatrolDestination();
                yield return waitforSeconds;
            }
            yield return null;
        }
        yield return null;
    }
    
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

#if false
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, border);
    }
#endif
}
