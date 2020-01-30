
namespace Snake3D
{
    public class Tags
    {
        #region Static Variables

        public static string WALL = "Wall";
        public static string FRUIT = "Fruit";
        public static string TAIL = "Tail";
        public static string BOMB = "Bomb";
        public static string INSVERSE = "Inverse";

        #endregion
    }

    public class Metrics
    {
        #region Static Variables
        public static float NODEOFFSET = 1f;
        #endregion
    }

    public enum PlayerDirection
    {
        LEFT = 0,
        UP = 1,
        RIGHT = 2,
        DOWN = 3,
        COUNT = 4
    }
}