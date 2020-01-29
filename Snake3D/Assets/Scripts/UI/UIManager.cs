using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Snake3D
{
    public class UIManager : MonoBehaviour
    {

        #region Variables

        public Text maxScoreValueText;

        #endregion

        #region Builtin Methods

        void Start()
        {
            maxScoreValueText.text = SaveAndLoadSystem.saveLoad.LoadPlayerScore().ToString();
        }

        #endregion

        #region Custom Methods

        public void LoadGameScene()
        {
            SceneManager.LoadScene(1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        #endregion

    }
}