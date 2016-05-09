using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MiniIOC.Framwork;
using MiniIOC.Framwork.Dependency;

namespace DependencyTest
{
    class Program
    {
        static void Main(string[] args)
        {
           //MemberInfo[] res= typeof(TestClass).GetFields(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            //MemberInfo[] res = typeof(OperationMain).GetFields(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static|BindingFlags.Instance);

            //var res = typeof(OperationMain).GetConstructors()[0].GetCustomAttributes(typeof(DependencyAttribute),false);//.GetType().Equals(typeof(DependencyAttribute));
            test1(args);
        
        }
        static IMiniContainer container = MiniIocContainer.Instance;
        static void init()
        {
            container.Regiter<IPlayer, Player>();
            container.Regiter<IMediaFile, MediaFile>();
        }
        static void test1(string[] args)
        {
            //第一种注入方式
            //init();

            OperationMain op1 = container.Resolve<OperationMain>();
            op1.PlayMedia();
            OperationMain op3 = container.Resolve<OperationMain>();
            op3._mtype.FilePath = "testpath";
            op3.PlayMedia();

            //普通方式
            OperationMain op2 = new OperationMain(new Player(), new MediaFile());
            op2.PlayMedia();

            Console.Read();
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
            get
            {
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
}
