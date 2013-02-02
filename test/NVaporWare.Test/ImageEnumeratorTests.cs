// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageEnumeratorTests.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the ImageEnumeratorTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.Test
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ImageEnumeratorTests
    {
        #region Public Methods and Operators

        [TestMethod]
        [DeploymentItem(".\\TestData\\Evic11.dec")]
        public void EnumateTest()
        {
            using (var fileStream = new FileStream("Evic11.dec", FileMode.Open, FileAccess.Read))
            using (
                var imageEnumerator = new ImageEnumerator(
                    fileStream, Versions.Evic11.ImagesOffset, Versions.Evic11.ImagesLength))
            {
                Image image = null;
                while (imageEnumerator.MoveNext())
                {
                    image = imageEnumerator.Current;
                    Assert.IsNotNull(image);
                }

                Assert.IsNotNull(image);
                Assert.IsTrue(image.Offset == 0x3CAB);
                Assert.IsTrue(image.Size == 98);
            }
        }

        #endregion
    }
}