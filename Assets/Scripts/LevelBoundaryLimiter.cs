using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ������������ �������. �������� � ������ �� �������� LevelBoundary, ���� ����� ���� �� �����
    /// �������������� � �������, ������� ����� ����������
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (LevelBoundary.Instance == null) return; // ��������� ������� �� ����� ������������ LevelBoundary

            var lb = LevelBoundary.Instance;
            var r = LevelBoundary.Instance.Radius;

            if (transform.position.magnitude > r)
            {
                if (lb.LimitMode == LevelBoundary.Mode.Limit)
                {
                    transform.position = transform.position.normalized * r;
                }
                
                if (lb.LimitMode == LevelBoundary.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * r;
                }
            }
        }
    }

}

