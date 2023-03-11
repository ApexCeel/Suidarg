using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
   [SerializeField] private bool linearMovement;
   [SerializeField] private bool sineWaveMovement;
   [SerializeField] private bool circularMovement;
   [Space] [SerializeField] private float amplitude;
   [SerializeField] private float frequency;
   [Space]
   [SerializeField] private int speed = 4;
   
   [SerializeField] private int damage = 1;
   [SerializeField] private int scoreValue = 5;
   [Space]
   [SerializeField] private Animator explosionAnim;
   [Space] 
   [SerializeField] private AudioClip explosionClip;
   [Space] 
   [SerializeField] private GameObject laserPrefab;
   [SerializeField] private Vector3 laserOffset;
   [SerializeField] private float enemyRateOfFire = 1.0f;

   private AudioSource _audioSource;
   private Player _player;
   private static readonly int DeathTrigger = Animator.StringToHash("DeathTrigger");
   

   private void Awake()
   {
      _player = FindObjectOfType<Player>();
      explosionAnim = GetComponentInChildren<Animator>();

      _audioSource = GetComponent<AudioSource>();
      if (_audioSource == null)
      {
         Debug.LogError("Audio Source on Enemy is NULL!");
      }
      else
      {
         _audioSource.clip = explosionClip;
      }

   }

   private void Start()
   {
      StartCoroutine(FireLaserRoutine());
   }

   private void Update()
   {
      Movement();
      
      // if (transform.position.x < -14.0f)
      // {
      //    ResetToStart();
      // }
      // else
      // {
      //    Movement();
      // }
   }

   private IEnumerator FireLaserRoutine()
   {
      while (true)
      {
         Instantiate(laserPrefab, transform.position + laserOffset, Quaternion.identity);
         yield return new WaitForSeconds(enemyRateOfFire);
      }
   }

   private void Movement()
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

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Laser"))
      {
         // if (other.gameObject.TryGetComponent(out Laser laser))
         // {
         //    
         //    if (laser.isEnemyLaser)
         //    {
         //       return;
         //    }
         // }
         // This is the laser hitting the enemy
         Debug.Log("HIT HIT HIT");
         Destroy(other.gameObject);
         _audioSource.Play();
         explosionAnim.SetTrigger(DeathTrigger);
         speed = 0;
         Destroy(GetComponent<Collider2D>());
         Destroy(gameObject, 0.42f);
         
         // add points
         if (_player != null)
         {
            _player.AddScore(scoreValue);
         }
      }
      
      // this is the enemy colliding with the player
      else if (other.CompareTag("Player"))
      {
         Debug.Log("Collided with player SHIP");
         _player.Damage(damage);
         _audioSource.Play();
         explosionAnim.SetTrigger(DeathTrigger);
         speed = 0;
         Destroy(gameObject, 0.42f);
      }
   }

   private void OnDestroy()
   {
      explosionAnim.ResetTrigger(DeathTrigger);
   }
}
