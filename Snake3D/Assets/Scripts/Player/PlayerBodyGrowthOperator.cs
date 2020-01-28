using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake3D
{
    public class PlayerBodyGrowthOperator : MonoBehaviour
    {

        #region Variables
        #endregion

        #region Builtin Methods
        #endregion

        #region Custom Methods

        public List<Rigidbody> AddTail(GameObject _tailPrefab, List<Rigidbody> _nodes)
        {
            GameObject newTail = Instantiate(_tailPrefab, _nodes[_nodes.Count - 1].position, Quaternion.identity) as GameObject;

            newTail.transform.SetParent(transform, true);
            _nodes.Add(newTail.GetComponent<Rigidbody>());

            return _nodes;
        }

        #endregion

    }
}