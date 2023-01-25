using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config params")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float ladderClimbSpeed;
    [SerializeField] Vector2 deathKick;

    [Header("State")]
    bool isAlive = true;

    [Header("Cached component refs")]
    Rigidbody2D myRB;
    Animator myAnim;
    Collider2D myBodyCollider;
    CircleCollider2D footCollider;
    float startingGravity;

    
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        startingGravity = myRB.gravityScale;
        myAnim = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        footCollider = GetComponent<CircleCollider2D>();
    }

    
    void Update()
    {
        if(!isAlive) { return; }
        Run();
        Jump();
        ClimbLadder();
        FlipSprite();
        Die();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); // value is between -1 and +1
        Vector2 runVelocity = new Vector2(controlThrow * moveSpeed, myRB.velocity.y);
        myRB.velocity = runVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRB.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("running", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if(!footCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRB.velocity += jumpVelocityToAdd;
        }
    }

    private void ClimbLadder()
    {
        if (!footCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {
            myAnim.SetBool("climbing", false);
            myRB.gravityScale = startingGravity;
            return; 
        }
        myRB.gravityScale = 0;
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRB.velocity.x, controlThrow * ladderClimbSpeed);
        myRB.velocity = climbVelocity;

        bool playerHasNoVerticalSpeed = Mathf.Abs(myRB.velocity.y) < Mathf.Epsilon;
        myAnim.SetBool("onLadderNotClimbing", playerHasNoVerticalSpeed);

        bool playerHasVerticalSpeed = Mathf.Abs(myRB.velocity.y) > Mathf.Epsilon;
        myAnim.SetBool("climbing", playerHasVerticalSpeed);
    }

    private void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            myAnim.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            isAlive = false;
            StartCoroutine(VelocityZero());
            StartCoroutine(ProcessDeath());
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRB.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRB.velocity.x), 1);
        }
    }

    IEnumerator VelocityZero()
    {
        yield return new WaitForSeconds(1);
        myRB.velocity = new Vector2(0f, 0f);
        myRB.simulated = false;
    }

    IEnumerator ProcessDeath()
    {
        yield return new WaitForSeconds(2);
        FindObjectOfType<GameSession>().ProcessRestart();
    }
}
