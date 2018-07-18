using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolyRents.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyRents.model.Tests
{
    [TestClass()]
    public class CardDataTests
    {
        [TestMethod()]
        public void staticParseLibraryNumberTest()
        {
            Assert.AreEqual("20150720560472", CardData.getLibNumberFromRawData("%2015072056047^STUDENT?"));
            Assert.AreEqual("20150497505643", CardData.getLibNumberFromRawData("%2015049750564^STUDENT?"));
            Assert.AreEqual("20150542738314", CardData.getLibNumberFromRawData("%2015054273831^STUDENT?"));
            Assert.AreEqual("20150671751278", CardData.getLibNumberFromRawData("%2015067175127^STUDENT?"));
            Assert.AreEqual("20150103126446", CardData.getLibNumberFromRawData("%2015010312644^STUDENT?"));
        }

        [TestMethod()]
        public void parseRoleTest()
        {
            CardData data1 = new CardData("%2015072056047^STUDENT?");
            CardData data2 = new CardData("%2015049750564^STUDENT?");
            CardData data3 = new CardData("%2015054273831^STUDENT?");
            CardData data4 = new CardData("%2015067175127^STUDENT?");
            CardData data5 = new CardData("%2015010312644^STUDENT?");

            Assert.AreEqual("STUDENT", data1.Role);
            Assert.AreEqual("STUDENT", data2.Role);
            Assert.AreEqual("STUDENT", data3.Role);
            Assert.AreEqual("STUDENT", data4.Role);
            Assert.AreEqual("STUDENT", data5.Role);
        }

        [TestMethod()]
        public void staticParseRoleTest()
        {
            Assert.AreEqual("STUDENT", CardData.getRoleFromRawData("%2015072056047^STUDENT?"));
            Assert.AreEqual("STUDENT", CardData.getRoleFromRawData("%2015049750564^STUDENT?"));
            Assert.AreEqual("FACULTY", CardData.getRoleFromRawData("%2015054273831^FACULTY?"));
            Assert.AreEqual("FACULTY", CardData.getRoleFromRawData("%2015067175127^FACULTY?"));
            Assert.AreEqual("STUDENT", CardData.getRoleFromRawData("%2015010312644^STUDENT?"));
        }
    }
}