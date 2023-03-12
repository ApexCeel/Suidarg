using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    
    // [SerializeField] private bool linearMovement;
    [SerializeField] private bool sineWaveMovement;
    [SerializeField] private bool circularMovement;
    [Space] [SerializeField] private float amplitude;
    [SerializeField] private float frequency;
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
            CircularMovement(1.5f,10.0f);
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

    private void CircularMovement(float radius, float rotationalSpeed)
    {
        float x = Mathf.Cos(Time.time * rotationalSpeed) * radius;
        float y = Mathf.Sin(Time.time * rotationalSpeed) * radius;
        transform.position = new Vector3(x, y, transform.position.z);
        transform.Translate(Vector3.left *  (speed *Time.deltaTime));
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
