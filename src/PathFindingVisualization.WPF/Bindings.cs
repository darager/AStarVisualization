﻿using System.Windows.Input;
using Ninject.Modules;
using PathFindingVisualization.Core.PathSolvers;
using PathFindingVisualization.WPF.Commands;
using PathFindingVisualization.WPF.Commands.MapEditing;
using PathFindingVisualization.WPF.ViewModels;

namespace PathFindingVisualization.WPF
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommand>().To<ClearMapCommand>().Named("ClearMapCommand");
            Bind<ICommand>().To<PlaceStartCommand>().Named("PlaceStartCommand");
            Bind<ICommand>().To<PlaceGoalCommand>().Named("PlaceGoalCommand");
            Bind<ICommand>().To<PlaceTileCommand>().Named("PlaceTileCommand");
            Bind<ICommand>().To<ProcessMouseMovementCommand>().Named("ProcessMouseMovementCommand");
            Bind<ICommand>().To<RemoveTileCommand>().Named("RemoveTileCommand");

            Bind<ICommand>().To<StartAlgorithmCommand>().Named("StartAlgorithmCommand"); // HACK: for testing purposes

            Bind<AlgorithmControlViewModel>().To<AlgorithmControlViewModel>().InSingletonScope();
            Bind<MainViewModel>().To<MainViewModel>().InSingletonScope();
            Bind<MainWindow>().To<MainWindow>().InSingletonScope();
            Bind<IPathSolverFactory>().To<PathSolverFactory>().InSingletonScope();
        }
    }
}
