namespace ParallelBfs.Sdk.Alghorithms
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Helpers;

    public class AsynchronousBfs
    {
        private const int StartNode = 0;

        private ConcurrentQueue<int> nodesToBeVisited;
        private ConcurrentDictionary<int, int> visitedNodes;
        private static object lockObject = new object();

        public AsynchronousBfs()
        {
            this.nodesToBeVisited = new ConcurrentQueue<int>();
            this.visitedNodes = new ConcurrentDictionary<int, int>();
        }

        public bool Search(AdjacencyMatrix matrix)
        {
            Bfs(matrix);

            bool allNodesVisited = this.visitedNodes.Count == matrix.NodesCount();

            return allNodesVisited;
        }

        private void Bfs(AdjacencyMatrix matrix)
        {
            this.nodesToBeVisited.Enqueue(StartNode);

            while (this.visitedNodes.Count != matrix.NodesCount())
            {
                int nodeToVisit = 0;

                if (nodesToBeVisited.TryDequeue(out nodeToVisit) && NotAlreadyVisited(nodeToVisit))
                {
                    MarkAsVisited(nodeToVisit);

                    Task.Run(() => VisitNode(matrix, nodeToVisit))
                        .ContinueWith((result) =>
                        {
                            System.Console.WriteLine(nodeToVisit);
                        });
                }
            }
        }

        private void VisitNode(AdjacencyMatrix matrix, int nodeToVisit)
        {
            IEnumerable<int> neighbourNodes = matrix.Neighbours(nodeToVisit);

            AddAllNeighboursToTheQueue(neighbourNodes);
        }

        private void AddAllNeighboursToTheQueue(IEnumerable<int> neighbourNodes)
        {
            neighbourNodes
                .Where(n => NotAlreadyVisited(n))
                .ForEach(n => this.nodesToBeVisited.Enqueue(n));
        }

        private void MarkAsVisited(int nodeToVisit)
        {
            this.visitedNodes.TryAdd(nodeToVisit, nodeToVisit);
        }

        private bool NotAlreadyVisited(int nodeToVisit)
        {
            bool visited = this.visitedNodes.ContainsKey(nodeToVisit);

            return !visited;
        }
    }
}
