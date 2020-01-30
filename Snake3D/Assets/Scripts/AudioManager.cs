using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake3D
{
    public class AudioManager : MonoBehaviour
    {

        #region Variables

        public static AudioManager audioManager;

        public AudioClip fruitPickupSFX;
        public AudioClip bombPickupSFX;
        //public AudioClip reverseBombPickupSFX;
        //public AudioClip tailEatSFX;

        public AudioClip collideWallSFX;

        #endregion

        #region Builtin Methods

        void Awake()
        {
            if(audioManager == null)
            {
                DontDestroyOnLoad(this);
                audioManager = this;
            }
            else if(audioManager != this)
            {
                Destroy(this);
                return;
            }
        }

        #endregion

        #region Custom Methods

        public void CollectFruitSFX()
        {
            AudioSource.PlayClipAtPoint(fruitPickupSFX, Camera.main.transform.position, 0.065f);
        }

        public void CollectBombSFX()
        {
            AudioSource.PlayClipAtPoint(bombPickupSFX, Camera.main.transform.position, 0.2f);
        }

        public void CollideWithWallSFX()
        {
            AudioSource.PlayClipAtPoint(collideWallSFX, Camera.main.transform.position, 0.35f);
        }

        #endregion

    }
}