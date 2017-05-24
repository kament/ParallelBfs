namespace ParallelBfs.Sdk.UserInputs
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FileUserInput : IUserInput
    {
        private const int RowsLineIndex = 0;

        private List<string> lines;
        private int rows;

        public FileUserInput(string filePath)
        {
            this.lines = File.ReadLines(filePath).ToList();

            if (this.lines.Count == 0)
            {
                throw new ArgumentException("File is empty");
            }

            this.rows = int.Parse(lines[RowsLineIndex]);

            if (this.lines.Count - 1 != this.rows)
            {
                throw new ArgumentException("Rows of the matrix must be exactly as first line parameter!");
            }
        }

        public string Row(int rowIndex)
        {
            return lines[rowIndex + 1];
        }

        public int RowsCount()
        {
            return this.rows;
         }
    }
}
