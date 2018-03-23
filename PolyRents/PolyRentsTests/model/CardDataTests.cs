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
        public void calculateCheckDigitTest()
        {
            CardData data1 = new CardData("%2015072056047^STUDENT?");
            CardData data2 = new CardData("%2015049750564^STUDENT?");
            CardData data3 = new CardData("%2015054273831^STUDENT?");
            CardData data4 = new CardData("%2015067175127^STUDENT?");
            CardData data5 = new CardData("%2015010312644^STUDENT?");

            Assert.IsTrue(data1.CheckDigit == 2, "checkDigit calculation failed");
            Assert.IsTrue(data2.CheckDigit == 3, "checkDigit calculation failed");
            Assert.IsTrue(data3.CheckDigit == 4, "checkDigit calculation failed");
            Assert.IsTrue(data4.CheckDigit == 8, "checkDigit calculation failed");
            Assert.IsTrue(data5.CheckDigit == 6, "checkDigit calculation failed");
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
        public void parseEncodedNumberTest()
        {
            CardData data1 = new CardData("%2015072056047^STUDENT?");
            CardData data2 = new CardData("%2015049750564^STUDENT?");
            CardData data3 = new CardData("%2015054273831^STUDENT?");
            CardData data4 = new CardData("%2015067175127^STUDENT?");
            CardData data5 = new CardData("%2015010312644^STUDENT?");

            Assert.AreEqual(2015072056047, data1.EncodedNumber);
            Assert.AreEqual(2015049750564, data2.EncodedNumber);
            Assert.AreEqual(2015054273831, data3.EncodedNumber);
            Assert.AreEqual(2015067175127, data4.EncodedNumber);
            Assert.AreEqual(2015010312644, data5.EncodedNumber);
        }
    }
}