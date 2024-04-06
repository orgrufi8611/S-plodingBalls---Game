using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    [SerializeField] protected float gravity;
    [SerializeField] protected int points;
    [SerializeField] protected string ballName;
    [SerializeField] protected GameObject explosion;
    [SerializeField] GameObject floatingText;
    [SerializeField] protected Color color;
    [SerializeField] protected float power;
    [SerializeField] float maxDistance;
    public GameObject player;
    public  GameLogic gameLogic;
    public EnemySpawner spawner;


    private void Start()
    {
        CheckDupes();
    }

    protected void CheckDupes()
    {
        //check for ball overlaping and destroy overlap
        ContactFilter2D layerMask = new ContactFilter2D();
        layerMask.layerMask = LayerMask.GetMask("Balls");
        layerMask.useLayerMask = true;
        List<Collider2D> colliders = new List<Collider2D>();
        int overlap = GetComponent<Collider2D>().OverlapCollider(layerMask, colliders);
        if (overlap > 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        maxDistance = 1.5f * ScreenSize.screenUnitHeight;
        if(gameLogic.active)
            transform.Translate(0, -gravity * Time.deltaTime, 0);
        Vector3 distanceVector = transform.position - player.transform.position;
        float distance = Mathf.Abs(distanceVector.magnitude);
        if(distance > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    public virtual void ForceToPlayer()
    {
        player.GetComponent<PlayerController>().SlowDownLaunch(Vector2.up,power * player.GetComponent<PlayerController>().powerMultiplayer);
    }


    void ShowFloatingText()
    {
        //Debug.Log("Instantiating flating text");
        GameObject text = Instantiate(floatingText,transform.position,Quaternion.identity);
        text.GetComponent<FloatingText>().text.text = "+" + points.ToString();
        text.GetComponent<FloatingText>().text.color = color;
        text.GetComponent<FloatingText>().subText.text = "x"+gameLogic.multiplayer.ToString();
        //Debug.Log("new Text at:" + text.transform.position + "with scale of: " + text.transform.localScale);

    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            spawner.EnemyDown();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Player")
        {
            ShowFloatingText();
            ForceToPlayer();
            gameLogic.AddPoints(points);
            Instantiate(explosion,transform.position,Quaternion.identity);
            spawner.EnemyDown();
            Destroy(gameObject);
        }
    }
}
