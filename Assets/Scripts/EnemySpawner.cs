using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour


{

    [SerializeField] List<WaveConfig> waveConfig;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
        
    }
    private IEnumerator SpawnAllWaves()
    {

        for (int i =startingWave; i< waveConfig.Count; i++)
        {
            var currentWave = waveConfig[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }

    }
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {

        for (int i = 0; i < waveConfig.getNumberofEnemies(); i++)
        {
           var newEnemy = Instantiate(
                waveConfig.getEnemyPrefab(),
                waveConfig.getWayPoints()[0].transform.position,
                Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.getTimeBetweenSpawns());
            
        }
    }
}
