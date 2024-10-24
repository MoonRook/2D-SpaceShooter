using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Asteroid : Destructible
    {
        public enum Size
        {
            Small,
            Medium,
            Large,
            Huge
        }

        [SerializeField] private Size size;
        [SerializeField] private float spawnVelocity;
       
        [SerializeField] private SpriteRenderer asteroidViewMaterial; // Материал астероида

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            // Подписываемся на событие уничтожения объекта
            EventOnDeath.AddListener(OnAsteroidDestroyed);

            SetSize(size);
            // SetHitPoints(Mathf.Clamp(m_HitPoints / 2, 1, m_HitPoints));
        }

        private void NewOnDestroy()
        {
            // Отписываемся от события уничтожения объекта
            EventOnDeath.RemoveListener(OnAsteroidDestroyed);
        }

        private void OnAsteroidDestroyed()
        {
            if (size != Size.Small)
            {
                SpawnAsteroids();
            }

            Destroy(gameObject);
        }

        private void SpawnAsteroids()
        {
            for (int i = 0; i < 2; i++)
            {
                Asteroid asteroid = Instantiate(this, transform.position, Quaternion.identity);
                asteroid.SetSize(size - 1);
                // asteroid.SetHitPoints(Mathf.Clamp(m_HitPoints / 2, 1, m_HitPoints));
                asteroid.rb.AddForce(new Vector2((i % 2 * 2) - 1, 1) * spawnVelocity, ForceMode2D.Impulse);
            }
        }

        public void SetSize(Size size)
        {
            if (size < 0) return;

            transform.localScale = GetVectorFromSize(size);
            this.size = size;
            asteroidViewMaterial.sprite = GetSpriteFromSize(size);
        }

        private Vector3 GetVectorFromSize(Size size)
        {
            if (size == Size.Huge) return new Vector3(1, 1, 1);
            if (size == Size.Large) return new Vector3(0.75f, 0.75f, 0.75f);
            if (size == Size.Medium) return new Vector3(0.6f, 0.6f, 0.6f);
            if (size == Size.Small) return new Vector3(0.4f, 0.4f, 0.4f);

            return Vector3.one;
        }

        private Sprite GetSpriteFromSize(Size size)
        {
            // Загрузка соответствующего спрайта для каждого размера астероида
            switch (size)
            {
                case Size.Small:
                    return Resources.Load<Sprite>("Asteroid_Small");
                case Size.Medium:
                    return Resources.Load<Sprite>("Asteroid_Medium");
                case Size.Large:
                    return Resources.Load<Sprite>("Asteroid_Large");
                case Size.Huge:
                    return Resources.Load<Sprite>("Asteroid_Huge");
                default:
                    return null;
            }
        }
    }
}
