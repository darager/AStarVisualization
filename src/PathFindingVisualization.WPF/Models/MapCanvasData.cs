using System.Collections.Generic;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.WPF.Models
{
    public class MapCanvasData
    {
        public Map Map;
        public List<Node> Path;

        public Node Start = null;
        public Node Goal = null;

        public MapCanvasData()
        {
            Map = new Map();
            Path = new List<Node>();
        }
    }
}
