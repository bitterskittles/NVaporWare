// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitmapWriter.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the BitmapWriter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    public class BitmapWriter : IWriter<Bitmap>
    {
        #region Public Methods and Operators

        public void Write(Stream stream, int offset, Bitmap arg)
        {
            var bitmap = arg.Clone(new Rectangle(0, 0, arg.Width, arg.Height), PixelFormat.Format1bppIndexed);
            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);

            var bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format1bppIndexed);

            try
            {
                var image = new byte[bitmap.Height * bitmapData.Stride];
                Marshal.Copy(bitmapData.Scan0, image, 0, image.Length);

                stream.Seek(offset, SeekOrigin.Begin);
                stream.WriteByte((byte)bitmap.Height);
                stream.WriteByte((byte)bitmap.Width);

                for (var x = (bitmap.Width / 8) - 1; x >= 0; x--)
                {
                    for (var y = 0; y < bitmap.Height; y++)
                    {
                        var fY = y == 0 || y % 2 == 0 ? y + 1 : y - 1;
                        Debug.WriteLine((fY * bitmapData.Stride) + x);
                        stream.WriteByte(image[(fY * bitmapData.Stride) + x]);
                    }
                }
            }
            finally
            {
                if (bitmapData != null)
                {
                    bitmap.UnlockBits(bitmapData);
                }
            }
        }

        #endregion
    }
}