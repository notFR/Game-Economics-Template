using System.Collections.Generic;
using UnityEngine;

namespace TD
{
    public class UI_MissionsTabGroup : MonoBehaviour
    {
        #region Variable

        [SerializeField] private List<UI_MissionsTabButton> tabButtons;

        [SerializeField] private Color defaultColor;

        #endregion

        #region Function

        public void Subscribe(UI_MissionsTabButton button)
        {
            if (tabButtons == null)
            {
                tabButtons = new List<UI_MissionsTabButton>();
            }
            tabButtons.Add(button);
        }

        public void OnTabEnter(UI_MissionsTabButton button)
        {
            
        }
        
        public void OnTabExit(UI_MissionsTabButton button)
        {
            
        }
        
        public void OnTabSelected(UI_MissionsTabButton button)
        {
            ResetTabs();
            ResetHighlight();
            button.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            button.backgroundText.color = Color.white;
        }

        public void ResetTabs()
        {
            foreach (UI_MissionsTabButton button in tabButtons)
            {
                button.backgroundText.color = defaultColor;
            }
        }

        public void ResetHighlight()
        {
            foreach (UI_MissionsTabButton button in tabButtons)
            {
                button.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        #endregion

    }
}

