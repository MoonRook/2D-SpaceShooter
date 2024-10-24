using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(MeshRenderer))] // Добавляем дерективу
    public class BackgroundElement : MonoBehaviour
    {
        [Range(0.0f, 4.0f)]

        [SerializeField] private float m_ParallaxPower; // Сила параллакс эффекта

        [SerializeField] private float m_TextureScale; // Размер текстуры

        private Material m_QuadMaterial; // Ссылка на материал
        private Vector2 m_InitialOffset; // Изначальная точка оффсета

     
        /// <summary>
        /// Получает ссылку на материал и получает случайную точку оффсета в единичной окружности
        /// </summary>
        private void Start()
        {
            m_QuadMaterial = GetComponent<MeshRenderer>().material;
            m_InitialOffset = UnityEngine.Random.insideUnitCircle;

            // У ссылки на материал задаем размер тестуры = одиночный вектор 2 * размер текстуры
            m_QuadMaterial.mainTextureScale = Vector2.one * m_TextureScale; 
        }
        /// <summary>
        /// Создает вектор оффсета равный изначальной точке оффсета и изменяет оффсет
        private void Update()
        {
            Vector2 offset = m_InitialOffset;

            // Точка оффета х += позиции по х / локальный размер х / силу параллакс эффекта
            offset.x += transform.position.x / transform.localScale.x / m_ParallaxPower;

            // Точка оффета y += позиции по y / локальный размер y / силу параллакс эффекта
            offset.y += transform.position.y / transform.localScale.y / m_ParallaxPower;

            // У ссылки на материал задаем размер оффсета тестуры = точка оффсета
            m_QuadMaterial.mainTextureOffset = offset;
        }

    }
}
