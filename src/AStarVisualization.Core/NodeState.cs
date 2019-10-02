namespace AStarVisualization.Core
{
    public enum NodeState // TODO: implement statechange when added to the openset and removed from the openSet
    {
        Wall,
        Start,
        Goal,
        Ground,
        GroundToBeVisited, // when in openSet
        GroundVisited // when moved out of openSet
    }
}