using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
  [SerializeField] private float speed;

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
        player.ActivateTripleShot();
      }
      Destroy(gameObject);
    }
  }
}
