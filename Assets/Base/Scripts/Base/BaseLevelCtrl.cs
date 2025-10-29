using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BaseH
{
    public class BaseLevelCtrl : MonoBehaviour
    {
        [Header("CURRENT LEVEL")]
        private BaseLevel _currentLevel;
        public BaseLevel CurrentLevel 
        {
            get => _currentLevel; 
            set
            {
                _currentLevel = value;
            }
        }
    }
}
