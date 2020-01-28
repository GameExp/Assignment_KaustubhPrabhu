using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;

namespace Snake3D
{
    public class SaveAndLoadSystem : MonoBehaviour
    {

        #region Variables

        public static SaveAndLoadSystem saveLoad;
        [SerializeField]
        private TextAsset xmlRawFile;
        public string xmlPathPattern;

        public string highestScorePath;

        public BinaryFormatter binaryFormatter;
        public FileStream file;

        public TextAsset XmlRawFile { get { return xmlRawFile; } set { xmlRawFile = value; } }

        #endregion

        #region Builin Methods

        void Awake()
        {
            // implement singlton
            if (saveLoad == null)
            {
                DontDestroyOnLoad(this);
                saveLoad = this;
            }
            else if (saveLoad != this)
            {
                Destroy(gameObject);
                return;
            }

            highestScorePath = Application.persistentDataPath + "/HighestScore.data";
        }

        #endregion

        #region Custom Methods

        public void LoadFruits(string xmlData, out Color[] colorList, out int[] pointsList)
        {
            // Create and load xml document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(xmlData));

            // Get all the nodes for given pattern
            XmlNodeList nodeList = xmlDoc.SelectNodes(xmlPathPattern);

            colorList = new Color[nodeList.Count];
            pointsList = new int[nodeList.Count];

            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode color = nodeList[i].FirstChild;
                XmlNode points = color.NextSibling;

                XmlNode redNode = color.FirstChild;
                XmlNode greenNode = redNode.NextSibling;
                XmlNode blueNode = greenNode.NextSibling;
                XmlNode alphaNode = blueNode.NextSibling;

                // get the r,g,b for color and instantiate Fruit object
                float _red = float.Parse(redNode.InnerXml);
                float _green = float.Parse(greenNode.InnerXml);
                float _blue = float.Parse(blueNode.InnerXml);
                float _alpha = float.Parse(alphaNode.InnerXml);

                int _points = int.Parse(points.InnerXml);

                Color fruitColor = new Color(_red, _green, _blue, _alpha);

                colorList[i] = fruitColor;
                pointsList[i] = _points;

                /*Debug.Log("Fruit is: " +"red: " + _red + " green: " + _green 
                                                                 + " blue: " + _blue + " alpha: " + _alpha + " points: " + _points);*/
            }
        }

        public int LoadPlayerScore()
        {
            if(File.Exists(highestScorePath))
            {
                file = File.Open(highestScorePath, FileMode.Open);
                HighestScore highestScore = binaryFormatter.Deserialize(file) as HighestScore;
                file.Close();
                return highestScore.highestScore;
            }
            else
            {
                HighestScore hS = new HighestScore(0);
                return 0;
            }
        }

        public void SavePlayerScore(int score)
        {
            file = File.Create(highestScorePath);

            HighestScore highestScore = new HighestScore(score);

            binaryFormatter.Serialize(file, highestScore);
            file.Close();
        }

        #endregion

    }

    [System.Serializable]
    public class HighestScore
    {
        public int highestScore;

        public HighestScore(int _score)
        {
            highestScore = _score;
        }
    }
}
