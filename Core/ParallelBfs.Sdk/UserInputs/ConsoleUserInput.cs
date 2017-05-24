namespace ParallelBfs.Sdk.UserInputs
{
    using System;

    public class ConsoleUserInput : IUserInput
    {
        private int? rows;

        public ConsoleUserInput()
        {
            this.rows = null;
        }

        public int RowsCount()
        {
            this.rows = this.rows ?? int.Parse(Console.ReadLine());

            return (int)rows;
        }

        public string Row(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                string row = Console.ReadLine();

                return row;
            }
            else
            {
                throw new ArgumentException("rowIndex");
            }
        }
    }
}
