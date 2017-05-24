namespace ParallelBfs.Sdk
{
    using System;
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
            throw new NotImplementedException();
        }
    }
}
