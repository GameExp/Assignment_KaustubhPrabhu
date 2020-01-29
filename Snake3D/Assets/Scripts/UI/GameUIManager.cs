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

        #endregion

        #region Builtin Methods

        void Awake()
        {
            if(gameUIManager==null)
                gameUIManager = this;

            gameOverCanvas.SetActive(false);
        }

        #endregion

        #region Custom Methods

        public void LoadGameOverCanvas()
        {
            gameOverCanvas.SetActive(true);
            maxScoreValueText.text = GameMaster.gameMaster.HighestScore.ToString();
        }

        public void LoadSameScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        #endregion

    }
}