using System;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using AStarVisualization.Core.PathSolvers;

namespace AStarVisualization.WPF.Cleanup
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IPathSolverFactory>().To<PathSolverFactory>().InSingletonScope();
        }
    }
}
