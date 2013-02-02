// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitmapReaderTests.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the BitmapReaderTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.Test
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BitmapReaderTests
    {
        #region Public Methods and Operators

        [TestMethod]
        [DeploymentItem(".\\TestData\\Evic11.dec")]
        [DeploymentItem(".\\TestData\\Evic11_0x2FC1.bmp")]
        public void ReadTest()
        {
            using (var fileStream = new FileStream("Evic11.dec", FileMode.Open, FileAccess.Read))
            {
                var bitmapReader = new BitmapReader();
                var actualBitmap = bitmapReader.Read(fileStream, 0x2FC1);
                BitmapData actualBitmapData = null;
                byte[] actual;
                try
                {
                    actualBitmapData =
                        actualBitmap.LockBits(
                            new Rectangle(0, 0, actualBitmap.Width, actualBitmap.Height), 
                            ImageLockMode.WriteOnly, 
                            PixelFormat.Format1bppIndexed);
                    actual = new byte[actualBitmap.Height * actualBitmapData.Stride];
                    Marshal.Copy(actualBitmapData.Scan0, actual, 0, actual.Length);
                }
                finally
                {
                    if (actualBitmapData != null)
                    {
                        actualBitmap.UnlockBits(actualBitmapData);
                    }
                }

                var expectedBitmap = (Bitmap)Image.FromFile("Evic11_0x2FC1.bmp");
                BitmapData expectedBitmapData = null;
                byte[] expected;
                try
                {
                    expectedBitmapData =
                        expectedBitmap.LockBits(
                            new Rectangle(0, 0, expectedBitmap.Width, expectedBitmap.Height), 
                            ImageLockMode.WriteOnly, 
                            PixelFormat.Format1bppIndexed);
                    expected = new byte[expectedBitmap.Height * expectedBitmapData.Stride];
                    Marshal.Copy(expectedBitmapData.Scan0, expected, 0, expected.Length);
                }
                finally
                {
                    if (expectedBitmapData != null)
                    {
                        expectedBitmap.UnlockBits(expectedBitmapData);
                    }
                }

                CollectionAssert.AreEquivalent(expected, actual);
            }
        }

        #endregion
    }
}