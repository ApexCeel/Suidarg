using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    
    // [SerializeField] private bool linearMovement;
    [SerializeField] private bool sineWaveMovement;
    [Space] 
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;
    [Space]
    [SerializeField] private bool circularMovement;
    [Space]
    [SerializeField] private float rotationalSpeed = 1;
    [SerializeField] private float radius = 1;
    [Space]
    [Space]
    [SerializeField] private float speed = 4;


    public void Movement()
    {
        if (sineWaveMovement)
        {
            SineMovement();
        }
        else if (circularMovement)
        {
            CircularMovement();
        }
        else
        {
            LinearMovement();
        }
    }

    public void SetEnemySpeed(int speedToSet)
    {
        speed = speedToSet;
    }

    private void CircularMovement()
    {
        Vector3 initialPosition = transform.position;
        float x = initialPosition.x + Mathf.Cos(Time.time * rotationalSpeed) * radius;
        float y = initialPosition.y + Mathf.Sin(Time.time * rotationalSpeed) * radius;
        transform.position = new Vector3(x, y, transform.position.z);
        transform.Translate(Vector3.left *  (speed *Time.deltaTime), Space.Self);
    }

    private void LinearMovement()
    {
        transform.Translate(Vector3.left * (speed * Time.deltaTime));
    }

    private void SineMovement()
    {
        float y = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        transform.Translate(Vector3.left * (speed * Time.deltaTime));
    }

    private void ResetToStart()
    {
        transform.position = new Vector3(14.5f, UnityEngine.Random.Range(-6.0f, 8.0f), 0);
    }
}
