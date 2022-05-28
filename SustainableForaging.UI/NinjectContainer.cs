using Ninject;
using SustainableForaging.BLL;
using SustainableForaging.Core.Repositories;
using SustainableForaging.DAL;
using System;
using System.IO;

namespace SustainableForaging.UI
{
    class NinjectContainer
    {
        public static StandardKernel kernel {get; private set;}

        public static void Configure()
        {
            kernel = new StandardKernel();

            kernel.Bind<ConsoleIO>().To<ConsoleIO>();
            kernel.Bind<View>().To<View>();

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string forageFileDirectory = Path.Combine(projectDirectory, "data", "forage_data");
            string foragerFilePath = Path.Combine(projectDirectory, "data", "foragers.csv");
            string itemFilePath = Path.Combine(projectDirectory, "data", "items.txt");

            kernel.Bind<IForageRepository>().To<ForageFileRepository>().WithConstructorArgument(forageFileDirectory);
            kernel.Bind<IForagerRepository>().To<ForagerFileRepository>().WithConstructorArgument(foragerFilePath);
            kernel.Bind<IItemRepository>().To<ItemFileRepository>().WithConstructorArgument(itemFilePath);

            kernel.Bind<ForagerService>().To<ForagerService>();
            kernel.Bind<ForageService>().To<ForageService>();
            kernel.Bind<ItemService>().To<ItemService>();

        }
    }
}
