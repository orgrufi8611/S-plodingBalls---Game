using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ZoomEffect : MonoBehaviour
{
    float zoom;
    [SerializeField] float zoomMultiplayer;
    [SerializeField] float minZoom;
    [SerializeField] float maxZoom;
    [SerializeField] float velocity;
    [SerializeField] float smoothTime;
    [SerializeField] Camera cam;
    [SerializeField] Rigidbody2D player;
    [SerializeField] float playerMaxVelocity;
    bool zoomOut;
    // Start is called before the first frame update
    void Start()
    {
        zoomOut = false;
        minZoom = cam.orthographicSize;
        zoom = cam.orthographicSize;
        maxZoom = minZoom * 3;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Mathf.Abs(player.velocity.magnitude) > playerMaxVelocity)
        {
            zoomOut = true;
        }
        else
        {
            zoomOut = false;
        }
        if(zoomOut)
        {
            ZoomOut();
        }
        else
        {
            ZoomIn();
        }
    }

    void ZoomOut()
    {
        zoom = Mathf.Abs(player.velocity.magnitude);
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize,zoom,ref velocity,smoothTime); 
    }

    void ZoomIn() 
    {
        zoom = Mathf.Lerp(zoom, minZoom, velocity * Time.deltaTime);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
    }
}
