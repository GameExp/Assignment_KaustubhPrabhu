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

        [Header("Fruit Attributes")]

        [SerializeField]
        private GameObject fruitPrefab = null;
        public SpawnState state = SpawnState.SPAWN;

        private int fruitTypeCount;
        private Vector3 spawnPos = new Vector3();

        [Header("Position Offset")]
        [SerializeField]
        private float posRange = 4.5f;
        [SerializeField]
        private float yPos = 0.276f;

        #endregion

        #region Properties

        public Vector3 SpawnPos { get { return spawnPos; } }
        public int FruitTypeCount { get => fruitTypeCount; set => fruitTypeCount = value; }
        public float PosRange { get => posRange; set => posRange = value; }
        public float YPos { get => yPos; set => yPos = value; }

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
        }

        void Update()
        {
            if(state == SpawnState.SPAWN)
            {
                state = SpawnState.WAIT;
                SpawnFruit(Random.Range(0, FruitTypeCount));
            }
        }

        #endregion

        #region Custom Methods

        private void SpawnFruit(int spawnFruitIndex)
        {
            // get a random position to spawn within range
            spawnPos.x = Random.Range(-PosRange, PosRange);
            spawnPos.y = YPos;
            spawnPos.z = Random.Range(-PosRange, PosRange);

            // spawn and init a fruit
            Instantiate(fruitPrefab, spawnPos, Quaternion.identity, transform);
            fruitPrefab.GetComponent<Renderer>().sharedMaterial.color = GameMaster.gameMaster.fruitColorList[spawnFruitIndex];
            fruitPrefab.GetComponent<Fruit>().FruitColor = GameMaster.gameMaster.fruitColorList[spawnFruitIndex];
            fruitPrefab.GetComponent<Fruit>().pointsToAdd = GameMaster.gameMaster.pointsToAddList[spawnFruitIndex];
        }

        #endregion

    }
}