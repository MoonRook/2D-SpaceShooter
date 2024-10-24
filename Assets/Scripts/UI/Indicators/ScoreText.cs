using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private Text m_Text; // —сылка на элемент Text в HitPointBar

        private float lastScoreText;
        private void Update()
        {
            int score = Player.Instance.Score;

            if (lastScoreText != score)
            {
                m_Text.text = "Score : " + score.ToString();
                lastScoreText = score;
            }
        }
    }
}




