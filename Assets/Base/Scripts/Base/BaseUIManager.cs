using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BaseH
{
    public class BaseUIManager : MonoBehaviour
    {
        public PanelBase[] _arrUI;
        protected Dictionary<PanelType, PanelBase> _uis = new Dictionary<PanelType, PanelBase>();

        protected virtual void Awake()
        {

            foreach(var ui in _arrUI)
            {
                _uis[ui.Type] = ui; 
            }
        }

        public void Active(PanelType type)
        {
            if (!_uis.ContainsKey(type))
            {
                Debug.LogError($"UI {type} is not found!!!");
                return;
            }
            _uis[type].Show();
        }

        public void DeActive(PanelType type)
        {
            if (!_uis.ContainsKey(type))
            {
                Debug.LogError($"UI {type} is not found!!!");
                return;
            }
            _uis[type].Hide();
        }
    }
}

