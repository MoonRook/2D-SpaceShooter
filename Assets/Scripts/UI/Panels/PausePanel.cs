using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private GameObject m_Panel; // Ссылка на объект панель

        private void Start()
        {
            m_Panel.SetActive(false); // Выключает панель
            Time.timeScale = 1; // Включает время
        }

        public void ShowPause()
        {
            m_Panel.SetActive(true); // Включает панель
            Time.timeScale = 0; // Выключает время
        }
        public void HidePause()
        {
            m_Panel.SetActive(false); // Выключает панель
            Time.timeScale = 1; // Включает время
        }
        public void LoadMainMenu()
        {
            m_Panel.SetActive(false); // Выключает панель
            Time.timeScale = 1; // Включает время

            SceneManager.LoadScene(0); // Загружает сцену
        }
    }

}
