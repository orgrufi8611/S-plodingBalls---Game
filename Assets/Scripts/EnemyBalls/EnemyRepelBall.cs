using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRepelBall : EnemyBall
{
    float repelment;
    // Start is called before the first frame update
    void Start()
    {
        CheckDupes();
        repelment = 0.25f;
        gravity = 0;
        ballName = "Blue";
    }

    // Update is called once per frame
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            RepealPlayer();
        }
    }

    void RepealPlayer()
    {
        Vector2 force = transform.position - player.transform.position; 
        force.Normalize();
        player.GetComponent<Rigidbody2D>().AddForce(-force * repelment, ForceMode2D.Impulse);
    }
}
