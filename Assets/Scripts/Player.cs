using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    [SerializeField] private int speed = 5;
    [Space] 
    [SerializeField] private int playerScore;
    [Space]
    [SerializeField] private float xBoundary = 14.0f;
    [SerializeField] private float yBoundary = 9.0f;
    [Space]
    [SerializeField] private Transform laserPrefab;
    [SerializeField] private Transform waveBeamPrefab;
    [SerializeField] private Transform tripleShotPrefab;
    [SerializeField] private Transform shipShieldPrefab;
    [Space] [SerializeField] private Transform leftDamageAnim;
    [SerializeField] private Transform rightDamageAnim;
    [Space]
    [SerializeField] private Vector3 laserOffset;
    [Space]
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float nextFire = -1.0f;
    [Space] 
    [SerializeField] private bool isWaveBeamActive;
    [SerializeField] private bool isTripleShotActive;
    [SerializeField] private bool isShieldActive;
    [SerializeField] private float speedBonus = 1;
    [Space]
    [SerializeField] private float powerUpCooldownTime = 5.0f;

    private float _horizontalInput;
    private float _verticalInput;
    private float _speedBonus = 1;
    private SpawnManager _spawnManager;
    private Transform _pTransform;
    

    private void Awake()
    {
        _pTransform = transform;
        
        _spawnManager = FindObjectOfType<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Controller is NULL!");
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

        _pTransform.Translate(direction * (speed * Time.deltaTime * _speedBonus));

        var transPos = _pTransform.position;
        transPos = new Vector3(Mathf.Clamp(transPos.x, -xBoundary, xBoundary), 
                               Mathf.Clamp(transPos.y, -yBoundary + 4, yBoundary), // Remove magic number
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
        yield return new WaitForSeconds(powerUpCooldownTime);
        isTripleShotActive = false;
        
    }
    private IEnumerator SpeedDownRoutine()
    {
        yield return new WaitForSeconds(powerUpCooldownTime);
        _speedBonus = 1;
    }

    public void AddScore(int score)
    {
        playerScore += score;
        FindObjectOfType<UIController>().UpdateScore(playerScore);
    }
    public void Damage(int damage)
    {
        // If shields are active
        if (isShieldActive)
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
        isShieldActive = true;
        shipShieldPrefab.gameObject.SetActive(true);
        print("Shields are active");
    }
    private void DeactivateShields()
    {
        isShieldActive = false;
        shipShieldPrefab.gameObject.SetActive(false);
        print("Shield is deactivated.");
    }
    public void ActivateTripleShot()
    {
        isTripleShotActive = true;

        StartCoroutine(PowerDownRoutine());
    }
    public void ActivateSpeedBoost()
    {
        _speedBonus = speedBonus;
        StartCoroutine(SpeedDownRoutine());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
