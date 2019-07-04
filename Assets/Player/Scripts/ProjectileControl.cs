using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour
{

    private int aliveCount = 2;
    private Vector2 forceV = new Vector2(0,0);
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        forceV = direction * force;
        rigidbody2D.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GuardControl gc = collision.collider.GetComponent<GuardControl>();
        if(gc != null)
        {
            aliveCount--;
            if (aliveCount == 1)
            {
                Vector2 temp = new Vector2(-forceV.x, forceV.y);
                rigidbody2D.AddForce(temp);
            }
            else
            {
                Destroy(gameObject);
            }
            Debug.Log("Collision with Guard");
            return;
        }

        HitControl hc = collision.collider.GetComponent<HitControl>();
        if(hc != null)
        {
            hc.hitted();
            Destroy(gameObject);
            Debug.Log("Collision with Other");
        }
        Debug.Log("Collision!!!");
        Destroy(gameObject);
    }

}
