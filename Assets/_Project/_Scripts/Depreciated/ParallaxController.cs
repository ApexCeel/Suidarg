using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ParallaxController : MonoBehaviour
{
    
    public float parallaxSpeed = 0.1f;
    private float _length;
    private float _startPos;
    private float _lastCameraX;
    
    

    private void Start()
    {
        _length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        _startPos = transform.position.x;
        // _lastCameraX = Camera.main.transform.position.x;
    }


    private void Update()
    {
        
        float temp = (Camera.main.transform.position.x * (1 - parallaxSpeed));
        float dist = (Camera.main.transform.position.x * parallaxSpeed);
        
        transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);
        
        if (temp > _startPos + _length) _startPos += _length;
        else if (temp < _startPos - _length) _startPos -= _length;

    }
}




