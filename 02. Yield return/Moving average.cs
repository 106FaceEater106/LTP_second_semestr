using System.Collections.Generic;

namespace yield
{
	public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var pointInWindow = new Queue<DataPoint>();
			var sum = 0.0;
			foreach (var item in data)
			{
				if (pointInWindow.Count == windowWidth)
					sum -= pointInWindow.Dequeue().OriginalY;
				pointInWindow.Enqueue(item);
				sum += item.OriginalY;
				item.AvgSmoothedY = sum / pointInWindow.Count;
				yield return item;
			}
		}
	}
}
