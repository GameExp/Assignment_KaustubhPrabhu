﻿using UnityEngine.UI;
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

        public Color[] fruitColorList;

        public int[] pointsToAddList;

        private int highestScore; // read from file

        public Text scoreText;
        public Text maxScoreText;

        public int HighestScore { get { return highestScore; } set { highestScore = value; } }

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

            
        }

        void Start()
        {
            // fruits init
            SaveAndLoadSystem.saveLoad.LoadFruits(SaveAndLoadSystem.saveLoad.XmlRawFile.text, out fruitColorList, out pointsToAddList);

            FruitSpawner.spawner.fruitTypeCount = fruitColorList.Length;

            // player init
            HighestScore = SaveAndLoadSystem.saveLoad.LoadPlayerScore();
            maxScoreText.text = HighestScore.ToString();

            score = 0;
            scoreText.text = score.ToString();
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
            scoreText.text = score.ToString();
            UpdateHighestScore(score);
        }

        void UpdateHighestScore(int _score)
        {
            if (HighestScore < _score)
                HighestScore = _score;

            maxScoreText.text = HighestScore.ToString();
        }

        public void HandleLoseCondition()
        {
            // if the score is highest than previous store in file
            UpdateHighestScore(score);
            SaveAndLoadSystem.saveLoad.SavePlayerScore(HighestScore);
        }

        #endregion

    }
}