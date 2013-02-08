// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitmapWriterTests.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the BitmapWriterTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.Test
{
    using System.Drawing;
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BitmapWriterTests
    {
        #region Public Methods and Operators

        [TestMethod]
        [DeploymentItem(".\\TestData\\Evic11.dec")]
        [DeploymentItem(".\\TestData\\Evic11_0x2FC1.bmp")]
        public void WriteTest()
        {
            var bitmap = (Bitmap)Image.FromFile(".\\Evic11_0x2FC1.bmp");

            byte[] actual = null;
            byte[] expected = null;
            using (var fileStream = new FileStream("Evic11.dec", FileMode.Open, FileAccess.Read))
            using (var memoryStream = new MemoryStream(new byte[fileStream.Length]))
            {
                fileStream.CopyTo(memoryStream);

                var bitmapWriter = new BitmapWriter();
                bitmapWriter.Write(memoryStream, 0x2FC1, bitmap);
                actual = memoryStream.ToArray();

                expected = new byte[fileStream.Length];
                fileStream.Seek(0, SeekOrigin.Begin);
                fileStream.Read(expected, 0, expected.Length);
            }

            CollectionAssert.AreEquivalent(expected, actual);
        }

        #endregion
    }
}