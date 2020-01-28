using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake3D
{
    public class GameMaster : MonoBehaviour
    {

        #region Variables

        public static GameMaster gameMaster;

        public int score;
        public int streak;

        public Color prevFruitColor;

        private int highestScore; // read from file

        #endregion

        #region Builtin Methods

        void Awake()
        {
            // implement singleton pattern
            if(gameMaster == null)
            {
                DontDestroyOnLoad(this);
                gameMaster = this;
            }
            else if(gameMaster != this)
            {
                Destroy(gameObject);
                return;
            }

            score = 0;
            streak = 1;
            prevFruitColor = Color.white;
        }

        #endregion

        #region Custom Methods

        public void AddToScore(Color _fruitColor, int pointsToAdd)
        {
            if(prevFruitColor == _fruitColor)
            {
                streak++;
            }
            else if(prevFruitColor != _fruitColor)
            {
                prevFruitColor = _fruitColor;
                streak = 1;
            }

            score += pointsToAdd * streak;
        }

        #endregion

    }
}