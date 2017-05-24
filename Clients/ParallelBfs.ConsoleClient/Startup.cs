namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ParallelBfs.Sdk;
    using ParallelBfs.Sdk.Alghorithms;
    using ParallelBfs.Sdk.UserInputs;

    public class Startup
    {
        public static void Main(string[] args)
        {
            // Create console arguments object and fill it with the args array

            AdjacencyMatrix matrix = CreateMetrix();

            DateTime start = DateTime.UtcNow;

            bool result = TraverseMatrix(matrix);

            DateTime end = DateTime.UtcNow;

            LogResult(start, result, end);
        }

        private static AdjacencyMatrix CreateMetrix()
        {
            IUserInput input = new FileUserInput("C:\\Users\\Kamen\\Desktop\\RSA\\ParallelBfs\\ParallelBfs\\TestFiles\\Test1.txt");

            AdjacencyMatrixFactory factory = new AdjacencyMatrixFactory();
            AdjacencyMatrix matrix = factory.Create(input);

            return matrix;
        }

        private static void LogResult(DateTime start, bool result, DateTime end)
        {
            double totalMiliseconds = (end - start).TotalMilliseconds;
            string resultStatus = result ? "Success" : "Fail";

            Console.WriteLine($"Traverse finish for {totalMiliseconds} miliseconds, with status {resultStatus}");
        }

        private static bool TraverseMatrix(AdjacencyMatrix matrix)
        {
            SynchronousBfs bfsAlghorithm = new SynchronousBfs();
            bool result = bfsAlghorithm.Search(matrix);

            return result;
        }
    }
}
