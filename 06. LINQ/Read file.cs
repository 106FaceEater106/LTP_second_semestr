using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace linq_slideviews
{
	public class ParsingTask
	{
		public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
		{
			return lines
				.Select(x => x.Split(';'))
				.Where(x => int.TryParse(x[0], out _) && x.Length == 3 && SlideTypeTryParse(x[1]))
				.Select(x => new SlideRecord(int.Parse(x[0]), GetSlideType(x[1]), x[2]))
				.ToDictionary(x => x.SlideId, x => x);
		}

		private static bool SlideTypeTryParse(string s)
		{
			return s == "quiz" || s == "theory" || s == "exercise";
		}
		
		private static SlideType GetSlideType(string type)
		{
			switch (type)
			{
				case "quiz":
					return SlideType.Quiz;
				case "theory":
					return SlideType.Theory;
				case "exercise":
					return SlideType.Exercise;
			}
			throw new ArgumentException();
		}

		public static IEnumerable<VisitRecord> ParseVisitRecords(
			IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
		{
			return lines
				.Skip(1)
				.Select(x => x.Split(';'))
				.Where(x => VerifyCorrectness(x, slides))
				.Select(x => new VisitRecord(int.Parse(x[0]), 
					int.Parse(x[1]),
					DateTime.Parse(string.Concat(x[2], " ", x[3])),
						slides[int.Parse(x[1])].SlideType));
		}

		private static bool VerifyCorrectness(string[] line, IDictionary<int, SlideRecord> slides)
		{
			return !int.TryParse(line[0], out _)
			       || !int.TryParse(line[1], out _)
			       || !DateTime.TryParse(line[2], out _)
			       || !DateTime.TryParse(line[3], out _)
			       || !slides.ContainsKey(int.Parse(line[1]))
				? throw new FormatException("Wrong line [" + string.Join(";", line) + "]") : true;
		}
	}
}
