using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventSystem : MonoBehaviour
{
    class Person // Класс инициирующий событие OnDeath
    {
        public event EventHandler<EventArgs> OnDeath;
        
        public void Death()
        {
            Console.WriteLine("Он погиб");

            if (OnDeath != null)
                OnDeath(this, new EventArgs());
        }
    }

    class UI
    {
        public Person P; // Ссылка на класс Person

        public UI(Person _P)
        {
            P = _P;
            P.OnDeath += P_OnDeath; // Подписка на событие OnDeath
        }

        void P_OnDeath(object sender, EventArgs e)
        {
            ShowDeath(); // вызываем метод ShowDeath
            P.OnDeath -= P_OnDeath;// Отписка от события
        }

        public void ShowDeath()
        {
            Console.WriteLine("Показываем интерфейс");
        }
    }
    class Music
    {
        public Person P; // Ссылка на класс Person

        public Music(Person _P)
        {
            P = _P;
            P.OnDeath += P_OnDeath; // Подписка на событие OnDeath
        }

        void P_OnDeath(object sender, EventArgs e)
        {
            StartDeath(); // вызываем метод StartDeath
            P.OnDeath -= P_OnDeath;// Отписка от события
        }

        public void StartDeath()
        {
            Console.WriteLine("Включаем грустную музыку");
        }
    }
    class MainClass
    {
        public static void Main(string[] args)
        {
            Person p = new Person();

            UI u = new UI(p);

            p.Death();
        }
    }
}
