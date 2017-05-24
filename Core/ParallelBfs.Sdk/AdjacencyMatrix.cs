namespace ParallelBfs.Sdk
{
    using System.Collections.Generic;

    public class AdjacencyMatrix
    {
        private List<List<bool>> matrix;

        internal AdjacencyMatrix(List<List<bool>> matrix)
        {
            this.matrix = matrix;
        }

        internal int NodesCount()
        {
            int count = this.matrix.Count;

            return count;
        }

        internal IEnumerable<int> Neighbours(int nodeToVisit)
        {
            List<bool> neighboursNodes = matrix[nodeToVisit];

            for (int nodeId = 0; nodeId < neighboursNodes.Count; nodeId++)
            {
                if(IsNeighbour(neighboursNodes, nodeId))
                {
                    yield return nodeId;
                }
            }
        }

        private bool IsNeighbour(List<bool> neighboursNodes, int nodeId)
        {
            return neighboursNodes[nodeId];
        }
    }
}
