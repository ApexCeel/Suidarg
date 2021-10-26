using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField] private float rotateSpeed = 3.0f;
    [SerializeField] private GameObject asteroidExplosion;

    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(other);
            _renderer.gameObject.SetActive(false);
            Instantiate(asteroidExplosion, transform.localPosition, quaternion.identity);
            Destroy(gameObject, 0.48f);
        }
    }
}
