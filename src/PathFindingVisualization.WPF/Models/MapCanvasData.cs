using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using System.Collections.Generic;

namespace PathFindingVisualization.WPF.Models
{
    public class MapCanvasData
    {
        public Map Map;
        public List<Node> Path;

        public MapCanvasData()
        {
            Map = new Map();
            Path = new List<Node>();
        }
    }
}
