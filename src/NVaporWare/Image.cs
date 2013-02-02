// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Image.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the Image type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Drawing;

    public class Image
    {
        #region Fields

        private readonly Bitmap bitmap;

        private readonly int offset;

        private readonly int size;

        #endregion

        #region Constructors and Destructors

        public Image(int offset, Bitmap bitmap)
        {
            Contract.Requires<ArgumentOutOfRangeException>(offset >= 0);
            Contract.Requires<ArgumentNullException>(bitmap != null);
            Contract.Requires<ArgumentOutOfRangeException>(offset + (bitmap.Height * bitmap.Width / 8) - 1 <= 0xFFFF);

            this.offset = offset;
            this.bitmap = bitmap;
            this.size = (bitmap.Height * bitmap.Width / 8) + 2;
        }

        #endregion

        #region Public Properties

        public Bitmap Bitmap
        {
            get
            {
                return this.bitmap;
            }
        }

        public int Offset
        {
            get
            {
                return this.offset;
            }
        }

        public int Size
        {
            get
            {
                return this.size;
            }
        }

        #endregion
    }
}