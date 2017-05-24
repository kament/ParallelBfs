namespace ParallelBfs.Sdk.Alghorithms
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Helpers;

    public class SynchronousBfs
    {
        private const int StartNode = 0;

        private Queue<int> nodesToBeVisited;
        private HashSet<int> visitedNodes;

        public SynchronousBfs()
        {
            this.nodesToBeVisited = new Queue<int>();
            this.visitedNodes = new HashSet<int>();
        }

        public Task<bool> Search(AdjacencyMatrix matrix)
        {
            Bfs(matrix);

            bool allNodesVisited = this.visitedNodes.Count == matrix.NodesCount();

            return Task.FromResult(allNodesVisited);
        }

        private void Bfs(AdjacencyMatrix matrix)
        {
            this.nodesToBeVisited.Enqueue(StartNode);

            while (nodesToBeVisited.Count != 0)
            {
                int nodeToVisit = nodesToBeVisited.Dequeue();
                
                if (NotAlreadyVisited(nodeToVisit))
                {
                    MarkAsVisited(nodeToVisit);

                    IEnumerable<int> neighbourNodes = matrix.Neighbours(nodeToVisit);

                    AddAllNeighboursToTheQueue(neighbourNodes);
                }
            }
        }

        private void AddAllNeighboursToTheQueue(IEnumerable<int> neighbourNodes)
        {
            neighbourNodes
                .Where(n => NotAlreadyVisited(n))
                .ForEach(n => this.nodesToBeVisited.Enqueue(n));
        }

        private void MarkAsVisited(int nodeToVisit)
        {
            this.visitedNodes.Add(nodeToVisit);
        }

        private bool NotAlreadyVisited(int nodeToVisit)
        {
            bool visited = this.visitedNodes.Contains(nodeToVisit);

            return !visited;
        }
    }
}
