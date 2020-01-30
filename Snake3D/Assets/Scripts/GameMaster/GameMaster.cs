using UnityEngine.UI;
using UnityEngine;

namespace Snake3D
{
    public class GameMaster : MonoBehaviour
    {

        #region Variables

        public static GameMaster gameMaster;

        [Header("UI")]
        public Text scoreText;
        public Text maxScoreText;

        // colors and points fetched from file for fruits
        [Header("Data from xml")]
        public Color[] fruitColorList;
        public int[] pointsToAddList;

        private int highestScore; // read from file
        private int score;
        private int streak;
        private Color prevFruitColor;

        #endregion

        #region Properties

        public int HighestScore { get { return highestScore; } set { highestScore = value; } }
        public int Streak { get => streak; set => streak = value; }

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
            FruitSpawner.fruitSpawner.FruitTypeCount = fruitColorList.Length;

            // player init
            HighestScore = SaveAndLoadSystem.saveLoad.LoadPlayerScore();
            maxScoreText.text = HighestScore.ToString();

            score = 0;
            scoreText.text = score.ToString();
            Streak = 1;
            prevFruitColor = Color.white;
        }

        #endregion

        #region Custom Methods

        public void AddToScore(Color _fruitColor, int pointsToAdd)
        {
            // update streak
            if(prevFruitColor == _fruitColor)
            {
                Streak++;
            }
            else if(prevFruitColor != _fruitColor)
            {
                prevFruitColor = _fruitColor;
                Streak = 1;
            }

            // add score
            score += pointsToAdd * Streak;
            scoreText.text = score.ToString();
            UpdateHighestScore(score);
            GameUIManager.gameUIManager.fadingScoreValueText.enabled = true;
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
            GameUIManager.gameUIManager.LoadGameOverCanvas();
        }

        public void ResetGameMaster()
        {
            Destroy(this.gameObject);
            return;
        }

        #endregion

    }
}