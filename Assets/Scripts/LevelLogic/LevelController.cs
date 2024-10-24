using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelController : SingletonBase<LevelController>
    {
        private const string MainMenuSceneName = "Main_menu"; // Тестовая константа

        public event UnityAction LevelPassed; // Событие выигрыша
        public event UnityAction LevelLost; // Событие проигрыша

        [SerializeField] private LevelProperties m_LevelProperties;
        [SerializeField] private LevelCondition[] m_Conditions; // Массив условий победы

        private bool m_IsLevelCompleted; // Проверка на завершение уровня
        private float m_LevelTime;
        
        // Проверка на наличие следующего уровня
        public bool HasNextLevel => m_LevelProperties.NextLevel != null;
        public float LevelTime => m_LevelTime;

        private void Start()
        {
            Time.timeScale = 1;
            m_LevelTime = 0;
        }
        private void Update()
        {
            if (m_IsLevelCompleted == false) // Если уровень не пройден прибавляем время
            {
                m_LevelTime += Time.deltaTime;
                CheckLevelConditions();
            }
            
            if (Player.Instance.NumLives == 0) // Если количество жизней игрока закончилось
            {
                Lose();
            }
        }

        private void CheckLevelConditions() // Выполняет проверку на завершение уровня
        {
            if (m_IsLevelCompleted == true) return;

            int numCompleted = 0;

            for (int i = 0; i < m_Conditions.Length; i++)
            {
                if (m_Conditions[i].IsCompleted == true)
                {
                    numCompleted++;
                }
            }
            
            if (numCompleted == m_Conditions.Length)
            {
                m_IsLevelCompleted = true;

                Pass();
            }
        }
        private void Lose()
        {
            LevelLost?.Invoke();
            Time.timeScale = 0;
        }
        private void Pass()
        {
            LevelPassed?.Invoke();
            Time.timeScale = 0;
        }

        public void LoadNextLevel() // Загружает следующий уровень
        {
            if (HasNextLevel == true)
                SceneManager.LoadScene(m_LevelProperties.NextLevel.SceneName);
            else
                SceneManager.LoadScene(MainMenuSceneName);
        }

        public void Restartlevel() // Загружает текущий уровень
        {
            SceneManager.LoadScene(m_LevelProperties.SceneName);
        }
    }
}

