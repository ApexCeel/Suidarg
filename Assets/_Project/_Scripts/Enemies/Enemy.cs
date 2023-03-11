using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

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

   private EnemyMover _enemyMover;
   private AudioSource _audioSource;
   private Player _player;
   private static readonly int DeathTrigger = Animator.StringToHash("DeathTrigger");
   

   private void Awake()
   {
      _player = FindObjectOfType<Player>();
      explosionAnim = GetComponentInChildren<Animator>();
      _enemyMover = GetComponent<EnemyMover>();

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
      _enemyMover.Movement();
     
   }

   private IEnumerator FireLaserRoutine()
   {
      while (true)
      {
         Instantiate(laserPrefab, transform.position + laserOffset, Quaternion.identity);
         yield return new WaitForSeconds(enemyRateOfFire);
      }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Laser"))
      {
         
         // This is the laser hitting the enemy
         Debug.Log("HIT HIT HIT");
         Destroy(other.gameObject);
         Die();

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
         Die();
         // _audioSource.Play();
         // explosionAnim.SetTrigger(DeathTrigger);
         // _enemyMover.SetEnemySpeed(0);
         // Destroy(gameObject, 0.42f);
      }
   }

   private void Die()
   {
      _audioSource.Play();
      explosionAnim.SetTrigger(DeathTrigger);
      _enemyMover.SetEnemySpeed(0);
      Destroy(GetComponent<Collider2D>());
      Destroy(gameObject, 0.42f);
   }

   private void OnDestroy()
   {
      explosionAnim.ResetTrigger(DeathTrigger);
   }
}
