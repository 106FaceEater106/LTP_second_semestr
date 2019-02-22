using System.Collections.Generic;

namespace yield
{
	public static class ExpSmoothingTask
	{
		public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
		{
			DataPoint previousPoint = null;
			foreach (var item in data)
			{
				if (previousPoint == null)
				{
					previousPoint = item;
					item.ExpSmoothedY = item.OriginalY;
				}
				
				else
				{
					item.ExpSmoothedY = previousPoint.ExpSmoothedY + alpha * (item.OriginalY - previousPoint.ExpSmoothedY);
					previousPoint = item;
				}

				yield return item;
			}
		}
	}
}
