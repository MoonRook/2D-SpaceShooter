using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [CreateAssetMenu]
    public class LevelProperties : ScriptableObject
    {
        [SerializeField] private string m_Title; // Название уровня
        [SerializeField] private string m_SceneName; // Название сцены
        [SerializeField] private Sprite m_PreviewImage; // Изображение 
        [SerializeField] private LevelProperties m_NextLevel; // Ссылка на новый уровень

        public string Title => m_Title;
        public string SceneName => m_SceneName;
        public Sprite PreviewImage => m_PreviewImage;
        public LevelProperties NextLevel => m_NextLevel;
    }
}

