using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtractBall : EnemyBall
{
    float attraction;
    // Start is called before the first frame update
    void Start()
    {
        CheckDupes();
        attraction = 1;
        gravity = 0;
        ballName = "Black";
    }

    // Update is called once per frame


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RepealPlayer();
        }
    }

    void RepealPlayer()
    {
        Vector2 force = player.transform.position - transform.position;
        force.Normalize();
        player.GetComponent<Rigidbody2D>().AddForce(-force * attraction, ForceMode2D.Impulse);
    }
}
