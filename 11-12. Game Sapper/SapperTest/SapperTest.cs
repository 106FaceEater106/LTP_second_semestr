using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using saper;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateField()
        {
            new Field(10, 15);
        }

        [TestMethod]
        public void FieldLenght()
        {
            var f = new Field(10, 15);
            Assert.AreEqual(10, f.N);
        }

        [TestMethod]
        public void FieldLevelsName()
        {
            var f = new Field(10, 15);
            var level = new LevelEl { name = "Easy", percent = 15 };
            Assert.AreEqual(level.name, f.levels[0].name);
        }


        [TestMethod]
        public void FieldMinAround()
        {
            var f = new Field(10, 15);
            int res = 0;
            for (int k = 0; k < f.N; k++)
            {
                for (int t = 0; t < f.N; t++)
                {
                    if (Math.Abs(2 - k) <= 1 && Math.Abs(t - 3) <= 1 && f.cells[k, t].HasMine)
                    {
                        res++;
                    }
                }
            }
            Assert.AreEqual(res, f.MinesAround(2, 3));
        }

    }
}
