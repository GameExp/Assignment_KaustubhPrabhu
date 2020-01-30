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

        public static FruitSpawner fruitSpawner;

        public int fruitTypeCount;
        public GameObject fruitPrefab;
        
        public SpawnState state = SpawnState.SPAWN;

        public float posRange = 4.5f;
        public float yPos = 0.276f;

        private Vector3 spawnPos = new Vector3();

        public Vector3 SpawnPos { get { return spawnPos; } }

        #endregion

        #region Builtin Methods

        void Awake()
        {
            // implement singlton
            if (fruitSpawner == null)
            {
                DontDestroyOnLoad(this);
                fruitSpawner = this;
            }
            else if (fruitSpawner != this)
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

            
            spawnPos.x = Random.Range(-posRange, posRange);
            spawnPos.y = yPos;
            spawnPos.z = Random.Range(-posRange, posRange);

            Instantiate(fruitPrefab, spawnPos, Quaternion.identity, transform);
            fruitPrefab.GetComponent<Renderer>().sharedMaterial.color = GameMaster.gameMaster.fruitColorList[spawnFruitIndex];
            fruitPrefab.GetComponent<Fruit>().fruitColor = GameMaster.gameMaster.fruitColorList[spawnFruitIndex];
            fruitPrefab.GetComponent<Fruit>().pointsToAdd = GameMaster.gameMaster.pointsToAddList[spawnFruitIndex];
        }

        #endregion

    }
}