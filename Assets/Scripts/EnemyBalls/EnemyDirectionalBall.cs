using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirectionalBall : EnemyBall
{
    int rotationAngle;
    // Start is called before the first frame update
    void Start()
    {
        CheckDupes();
        ballName = "Orange";
        rotationAngle = Random.Range(0,360);
        transform.Rotate(0,0,rotationAngle);
        gravity = 0;
    }

    public override void ForceToPlayer()
    {
        Vector2 direction = Quaternion.Euler(0, 0, rotationAngle) * Vector3.up;
        player.GetComponent<PlayerController>().SlowDownLaunch(direction, power);
    }
}
