using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResaultPanel : MonoBehaviour
    {
        private const string PassedText = "Passed";
        private const string Lose = "Lose";
        private const string RestartText = "Restart";
        private const string NextText = "Next";
        private const string MainMenuText = "Main menu";
        private const string KillsTextPrefix = "Kills : ";
        private const string ScoreTextPrefix = "Score : ";
        private const string TimeTextPrefix = "Time : ";


        [SerializeField] private Text m_Kills; // Ссылка на количество уничтоженых вражеских кораблей
        [SerializeField] private Text m_Score; // Ссылка на счет игрока
        [SerializeField] private Text m_Time; // Ссылка на время игры
        [SerializeField] private Text m_Resault; // Ссылка на результат игры
        [SerializeField] private Text m_ButtonNextText; // Ссылка на кнопку следующего уровня

        private bool m_LevelPassed = false; // Проверка на прохождение уровеня

        private void Start()
        {
            gameObject.SetActive(false); // Выключаем ResaultPanel
            LevelController.Instance.LevelLost += OnLevelLost; // Подписывается на событие поражения
            LevelController.Instance.LevelPassed += OnLevelPassed; // Подписывается на побытие выигрыша
        }

        private void OnDestroy()
        {
            LevelController.Instance.LevelLost -= OnLevelLost; // Отписывается от события поражения
            LevelController.Instance.LevelPassed -= OnLevelPassed; // Отписывается от побытия выигрыша
        }

        private void OnLevelPassed()
        {
            gameObject.SetActive(true); // Включаем ResaultPanel

            m_LevelPassed = true;
            
            FillLevelStatistics();

            m_Resault.text = PassedText; // Текст победы

            if (LevelController.Instance.HasNextLevel == true) // Если есть следующий уровень
            {
                m_ButtonNextText.text = NextText; // То надпись на кнопке
            }
            else
            {
                m_ButtonNextText.text = MainMenuText; // Иначе надпись на кнопке
            }
        }

        private void OnLevelLost()
        {
            gameObject.SetActive(true); // Включаем ResaultPanel
            
            FillLevelStatistics();

            m_Resault.text = Lose; // Текст поражения
            m_ButtonNextText.text = RestartText; // Hадпись на кнопке
        }

        private void FillLevelStatistics() // Выводит статистические данные игрока
        {
            m_Kills.text = KillsTextPrefix + Player.Instance.NumKills.ToString();
            m_Score.text = ScoreTextPrefix + Player.Instance.Score.ToString();
            m_Time.text = TimeTextPrefix + LevelController.Instance.LevelTime.ToString("F0");
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            // Если уровень пройден
            if (m_LevelPassed == true)
            {
                // Загружает следующий уровень
                LevelController.Instance.LoadNextLevel();
            }
            else
            {
                // Загружает текущий уровень
                LevelController.Instance.Restartlevel();
            }
        }
    }
}
