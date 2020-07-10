using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLoader.Tests
{
    [TestFixture]
    public class ModelTests
    {
        readonly string[] CorrectList = new string[] 
            {
                "Name;East;North;Hight;Value;Description",
                "point1;48.02;7.35;0;2.3345;Точка 1",
                "point5;35.50;6.95;0;1.2345;Точка 5",
                "point7;43.11;7.84;0;2.0932;Точка 7"
            };
        readonly string[] CorrectListReorderedColumns = new string[]
            {
                "Description;Name;North;Hight;Value;East;",
                "Точка 1;point1;7.35;0;2.3345;48.02;",
                "Точка 5;point5;6.95;0;1.2345;35.50;",
                "Точка 7;point7;7.84;0;2.0932;43.11;"
            };
        readonly string[] InCorrectListMissedColumns = new string[]
            {
                "Description;Name;North;Hight;East;",
                "Точка 1;point1;7.35;0;2.3345;48.02;",
                "Точка 5;point5;6.95;0;1.2345;35.50;",
                "Точка 7;point7;7.84;0;2.0932;43.11;"
            };
        readonly string[] InCorrectListMissedValue = new string[]
            {
                "Description;Name;North;Hight;Value;East;",
                "Точка 1;point1;7.35;0;2.3345;48.02;",
                "Точка 5;point5;6.95;0;1.2345;35.50;",
                "Точка 7;point7;7.84;0;2.0932;"
            };

        #region CheckFileFormat()
        [Test]
        public void CheckFileFormat_CorrectBaseOrderedInput_ReturnTrue()
        {
            Assert.IsTrue(Model.CheckFileFormat(CorrectList));
        }
        [Test]
        public void CheckFileFormat_CorrectReorderedInput_ReturnTrue()
        {
            Assert.IsTrue(Model.CheckFileFormat(CorrectListReorderedColumns));
        }
        [Test]
        public void CheckFileFormat_InCorrectInput_MissedColumnName_ReturnFalse()
        {
            Assert.IsFalse(Model.CheckFileFormat(InCorrectListMissedColumns));
        }
        [Test]
        public void CheckFileFormat_InCorrectInput_MissedValue_ReturnTrue()
        {
            Assert.IsTrue(Model.CheckFileFormat(InCorrectListMissedValue));
        }
        [Test]
        public void CheckFileFormat_EmptyInput_ReturnFalse()
        {
            Assert.IsFalse(Model.CheckFileFormat(new string[] { }));
        }
        [Test]
        public void CheckFileFormat_NullInput_ReturnFalse()
        {
            Assert.IsFalse(Model.CheckFileFormat(null));
        }
        [Test]
        public void CheckFileFormat_InCorrectInput_LogPhraseOutput()
        {
            Model.CheckFileFormat(InCorrectListMissedColumns);
            Assert.AreEqual(Model.Log.Last(), "Отсутствует столбец Value");
        }
        [Test]
        public void CheckFileFormat_EmptyInput_LogPhraseOutput()
        {
            Model.CheckFileFormat(new string[] { });
            Assert.AreEqual(Model.Log.Last(), "Указанный файл пуст");
        }
        #endregion

        #region LoadData()
        [Test]
        public void LoadData_CorrectInputTest()
        {
            List<PointerData> sampleData = new List<PointerData>()
            {
                new PointerData("point1","Точка 1",48.02, 7.35, 0, 2.3345),
                new PointerData("point5","Точка 5",35.50, 6.95, 0, 1.2345),
                new PointerData("point7","Точка 7",43.11, 7.84, 0, 2.0932),
            };
            var testData = Model.LoadData(CorrectList);
            Assert.AreEqual(sampleData, testData);
        }
        [Test]
        public void LoadData_ReorderedInputTest()
        {
            List<PointerData> sampleData = new List<PointerData>()
            {
                new PointerData("point1","Точка 1",48.02, 7.35, 0, 2.3345),
                new PointerData("point5","Точка 5",35.50, 6.95, 0, 1.2345),
                new PointerData("point7","Точка 7",43.11, 7.84, 0, 2.0932),
            };
            var testData = Model.LoadData(CorrectListReorderedColumns);
            Assert.AreEqual(sampleData, testData);
        }
        [Test]
        public void LoadData_InCorrectInputTest_MissedColumn_ReturnEmptyList()
        {
            List<PointerData> sampleData = new List<PointerData>(){};
            var testData = Model.LoadData(InCorrectListMissedColumns);
            Assert.AreEqual(sampleData, testData);
        }
        [Test]
        public void LoadData_InCorrectInputTest_MissedValue()
        {
            List<PointerData> sampleData = new List<PointerData>()
            {
                new PointerData("point1","Точка 1",48.02, 7.35, 0, 2.3345),
                new PointerData("point5","Точка 5",35.50, 6.95, 0, 1.2345),
            };
            var testData = Model.LoadData(InCorrectListMissedValue);
            Assert.AreEqual(sampleData, testData);
        }
        [Test]
        public void LoadData_NullInputTes_ReturnEmptyList()
        {
            List<PointerData> sampleData = new List<PointerData>() { };
            var testData = Model.LoadData(null);
            Assert.AreEqual(sampleData, testData);
        }
        #endregion
    }
}
