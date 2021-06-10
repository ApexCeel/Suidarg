using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerLives = 5;
    [SerializeField] private int speed = 5;
    [Space]
    [SerializeField] private float xBoundary = 10.1f;
    [SerializeField] private float yBoundary = 4.0f;
    [Space]
    [SerializeField] private Transform laserPrefab;
    [Space]
    [SerializeField] private Vector3 laserOffset;
    [Space]
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float nextFire = -1.0f;

    private float _horizontalInput;
    private float _verticalInput;
    private SpawnController _spawnController;

    private void Awake()
    {
        _spawnController = FindObjectOfType<SpawnController>();
        if (_spawnController == null)
        {
            Debug.LogWarning("Spawn Controller is NULL!");
        }
    }

    private void Start()
    {
        transform.position = new Vector3(0, -3.0f);
    }
    private void Update()
    {
        CalculateMovement();
        FireLaser();
    }
    private void CalculateMovement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        var direction = new Vector3(_horizontalInput, _verticalInput, 0);

        transform.Translate(direction * (speed * Time.deltaTime));

        var transPos = transform.position;
        transPos = new Vector3(transPos.x, Mathf.Clamp(transPos.y, -yBoundary, yBoundary), transPos.z);
        transform.position = transPos;

        if (transform.position.x > xBoundary)
        {
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -xBoundary)
        {
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
        }
    }
    private void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(laserPrefab, transform.position + laserOffset, Quaternion.identity);
        }
    }
    public void Damage(int damage)
    {
        playerLives -= damage;

        if (playerLives <= 0)
        {
            playerLives = 0;
            _spawnController.OnPlayerDeath();
            Destroy(gameObject);
        }
    }
}
