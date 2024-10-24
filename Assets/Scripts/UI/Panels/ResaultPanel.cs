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


        [SerializeField] private Text m_Kills; // ������ �� ���������� ����������� ��������� ��������
        [SerializeField] private Text m_Score; // ������ �� ���� ������
        [SerializeField] private Text m_Time; // ������ �� ����� ����
        [SerializeField] private Text m_Resault; // ������ �� ��������� ����
        [SerializeField] private Text m_ButtonNextText; // ������ �� ������ ���������� ������

        private bool m_LevelPassed = false; // �������� �� ����������� �������

        private void Start()
        {
            gameObject.SetActive(false); // ��������� ResaultPanel
            LevelController.Instance.LevelLost += OnLevelLost; // ������������� �� ������� ���������
            LevelController.Instance.LevelPassed += OnLevelPassed; // ������������� �� ������� ��������
        }

        private void OnDestroy()
        {
            LevelController.Instance.LevelLost -= OnLevelLost; // ������������ �� ������� ���������
            LevelController.Instance.LevelPassed -= OnLevelPassed; // ������������ �� ������� ��������
        }

        private void OnLevelPassed()
        {
            gameObject.SetActive(true); // �������� ResaultPanel

            m_LevelPassed = true;
            
            FillLevelStatistics();

            m_Resault.text = PassedText; // ����� ������

            if (LevelController.Instance.HasNextLevel == true) // ���� ���� ��������� �������
            {
                m_ButtonNextText.text = NextText; // �� ������� �� ������
            }
            else
            {
                m_ButtonNextText.text = MainMenuText; // ����� ������� �� ������
            }
        }

        private void OnLevelLost()
        {
            gameObject.SetActive(true); // �������� ResaultPanel
            
            FillLevelStatistics();

            m_Resault.text = Lose; // ����� ���������
            m_ButtonNextText.text = RestartText; // H������ �� ������
        }

        private void FillLevelStatistics() // ������� �������������� ������ ������
        {
            m_Kills.text = KillsTextPrefix + Player.Instance.NumKills.ToString();
            m_Score.text = ScoreTextPrefix + Player.Instance.Score.ToString();
            m_Time.text = TimeTextPrefix + LevelController.Instance.LevelTime.ToString("F0");
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            // ���� ������� �������
            if (m_LevelPassed == true)
            {
                // ��������� ��������� �������
                LevelController.Instance.LoadNextLevel();
            }
            else
            {
                // ��������� ������� �������
                LevelController.Instance.Restartlevel();
            }
        }
    }
}
