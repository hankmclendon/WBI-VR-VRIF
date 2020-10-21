using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

namespace Sinthetik.MissionControl
{
    public class MenuPanel : MonoBehaviour
    {
    public GameObject menuItemPrefab;
        
        public static event Action<SubSection> itemSelected;

        public void BuildMenu(List<SubSection> currentList)
        {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach(SubSection subSection in currentList)
            {
                GameObject menuItem = Instantiate(menuItemPrefab);
                menuItem.transform.SetParent(gameObject.transform, false);
                menuItem.transform.GetChild(0).GetComponent<Text>().text = subSection.name;
                Button button = menuItem.transform.GetChild(1).GetComponent<Button>();
                if(!subSection.isComplete && !subSection.deactivated)
                {
                    button.onClick.AddListener( () => ItemSelected(subSection));
                }
                else if(subSection.deactivated)
                {
                    button.interactable = false;
                    menuItem.transform.GetChild(0).GetComponent<Text>().text = "Deactivated";
                }
                else
                {
                    button.interactable = false;
                }
            }
        }

        void ItemSelected(SubSection subSection)
        {
            itemSelected?.Invoke(subSection);
            gameObject.SetActive(false);
        }
    }
}
