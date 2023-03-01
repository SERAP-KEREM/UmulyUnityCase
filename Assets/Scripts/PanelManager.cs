using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PanelManager : MonoBehaviour
{
    public GameObject[] menuPanels;
    public int panelID = 0;
    public void NextButton()
    {
        panelID += 1;
        menuPanels[panelID].SetActive(true);
    }
    public void BackButton()
    {
        panelID -= 1;
        
        for (int i = 0; i < 8; i++)
        {
            if (i == panelID)
            {
                menuPanels[panelID].SetActive(true);
            }
            if(i!=panelID)
            {
                menuPanels[i].SetActive(false);
            }
        }
    }
}