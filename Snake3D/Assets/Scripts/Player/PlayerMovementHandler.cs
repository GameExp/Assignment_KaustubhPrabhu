using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake3D
{
    public class PlayerMovementHandler : MonoBehaviour
    {

        #region Variables

        public PlayerDirection direction;
        public float stepLength = 0.2f;
        public float moveFrequency = 0.1f;

        private float countDown;
        private bool createNodeAtTail;
        private List<Vector3> deltaPositions;

        // rigidbodies
        private List<Rigidbody> nodes;
        private Rigidbody mainBody;
        private Rigidbody headBody;

        #endregion

        #region Builtin Methods

        void Awake()
        {
            mainBody = GetComponent<Rigidbody>();

            InitSnakeNodes();
            InitPlayer();

            SetDeltaPositions();
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

            headBody = nodes[0];
        }

        private void InitPlayer()
        {
            // set a random intial direction
            SetRandomDirection();
            // based on the direction change the position of 2 tailing objects respectively
            switch (direction)
            {
                case PlayerDirection.LEFT:

                    nodes[1].position = nodes[0].position + new Vector3(Metrics.NODEOFFSET, 0f, 0f);
                    nodes[2].position = nodes[0].position + new Vector3(Metrics.NODEOFFSET * 2f, 0f, 0f);

                    break;

                case PlayerDirection.UP:

                    nodes[1].position = nodes[0].position - new Vector3(0f, 0f, Metrics.NODEOFFSET);
                    nodes[2].position = nodes[0].position - new Vector3(0f, 0f, Metrics.NODEOFFSET * 2f);

                    break;

                case PlayerDirection.RIGHT:

                    nodes[1].position = nodes[0].position - new Vector3(Metrics.NODEOFFSET, 0f, 0f);
                    nodes[2].position = nodes[0].position - new Vector3(Metrics.NODEOFFSET * 2f, 0f, 0f);

                    break;

                case PlayerDirection.DOWN:

                    nodes[1].position = nodes[0].position + new Vector3(0f, 0f, Metrics.NODEOFFSET);
                    nodes[2].position = nodes[0].position + new Vector3(0f, 0f, Metrics.NODEOFFSET * 2f);

                    break;
            }
        }

        private void SetRandomDirection()
        {
            int randomDir = Random.Range(0, (int)PlayerDirection.COUNT);
            direction = (PlayerDirection)randomDir;
        }

        private void SetDeltaPositions()
        {
            // Vector3 position to move by in each direction
            deltaPositions = new List<Vector3>()
            {
                new Vector3 (-stepLength, 0f, 0f),   // -dx => LEFT
                new Vector3 (0f, 0f, stepLength),   //  dy => UP
                new Vector3 (stepLength, 0f, 0f),  //  dx => RIGHT
                new Vector3 (0f, 0f, -stepLength) // -dy => DOWN
            };
        }

        public void CanMove()
        {
            // call the move method at moveFrequency intervals
            countDown += Time.deltaTime;

            if (countDown >= moveFrequency)
            {
                countDown = 0f;
                Move();
            }
        }

        private void Move()
        {
            // get the delta position Vector3 to move by in this call
            Vector3 dPos = deltaPositions[(int)direction];
            Debug.Log("Inside Move" + " dPos: " + dPos);
            Vector3 parentPos = headBody.position;
            Vector3 prevPos;

            // head will move by dPos in this call to its position
            mainBody.position += dPos;
            headBody.position += dPos;

            // loop through all nodes except headBody and move each node to its prev node position
            for (int i = 1; i < nodes.Count; i++)
            {
                prevPos = nodes[i].position;
                nodes[i].position = parentPos;
                parentPos = prevPos;
            }

            // check if we eaten a food and create a new node
            if (createNodeAtTail)
            {

            }
        }

        public void ForceMove()
        {
            Debug.Log("Inside ForceMove");
            countDown = 0f;
            Move();
        }

        #endregion

    }
}