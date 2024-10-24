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
        public enum AIBehaviour // ������������ ��������� ��������� �������
        {
            Null,  // �����������
            Patrol // ��������������
        }

        [SerializeField] private AIBehaviour m_AIBehaviour; // ������ ��� ���������

        [SerializeField] private AIPointerPatrol m_PatrolPoint; // ������ ������� �������������� AI �������

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLiner; // ����������� �������� �����������

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular; // ����������� �������� ��������

        [SerializeField] private float m_RandomSelectMovePointTime; // �������� ��������� �������

        [SerializeField] private float m_FindNewTargetTime; // �������� ��������� ����

        [SerializeField] private float m_ShootDelay; // �������� ��������� ��������

        [SerializeField] private float m_EvadeRayLength; // ������ ����

        private SpaceShip m_SpaceShip; // ������ �� �������

        private Vector3 m_MovePosition; // ������� ����� ��������

        private Destructible m_SelectedTarget; // ������ �� ������ �������� (��������� ����)

        private Timer m_RandomizeDirectionTimer;
        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;

        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>(); // �������� ������ �� ��������� SpaceShip

            InitTimers(); // �������������� �������
        }
        
        private void Update()
        {
            UpdateTimers(); // ��������� ������

            UpdateAI(); // ��������� AI
        }

        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol) // ���� ��� ��������� ��������������
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol() // ��������� ��� ��������� ��������������
        {
            ActionFindNewMovePosition(); // ��������� ������� ����� ��������
            ActionControlShip(); // ��������� ����� AI �������
            ActionFindNewAttakTarget(); // ������� ����� ���� ��� �����
            ActionFire(); // �������� �� ����
            ActionEvadeCollision(); // �������� ������������
        }

        private List<Vector3> m_PatrolRoute = new List<Vector3>(); // ������ ����� ��������
        private int m_CurrentPatrolPointIndex; // ������ ������� ����� ��������

        // ��������� �������� AI
        private void MakeLead(Vector3 targetPosition, Vector3 targetVelocity, float projectileSpeed)
        {
            // �������� ����� ������ ������� �� ����
            float timeTarget = Vector3.Distance(transform.position, targetPosition) / projectileSpeed;

            // ��������� ������� ������� ���� �� ������ ������� ������� �� ���
            Vector3 leadPosition = targetPosition + (targetVelocity * timeTarget);

            // ������������� ����� �������� ������� ��� ������� ������� ����
            m_MovePosition = leadPosition;
        }

        private void ActionFindNewMovePosition()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol) // ���� ��� ��������� ��������������
            {
                if (m_SelectedTarget != null) // ���� ������ �������� (��������� ����) �� = 0 
                {
                    // ������� ����� �������� = ������� ������� ��������
                    //m_MovePosition = m_SelectedTarget.transform.position; 
                    
                    Rigidbody2D targetRigidbody = m_SelectedTarget.GetComponent<Rigidbody2D>();
                    
                    if (targetRigidbody != null)
                    {
                        Vector3 targetVelocity = m_SelectedTarget.GetComponent<Rigidbody2D>().velocity;
                        float projectileSpeed = 10f; // ������ ��������������� �������� �������

                        MakeLead(m_SelectedTarget.transform.position, targetVelocity, projectileSpeed);
                    }                     
                }
                else
                {
                    if (m_PatrolPoint != null) // ���� ������� �������������� AI ������� �� = 0
                    {
                        // �������� ��������� �� ������� � ���� ��������������
                        // = ��������� �� ����������� ����� ���� ��������������
                        // < ������� ������� ���� �������������� AI �������
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude
                            < m_PatrolPoint.Radius * m_PatrolPoint.Radius;

                        if (isInsidePatrolZone == true) // ���� ������� ��������� � ���� ��������������
                        {
                            if (m_RandomizeDirectionTimer.isFinished == true)
                            {
                                // �������� ��������� ����� � ���� �������������� * �� ������ ���� ��������������
                                // + ������� ������������ ������ ���� �������������� 
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius
                                    + m_PatrolPoint.transform.position;

                                m_MovePosition = newPoint;

                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                            if (m_PatrolRoute.Count > 0) // ��������� ������� ����� ��������
                            {
                                // ���� ��������� ����� ������, ��������� � ������ ����� ��������.
                                if (m_CurrentPatrolPointIndex >= m_PatrolRoute.Count)
                                {
                                    m_CurrentPatrolPointIndex = 0;
                                }
                                // ������� ����� �������� = ������ ������� ����� ��������
                                m_MovePosition = m_PatrolRoute[m_CurrentPatrolPointIndex];

                                // ����������� ������ ������� ����� �������� ��� ���������� ���� ��������������
                                m_CurrentPatrolPointIndex++;

                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                        }
                        else
                        {
                            // ������� ����� �������� = ������� ����� ��������
                            m_MovePosition = m_PatrolPoint.transform.position;
                        }
                    }
                } 
            }
        }
        private void ActionEvadeCollision() // �������� ������������
        {
            // ������� ��� ������������ ������
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == true)
            {
                // ���� ��� ��������� � �����������, ������� ������������ ������� ������ 
                m_MovePosition = transform.position + transform.right * 100.0f; 
            }
        }
        
        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLiner; // ������ ����������� �������� �����������

            // ������ ����������� �������� ��������
            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) 
                * m_NavigationAngular;
        }

        private const float MAX_ANGLE = 45.0f;
        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship) // ���������� ����
        {
            // ��������� ������ � ��������� ����������
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition); 

            // �������� ���� ����� ����� ��������� ������ �������� ����������, ������ �������� �����
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            // ������������ ���� ��� ��������
            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }

        /// <summary>
        /// ������� ����� ���� ��� �����
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
            if (m_SelectedTarget != null) // �������� ���� �� = 0
            {
                if (m_FireTimer.isFinished == true) // ���� ������ = 0
                {
                    m_SpaceShip.Fire(TurretMode.Primary); // ��������� �������� �� ��������� ������

                    m_FireTimer.Start(m_ShootDelay); // ������������� ������
                }
            }
        }

        // ���������� ��������� ����������� ������ (����� ���������� ���� �� ������������ �������)
        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = float.MaxValue; // ������������ ���������

            Destructible potentialTarget = null; // ������������� ����������� ���� = 0

            foreach (var v in Destructible.AllDestrutibles) // ��������� � ������ ����������� �������
            {
                // ���� ������ ����� ��� ����, �� ������������� �� ��������� ������ � ������
                if (v.GetComponent<SpaceShip>() == m_SpaceShip) continue;

                // ���� ������ ������ �� ����������� �������, �� ������������� �� ��������� ������ � ������
                if (v.TeamId == Destructible.TeamIdNeutral) continue;

                // ���� ������ ������ �� ����� �������, �� ������������� �� ��������� ������ � ������
                if (v.TeamId == m_SpaceShip.TeamId) continue;

                // �������� ��������� = Vector2 (������� �������, ������� ������� �� ������ ����������� �������)
                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);

                // ���� ��������� �������� ������� < m����������� ���������
                if (dist < maxDist)
                {
                    maxDist = dist; // ������������ ��������� = ������� ���������

                    potentialTarget = v; // ������������� ���� = ������ �� ������ ����������� �������
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
            m_AIBehaviour = AIBehaviour.Patrol; // ��� ��������� ��������������

            m_PatrolPoint = point; // ������� �������������� AI ������� � �������� �����
        }

        #endregion
    }
}

