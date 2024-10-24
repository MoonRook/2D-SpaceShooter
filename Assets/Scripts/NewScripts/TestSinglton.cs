using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestSinglton : MonoBehaviour
{
    class Physics
    {
        public static Physics Instance;
        
        public float Gravity;

        public Physics()
        {
            if (Instance != null)
            {
                Console.WriteLine("Класс Physics был создан");
                return;
            }
            Instance = this;
            
            Gravity = 9.8f;
        }
    }
    class Test
    {
        public Test()
        {
            Console.WriteLine(Physics.Instance.Gravity);
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Physics p = new Physics();
           
            Test t = new Test();

            Console.WriteLine(Physics.Instance.Gravity);
        }
    }
}
