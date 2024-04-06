using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopTextTest : MonoBehaviour
{
    [SerializeField] GameObject popUpText;
    [SerializeField] float cooldown;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > cooldown)
        {
            time = 0;
            Instantiate(popUpText,transform.position,Quaternion.identity);
        }
    }
}
