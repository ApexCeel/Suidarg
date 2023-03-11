using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
   // translate laser up at a predefined speed.

   [SerializeField] private int speed = 10;
   private float maxDistance;
   [SerializeField] private float range = 12;

   [SerializeField] public bool isEnemyLaser;

   private void Start()
   {
      maxDistance = transform.position.x + range;
   }

   private void Update()
   {
      if (!isEnemyLaser)
      {
         // maxDistance = transform.position.x + 12f;
         MoveRight();
      }
      else
      {
         // maxDistance = transform.position.x + 12f;
         MoveLeft();
      }
      
   }

   private void MoveLeft()
   {
      transform.Translate(Vector3.left * (speed * Time.deltaTime));

      if (transform.position.x < -maxDistance)
      {
         if (transform.parent != null)
         {
            Destroy(transform.parent.gameObject);
         }

         Destroy(gameObject);
      }
   }

   private void MoveRight()
   {
      transform.Translate(Vector3.right * (speed * Time.deltaTime));

      if (transform.position.x > maxDistance)
      {
         if (transform.parent != null)
         {
            Destroy(transform.parent.gameObject);
         }

         Destroy(gameObject);
      }
   }
}
