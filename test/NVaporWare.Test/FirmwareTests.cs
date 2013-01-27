// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FirmwareTests.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the FirmwareTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NVaporWare.Test
{
    using System.IO;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FirmwareTests
    {
        #region Public Methods and Operators

        [TestMethod]
        [DeploymentItem(".\\TestData\\code.bin")]
        [DeploymentItem(".\\TestData\\code.hex")]
        public void DecryptionTest()
        {
            byte[] actual;
            using (var encryptedFile = new FileStream("code.bin", FileMode.Open, FileAccess.Read))
            using (var firmware = new Firmware(encryptedFile))
            {
                actual = new byte[firmware.Stream.Length];
                firmware.Stream.Read(actual, 0, actual.Length);
            }

            byte[] expected;
            using (var decryptedFile = new FileStream("code.hex", FileMode.Open, FileAccess.Read))
            {
                expected = new byte[actual.Length];
                decryptedFile.Read(expected, 0, expected.Length);
            }

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        [DeploymentItem(".\\TestData\\code.bin")]
        [DeploymentItem(".\\TestData\\code.hex")]
        public void EncryptionTest()
        {
            var key = Encoding.ASCII.GetBytes("20121109joyetechjoyetech20128850");

            byte[] actual;
            using (var decryptedFile = new FileStream("code.hex", FileMode.Open, FileAccess.Read))
            using (var firmware = new Firmware(key))
            {
                decryptedFile.CopyTo(firmware.Stream);
                firmware.UpdateHeader();

                actual = new byte[firmware.BaseStream.Length];
                firmware.BaseStream.Seek(0, SeekOrigin.Begin);
                firmware.BaseStream.Read(actual, 0, actual.Length);
            }

            byte[] expected;
            using (var encryptedFile = new FileStream("code.bin", FileMode.Open, FileAccess.Read))
            {
                expected = new byte[actual.Length];
                encryptedFile.Read(expected, 0, expected.Length);
            }

            CollectionAssert.AreEquivalent(expected, actual);
        }

        #endregion
    }
}