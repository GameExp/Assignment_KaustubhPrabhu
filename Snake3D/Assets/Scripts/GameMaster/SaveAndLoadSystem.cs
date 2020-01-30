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

        private string xmlFruitPathPattern = "//pickupitem/fruits";
        private string highestScorePath;
        private BinaryFormatter binaryFormatter;
        private FileStream file;

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

            binaryFormatter = new BinaryFormatter();
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
            XmlNodeList nodeList = xmlDoc.SelectNodes(xmlFruitPathPattern);

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
            }
        }

        public int LoadPlayerScore()
        {
            if (File.Exists(highestScorePath))
            {
                file = File.Open(highestScorePath, FileMode.Open);
                HighestScore highestScore = binaryFormatter.Deserialize(file) as HighestScore;
                file.Close();
                return highestScore.highestScore;
            }
            else
            {
                HighestScore hS = new HighestScore();
                hS.highestScore = 0;
                SavePlayerScore(hS.highestScore);
                return 0;
            }
        }

        public void SavePlayerScore(int score)
        {
            file = File.Create(highestScorePath);

            HighestScore highestScore = new HighestScore();
            highestScore.highestScore = score;      

            binaryFormatter.Serialize(file, highestScore);
            file.Close();
        }

        public XmlNodeList GetXMLNodes(string data, string pattern)
        {
            // Create and load xml document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(data));

            // Get all the nodes for given pattern
            XmlNodeList nodeList = xmlDoc.SelectNodes(pattern);

            return nodeList;
        }

        #endregion

    }
}
