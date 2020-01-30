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

        // Growth increase
        [SerializeField]
        private GameObject tailPrefab = null;

        private bool createNodeAtTail;
        private static List<Rigidbody> nodes;
        private bool isAlive;

        // player input
        private int horizontal = 0;
        private int vertical = 0;

        #endregion

        #region Cached Variables

        private PlayerMovementHandler movement;
        private PlayerInputManager inputManager;
        private PlayerBodyGrowthOperator body;

        #endregion

        #region Properties

        public static List<Rigidbody> Nodes { get => nodes; set => nodes = value; }

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


            // check if we eaten a fruit and if yes then create a new node
            if (createNodeAtTail)
            {
                createNodeAtTail = false;
                if (body == null)
                    return;

                // add a new tail to the snake
                List<Rigidbody> tempNodes = new List<Rigidbody>();

                tempNodes = body.AddTail(tailPrefab, Nodes);
                Nodes = tempNodes;

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
                GameObject fruitItem = otherCollider.gameObject;
                if(fruitItem == null)
                {
                    Debug.Log("Wrong assignment of FRUIT Tag on collider");
                    return;
                }

                // play fruit pick up SFX
                AudioManager.audioManager.CollectFruitSFX();

                // get the color of the fruit and pointsToAdd
                Renderer fruitRenderer = fruitItem.GetComponent<Renderer>();
                Color fruitColor = fruitRenderer.material.color;
                Fruit fruit = fruitItem.GetComponent<Fruit>();

                GameMaster.gameMaster.AddToScore(fruitColor, fruit.PointsToAdd);

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
            Nodes = new List<Rigidbody>();

            Nodes.Add(transform.GetChild(0).GetComponent<Rigidbody>());
            Nodes.Add(transform.GetChild(1).GetComponent<Rigidbody>());
            Nodes.Add(transform.GetChild(2).GetComponent<Rigidbody>());

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

            if (_dir == PlayerDirection.LEFT && movement.Direction == PlayerDirection.RIGHT ||
                _dir == PlayerDirection.UP && movement.Direction == PlayerDirection.DOWN ||
                _dir == PlayerDirection.RIGHT && movement.Direction == PlayerDirection.LEFT ||
                _dir == PlayerDirection.DOWN && movement.Direction == PlayerDirection.UP)
                return;

            movement.Direction = _dir;

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
            inputManager.InverseControls = -1;

            StartCoroutine("WaitAndChange");
        }

        IEnumerator WaitAndChange()
        {
            yield return new WaitForSeconds(inputManager.inverseControlsForTime);
            inputManager.InverseControls = 1;
        }

        #endregion

    }
}