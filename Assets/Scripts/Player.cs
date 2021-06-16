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
    [SerializeField] private Transform waveBeamPrefab;
    [SerializeField] private Transform tripleShotPrefab;
    [Space]
    [SerializeField] private Vector3 laserOffset;
    [Space]
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float nextFire = -1.0f;
    [Space] 
    [SerializeField] private bool isWaveBeamActive;
    [SerializeField] private bool isTripleShotActive;
    [Space]
    [SerializeField] private float cooldownTime = 5.0f;

    private float _horizontalInput;
    private float _verticalInput;
    private SpawnController _spawnController;
    private Transform _pTransform;
    

    private void Awake()
    {
        _pTransform = transform;
        
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

        _pTransform.Translate(direction * (speed * Time.deltaTime));

        var transPos = _pTransform.position;
        transPos = new Vector3(Mathf.Clamp(transPos.x, -xBoundary, xBoundary), 
                               Mathf.Clamp(transPos.y, -yBoundary, yBoundary), 
                               transPos.z);
        transform.position = transPos;

    }
    private void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (isWaveBeamActive)
            {
                Instantiate(waveBeamPrefab, transform.position + laserOffset, Quaternion.identity);
            }
            else if (isTripleShotActive)
            {
                Instantiate(tripleShotPrefab, transform.position + laserOffset, Quaternion.identity);
            }
            else
            {
                Instantiate(laserPrefab, transform.position + laserOffset, Quaternion.identity);
            }
        }
    }
    private IEnumerator PowerDownRoutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        isTripleShotActive = false;
        
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
    public void ActivateTripleShot()
    {
        isTripleShotActive = true;

        StartCoroutine(PowerDownRoutine());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
