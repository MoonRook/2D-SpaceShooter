using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField] private float m_Velocity; // Скорость снаряда

        [SerializeField] private float m_Lifetime; // Общее время жизни снаряда

        [SerializeField] private int m_Damage; // Урон

        [SerializeField] private ImpactEffect m_ImpactEffectPrefab; // Визуальный эффект 
        
        private float m_Timer; // время жизни

        private void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity; // Шаг смещения снаряда в каждом кадре * скорость снаряда
           
            Vector2 step = transform.up * stepLength; // Направление сняряда вверх * шаг смещения снаряда в каждом кадре

            // Проверка есть ли на пути луча объект
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                // Получает ссылку на Destructible у родительского объекта корабля
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

                // Если Destructible не равен null или самому себе (чтобы при стрельбе нельзя было попадать в самого себя)
                if (dest != null && dest != m_Parent)
                {
                    dest.ApplyDamage(m_Damage);

                    if (dest.HitPoints <= 0)
                    {
                        // Если выпущенный снаряд является снарядом игрока
                        if (m_Parent == Player.Instance.ActiveShip)
                        {
                            Player.Instance.AddScore(dest.ScoreValue); // Добавляет количество очков из класса Destructible

                            if (dest is SpaceShip)
                            {
                                Player.Instance.AddKill(); // То добавляем игроку уничтоженный объект
                            }
                        }
                    }
                }

                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime; // время жизни + частота обновления кадра

            if (m_Timer > m_Lifetime) // если время жизни больше, чем общее время жизни снаряда, то уничтожаем объект
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0); // Вектор перемещения снаряда
        }

        // Здесь можно добавить визуальный эффект и звуковой эффект
        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }

        private Destructible m_Parent;
        
        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }
    }
}
