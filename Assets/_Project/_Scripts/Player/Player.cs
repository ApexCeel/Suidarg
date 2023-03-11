using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    [Space] 
    [SerializeField] private int playerScore;
    [Space]
    [Space]
    [SerializeField] private Transform laserPrefab;
    [SerializeField] private Transform waveBeamPrefab;
    [SerializeField] private Transform tripleShotPrefab;
    [SerializeField] private Transform shipShieldPrefab;
    [Space] 
    [Space] 
    [SerializeField] private Transform leftDamageAnim;
    [SerializeField] private Transform rightDamageAnim;
    [Space]
    [SerializeField] private Vector3 laserOffset;
    [Space]
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float nextFire = -1.0f;
    [Space] 
    [Space] 
    [SerializeField] private bool isWaveBeamActive;
    [SerializeField] private bool isTripleShotActive;
    [SerializeField] private bool shieldIsActive;
    [Space] 
    [Space] 
    [SerializeField] private int speed = 25;
    [SerializeField] private float speedBonus = 1;
    [Space] 
    [SerializeField] private float powerUpCooldownTime = 5.0f;
    [Space] 
    [SerializeField] private AudioClip laserClip;

    private AudioSource _audioSource;
    private PlayerMover _playerMover;
    private SpawnManager _spawnManager;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        
        _spawnManager = FindObjectOfType<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Controller is NULL!");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on Player is NULL!");
        }
        else
        {
            _audioSource.clip = laserClip;
        }
    }

    private void Start()
    {
        _playerMover.SetSpeed(speed);
    }

    private void Update()
    {
        // _playerMover.SetSpeed(speed);
        _playerMover.CalculateMovement();
        FireLaser();
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
            
            //Play laser sound effect
            _audioSource.Play();
            
        }
    }

    public void AddScore(int score)
    {
        playerScore += score;
        FindObjectOfType<UIController>().UpdateScore(playerScore);
    }
    public void Damage(int damage)
    {
        // If shields are active
        if (shieldIsActive)
        {
            // do nothing...
            print("Shields took a hit");
            // deactivate shields
            DeactivateShields();
            return;
        }
        
        playerLives -= damage;
        FindObjectOfType<UIController>().UpdateLives(playerLives);
        switch (playerLives)
        {
            case 2:
                leftDamageAnim.gameObject.SetActive(true);
                break;
            case 1:
                rightDamageAnim.gameObject.SetActive(true);
                break;
        }

        if (playerLives <= 0)
        {
            playerLives = 0;
            _spawnManager.OnPlayerDeath();
            
            Destroy(gameObject);
        }
    }
    public void ActivateShields()
    {
        shieldIsActive = true;
        shipShieldPrefab.gameObject.SetActive(true);
        print("Shields are active");
    }
    
    public void ActivateTripleShot()
    {
        isTripleShotActive = true;

        StartCoroutine(PowerDownRoutine());
        Debug.Log("Routine Ended");
    }
    public void ActivateSpeedBoost()
    {
        _playerMover.SpeedBonus = speedBonus;
        StartCoroutine(SpeedDownRoutine());
    }
    private void DeactivateShields()
    {
        shieldIsActive = false;
        shipShieldPrefab.gameObject.SetActive(false);
        print("Shield is deactivated.");
    }
    private IEnumerator SpeedDownRoutine()
    {
        yield return new WaitForSeconds(powerUpCooldownTime);
        _playerMover.SpeedBonus = 1;
    }
    private IEnumerator PowerDownRoutine()
    {
        yield return new WaitForSeconds(powerUpCooldownTime);
        isTripleShotActive = false;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}