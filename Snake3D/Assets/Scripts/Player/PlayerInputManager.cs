﻿using UnityEngine;

namespace Snake3D
{
    public class PlayerInputManager : MonoBehaviour
    {

        #region Variables

        public int inverseControls = 1;
        public float inverseControlsForTime = 5f;

        #endregion

        #region Enums

        public enum Axis
        {
            Horizontal,
            Vertical
        }

        #endregion

        #region Builtin Methods
        #endregion

        #region Custom Methods

        public void GetKeyboardInput(out int _horizontal, out int _vertical)
        {
            _horizontal = GetAxisRaw(Axis.Horizontal);
            _vertical = GetAxisRaw(Axis.Vertical);

            if (_horizontal != 0)
                _vertical = 0;
        }

        private int GetAxisRaw(Axis axis)
        {
            if(axis == Axis.Horizontal)
            {
                bool left = Input.GetKeyDown(KeyCode.A);
                bool right = Input.GetKeyDown(KeyCode.D);

                if(left)
                {
                    return -1 * inverseControls;
                }

                if(right)
                {
                    return 1 * inverseControls;
                }

                return 0;
            }
            else if (axis == Axis.Vertical)
            {
                bool up = Input.GetKeyDown(KeyCode.W);
                bool down = Input.GetKeyDown(KeyCode.S);

                if(up)
                {
                    return 1 * inverseControls;
                }

                if(down)
                {
                    return -1 * inverseControls;
                }

                return 0;
            }


            return 0;
        }

        #endregion

    }
}
