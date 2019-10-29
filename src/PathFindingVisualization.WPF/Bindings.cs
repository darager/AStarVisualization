using System.Windows.Input;
using Ninject.Modules;
using PathFindingVisualization.WPF.Commands;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {

            Bind<AlgorithmControlViewModel>().To<AlgorithmControlViewModel>().InSingletonScope();
            Bind<MainViewModel>().To<MainViewModel>().InSingletonScope();

            // Singleton?
            Bind<ICommand>().To<ClearMapCommand>().Named("ClearMapCommand");
            Bind<ICommand>().To<PlaceTileCommand>().Named("PlaceTileCommand");
            Bind<ICommand>().To<RemoveTileCommand>().Named("RemoveTileCommand");

            Bind<MainWindow>().To<MainWindow>().InSingletonScope();
            Bind<MapCanvasData>().To<MapCanvasData>().InSingletonScope();
            Bind<MapEditor>().To<MapEditor>().InSingletonScope();
        }
    }
}
