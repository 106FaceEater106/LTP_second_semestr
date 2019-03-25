using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class StatisticsTask
	{
		public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
		{
			return  visits
					.GroupBy(x => x.UserId)
					.SelectMany(x => x.OrderBy(y => y.DateTime)
						.Bigrams()
						.Where(y => y.Item1.SlideType == slideType && y.Item1.SlideId != y.Item2.SlideId
						            && y.Item2.DateTime - y.Item1.DateTime >= TimeSpan.FromMinutes(1)
						            && y.Item2.DateTime - y.Item1.DateTime <= TimeSpan.FromHours(2))
						.Select(y => (y.Item2.DateTime - y.Item1.DateTime).TotalMinutes))
					.DefaultIfEmpty(0)
					.Median();
		}
	}
}
