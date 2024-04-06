using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    bool initiate;
    bool follow;
    [SerializeField]GameObject player;
    [SerializeField] float velocity;
    [SerializeField] Collider2D camArea;
    public Vector3 lastLoction;
    // Start is called before the first frame update
    void Start()
    {
        follow = true;
        initiate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!initiate)
        {
            lastLoction = player.transform.position;
            initiate = true;
        }
        Vector2 delta = player.transform.position - lastLoction;
        if(follow) 
        {
            transform.Translate(delta,0);
            lastLoction = player.transform.position;
        }
    }

    public void StopFollow()
    {
        follow = false;
    }

    public void Follow()
    {
        follow=true;
    }
}
