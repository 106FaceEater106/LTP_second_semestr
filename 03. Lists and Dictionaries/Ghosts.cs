using System;
using System.Text;

namespace hashes
{
	public class GhostsTask : IFactory<Document>, IFactory<Vector>, IFactory<Segment>, 
							  IFactory<Cat>, IFactory<Robot>, IMagic
	{
		private static readonly byte[] Content = new byte[] {0, 0};
		private readonly Document document = new Document("MyBook", Encoding.ASCII, Content);
		private readonly Vector vector = new Vector(0, 0);
		private readonly Segment segment = new Segment(new Vector(0, 0), new Vector(1, 1));
		private readonly Cat cat = new Cat("Ki", "maine coon", new DateTime(4013,12,13));
		private readonly Robot robot = new Robot("Robot", 0);
		
		Document IFactory<Document>.Create() => document;
		Vector IFactory<Vector>.Create() => vector;
		Segment IFactory<Segment>.Create() => segment;
		Cat IFactory<Cat>.Create() => cat;
		Robot IFactory<Robot>.Create() => robot;
		
		public void DoMagic()
		{
			Robot.BatteryCapacity++;
			cat.Rename("12");
			segment.Start.Add(new Vector(1, 0));
			vector.Add(new Vector(1,0));
			Content[0]++;
		}
	}
}
