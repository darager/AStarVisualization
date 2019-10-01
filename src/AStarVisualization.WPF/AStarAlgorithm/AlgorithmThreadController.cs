using AStarVisualization.WPF.AStarAlgorithm.AStarImplementation.Algorithmthread;
using AStarVisualization.WPF.Controllers;
using System;
using System.Threading;
using System.Windows.Threading;

namespace AStarVisualization.WPF.AStarAlgorithm.AStarImplementation
{
    public class AlgorithmThreadController : IController
    {
        private static Dispatcher MainDispatcher;
        private Thread AlgorithmThread;
        private AlgorithmController algorithmController;

        public AlgorithmThreadController(Dispatcher mainThreadDispatcher)
        {
            MainDispatcher = mainThreadDispatcher;
        }

        public void StartControlling()
        {
            StateObserver.StartAlgorithm += StartThread;
            StateObserver.ResetAlgorithm += ResetThread;
            AStarValues.GridDimensionChanged += ResetThread;
        }
        public void StopControlling()
        {
            StateObserver.StartAlgorithm -= StartThread;
            StateObserver.ResetAlgorithm -= ResetThread;
            AStarValues.GridDimensionChanged -= ResetThread;
        }

        private void StartThread(object sender, EventArgs e)
        {
            if (AlgorithmThread != null && AlgorithmThread.IsAlive)
                return;

            AlgorithmThread = new Thread(() =>
            {
                algorithmController = new AlgorithmController(MainDispatcher);
            });
            this.AlgorithmThread.Start();
        }
        private void ResetThread(object sender, EventArgs e)
        {
            CallEvent(StopThread, EventArgs.Empty);
            this.AlgorithmThread?.Abort();
            this.algorithmController?.Reset();

            AStarValues.AStarState = State.HasNotStarted;
        }

        private void CallEvent(Delegate handler, EventArgs args)
        {
            if (handler != null)
                handler.DynamicInvoke(null, args);
        }
        public static event EventHandler StopThread;
    }
}