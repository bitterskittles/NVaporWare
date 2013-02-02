// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SegmentEnumeratorTests.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the SegmentEnumeratorTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.Test
{
    using System;
    using System.Drawing;
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using StringReader = NVaporWare.StringReader;

    [TestClass]
    public class SegmentEnumeratorTests
    {
        #region Public Methods and Operators

        [TestMethod]
        [DeploymentItem(".\\TestData\\Evic11.dec")]
        public void SegmentEnumeratorBitmap()
        {
            using (var fileStream = new FileStream("Evic11.dec", FileMode.Open, FileAccess.Read))
            {
                var imageEnumerator = new SegmentEnumerator<Bitmap>(
                    fileStream, Versions.Evic11.Images, new BitmapReader());

                ISegmentData<Bitmap> segmentData = null;
                while (imageEnumerator.MoveNext())
                {
                    segmentData = imageEnumerator.Current;
                    Assert.IsNotNull(segmentData);
                }

                Assert.IsNotNull(segmentData);
                Assert.IsNull(imageEnumerator.Current);
                Assert.IsTrue(fileStream.Position == Versions.Evic11.Images.Offset + Versions.Evic11.Images.Size);
            }
        }

        [TestMethod]
        [DeploymentItem(".\\TestData\\Evic11.dec")]
        public void SegmentEnumeratorString()
        {
            using (var fileStream = new FileStream("Evic11.dec", FileMode.Open, FileAccess.Read))
            {
                var imageEnumerator = new SegmentEnumerator<string>(
                    fileStream, Versions.Evic11.Strings, new StringReader());

                ISegmentData<string> segmentData = null;
                while (imageEnumerator.MoveNext())
                {
                    segmentData = imageEnumerator.Current;
                    Assert.IsNotNull(segmentData);
                    Assert.IsTrue(string.IsNullOrWhiteSpace(segmentData.Data) == false);
                    Console.WriteLine(segmentData.Data);
                }

                Assert.IsNotNull(segmentData);
                Assert.IsNull(imageEnumerator.Current);
                Assert.IsTrue(fileStream.Position == Versions.Evic11.Strings.Offset + Versions.Evic11.Strings.Size);
            }
        }

        #endregion
    }
}