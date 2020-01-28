using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake3D
{
    public class FruitSpawner : MonoBehaviour
    {

        #region Enums

        public enum SpawnState { SPAWN, WAIT };

        #endregion

        #region Variables

        public static FruitSpawner spawner;

        public int fruitTypeCount;
        public GameObject fruitPrefab;
        
        public SpawnState state = SpawnState.SPAWN;

        public float range = 4.5f;
        public float yPos = 0.276f;

        #endregion

        #region Builtin Methods

        void Awake()
        {
            // implement singlton
            if (spawner == null)
            {
                DontDestroyOnLoad(this);
                spawner = this;
            }
            else if (spawner != this)
            {
                Destroy(gameObject);
                return;
            }

            //spawningFruits = new GameObject[];
        }

        void Update()
        {
            if(state == SpawnState.SPAWN)
            {
                state = SpawnState.WAIT;
                SpawnFruit(Random.Range(0, fruitTypeCount));
            }
        }

        #endregion

        #region Custom Methods

        void SpawnFruit(int spawnFruitIndex)
        {
            Debug.Log("Spawning Fruit" + spawnFruitIndex);

            Vector3 spawnPos = new Vector3();
            spawnPos.x = Random.Range(-range, range);
            spawnPos.y = yPos;
            spawnPos.z = Random.Range(-range, range);

            Instantiate(fruitPrefab, spawnPos, Quaternion.identity, transform);
            fruitPrefab.GetComponent<Renderer>().sharedMaterial.color = GameMaster.gameMaster.fruitColorList[spawnFruitIndex];
            fruitPrefab.GetComponent<Fruit>().fruitColor = GameMaster.gameMaster.fruitColorList[spawnFruitIndex];
            fruitPrefab.GetComponent<Fruit>().pointsToAdd = GameMaster.gameMaster.pointsToAddList[spawnFruitIndex];
        }

        #endregion

    }
}