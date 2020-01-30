using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake3D
{
    public class MusicPlayer : MonoBehaviour
    {

        #region Variables

        private int numberOfMusicPlayers = 0;

        #endregion

        #region Builin Methods

        void Awake()
        {
            numberOfMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;

            if (numberOfMusicPlayers > 1)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }

        #endregion

    }
}
