// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Images.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the Images type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.IO;

    public class Images : IEnumerable<Image>
    {
        #region Fields

        private readonly FirmwareVersion firmwareVersion;

        private readonly Stream stream;

        #endregion

        #region Constructors and Destructors

        public Images(Stream stream, FirmwareVersion firmwareVersion)
        {
            Contract.Requires<ArgumentNullException>(stream != null);
            Contract.Requires<ArgumentException>(stream.CanRead);
            Contract.Requires<ArgumentException>(stream.CanSeek);
            Contract.Requires<ArgumentNullException>(firmwareVersion != null);

            this.stream = stream;
            this.firmwareVersion = firmwareVersion;
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerator<Image> GetEnumerator()
        {
            return new ImageEnumerator(
                this.stream, this.firmwareVersion.ImagesOffset, this.firmwareVersion.ImagesLength);
        }

        #endregion

        #region Explicit Interface Methods

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ImageEnumerator(
                this.stream, this.firmwareVersion.ImagesOffset, this.firmwareVersion.ImagesLength);
        }

        #endregion

        #region Methods

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.stream != null);
            Contract.Invariant(this.stream.CanRead);
            Contract.Invariant(this.stream.CanSeek);
            Contract.Invariant(this.firmwareVersion != null);
        }

        #endregion
    }
}