using System;
using System.Collections.Generic;
using System.Drawing;

namespace func_rocket
{
	public class LevelsTask
	{
		private static readonly Physics StandardPhysics = new Physics();
		private static readonly Rocket Rocket = new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
		private static readonly Vector Vector = new Vector(600, 200);
		private static readonly Vector UpVector = new Vector(700, 500);
		
		public static IEnumerable<Level> CreateLevels()
		{
			yield return CreateLevel("Zero", Vector, GetZeroGravity);
			yield return CreateLevel("Heavy", Vector, GetHeavyGravity);
			yield return CreateLevel("Up", UpVector, GetUpGravity);
			yield return CreateLevel("WhiteHole", Vector, GetWhiteHoleGravity);
			yield return CreateLevel("BlackHole", Vector, GetBlackHoleGravity );
			yield return CreateLevel("BlackAndWhite", Vector, GetBlackAndWhiteGravity);
		}
		
		private static Level CreateLevel(string name, Vector vector, Gravity gravity) =>
			new Level(name, Rocket, vector, gravity, StandardPhysics);
		private static Vector GetZeroGravity(Size size, Vector v) => Vector.Zero;
		private static Vector GetHeavyGravity(Size size, Vector v) => new Vector(0, 0.9);
		private static Vector GetUpGravity(Size size, Vector v) => 
			new Vector(0, - 300 / (size.Height - v.Y + 300.0));
		private static Vector GetWhiteHoleGravity(Size size, Vector v) => 
			GetAnyHoleGravity(-140, Vector - v);
		private static Vector GetBlackHoleGravity(Size size, Vector v) => 
			GetAnyHoleGravity(300,  ((Vector + Rocket.Location) / 2) - v);
		private static Vector GetBlackAndWhiteGravity(Size size, Vector v) =>
			(GetBlackHoleGravity(size, v) + GetWhiteHoleGravity(size, v)) / 2;
		private static Vector GetAnyHoleGravity(int constanta, Vector d) => 
			d.Normalize() * constanta * d.Length / (d.Length * d.Length + 1);
	}
}

