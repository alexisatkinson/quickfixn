﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using QuickFix;

namespace UnitTests
{
    [TestFixture]
    public class DD2Tests
    {
        [Test]
        public void VersionTest()
        {
            QuickFix.DataDictionary.DD2 dd = new QuickFix.DataDictionary.DD2();
            dd.Load("../../../spec/fix/FIX44.xml");
            Assert.That(dd.MajorVersion, Is.EqualTo("4"));
            Assert.That(dd.MinorVersion, Is.EqualTo("4"));
            Assert.That(dd.Version, Is.EqualTo("FIX.4.4"));
        }

        [Test]
        public void LoadFieldsTest()
        {
            QuickFix.DataDictionary.DD2 dd = new QuickFix.DataDictionary.DD2();
            dd.Load("../../../spec/fix/FIX44.xml");
            Assert.That(dd.FieldsByTag[1].Name, Is.EqualTo("Account"));
            Assert.That(dd.FieldsByName["Account"].Tag, Is.EqualTo(1));
            Assert.That(dd.FieldsByTag[1].Enums.Count, Is.EqualTo(0));
            Assert.That(dd.FieldsByTag[QuickFix.Fields.Tags.StatusValue].Enums.Count, Is.EqualTo(4));
        }

        [Test]
        public void FieldHasValueTest()
        {
            QuickFix.DataDictionary.DD2 dd = new QuickFix.DataDictionary.DD2();
            dd.Load("../../../spec/fix/FIX44.xml");
            Assert.That(dd.FieldHasValue(QuickFix.Fields.Tags.StatusValue, "1"), Is.EqualTo(true));
            Assert.That(dd.FieldHasValue(QuickFix.Fields.Tags.StatusValue, "CONNECTED"), Is.EqualTo(false));
        }

        [Test]
        public void BasicMessageTest()
        {
            QuickFix.DataDictionary.DD2 dd = new QuickFix.DataDictionary.DD2();
            dd.Load("../../../spec/fix/FIX44.xml");
            Assert.That(dd.Messages["3"].Fields.Count, Is.EqualTo(7));
        }

        [Test]
        public void ComponentSmokeTest()
        {
            QuickFix.DataDictionary.DD2 dd = new QuickFix.DataDictionary.DD2();
            dd.Load("../../../spec/fix/FIX44.xml");
            QuickFix.DataDictionary.DDMap tcr = dd.Messages["AE"];
            Assert.True(tcr.Fields.ContainsKey(55));
            Assert.False(tcr.Fields.ContainsKey(5995));
        }

        [Test]
        public void GroupTest()
        {
            QuickFix.DataDictionary.DD2 dd = new QuickFix.DataDictionary.DD2();
            dd.Load("../../../spec/fix/FIX44.xml");
            QuickFix.DataDictionary.DDMap tcrr = dd.Messages["AD"];
            Assert.That(tcrr.Groups[711].Fields[311].Name, Is.EqualTo("UnderlyingSymbol"));
            QuickFix.DataDictionary.DDMap tcr = dd.Messages["AE"];
            Assert.That(tcr.Groups[711].Groups[457].Fields[458].Name, Is.EqualTo("UnderlyingSecurityAltID"));
        }

        [Test]
        public void ReqFldTest()
        {
            QuickFix.DataDictionary.DD2 dd = new QuickFix.DataDictionary.DD2("../../../spec/fix/FIX44.xml");
            Assert.True(dd.Messages["AE"].ReqFields.Contains(571));
            Assert.False(dd.Messages["AE"].ReqFields.Contains(828));
        }

        [Test]
        public void HeaderTest()
        {
            QuickFix.DataDictionary.DD2 dd = new QuickFix.DataDictionary.DD2("../../../spec/fix/FIX44.xml");
            Assert.True(dd.Header.ReqFields.Contains(9));
            Assert.That(dd.Header.Fields.Count, Is.EqualTo(26));
        }

        [Test]
        public void TrailerTest()
        {
            QuickFix.DataDictionary.DD2 dd = new QuickFix.DataDictionary.DD2("../../../spec/fix/FIX44.xml");
            Assert.True(dd.Trailer.ReqFields.Contains(10));
            Assert.That(dd.Trailer.Fields.Count, Is.EqualTo(3));
        }
    }
}
