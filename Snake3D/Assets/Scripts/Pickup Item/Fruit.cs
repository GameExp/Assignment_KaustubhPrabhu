using UnityEngine;

namespace Snake3D
{
    public class Fruit : MonoBehaviour
    {

        #region Variables

        private Color fruitColor;
        private int pointsToAdd;

        #endregion

        #region Properties

        public Color FruitColor { get => fruitColor; set => fruitColor = value; }
        public int PointsToAdd { get => pointsToAdd; set => pointsToAdd = value; }

        #endregion

        #region Custom Methods

        public void InitFruit(Color _fruitColor, int _pointsToAdd)
        {
            FruitColor = _fruitColor;
            PointsToAdd = _pointsToAdd;
        }

        #endregion

    }
}