using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    [SerializeField] private Vector2 border;
    
    private Vector2 screenBounds = new Vector2();
    private Vector2 destination = new Vector2();

    private Transform m_transform;
    private WaitForSeconds waitforSeconds;

    // Start is called before the first frame update
    void Start()
    {
        //Transform
        m_transform = transform;
        //Less garbage
        waitforSeconds = new WaitForSeconds(waitTime);

        //Screenbounds
        CameraHandler c = CameraHandler.instance;
        screenBounds = c.CalculateScreenToWorldView();

        //Initialize
        SetDestination();
        StartCoroutine("Move");
    }

    private void LateUpdate()
    {
        float step = speed * Time.deltaTime;
        m_transform.position = Vector3.MoveTowards(m_transform.position, destination, step);
    }

    private void SetDestination()
    {
        float x = m_transform.position.x + Random.Range(-border.x, border.x);
        float y = m_transform.position.y + Random.Range(-border.y, border.y);

        x = Mathf.Clamp(x, -screenBounds.x, screenBounds.x);
        y = Mathf.Clamp(y, -screenBounds.y, screenBounds.y);

        destination.x = x;
        destination.y = y;
    }

    IEnumerator Move()
    {
        while (gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(m_transform.position, destination) <= 0.1f)
            {
                SetDestination();
                yield return waitforSeconds;
            }
        }

        yield return null;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, border);
    }
}
