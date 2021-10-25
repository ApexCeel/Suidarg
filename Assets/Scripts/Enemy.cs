using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
   
   [SerializeField] private int speed = 4;
   [SerializeField] private int damage = 1;
    


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
         // print("Hit by a laser");
         print("Add point to playerPoints");
         Destroy(other.gameObject);
         Destroy(gameObject);
      }

      else if (other.CompareTag("Player"))
      {
         // print("Hit the Player");
         // other.GetComponent<Player>().Damage(damage);
         if(other.TryGetComponent(out Player player))
         {
            player.Damage(damage);
         }
         Destroy(gameObject);
      }
   }
}
