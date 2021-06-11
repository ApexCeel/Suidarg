using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
   [SerializeField] private GameObject enemyPrefab;
   [SerializeField] private float waitTime;
   [Space] [SerializeField] private Transform enemyContainer;

   private bool _stopSpawning;
   

   private void Start()
   {
      StartCoroutine(SpawnRoutine());
   }

   private IEnumerator SpawnRoutine()
   {
      while (!_stopSpawning)
      {
         var posOffset = new Vector3(Random.Range(-9.0f, 9.0f), 0);
         var enemy = Instantiate(enemyPrefab, transform.position + posOffset, Quaternion.identity);
         enemy.transform.parent = enemyContainer;
         yield return new WaitForSeconds(waitTime);
      }
   }

   public void OnPlayerDeath()
   {
      _stopSpawning = true;
   }
}
