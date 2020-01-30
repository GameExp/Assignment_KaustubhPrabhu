using System.Collections.Generic;
using UnityEngine;

namespace Snake3D
{
    public class PlayerMovementHandler : MonoBehaviour
    {

        #region Variables

        // direction, distance, freq at which to move
        private PlayerDirection direction;
        [SerializeField]
        private float stepLength = 0.2f;
        [SerializeField]
        private float moveFrequency = 0.1f;

        private float countDown;
        private List<Vector3> deltaPositions;

        // rigidbodies
        private Rigidbody mainBody;
        private Rigidbody headBody;

        #endregion

        #region Properties

        public PlayerDirection Direction { get => direction; set => direction = value; }

        #endregion

        #region Builtin Methods

        void Awake()
        {
            SetDeltaPositions();
        }

        #endregion

        #region Custom Methods

        public void InitPlayer()
        {
            List<Rigidbody> _nodes = PlayerController.Nodes;
            mainBody = GetComponent<Rigidbody>();
            headBody = _nodes[0];

            // set a random intial direction
            SetRandomDirection();
            // based on the direction change the position of 2 tailing objects respectively
            switch (Direction)
            {
                case PlayerDirection.LEFT:

                    _nodes[1].position = _nodes[0].position + new Vector3(Metrics.NODEOFFSET, 0f, 0f);
                    _nodes[2].position = _nodes[0].position + new Vector3(Metrics.NODEOFFSET * 2f, 0f, 0f);

                    break;

                case PlayerDirection.UP:

                    _nodes[1].position = _nodes[0].position - new Vector3(0f, 0f, Metrics.NODEOFFSET);
                    _nodes[2].position = _nodes[0].position - new Vector3(0f, 0f, Metrics.NODEOFFSET * 2f);

                    break;

                case PlayerDirection.RIGHT:

                    _nodes[1].position = _nodes[0].position - new Vector3(Metrics.NODEOFFSET, 0f, 0f);
                    _nodes[2].position = _nodes[0].position - new Vector3(Metrics.NODEOFFSET * 2f, 0f, 0f);

                    break;

                case PlayerDirection.DOWN:

                    _nodes[1].position = _nodes[0].position + new Vector3(0f, 0f, Metrics.NODEOFFSET);
                    _nodes[2].position = _nodes[0].position + new Vector3(0f, 0f, Metrics.NODEOFFSET * 2f);

                    break;
            }
        }

        private void SetRandomDirection()
        {
            int randomDir = Random.Range(0, (int)PlayerDirection.COUNT);
            Direction = (PlayerDirection)randomDir;
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
                Move(PlayerController.Nodes);
            }
        }

        private void Move(List<Rigidbody> _nodes)
        {
            // get the delta position Vector3 to move by in this call
            Vector3 dPos = deltaPositions[(int)Direction];
            Vector3 parentPos = headBody.position;
            Vector3 prevPos;

            // head will move by dPos in this call to its position
            mainBody.position += dPos;
            headBody.position += dPos;

            // loop through all nodes except headBody and move each node to its prev node position
            for (int i = 1; i < _nodes.Count; i++)
            {
                prevPos = _nodes[i].position;
                _nodes[i].position = parentPos;
                parentPos = prevPos;
            }
        }

        public void ForceMove()
        {
            countDown = 0f;
            Move(PlayerController.Nodes);
        }

        #endregion

    }
}