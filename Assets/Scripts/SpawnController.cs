using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
   [SerializeField] private GameObject powerUpPrefab;
   [SerializeField] private GameObject enemyPrefab;
   [Space] 
   [SerializeField] private float powerUpWaitTime;
   [SerializeField] private float waitTime;
   [Space] 
   [SerializeField] private Transform enemyContainer;

   private bool _stopSpawning;
   
   

   private void Start()
   {
      StartCoroutine(SpawnRoutine());

      StartCoroutine(PowerUpSpawnRoutine());
   }

   private IEnumerator SpawnRoutine()
   {
      while (!_stopSpawning)
      {
         var posOffset = new Vector3(0, Random.Range(-5.0f, 8.0f));
         var enemy = Instantiate(enemyPrefab, transform.position + posOffset, Quaternion.identity);
         enemy.transform.parent = enemyContainer;
         yield return new WaitForSeconds(waitTime);
      }
   }

   private IEnumerator PowerUpSpawnRoutine()
   {
      while (!_stopSpawning)
      {
         var posOffset = new Vector3(Random.Range(-14.0f, 14.0f), 9.0f);
         var powerUp = Instantiate(powerUpPrefab, posOffset, quaternion.identity);

         yield return new WaitForSeconds(powerUpWaitTime);
      }
   }

   public void OnPlayerDeath()
   {
      _stopSpawning = true;
   }
}
