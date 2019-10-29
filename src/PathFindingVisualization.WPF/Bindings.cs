using Ninject.Modules;
using PathFindingVisualization.WPF.Commands;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;
using System.Windows.Input;

namespace PathFindingVisualization.WPF
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {

            Bind<AlgorithmControlViewModel>().To<AlgorithmControlViewModel>().InSingletonScope();
            Bind<MainViewModel>().To<MainViewModel>().InSingletonScope();

            Bind<ICommand>().To<ClearMapCommand>().Named("ClearMapCommand");
            Bind<ICommand>().To<PlaceTileCommand>().Named("PlaceTileCommand");
            Bind<ICommand>().To<RemoveTileCommand>().Named("RemoveTileCommand"); // Singleton?

            Bind<MainWindow>().To<MainWindow>().InSingletonScope();
            Bind<MapCanvasData>().To<MapCanvasData>().InSingletonScope();
            Bind<MapEditor>().To<MapEditor>().InSingletonScope();
        }
    }
}
