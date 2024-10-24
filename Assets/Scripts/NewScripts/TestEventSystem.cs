using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventSystem : MonoBehaviour
{
    class Person // ����� ������������ ������� OnDeath
    {
        public event EventHandler<EventArgs> OnDeath;
        
        public void Death()
        {
            Console.WriteLine("�� �����");

            if (OnDeath != null)
                OnDeath(this, new EventArgs());
        }
    }

    class UI
    {
        public Person P; // ������ �� ����� Person

        public UI(Person _P)
        {
            P = _P;
            P.OnDeath += P_OnDeath; // �������� �� ������� OnDeath
        }

        void P_OnDeath(object sender, EventArgs e)
        {
            ShowDeath(); // �������� ����� ShowDeath
            P.OnDeath -= P_OnDeath;// ������� �� �������
        }

        public void ShowDeath()
        {
            Console.WriteLine("���������� ���������");
        }
    }
    class Music
    {
        public Person P; // ������ �� ����� Person

        public Music(Person _P)
        {
            P = _P;
            P.OnDeath += P_OnDeath; // �������� �� ������� OnDeath
        }

        void P_OnDeath(object sender, EventArgs e)
        {
            StartDeath(); // �������� ����� StartDeath
            P.OnDeath -= P_OnDeath;// ������� �� �������
        }

        public void StartDeath()
        {
            Console.WriteLine("�������� �������� ������");
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
