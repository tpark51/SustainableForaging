using Ninject;
using Ninject.Modules;
using SustainableForaging.BLL;
using SustainableForaging.DAL;
using System;
using System.IO;

namespace SustainableForaging.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NinjectContainer.Configure();
            Controller controller = NinjectContainer.kernel.Get<Controller>();
            controller.Run();
        }

    }
}
