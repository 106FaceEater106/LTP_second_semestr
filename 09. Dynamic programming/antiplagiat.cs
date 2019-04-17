using System;
using System.Collections.Generic;

using DocumentTokens = System.Collections.Generic.List<string>;

namespace Antiplagiarism
{
    public class LevenshteinCalculator
    {
        public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
        {
            var comparisonList = new List<ComparisonResult>();
            for (var i = 0; i < documents.Count; i ++)
                for (var j = i + 1; j < documents.Count; j++)
                {
                    var comparison = new ComparisonResult(documents[i], documents[j],
                        LevenshteinDistance(documents[i], documents[j]));
                    comparisonList.Add(comparison);
                }

            return comparisonList;
        }
        
        private static double LevenshteinDistance(DocumentTokens firstDocument, DocumentTokens secondDovument)
        {
            var opt = new double[firstDocument.Count + 1, secondDovument.Count + 1];
            for (var i = 0; i <= firstDocument.Count; ++i)
                opt[i, 0] = i;
            for (var i = 0; i <= secondDovument.Count; ++i)
                opt[0, i] = i;
            for (var i = 1; i <= firstDocument.Count; ++i)
                for (var j = 1; j <= secondDovument.Count; ++j)
                    if (firstDocument[i - 1] != secondDovument[j - 1]) 
                        opt[i, j] = Math.Min(Math.Min(1 + opt[i - 1, j], 1 + opt[i, j - 1]),
                            opt[i - 1, j - 1] + TokenDistanceCalculator
                                .GetTokenDistance(firstDocument[i - 1], secondDovument[j - 1]));
                    else
                        opt[i, j] = opt[i - 1, j - 1];
            return opt[firstDocument.Count, secondDovument.Count];
        }
    }
}


