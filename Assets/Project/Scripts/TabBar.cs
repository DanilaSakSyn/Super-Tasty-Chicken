using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TabBar : MonoBehaviour
{
    [System.Serializable]
    public class Tab
    {
        public Button tabButton;
        public GameObject tabPanel;
    }

    public List<Tab> tabs;
    public Color selectedColor = Color.white;
    public Color normalColor = Color.gray;
    private int currentTab = 0;

    void Start()
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            int index = i; // Capture index for closure
            tabs[i].tabButton.onClick.AddListener(() => SelectTab(index));
        }
        SelectTab(0);
    }

    public void SelectTab(int index)
    {
        currentTab = index;
        for (int i = 0; i < tabs.Count; i++)
        {
            bool isSelected = (i == index);
            tabs[i].tabPanel.SetActive(isSelected);
            var colors = tabs[i].tabButton.colors;
            colors.normalColor = isSelected ? selectedColor : normalColor;
            tabs[i].tabButton.colors = colors;
        }
    }
} 