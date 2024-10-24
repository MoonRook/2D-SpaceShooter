using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_LevelSelectionPanel; // —сылка на панель уровней
        [SerializeField] private GameObject m_ShipSelectionPanel; // —сылка на панель кораблей
        [SerializeField] private GameObject m_MainPanel; // —сылка на панель меню

        private void Start()
        {
            ShowMainPanel();
        }
        public void ShowMainPanel()
        {
            m_MainPanel.SetActive(true);
            m_ShipSelectionPanel.SetActive(false);
            m_LevelSelectionPanel.SetActive(false);
        }
        
        public void ShowShipSelection()
        {
            m_ShipSelectionPanel.SetActive(true);
            m_MainPanel.SetActive(false);
        }

        public void ShowLevelSelection()
        {
            m_LevelSelectionPanel.SetActive(true);
            m_MainPanel.SetActive(false);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}

