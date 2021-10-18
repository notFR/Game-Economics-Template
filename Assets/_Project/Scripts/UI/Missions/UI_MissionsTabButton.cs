using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace TD
{
    public class UI_MissionsTabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        #region Variable
        
        
        
        

        private UI_MissionsTabGroup tabGroup;
        [HideInInspector] public TMP_Text backgroundText;
        
        #endregion

        #region Function

        private void Start()
        {
            tabGroup = Object.FindObjectOfType<UI_MissionsTabGroup>().GetComponent<UI_MissionsTabGroup>();
            backgroundText = GetComponent<TMP_Text>();
            
            tabGroup.Subscribe(this);
        }

        #endregion

        public void OnPointerEnter(PointerEventData eventData)
        {
            tabGroup.OnTabEnter(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            tabGroup.OnTabSelected(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tabGroup.OnTabExit(this);
        }
    }
}

