using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float powerMultiplayer;
    [SerializeField] GameLogic gameLogic;
    bool canShoot;

    Vector3 objectPosition;

    //dragNshoot parameters
    Vector3 startPoint;
    Vector3 endPoint;
    Vector2 force;
    [SerializeField] Vector2 maxForce;
    [SerializeField] Vector2 minForce;
    public float power;

    Vector2 lastVelocity;
    DragTrail trail;
    Camera cam;
    Rigidbody2D rb;
    [SerializeField] SlowMotion slowMotion;
    [SerializeField] AudioClip launch;
    [SerializeField] AudioSource audioSource;

    bool paused;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        powerMultiplayer = 1;
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponent<DragTrail>();
        canShoot = true;

        
    }

    // Update is called once per frame
    void Update()
    {
        objectPosition = transform.position;
        
        if (gameLogic.active)
        {
            if(rb.velocity == Vector2.zero)
            {
                rb.velocity = lastVelocity;
            }
            if (canShoot)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                    startPoint.z = 0;
                    cam.GetComponent<CameraMovement>().StopFollow();
                }
                if (Input.GetMouseButton(0))
                {
                    Vector3 currPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                    
                    //trail from first tuoch to current mouse position
                    //trail.RenderLine(startPoint, currPoint);

                    //trail from player ball to the trajectory direction
                    trail.RenderLine(objectPosition, currPoint + (objectPosition - startPoint));
                    slowMotion.SlowMotionEffect();
                }
                if (Input.GetMouseButtonUp(0) && !paused)
                {
                    endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                    endPoint.z = 0;
                    force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minForce.x, maxForce.x),
                                     Mathf.Clamp(startPoint.y - endPoint.y, minForce.y, maxForce.y));
                    rb.velocity = Vector2.zero;
                    Launch(force, 1);
                    canShoot = false;
                    trail.EndLine();
                    slowMotion.NormalSpeed();
                    cam.GetComponent<CameraMovement>().Follow();
                }
                paused = false;
            }
        }
        if (!gameLogic.active)
        {
            trail.EndLine();
            paused = true;
        }
        if (gameLogic.combo % 3 == 0 && gameLogic.combo >= 3) powerMultiplayer = 1.5f;
    }

    public void Launch(Vector3 force,float multiplayer)
    {
        powerMultiplayer = 1;
        rb.AddForce(force * power * multiplayer, ForceMode2D.Impulse);
        audioSource.PlayOneShot(launch);
    }

    public void SlowDownLaunch(Vector3 force, float multiplayer)
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y/10);
        }
        else
        {
            rb.velocity /= 2;
        }
        Launch(force, multiplayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            gameLogic.ReduceLife(gameLogic.life);
        }
        if((collision.gameObject.tag == "Ball"))
        {
            canShoot=true;
            gameLogic.combo++;
        }
    }
}
