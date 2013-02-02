// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageEnumerator.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the ImageEnumerator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    public class ImageEnumerator : IEnumerator<Image>
    {
        #region Fields

        private readonly int count;

        private readonly int offset;

        private readonly Stream stream;

        private int position;

        #endregion

        #region Constructors and Destructors

        public ImageEnumerator(Stream stream, int offset, int count)
        {
            Contract.Requires<ArgumentNullException>(stream != null);
            Contract.Requires<ArgumentOutOfRangeException>(offset > 0);
            Contract.Requires<ArgumentOutOfRangeException>(offset <= 0xFFFF);
            Contract.Requires<ArgumentOutOfRangeException>(count > 0);
            Contract.Requires<ArgumentOutOfRangeException>(count + offset - 1 <= 0xFFFF);

            this.stream = stream;
            this.offset = offset;
            this.count = count;
            this.position = offset;
        }

        #endregion

        #region Public Properties

        public Image Current { get; private set; }

        #endregion

        #region Explicit Interface Properties

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool MoveNext()
        {
            if (this.position >= this.offset + this.count)
            {
                this.Current = null;
                return false;
            }

            try
            {
                this.stream.Seek(this.position, SeekOrigin.Begin);
                var height = this.stream.ReadByte();
                var width = this.stream.ReadByte();
                if (height < 2)
                {
                    height = 2;
                }

                var bitmap = new Bitmap(width, height, PixelFormat.Format1bppIndexed);
                var bitmapData = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height), 
                    ImageLockMode.WriteOnly, 
                    PixelFormat.Format1bppIndexed);

                try
                {
                    var bytes = height * bitmapData.Stride;
                    var image = new byte[bytes];

                    for (var x = (width / 8) - 1; x >= 0; x--)
                    {
                        for (var y = 0; y < height; y++)
                        {
                            int fY = y == 0 || y % 2 == 0 ? y + 1 : y - 1;
                            image[(fY * bitmapData.Stride) + x] = (byte)this.stream.ReadByte();
                        }
                    }
                    
                    Marshal.Copy(image, 0, bitmapData.Scan0, bytes);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }

                bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                this.Current = new Image(this.position, bitmap);
                this.position = this.position + this.Current.Size;
                return true;
            }
            catch
            {
                this.Current = null;
                return false;
            }
        }

        public void Reset()
        {
            this.Current = null;
            this.position = this.offset;
        }

        #endregion

        #region Methods

        protected void Dispose(bool disposing)
        {
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.stream != null);
            Contract.Invariant(this.offset > 0);
            Contract.Invariant(this.offset <= 0xFFFF);
            Contract.Invariant(this.count > 0);
            Contract.Invariant(this.count + this.offset - 1 <= 0xFFFF);
        }

        #endregion
    }
}