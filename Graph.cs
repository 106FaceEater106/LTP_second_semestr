using System.Collections.Generic;

namespace Graphs
{
    class Graphs
    {
        public static IEnumerable<List<int>> ConvertMatrixToList(int[,] matrixIncedentNode)
        {
            for (var i = 0; i < matrixIncedentNode.GetLength(0); i++)
            {
                var incidentList = new List<int>();
                for (var j = 0; j < matrixIncedentNode.GetLength(1); j++)
                    if (matrixIncedentNode[i,j] != 0)
                        incidentList.Add(j);
                yield return incidentList;
            }
        }

        public static int[,] ConvertListToMatrix(List<List<int>> incidentList)
        {
            var matrix = new int [incidentList.Count, incidentList.Count];
            for (var i = 0; i < incidentList.Count; i++)
                for (var j = 0; j < incidentList[i].Count; j++)
                    matrix[i,incidentList[i][j]] = 1;
            return matrix;
        }
    }
}
