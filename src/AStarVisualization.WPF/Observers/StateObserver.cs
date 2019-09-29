using AStarVisualization.WPF.Observers.Helpers;
using System;

namespace AStarVisualization.WPF.AStarAlgorithm
{
    public class StateObserver : IObserver
    {
        private State lastState;

        public StateObserver()
        {
            lastState = State.HasNotStarted;
        }

        public void StartObserving()
        {
            AStarValues.AlgorithmStateChanged += HandleAlgorithmStateChange;
        }
        public void StopObserving()
        {
            AStarValues.AlgorithmStateChanged -= HandleAlgorithmStateChange;
        }

        private void HandleAlgorithmStateChange(object sender, EventArgs e)
        {
            State state = AStarValues.AStarState;

            CallProperEvent(state, lastState);
        }
        private void CallProperEvent(State currentState, State lastState)
        {
            this.lastState = currentState;

            if (currentState == State.HasToBeReset)
                CallEvent(ResetAlgorithm);
            else if (currentState == State.Finished)
                CallEvent(AlgorithmIsDone);
            else if (lastState == State.HasNotStarted && currentState == State.Run)
                CallEvent(StartAlgorithm);
            else if (lastState == State.Run && currentState == State.Pause)
                CallEvent(PauseAlgorithm);
            else if (lastState == State.Pause && currentState == State.Run)
                CallEvent(ContinueAlgorithm);
        }

        public void CallEvent(Delegate eventHandler)
        {
            if (eventHandler != null)
                eventHandler.DynamicInvoke(null, EventArgs.Empty);
        }
        public static event EventHandler StartAlgorithm;
        public static event EventHandler PauseAlgorithm;
        public static event EventHandler ContinueAlgorithm;
        public static event EventHandler ResetAlgorithm;
        public static event EventHandler AlgorithmIsDone;
    }
}
