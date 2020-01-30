using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake3D
{
    [RequireComponent(typeof(PlayerMovementHandler))]
    [RequireComponent(typeof(PlayerInputManager))]
    [RequireComponent(typeof(PlayerBodyGrowthOperator))]
    public class PlayerController : MonoBehaviour
    {

        #region Variables

        [Header("Movement")]
        private int horizontal = 0;
        private int vertical = 0;

        // Growth increase
        [SerializeField]
        private GameObject tailPrefab;
        private bool createNodeAtTail;

        public static List<Rigidbody> nodes;
        public bool isAlive;

        #endregion

        #region Cached Variables

        private PlayerMovementHandler movement;
        private PlayerInputManager inputManager;
        private PlayerBodyGrowthOperator body;

        #endregion

        #region Builtin Methods

        void Awake()
        {
            isAlive = true;

            movement = GetComponent<PlayerMovementHandler>();
            inputManager = GetComponent<PlayerInputManager>();
            body = GetComponent<PlayerBodyGrowthOperator>();

            InitSnakeNodes();

            if (movement == null)
                return;

            movement.InitPlayer();
        }

        void Update()
        {
            if (!isAlive)
                return;

            if (inputManager == null)
                return;

            GetInput();


            // check if we eaten a food and create a new node
            if (createNodeAtTail)
            {
                createNodeAtTail = false;
                if (body == null)
                    return;

                // add a new tail to the snake
                List<Rigidbody> tempNodes = new List<Rigidbody>();

                tempNodes = body.AddTail(tailPrefab, nodes);
                nodes = tempNodes;

            }
        }

        void FixedUpdate()
        {
            if (!isAlive)
                return;

            if (movement == null)
                return;

            movement.CanMove();
        }

        void OnTriggerEnter(Collider otherCollider)
        {
            string colliderTag = otherCollider.tag;

            if(colliderTag.Equals(Tags.FRUIT))
            {
                Debug.Log("Ate a fruit");

                GameObject fruitItem = otherCollider.gameObject;
                if(fruitItem == null)
                {
                    Debug.LogError("Wrong assignment of FRUIT Tag on collider");
                    return;
                }

                // play fruit pick up SFX
                AudioManager.audioManager.CollectFruitSFX();


                // get the color of the fruit and pointsToAdd
                Renderer fruitRenderer = fruitItem.GetComponent<Renderer>();
                Color fruitColor = fruitRenderer.material.color;
                Fruit fruit = fruitItem.GetComponent<Fruit>();

                GameMaster.gameMaster.AddToScore(fruitColor, fruit.pointsToAdd);

                // Increase the size of the snake
                createNodeAtTail = true;

                // Disable and destroy the fruit gameObject
                DisablePickupItem(otherCollider.gameObject);


                // spawn a new fruit
                FruitSpawner.fruitSpawner.state = FruitSpawner.SpawnState.SPAWN;
            }

            if(colliderTag.Equals(Tags.WALL) || colliderTag.Equals(Tags.TAIL))
            {
                AudioManager.audioManager.CollideWithWallSFX();
                isAlive = false;
                GameMaster.gameMaster.HandleLoseCondition();
            }

            if(colliderTag.Equals(Tags.BOMB))
            {
                otherCollider.gameObject.SetActive(false);
                AudioManager.audioManager.CollectBombSFX();
                isAlive = false;
                GameMaster.gameMaster.HandleLoseCondition();
            }

            if(colliderTag.Equals(Tags.INSVERSE))
            {
                otherCollider.gameObject.SetActive(false);
                AudioManager.audioManager.CollectBombSFX();

                InverseControls();
            }
        }

        #endregion

        #region Custom Methods

        private void InitSnakeNodes()
        {
            // get all the rigidbodies for inital 3 nodes
            nodes = new List<Rigidbody>();

            nodes.Add(transform.GetChild(0).GetComponent<Rigidbody>());
            nodes.Add(transform.GetChild(1).GetComponent<Rigidbody>());
            nodes.Add(transform.GetChild(2).GetComponent<Rigidbody>());

        }

        private void GetInput()
        {
            horizontal = 0;
            vertical = 0;
            inputManager.GetKeyboardInput(out horizontal, out vertical);

            SetMovement();
        }

        private void SetMovement()
        {
            if (vertical != 0)
            {
                SetInputDirection((vertical == 1) ? PlayerDirection.UP : PlayerDirection.DOWN);
            }
            else if (horizontal != 0)
            {
                SetInputDirection((horizontal == 1) ? PlayerDirection.RIGHT : PlayerDirection.LEFT);
            }
        }

        private void SetInputDirection(PlayerDirection _dir)
        {
            if (movement == null)
                return;

            if (_dir == PlayerDirection.LEFT && movement.direction == PlayerDirection.RIGHT ||
                _dir == PlayerDirection.UP && movement.direction == PlayerDirection.DOWN ||
                _dir == PlayerDirection.RIGHT && movement.direction == PlayerDirection.LEFT ||
                _dir == PlayerDirection.DOWN && movement.direction == PlayerDirection.UP)
                return;

            movement.direction = _dir;

            movement.ForceMove();
        }

        private void DisablePickupItem(GameObject _pickupItem)
        {
            _pickupItem.SetActive(false);
            Destroy(_pickupItem, 3f);
        }

        void InverseControls()
        {
            Debug.Log("Reverse the controls");
            inputManager.inverseControls = -1;

            StartCoroutine("WaitAndChange");
        }

        IEnumerator WaitAndChange()
        {
            yield return new WaitForSeconds(inputManager.inverseControlsForTime);
            inputManager.inverseControls = 1;
        }

        #endregion

    }
}