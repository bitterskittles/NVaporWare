// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EvicEncodingTests.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the EvicEncodingTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.Test
{
    using System.Globalization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EvicEncodingTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void GetStringTest()
        {
            var encoded = new byte[] { 0x1E, 0x40, 0x3F, 0x35, 0x32, 0x4A };

            var evicEncoding = new EvicEncoding();
            var actual = evicEncoding.GetString(encoded, 0, encoded.Length);

            const string Expected = "Monday";

            Assert.AreEqual(Expected, actual, false, CultureInfo.InvariantCulture);
        }

        #endregion
    }
}