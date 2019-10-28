using Ninject.Modules;
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
            Bind<MainWindow>().To<MainWindow>().InSingletonScope();
            Bind<MapCanvasData>().To<MapCanvasData>().InSingletonScope();
        }
    }
}
