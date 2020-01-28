using UnityEngine;

namespace Snake3D
{
    public class Fruit : MonoBehaviour
    {

        #region Variables


        public Color fruitColor;
        public int pointsToAdd = 10;

        #endregion

        #region Builtin Methods
        #endregion

        #region Custom Methods

        public void InitFruit(Color _fruitColor, int _pointsToAdd)
        {
            fruitColor = _fruitColor;
            pointsToAdd = _pointsToAdd;
        }

        #endregion

    }
}