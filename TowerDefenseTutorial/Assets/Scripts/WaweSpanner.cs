using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaweSpanner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;
     
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countDown = 2f;
    private int waveNumber = 0;
    public Text countText;

    private void Update()
    {
        if (EnemiesAlive>0)
        {
            return;
        }
        if (countDown<=0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
            return;
        }

        countDown -= Time.deltaTime;

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        countText.text = string.Format("{0:00.00}", countDown);
    }

    IEnumerator SpawnWave()
    {
        
        PlayerStats.rounds++;
        Wave wave = waves[waveNumber];
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate);
        }
        waveNumber++;
        if (waveNumber==waves.Length)
        {
            Debug.Log("LEVEL WON");
            this.enabled = false;
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy,spawnPoint.position,spawnPoint.rotation);
        EnemiesAlive++;
    }
    
    
}
