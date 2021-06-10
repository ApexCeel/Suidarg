using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
   // translate laser up at a predefined speed.

   [SerializeField] private int speed = 10;
   [SerializeField] private int maxDistance = 12;

   private void Update()
   {
      transform.Translate(Vector3.up * (speed *Time.deltaTime));

      if (transform.position.y > maxDistance)
      {
         Destroy(gameObject);
      }
   }
}
