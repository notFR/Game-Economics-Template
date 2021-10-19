using System.Collections.Generic;
using UnityEngine;

namespace TD
{
    public class UI_MissionsTabGroup : MonoBehaviour
    {
        #region Variable
        
        [SerializeField] private Color defaultColor;
        [Space(15)] 
        [SerializeField] private List<GameObject> objToSwap;
        
        
        
        private List<UI_MissionsTabButton> tabButtons;
        private UI_MissionsTabButton selectedTab;

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

            selectedTab = button;
            
            button.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            button.backgroundText.color = Color.white;

            int index = button.transform.GetSiblingIndex();
            for (int i = 0; i < objToSwap.Count; i++)
            {
                if (i == index)
                {
                    objToSwap[i].SetActive(true);
                }
                else
                {
                    objToSwap[i].SetActive(false);
                }
            }

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

