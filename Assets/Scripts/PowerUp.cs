using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
  [SerializeField] private float speed;
  [SerializeField] private int powerUpID;
  
  // Id for powerups
  // 0 = Triple shot
  // 1 = Speed
  // 2 = Shield

  private void Update()
  {
    transform.Translate(Vector3.down * (speed * Time.deltaTime));

    if (transform.position.y < -9)
    {
      Destroy(gameObject);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    // print("Trigger " + other.gameObject);
    
    if (other.CompareTag("Player"))
    {
      if (other.TryGetComponent(out Player player))
      {
        switch (powerUpID)
        {
          case 0:
            print("Collected triple shot power up");
            player.ActivateTripleShot();
            break;
          case 1:
            print("Collected speed power up.");
            player.ActivateSpeedBoost();
            break;
          case 2:
            print("Collected shield power up");
            break;
        }
        
      }
      Destroy(gameObject);
    }
  }
}
