using Ninject.Modules;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<MapCanvasData>().To<MapCanvasData>().InSingletonScope();
            Bind<MainWindow>().To<MainWindow>().InSingletonScope();
            Bind<MainViewModel>().To<MainViewModel>().InSingletonScope();
            Bind<AlgorithmControlViewModel>().To<AlgorithmControlViewModel>().InSingletonScope();
        }
    }
}
