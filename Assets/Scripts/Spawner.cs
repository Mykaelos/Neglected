using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject EnemyPrefab;

    public Rect SpawnArea = new Rect();

    public int EnemiesMax = 100;
    public float SpawnRate = 5;
    private Timer SpawnTimer;


    void Awake() {
        SpawnTimer = new Timer();
    }

    void Update() {
        if (transform.childCount < EnemiesMax && SpawnTimer.Check(SpawnRate)) {
            SpawnTimer.Reset();

            Spawn();
        }

    }

    void Spawn() {
        GameObject enemy = GameObject.Instantiate<GameObject>(EnemyPrefab, SpawnArea.RandomPoint(), Quaternion.identity);
        enemy.transform.SetParent(transform, true);
    }
}
