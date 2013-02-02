// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringReaderTests.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the StringReaderTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.Test
{
    using System.Globalization;
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using StringReader = NVaporWare.StringReader;

    [TestClass]
    public class StringReaderTests
    {
        #region Public Methods and Operators

        [TestMethod]
        [DeploymentItem(".\\TestData\\Evic11.dec")]
        public void ReadTest()
        {
            using (var fileStream = new FileStream("Evic11.dec", FileMode.Open, FileAccess.Read))
            {
                var stringReader = new StringReader();
                var actual = stringReader.Read(fileStream, Versions.Evic11.Strings.Offset);

                const string Expected = "Monday";

                Assert.AreEqual(Expected, actual, false, CultureInfo.InvariantCulture);
            }
        }

        #endregion
    }
}