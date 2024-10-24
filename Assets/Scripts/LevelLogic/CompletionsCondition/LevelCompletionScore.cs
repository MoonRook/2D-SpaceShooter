using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelCompletionScore : LevelCondition
    {
        [SerializeField] private int m_Score; // Количество очков
        public override bool IsCompleted // Выполняет проверкку на логику завершения уровня
        {
            get
            {
                // Проверяет если сейчас активный корабль
                if (Player.Instance.ActiveShip == null) return false;
                
                // Если значение очков игрока больше указанного знaчения возврщает true
                if (Player.Instance.Score >= m_Score)
                {
                    return true;
                }
                
                return false;
            }
        }
    }
}

