using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
   
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
      
      if (transform.position.x < -14.0f)
      {
         ResetToStart();
      }
      else
      {
         Movement();
      }
   }

   private IEnumerator FireLaserRoutine()
   {
      while (true)
      {
         Instantiate(laserPrefab, transform.position + laserOffset, Quaternion.identity);
         yield return new WaitForSeconds(3.0f);
      }
   }

   private void Movement()
   {
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

      else if (other.CompareTag("Player"))
      {
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
