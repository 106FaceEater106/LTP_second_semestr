using System;

namespace func_rocket
{
	public class ControlTask
	{
		public static Turn ControlRocket(Rocket rocket, Vector target)
		{
			var distance = target - rocket.Location;
			var resultRocketVector = rocket.Velocity.Normalize() + new Vector(1, 0).Rotate(rocket.Direction);
			return resultRocketVector.Angle > distance.Angle ? Turn.Left :
				resultRocketVector.Angle < distance.Angle ? Turn.Right : Turn.None;
		}
	}
}
