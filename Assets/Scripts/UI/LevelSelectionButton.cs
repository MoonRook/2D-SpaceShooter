using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class LevelSelectionButton : MonoBehaviour
    {
        [SerializeField] private LevelProperties m_LevelProperties; // —сылка на уровень
        [SerializeField] private Text m_LevelTitle; // —сылка на название уровн€
        [SerializeField] private Image m_PreviewImage; // —сылка на изображение уровн€

        private void Start()
        {
            if (m_LevelProperties == null) return;
            
            m_PreviewImage.sprite = m_LevelProperties.PreviewImage;
            m_LevelTitle.text = m_LevelProperties.Title;
        }

        public void Loadlevel()
        {
            SceneManager.LoadScene(m_LevelProperties.SceneName);
        }
    }
}


