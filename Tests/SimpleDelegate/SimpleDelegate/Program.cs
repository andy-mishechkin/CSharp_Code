using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Delegates as event enablers *****\n");
            // Сначала создадим объект Car. 
            Car cl = new Car("SlugBug", 100, 10);
            // Теперь сообщим ему, какой метод вызывать, 
            // когда он захочет отправить сообщение. 
            cl.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));
            cl.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent2));

            // Ускорим (это инициирует события). 
            Console.WriteLine("***** Speeding up *****");
            for (int I = 0; I < 6; I++)
                cl.Accelerate(20);
            Console.ReadLine();
        }
        // Это цель для входящих сообщений. 
        public static void OnCarEngineEvent(string msg)
        {
            Console.WriteLine("\n***** Message From Car Object *****");
            Console.WriteLine("=> {0}", msg);
        }
        public static void OnCarEngineEvent2(string msg)
        {
            Console.WriteLine("=> {0}", msg.ToUpper());
        }

    }

    public class Car
    {
        // Данные состояния. 
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public string PetName { get; set; }

        // Исправен ли автомобиль? 
        private bool carlsDead;
        
        // Конструкторы класса, 
        public Car() { MaxSpeed = 100; }
        public Car(string name, int maxSp, int currSp)
        { 
            CurrentSpeed = currSp;
            MaxSpeed = maxSp; 
            PetName = name; 
        }
        // 1. Определить тип делегата. 
        public delegate void CarEngineHandler(string msgForCaller);
        //2. Определить переменную-член типа этого делегата.
        private CarEngineHandler listOfHandlers;
        // 3. Добавить регистрационную функцию для вызывающего кода. 
        public void RegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers += methodToCall;
        }

        public void Accelerate(int delta)
        {
            // Если этот автомобиль сломан, отправить сообщение об этом, 
            if (carlsDead)
            {
                if (listOfHandlers != null) 
                    listOfHandlers("Sorry, this car is dead...");
            }
            else
            {
                CurrentSpeed += delta;
                // Автомобиль почти сломан? 
                if (10 == (MaxSpeed - CurrentSpeed) && listOfHandlers != null)
                {
                    listOfHandlers("Careful buddy! Gonna blow!");
                }
                if (CurrentSpeed >= MaxSpeed)
                    carlsDead = true;
                else
                    Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);
            }
        }
    }
}
