using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] float destroyTime;
    public TextMeshPro text;
    public TextMeshPro subText;
    [SerializeField] Vector3 offset = new Vector3 (0, 0.5f, 0);
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        transform.localPosition += offset;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time > destroyTime) 
        {
            Destroy(gameObject, destroyTime);
        }
    }

}
