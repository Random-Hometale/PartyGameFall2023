using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float xLimit;

    [SerializeField]
    private float[] xPosition;

    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private Wave[] wave;

    private float currentTime;

    List<float> remainingPositions = new List<float>();
    private int waveIndex;
    float xPos = 0;
    int rand;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        remainingPositions.AddRange(xPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController1.instance.StartMoving == true)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                SelectWave();
            }
        }
    }

    void SpawnEnemy(float xPos)
    {
        int r = Random.Range(0, 2);
        string enemyName = "";
        if (r == 0) enemyName = "Spider1";
        else if (r == 1) enemyName = "Spider2";

        GameObject enemy = ObjectPooling.instance.GetPooledObject(enemyName);
        enemy.transform.position = new Vector3(xPos, transform.position.y, 0);
        enemy.SetActive(true);
    }

    void SelectWave()
    {
        remainingPositions = new List<float>();
        remainingPositions.AddRange(xPosition);

        waveIndex = Random.Range(0, wave.Length);

        currentTime = wave[waveIndex].delayTime;

        if(wave[waveIndex].spawnAmount == 1)
            xPos = Random.Range(-xLimit, xLimit);
        else if(wave[waveIndex].spawnAmount > 1)
        {
            rand = Random.Range(0, remainingPositions.Count);
            xPos = remainingPositions[rand];
            remainingPositions.RemoveAt(rand);
        }

        for (int i = 0; i < wave[waveIndex].spawnAmount; i++)
        {
            SpawnEnemy(xPos);
            rand = Random.Range(0, remainingPositions.Count);
            xPos = remainingPositions[rand];
            remainingPositions.RemoveAt(rand);
        }
    }
}

[System.Serializable]
public class Wave
{
    public float delayTime;
    public float spawnAmount;
}
