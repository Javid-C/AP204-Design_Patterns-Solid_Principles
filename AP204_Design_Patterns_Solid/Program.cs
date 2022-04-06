using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Xml2CSharp;

namespace AP204_Design_Patterns_Solid
{
    class Program
    {
        static void Main(string[] args)
        {
            #region XML
            //using (StreamReader sr = new StreamReader(@"C:\Users\Lenovo\source\repos\AP204_Design_Patterns_Solid\AP204_Design_Patterns_Solid\XMLFile1.xml"))
            //{
            //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(ValCurs));

            //    ValCurs valCurs = (ValCurs)xmlSerializer.Deserialize(sr);

            //    //foreach (var valType in valCurs.ValType)
            //    //{
            //    //    foreach (var valute in valType.Valute)
            //    //    {
            //    //        Console.WriteLine(valute.Name + "- " + valute.Nominal + "-" + valute.Value);
            //    //    }
            //    //}

            //    ValType valType = valCurs.ValType.FirstOrDefault(x => x.Type == "Xarici valyutalar");

            //    Valute valute = valType.Valute.FirstOrDefault(x=>x.Code == "USD");

            //    if(valute == null)
            //    {
            //        return;
            //    }

            //    Console.WriteLine(valute.Code + " - " + valute.Name + " - " + valute.Value);

            #endregion
            #region Singleton
            //Singleton singleton = Singleton.getInstance();
            //Singleton singleton1 = Singleton.getInstance();
            //Console.WriteLine(singleton == singleton1);
            #endregion

            #region Adapter
            //Customer customer = new Customer();

            //customer.Order();

            //Customer customer1 = new Adapter();

            //customer1.Order();
            #endregion


            Student student = new Student();

            student.AddObserver(new FamilyObserver());
            student.AddObserver(new UniversityObserver());
            student.AddObserver(new AcademyObserver());


            Console.WriteLine("Telebenin balini qeyd edin");
            byte number;
            bool result = byte.TryParse(Console.ReadLine(), out number);

            if (result)
            {
                student.Point = number;
                Console.WriteLine("Telebenin bali " + student.Point);
            }
            else
            {
                Console.WriteLine("Duzgun reqem daxil edin");
            }
        }

        #region Singleton
        sealed class Singleton
        {
            private static Singleton _instance;



            public static Singleton getInstance()
            {
                if (_instance == null)
                {
                    _instance = new Singleton();
                }
                return _instance;
            }
        }
        #endregion

        #region Adapter
        class Customer
        {
            public virtual void Order()
            {
                double bookPrice = 12.5;
                double bookMark = 2.4;

                Console.WriteLine("Kitabin qiymeti: " + bookPrice);
                Console.WriteLine("Elfecinin qiymeti : " + bookMark);
            }
        }

        class Service
        {
            public void Book()
            {
                Console.WriteLine("Kitabin adi: Aklinda bir sayi tut \n Kitabin qiymeti: 15.4 \n Sehifelerin sayi: 240");
            }

            public void Bookmark()
            {
                Console.WriteLine("Elfecinin kateqoriyasi: Fantastika \n Elfecinin qiymeti: 1.7 \n Olcusu: 5sm x 20sm");
            }
        }

        class Adapter : Customer
        {
            public Service Adapte = new Service();

            public override void Order()
            {
                Adapte.Book();
                Adapte.Bookmark();
            }
        }
        #endregion

        #region Observer
        interface IObserver
        {
            void Update();
        }

        class FamilyObserver : IObserver
        {
            public void Update()
            {
                Console.WriteLine("Gelin aparin bunu");
            }
        }

        class UniversityObserver : IObserver
        {
            public void Update()
            {
                Console.WriteLine("Geriye qayidan biri var");
            }
        }

        class AcademyObserver : IObserver
        {
            public void Update()
            {
                Console.WriteLine("Akademiyaya xeber verildi");
            }
        }

        class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            private byte _point;
            public byte Point
            {
                get => _point;
                set
                {
                    if(value < 51)
                    {
                        Notify();
                        _point = value;
                    }
                    else
                    {
                        _point = value;
                    }
                }
            }

            public List<IObserver> observers;

            public Student()
            {
                observers = new List<IObserver>();
            }

            public void AddObserver(IObserver observer)
            {
                observers.Add(observer);
            }

            public void RemoveObserver(IObserver observer)
            {
                observers.Remove(observer);
            }

            public void Notify()
            {
                //foreach (var observer in observers)
                //{
                //    observer.Update();
                //}

                observers.ForEach(x => x.Update());
            }
        }
        #endregion
    }
}
