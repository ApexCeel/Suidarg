using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
   // translate laser up at a predefined speed.

   [SerializeField] private int speed = 10;
   [SerializeField] private int maxDistance = 12;

   [SerializeField] private bool isEnemyLaser;

   private void Update()
   {
      if (!isEnemyLaser)
      {
         MoveRight();
      }
      else
      {
         MoveLeft();
      }
   }

   private void MoveLeft()
   {
      transform.Translate(Vector3.left * (speed * Time.deltaTime));

      if (transform.position.x > maxDistance)
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
