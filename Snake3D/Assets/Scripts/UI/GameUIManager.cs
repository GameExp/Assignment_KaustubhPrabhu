using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Snake3D
{
    public class GameUIManager : MonoBehaviour
    {

        #region Variables

        public static GameUIManager gameUIManager;
        public GameObject gameOverCanvas;
        public Text maxScoreValueText;

        public Text fadingScoreValueText;

        private Vector3 initialPosition;
        private float alphaValue = 1f;
        private float moveSpeed = 2f;

        #endregion

        #region Builtin Methods

        void Awake()
        {
            if(gameUIManager==null)
                gameUIManager = this;

            gameOverCanvas.SetActive(false);

            fadingScoreValueText.enabled = false;
            initialPosition = fadingScoreValueText.transform.position;
        }

        void  Update()
        {
            UpdateScoreBoard();
        }

        #endregion

        #region Custom Methods

        public void UpdateScoreBoard()
        {
            if (fadingScoreValueText.enabled)
            {
                fadingScoreValueText.text = " X " + GameMaster.gameMaster.streak.ToString() + " STREAK";
                if (alphaValue <= 0.0f)
                {
                    fadingScoreValueText.enabled = false;
                    fadingScoreValueText.transform.position = initialPosition;
                    alphaValue = 1.0f;
                    fadingScoreValueText.color = new Color(1.0f, 1.0f, 1.0f, alphaValue);
                }
                else
                {
                    fadingScoreValueText.transform.Translate(new Vector3(0, moveSpeed, 0) * Time.deltaTime);
                    alphaValue -= Time.deltaTime;
                    fadingScoreValueText.color = new Color(1.0f, 1.0f, 1.0f, alphaValue);
                }
            }
        }

        public void LoadGameOverCanvas()
        {
            gameOverCanvas.SetActive(true);
            maxScoreValueText.text = GameMaster.gameMaster.HighestScore.ToString();
        }

        public void LoadSameScene()
        {
            GameMaster.gameMaster.ResetGameMaster();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadMainMenu()
        {
            GameMaster.gameMaster.ResetGameMaster();
            SceneManager.LoadScene(0);
        }

        #endregion

    }
}