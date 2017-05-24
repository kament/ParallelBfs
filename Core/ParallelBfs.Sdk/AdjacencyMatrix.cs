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
    }
}
