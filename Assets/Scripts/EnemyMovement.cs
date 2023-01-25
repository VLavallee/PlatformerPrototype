using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    CircleCollider2D footCollider;
    [SerializeField] float moveSpeed = 1f;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Movement();
        
    }

    void Movement()
    {
        if(IsFacingRight())
        {
            myRigidbody.velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0);
        }
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }
}
