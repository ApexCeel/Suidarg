using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Asteroid : MonoBehaviour
{

    [SerializeField] private float rotateSpeed = 3.0f;
    [SerializeField] private GameObject asteroidExplosion;
    [Space] [SerializeField] private AudioClip asteroidExplosionClip;

    private AudioSource _audioSource;
    private SpriteRenderer _renderer;
    private SpawnManager _spawnManager;

    private void Start()
    {
        
        _renderer = GetComponent<SpriteRenderer>();
        _spawnManager = FindObjectOfType<SpawnManager>();

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on Asteroid is NULL!");
        }
        else
        {
            _audioSource.clip = asteroidExplosionClip;
        }
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
            
            _renderer.enabled = false;
            
            Instantiate(asteroidExplosion, transform.localPosition, quaternion.identity);
            _audioSource.Play();
            _spawnManager.StartSpawning();
            Destroy(gameObject, 0.48f);
        }
    }
}
