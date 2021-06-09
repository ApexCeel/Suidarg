using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;

    
    
    void Start()
    {
        
        transform.position = new Vector3(0, -3.0f);
        
    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        var direction = new Vector3(horizontalInput, verticalInput, 0);
        
        transform.Translate(direction * (speed * Time.deltaTime));

        if (transform.position.x > 9.0f)
        {
            transform.position = new Vector3(9.0f, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -9.0f)
        {
            transform.position = new Vector3(-9.0f, transform.position.y, transform.position.z);
        }
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);
        }
    }
}
