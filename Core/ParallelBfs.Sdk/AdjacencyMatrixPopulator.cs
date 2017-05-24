namespace ParallelBfs.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UserInputs;

    public class AdjacencyMatrixPopulator
    {
        public AdjacencyMatrix Create(IUserInput input)
        {
            int rows = input.RowsCount();
            List<List<bool>> matrix = new List<List<bool>>(rows);

            for (int line = 0; line < rows; line++)
            {
                List<bool> row = PopulateCurrentRow(input, line);

                ValidateRowColumns(rows, line, row);

                matrix.Add(row);
            }

            return new AdjacencyMatrix(matrix);
        }

        private static List<bool> PopulateCurrentRow(IUserInput input, int line)
        {
            List<bool> row = input.Row(line)
                .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(c => ToBool(c))
                .ToList();

            return row;
        }

        private static void ValidateRowColumns(int rows, int line, List<bool> row)
        {
            if (row.Count != rows)
            {
                throw new DataMisalignedException($"Row contains less columns than expected rowId={line}, expected={rows}, actual={row.Count}");
            }
        }

        private static bool ToBool(string character)
        {
            if (character == "0")
            {
                return false;
            }
            else if (character == "1")
            {
                return true;
            }
            else
            {
                throw new ArgumentException("Only 1 and 0 are allowed for the metrix content");
            }
        }
    }
}
