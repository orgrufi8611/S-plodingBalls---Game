using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float cooldown,minCooldown,maxCooldown;
    [SerializeField] float time;
    [SerializeField] float multiplayer;
    [SerializeField] int maxBallsOnScreen;
    [SerializeField] int ballsOnScreen;
    [SerializeField] float initScreenSize;
    [SerializeField] List<EnemyBall> balls = new List<EnemyBall>();
    [SerializeField] GameObject player;
    [SerializeField] GameLogic gameLogic;
    [SerializeField] BoxCollider2D boxCollider;
    int ballsDestroyed;
    int ballsAllowed;
    [SerializeField] float borderWidth,borderHeight;
    bool init;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        init = false;
        ballsDestroyed = 0;
        ballsAllowed = 0;
        maxBallsOnScreen = 10;
        multiplayer = 1;
        time = 0;
        for(int i = 0; i < maxBallsOnScreen; i++)
        {
            SpawnBall();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!init) 
        {
            initScreenSize = ScreenSize.screenUnitWidth;
            boxCollider.size = new Vector2 (ScreenSize.screenUnitWidth, ScreenSize.screenUnitHeight);
            init = true;
        }
        if (gameLogic.active)
        {
            //check how many balls on screen to know if to spawn more
            CountBallsOnSceen(boxCollider);
            //change the maximum balls on screen with the screen size
            maxBallsOnScreen = 10 * (int)(ScreenSize.screenUnitWidth / initScreenSize);
            multiplayer = ScreenSize.screenUnitWidth / 4;
            time += Time.deltaTime;
            if (time > cooldown)
            {
                time = 0;
                cooldown = Random.Range(minCooldown, maxCooldown);
                if (ballsOnScreen < maxBallsOnScreen)
                {
                    SpawnBall();
                }
            }
            if (ballsDestroyed % (10 + 5 * count) == 0 && ballsDestroyed >= (10 + 5 * count))
            {
                //what type of balls are allowed to spawn
                ballsAllowed = Mathf.Clamp(ballsAllowed + 1, 0, balls.Count);
                count++;
            }
        }
    }

    void SpawnBall()
    {
        for(int i = 0; i < multiplayer; i++)
        {
            int r = Random.Range(0, ballsAllowed + 5);
            if(r < 5) r = 0;
            EnemyBall ball = Instantiate(balls[r % 5], SpawnPoint(), Quaternion.identity);
            ball.spawner = this;
            ball.gameLogic = gameLogic;
            ball.player = player;
        }
    }

    //generate the spawn point to be inside the screen and far enough from the player
    Vector3 SpawnPoint()
    {
        Vector3 spawnPoint;
        float width = ScreenSize.screenUnitWidth, height = ScreenSize.screenUnitHeight;
        float randomX = Random.Range(-width / 2, width / 2);
        float x = Mathf.Clamp(transform.position.x + randomX,
                            transform.position.x - ScreenSize.screenUnitWidth,
                            transform.position.x + ScreenSize.screenUnitWidth);
        float randomY = Random.Range(-height / 2, height / 2);
        float y = Mathf.Clamp(transform.position.y + randomY,
                            transform.position.y - ScreenSize.screenUnitHeight,
                            transform.position.y + ScreenSize.screenUnitHeight);
        x = Mathf.Clamp(x, -borderWidth / 2, borderWidth / 2);
        y = Mathf.Clamp(y, -borderHeight/ 2, borderHeight/ 2);
        spawnPoint = new Vector3(x, y, 0);
        float distanceFromPlayer = (spawnPoint - player.transform.position).magnitude;
        if (distanceFromPlayer > 1)
        {
            return spawnPoint;
        }
        else return SpawnPoint();
    }

    //count how many balls are on the screen (camera view
    void CountBallsOnSceen(Collider2D collider)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(collider.bounds.center, collider.bounds.size, 0f);
        ballsOnScreen = colliders.Length;
    }

    public void EnemyDown()
    {
        ballsDestroyed++;
    }
}
