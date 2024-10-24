using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaseShooter
{
    public class ExplosionEffect : MonoBehaviour
    {
        [SerializeField] private GameObject m_ExplosionPrefab; // ������ �� ������ ������
        [SerializeField] private float m_ExplosionDelay = 0.1f; // �������� ����� ���������������� ������

        public void PlayExplosion(Transform targetTransform)
        {
            StartCoroutine(PlayExplosionCoroutine(targetTransform));
        }

        private IEnumerator PlayExplosionCoroutine(Transform targetTransform)
        {
            yield return new WaitForSeconds(m_ExplosionDelay);
            Instantiate(m_ExplosionPrefab, targetTransform.position, Quaternion.identity);
        }
    }
}
