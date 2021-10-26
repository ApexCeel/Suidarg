using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
   [SerializeField] private GameObject[] powerUps;
   [SerializeField] private GameObject enemyPrefab;
   [Space] 
   [SerializeField] private float powerUpSpawnDelay;
   [SerializeField] private float enemySpawnDelay;
   [Space] 
   [SerializeField] private Transform enemyContainer;

   private bool _stopSpawning;
   
   

   private void Start()
   {
      StartCoroutine(EnemySpawnRoutine());

      StartCoroutine(PowerUpSpawnRoutine());
   }

   private IEnumerator EnemySpawnRoutine()
   {
      while (!_stopSpawning)
      {
         var posOffset = new Vector3(0, Random.Range(-5.0f, 8.0f));
         var enemy = Instantiate(enemyPrefab, transform.position + posOffset, Quaternion.identity);
         enemy.transform.parent = enemyContainer;
         yield return new WaitForSeconds(enemySpawnDelay);
      }
   }

   private IEnumerator PowerUpSpawnRoutine()
   {
      while (!_stopSpawning)
      {
         yield return new WaitForSeconds(powerUpSpawnDelay);
         
         var spawnPosition = new Vector2(Random.Range(-14.0f, 14.0f), 9.0f);
         var randPowerUp = Random.Range(0, 3);
         Instantiate(powerUps[randPowerUp], spawnPosition, quaternion.identity);
      }
   }

   public void OnPlayerDeath()
   {
      _stopSpawning = true;
      
   }
}
