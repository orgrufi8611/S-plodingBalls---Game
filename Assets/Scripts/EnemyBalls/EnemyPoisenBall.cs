using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoisenBall : EnemyBall
{
    float overTimeDamage;

    // Start is called before the first frame update
    void Start()
    {
        CheckDupes();
        overTimeDamage = 1;
        gravity = 0;
        ballName = "Green";
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            DamagePlayer();
        }
    }

    void DamagePlayer()
    {
        gameLogic.ReduceLife(overTimeDamage * Time.deltaTime);
    }
}
