using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;


    Transform m_Transform;
    // Start is called before the first frame update
    void Start()
    {
        m_Transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        m_Transform.position += m_Transform.right * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
