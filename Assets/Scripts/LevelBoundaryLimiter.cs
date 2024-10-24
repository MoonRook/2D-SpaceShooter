using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Ограничитель позиции. Работает в связке со скриптом LevelBoundary, если такой есть на сцене
    /// Прикрепляестся к объекту, который нужно ограничить
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (LevelBoundary.Instance == null) return; // Проверяем наличие на сцене ограничителя LevelBoundary

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

