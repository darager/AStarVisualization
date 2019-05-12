# AStarVisualization #

This visualization was built using C# and WPF.

The main advantage that the A* algorithm has over other pathfinding algorithms is that the path can be found faster, however it might not be the shortest path. That is acceptable since A* is a compromise between computational speed and finding the shortest path available.

If you want to find the absolutely shortest path use Dijkstra instead of A*.

---
## Heuristic :

This particular implementation of the A* Pathfinding Algorithm uses the popular **Manhattan distance** which is the standard heuristic for square grid layouts.

```
function heuristic(node) =
   dx = abs(node.x - goal.x)
   dy = abs(node.y - goal.y)
   return D * (dx + dy)
```
---

## Examples :

![](/images/AStarExample1.png)

![](/images/AStarExample2.png)

---


© Rager 2019