using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake3D
{
    [RequireComponent(typeof(PlayerMovementHandler))]
    [RequireComponent(typeof(PlayerInputManager))]
    public class PlayerController : MonoBehaviour
    {

        #region Variables

        [Header("Movement")]
        private int horizontal = 0;
        private int vertical = 0;

        // Growth increase
        [SerializeField]
        private GameObject tailPrefab;

        #endregion

        #region Cached Variables

        private PlayerMovementHandler movement;
        private PlayerInputManager inputManager;

        #endregion

        #region Builtin Methods

        void Awake()
        {
            movement = GetComponent<PlayerMovementHandler>();
            inputManager = GetComponent<PlayerInputManager>();
        }

        void Update()
        {
            if (inputManager == null)
                return;

            GetInput();
        }

        void FixedUpdate()
        {
            if (movement == null)
                return;

            movement.CanMove();
        }

        #endregion

        #region Custom Methods

        private void GetInput()
        {
            horizontal = 0;
            vertical = 0;
            inputManager.GetKeyboardInput(out horizontal, out vertical);

            SetMovement();
        }

        private void SetMovement()
        {
            Debug.Log("Inside SetMovement" + "horizontal: " + horizontal + " vertical: " + vertical);
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
            Debug.Log("Inside SetInputDirection: " + "dir: " + (int)_dir + " direction: " + (int)movement.direction);
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

        #endregion

    }
}