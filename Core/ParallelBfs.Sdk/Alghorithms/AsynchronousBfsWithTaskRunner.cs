﻿namespace ParallelBfs.Sdk.Alghorithms
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Helpers;

    public class AsynchronousBfsWithTaskRunner
    {
        private const int StartNode = 0;

        private ConcurrentQueue<int> nodesToBeVisited;
        private ConcurrentDictionary<int, int> visitedNodes;
        private static object lockObject = new object();
        private ICollection<TaskRunner> runnersPool;

        public AsynchronousBfsWithTaskRunner()
        {
            this.nodesToBeVisited = new ConcurrentQueue<int>();
            this.visitedNodes = new ConcurrentDictionary<int, int>();
            this.runnersPool = new List<TaskRunner>
            {
                new TaskRunner(),
                new TaskRunner(),
                new TaskRunner(),
                new TaskRunner(),
                new TaskRunner(),
                new TaskRunner(),
                new TaskRunner(),
                new TaskRunner(),
                new TaskRunner()
            };
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

                   VisitNode(matrix, nodeToVisit);
                }
            }

            this.runnersPool.ForEach(r => r.Stop());
        }

        private void VisitNode(AdjacencyMatrix matrix, int nodeToVisit)
        {
            while (true)
            {
                TaskRunner runner = this.runnersPool.FirstOrDefault(r => !r.Working);
                if(runner != null)
                {
                    runner.ExecuteAction(() =>
                    {
                        IEnumerable<int> neighbourNodes = matrix.Neighbours(nodeToVisit);

                        AddAllNeighboursToTheQueue(neighbourNodes);

                        
                    });
                    return;
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
            this.visitedNodes.TryAdd(nodeToVisit, nodeToVisit);
        }

        private bool NotAlreadyVisited(int nodeToVisit)
        {
            bool visited = this.visitedNodes.ContainsKey(nodeToVisit);

            return !visited;
        }
    }
}