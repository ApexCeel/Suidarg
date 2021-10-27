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

   private Player _player;
   
   private static readonly int DeathTrigger = Animator.StringToHash("DeathTrigger");

   private void Awake()
   {
      _player = FindObjectOfType<Player>();
      explosionAnim = GetComponentInChildren<Animator>();
      
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
         explosionAnim.SetTrigger(DeathTrigger);
         speed = 0;
         Destroy(gameObject, 0.38f);
         
         // add points
         if (_player != null)
         {
            _player.AddScore(scoreValue);
         }
      }

      else if (other.CompareTag("Player"))
      {
         _player.Damage(damage);
         
         explosionAnim.SetTrigger(DeathTrigger);
         speed = 0;
         Destroy(gameObject, 0.38f);
      }
   }

   private void OnDestroy()
   {
      explosionAnim.ResetTrigger(DeathTrigger);
   }
}
