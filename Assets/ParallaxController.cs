using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ParallaxController : MonoBehaviour
{
    
    public float parallaxSpeed = 0.1f;
    private float length;
    private float startPos;

    private void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        startPos = transform.position.x;
    }


    private void Update()
    {
        float temp = (Camera.main.transform.position.x * (1 - parallaxSpeed));
        float dist = (Camera.main.transform.position.x * parallaxSpeed);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }

}
