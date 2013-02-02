// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitmapReader.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the BitmapReader type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    public class BitmapReader : IReader<Bitmap>
    {
        #region Public Methods and Operators

        public Bitmap Read(Stream stream, int offset)
        {
            stream.Seek(offset, SeekOrigin.Begin);
            var height = stream.ReadByte();
            var width = stream.ReadByte();
            if (height < 2)
            {
                height = 2;
            }

            var bitmap = new Bitmap(width, height, PixelFormat.Format1bppIndexed);
            var bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

            try
            {
                var image = new byte[height * bitmapData.Stride];

                for (var x = (width / 8) - 1; x >= 0; x--)
                {
                    for (var y = 0; y < height; y++)
                    {
                        var fY = y == 0 || y % 2 == 0 ? y + 1 : y - 1;
                        image[(fY * bitmapData.Stride) + x] = (byte)stream.ReadByte();
                    }
                }

                Marshal.Copy(image, 0, bitmapData.Scan0, image.Length);
            }
            finally
            {
                if (bitmapData != null)
                {
                    bitmap.UnlockBits(bitmapData);
                }
            }

            bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
            return bitmap;
        }

        #endregion
    }
}