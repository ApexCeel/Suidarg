using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
   // translate laser up at a predefined speed.

   [SerializeField] private int speed = 10;
   private float _maxDistance;
   [SerializeField] private float range = 12;

   [SerializeField] public bool isEnemyLaser;

   private void Start()
   {
      
      if (!isEnemyLaser)
      {
         _maxDistance = transform.position.x + range;
      }
      else
      {
         _maxDistance = (transform.position.x - range);
      }
   }

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

      if (transform.position.x < _maxDistance - 0.1f)
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

      if (transform.position.x > _maxDistance)
      {
         if (transform.parent != null)
         {
            Destroy(transform.parent.gameObject);
         }

         Destroy(gameObject);
      }
   }
}
