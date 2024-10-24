using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour // Перечисление вариантов поведения корабля
        {
            Null,  // Бездействие
            Patrol // Патрулирование
        }

        [SerializeField] private AIBehaviour m_AIBehaviour; // Задает тип поведения

        [SerializeField] private AIPointerPatrol m_PatrolPoint; // Задает границу патрулирования AI корабля

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLiner; // Ограничение скорости перемещения

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular; // Ограничение скорости вращения

        [SerializeField] private float m_RandomSelectMovePointTime; // Величина изменения позиции

        [SerializeField] private float m_FindNewTargetTime; // Величина изменения цели

        [SerializeField] private float m_ShootDelay; // Величина изменения стрельбы

        [SerializeField] private float m_EvadeRayLength; // Длинна луча

        private SpaceShip m_SpaceShip; // Ссылка на корабль

        private Vector3 m_MovePosition; // Целевая точка движения

        private Destructible m_SelectedTarget; // Ссылка на объект слежения (выбранная цель)

        private Timer m_RandomizeDirectionTimer;
        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;

        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>(); // Получает ссылку на компонент SpaceShip

            InitTimers(); // Инициализируем таймеры
        }
        
        private void Update()
        {
            UpdateTimers(); // Обновляет таймер

            UpdateAI(); // Обновляет AI
        }

        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol) // Если тип поведения патрулирование
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol() // Обновляет тип поведения патрулирование
        {
            ActionFindNewMovePosition(); // Обновляет целеваю точку движения
            ActionControlShip(); // Управляет тягой AI корабля
            ActionFindNewAttakTarget(); // Находит новую цель для атаки
            ActionFire(); // Стреляет по цели
            ActionEvadeCollision(); // Избегает столкновений
        }

        private List<Vector3> m_PatrolRoute = new List<Vector3>(); // список точек маршрута
        private int m_CurrentPatrolPointIndex; // индекс текущей точки маршрута

        // Упреждает движения AI
        private void MakeLead(Vector3 targetPosition, Vector3 targetVelocity, float projectileSpeed)
        {
            // Вычиляем время полета снаряда до цели
            float timeTarget = Vector3.Distance(transform.position, targetPosition) / projectileSpeed;

            // Вычисляем будущую позицию цели на момент подлета снаряда до нее
            Vector3 leadPosition = targetPosition + (targetVelocity * timeTarget);

            // Устанавливаем точку движения корабля как будущую позицию цели
            m_MovePosition = leadPosition;
        }

        private void ActionFindNewMovePosition()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol) // Если тип поведения патрулирование
            {
                if (m_SelectedTarget != null) // Если объект слежения (выбранная цель) не = 0 
                {
                    // Целевая точка движения = позиции объекта слежения
                    //m_MovePosition = m_SelectedTarget.transform.position; 
                    
                    Rigidbody2D targetRigidbody = m_SelectedTarget.GetComponent<Rigidbody2D>();
                    
                    if (targetRigidbody != null)
                    {
                        Vector3 targetVelocity = m_SelectedTarget.GetComponent<Rigidbody2D>().velocity;
                        float projectileSpeed = 10f; // Должно соответствовать скорости снаряда

                        MakeLead(m_SelectedTarget.transform.position, targetVelocity, projectileSpeed);
                    }                     
                }
                else
                {
                    if (m_PatrolPoint != null) // Если граница патрулирования AI корабля не = 0
                    {
                        // Проверка находится ли корабль в зоне патрулирования
                        // = дистанция от центральной точки зоны патрулирования
                        // < радиуса границы зоны патрулирования AI корабля
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude
                            < m_PatrolPoint.Radius * m_PatrolPoint.Radius;

                        if (isInsidePatrolZone == true) // Если корабль находится в зоне патрулирования
                        {
                            if (m_RandomizeDirectionTimer.isFinished == true)
                            {
                                // Получает случайную точку в зоне патрулирования * на радиус зоны патрулирования
                                // + смещает относительно центра зоны патрулирования 
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius
                                    + m_PatrolPoint.transform.position;

                                m_MovePosition = newPoint;

                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                            if (m_PatrolRoute.Count > 0) // Проверяем наличие точек маршрута
                            {
                                // Если достигнут конец списка, переходим к первой точке маршрута.
                                if (m_CurrentPatrolPointIndex >= m_PatrolRoute.Count)
                                {
                                    m_CurrentPatrolPointIndex = 0;
                                }
                                // Целевая точка движения = индекс текущей точки маршрута
                                m_MovePosition = m_PatrolRoute[m_CurrentPatrolPointIndex];

                                // Увеличиваем индекс текущей точки маршрута для следующего шага патрулирования
                                m_CurrentPatrolPointIndex++;

                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                        }
                        else
                        {
                            // Целевая точка движения = текущая точка маршрута
                            m_MovePosition = m_PatrolPoint.transform.position;
                        }
                    }
                } 
            }
        }
        private void ActionEvadeCollision() // Избегает столкновений
        {
            // Бросает луч определенной длинны
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == true)
            {
                // Если луч утыкается в препятствие, корабль осуществляет поворот вправо 
                m_MovePosition = transform.position + transform.right * 100.0f; 
            }
        }
        
        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLiner; // Задает ограничение скорости перемещения

            // Задает ограничение скорости вращения
            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) 
                * m_NavigationAngular;
        }

        private const float MAX_ANGLE = 45.0f;
        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship) // Возвращает угол
        {
            // Переводит пзицию в локальные координаты
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition); 

            // Получает угол между двумя векторами вектор целевого назначения, вектор движения вверх
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            // Ограничивает угол мах поровота
            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }

        /// <summary>
        /// Находит новую цель для атаки
        /// </summary>
        private void ActionFindNewAttakTarget()
        {
            if (m_FindNewTargetTimer != null) // 
            {
                if (m_FindNewTargetTimer.isFinished == true) // 
                {
                    m_SelectedTarget = FindNearestDestructibleTarget();

                    m_FindNewTargetTimer.Start(m_ShootDelay); // 
                }
            }
        }
        private void ActionFire()
        {
            if (m_SelectedTarget != null) // Выбраная цель не = 0
            {
                if (m_FireTimer.isFinished == true) // Если таймер = 0
                {
                    m_SpaceShip.Fire(TurretMode.Primary); // Разрешает стрельбу из основного оружия

                    m_FireTimer.Start(m_ShootDelay); // Перезапускает таймер
                }
            }
        }

        // Возвращает ближайший разрушаемый объект (поиск ближайшего пути до разрушаемого объекта)
        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = float.MaxValue; // Максимальная дистанция

            Destructible potentialTarget = null; // Потенциальная разрущаемая цель = 0

            foreach (var v in Destructible.AllDestrutibles) // Обращение к списку разрущаемых объктов
            {
                // Если объект нашел сам себя, то переключается на следующий объект в списке
                if (v.GetComponent<SpaceShip>() == m_SpaceShip) continue;

                // Если найден объект из нейтральной команды, то переключается на следующий объект в списке
                if (v.TeamId == Destructible.TeamIdNeutral) continue;

                // Если найден объект из своей команды, то переключается на следующий объект в списке
                if (v.TeamId == m_SpaceShip.TeamId) continue;

                // Получает дистанцию = Vector2 (позиция корабля, позиция объекта из списка разрущаемых объктов)
                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);

                // Если дистанция текущего объекта < mаксимальная дистанция
                if (dist < maxDist)
                {
                    maxDist = dist; // Максимальная дистанция = текущей дистанции

                    potentialTarget = v; // Потенциальная цель = объект из списка разрущаемых объктов
                }
            }

            return potentialTarget;
        }

        #region Timer

        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
        }

        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
        }

        public void SetPatrolBehaviour(AIPointerPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol; // Тип поведения патрулирование

            m_PatrolPoint = point; // Граница патрулирования AI корабля в заданной точке
        }

        #endregion
    }
}

