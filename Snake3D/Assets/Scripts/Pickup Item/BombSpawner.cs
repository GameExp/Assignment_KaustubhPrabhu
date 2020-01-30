using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake3D
{
    public class BombSpawner : MonoBehaviour
    {

        #region Variables

        public static BombSpawner bombSpawner;

        public GameObject[] bombPrefabs;

        public float bombSelfDestroyTime;
        public float bombSpawnWaitTime;
        private float bombSpawnCountDown;

        public float posRange = 4.5f;
        public float yPos = 0.276f;

        #endregion

        #region Builin Methods

        void Awake()
        {
            // implement singlton
            if (bombSpawner == null)
            {
                DontDestroyOnLoad(this);
                bombSpawner = this;
            }
            else if (bombSpawner != this)
            {
                Destroy(gameObject);
                return;
            }

            bombSpawnCountDown = bombSpawnWaitTime;
        }

        void Update()
        {
            if(bombSpawnCountDown <= 0)
            {
                bombSpawnCountDown = bombSpawnWaitTime;
                SpawnBomb();
            }
            else
            {
                bombSpawnCountDown -= Time.deltaTime;
            }
        }

        #endregion

        #region Custom Methods

        void SpawnBomb()
        {
            Vector3 position = GetRandomPos();

            if(position == FruitSpawner.fruitSpawner.SpawnPos)
            {
                GetRandomPos();
            }
            else
            {
                GameObject bomb = Instantiate(bombPrefabs[Random.Range(0, bombPrefabs.Length)], position, Quaternion.identity, transform) as GameObject;
                Destroy(bomb, bombSelfDestroyTime);
            }
        }

        Vector3 GetRandomPos()
        {
            Vector3 spawnPos = new Vector3();
            spawnPos.x = Random.Range(-posRange, posRange);
            spawnPos.y = yPos;
            spawnPos.z = Random.Range(-posRange, posRange);

            return spawnPos;
        }

        #endregion

    }
}