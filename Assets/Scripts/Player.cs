using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        public static SpaceShip SelectedSpaceShip;
        
        [SerializeField] private int m_NumLives; // Количество жизней
        [SerializeField] private SpaceShip m_PlayerShipPrefab; // Ссылка на префаб корабля игрока

        public SpaceShip ActiveShip => m_Ship; // Запрет другим кораблям брать Powerup
        
        [SerializeField] private CameraController m_CameraController; // Следит за кораблем
        [SerializeField] private MovementController m_MovementController; // Управляет кораблем

        private SpaceShip m_Ship; // Ссылка на корабль игрока

        private int m_Score; // Количество очков игрока
        private int m_NumKills; // Количество уничтоженых кораблей

        public int Score => m_Score;
        public int NumKills => m_NumKills;
        public int NumLives => m_NumLives;

        public SpaceShip ShipPrefab
        {
            get
            {
                // Если корабль не выбран
                if (SelectedSpaceShip == null)
                {
                    return m_PlayerShipPrefab; // Задает ссылку на префаб корабля игрока 
                }
                else
                {
                    return SelectedSpaceShip;
                }
            }
        }

        private void Start()
        {
            Respawn(); // Убеждаемся в смерти корабля игрока
        }

        private void OnShipDeath()
        {
            m_NumLives--; // Отнимаем количество жизней игрока
            
            if (m_NumLives > 0)
                Respawn();
        }

        private void Respawn()
        {
            var newPlayerShip = Instantiate(ShipPrefab); // Создание корабля игрока, т.е. перенос префаба корабля игрока на сцену

            m_Ship = newPlayerShip.GetComponent<SpaceShip>(); // Получение компонента SpaceShip

            m_CameraController.SetTarget(m_Ship.transform); // Передает ссылку на корабль игрока
            m_MovementController.SetTargetShip(m_Ship);
            m_Ship.EventOnDeath.AddListener(OnShipDeath);
        }

        public void AddKill() // Прибавляет количество очков
        {
            m_NumKills += 1;
        }

        public void AddScore(int num) // Прибавляет количество уничтоженых кораблей
        {
            m_Score += num;
        }
    }
}
