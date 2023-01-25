using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderCentering : MonoBehaviour
{
    public Transform ladderPosX;
    GameObject playerObject;
    [SerializeField] bool shouldCenter = false;
    private void Update()
    {
        ladderPosX = this.transform;
        if(shouldCenter)
        {
            playerObject.transform.position = new Vector2(transform.position.x, playerObject.transform.position.y);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player") { return; }
        playerObject = collision.gameObject;
        shouldCenter = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shouldCenter = false;
        playerObject = null;
    }

}
