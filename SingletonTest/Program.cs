using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniIOC.Common;

namespace SingletonTest
{
    class Program
    {
    
        static void Main(string[] args)
        {
            Console.WriteLine(Singleton<TestClass>.Instance.GetCount());
            Console.WriteLine(Singleton<TestClass>.Instance.GetCount());
            Console.WriteLine(Singleton<TestClass>.Instance.GetCount());
            Console.WriteLine(Singleton<TestClass>.Instance.GetCount());
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(TestClass.Instance.GetCount());
            Console.WriteLine(TestClass.Instance.GetCount());
            Console.WriteLine(TestClass.Instance.GetCount());
            Console.WriteLine(TestClass.Instance.GetCount());
            Console.WriteLine("2----------------------------------------");
            Console.WriteLine(Singleton<TestClass2>.Instance.GetCount());
            Console.WriteLine(Singleton<TestClass2>.Instance.GetCount());
            Console.WriteLine(Singleton<TestClass2>.Instance.GetCount());
            Console.WriteLine(Singleton<TestClass2>.Instance.GetCount());
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(TestClass2.Instance.GetCount());
            Console.WriteLine(TestClass2.Instance.GetCount());
            Console.WriteLine(TestClass2.Instance.GetCount());
            Console.WriteLine(TestClass2.Instance.GetCount());
            Console.ReadLine();
        }
    }

    class TestClass
    {
        private int Count = 0;
        public TestClass()
        {
            //Count++;
        }
        private static TestClass _instance;
        public static TestClass Instance
        { 
            get{
                if (_instance == null)
                    _instance = new TestClass();//Singleton<TestClass>.Instance;
                return _instance;
            }
        }

        public int GetCount()
        {
            return Count++;
        }
    }
    class TestClass2
    {
        private int Count = 0;
        public TestClass2()
        {
            //Count++;
        }
        private static TestClass2 _instance;
        public static TestClass2 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TestClass2();//Singleton<TestClass>.Instance;
                return _instance;
            }
        }

        public int GetCount()
        {
            return Count++;
        }
    }
}
