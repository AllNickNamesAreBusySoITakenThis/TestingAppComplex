using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileLoader;

namespace FileLoader.Tests
{
    [TestFixture]
    public class PointerDataTests
    {
        [Test]
        public void GetPointerData_SampleDataTest()
        {
            PointerData tData = new PointerData("Name", "Description", 1, 2, 3, 4);
            Dictionary<string, object> correctInputData = new Dictionary<string, object>
            {
                {"NAME","Name"},
                {"DESCRIPTION","Description"},
                {"NORTH",2},
                {"EAST",1},
                {"VALUE",4},
                {"HIGHT",3}
            };
            Assert.AreEqual(tData, PointerData.GetPointerData(correctInputData));            
        }
        [Test]
        public void GetPointerData_UncorrectDataTest_NullReturn()
        {
            Dictionary<string, object> correctInputData = new Dictionary<string, object>
            {
                {"NAME","Name"},
                {"DESCRIPTION","Description"},
                {"NORTH",2},
                {"EAST",1},
                {"VALUE",4}
            };
            Assert.IsNull(PointerData.GetPointerData(correctInputData));
        }
    }
}
