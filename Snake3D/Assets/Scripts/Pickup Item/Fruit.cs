using UnityEngine;

namespace Snake3D
{
    public class Fruit : MonoBehaviour
    {

        #region Variables

        private Color fruitColor;
        public int pointsToAdd;

        #endregion

        #region Properties

        public Color FruitColor { get => fruitColor; set => fruitColor = value; }

        #endregion

        #region Custom Methods

        public void InitFruit(Color _fruitColor, int _pointsToAdd)
        {
            FruitColor = _fruitColor;
            pointsToAdd = _pointsToAdd;
        }

        #endregion

    }
}