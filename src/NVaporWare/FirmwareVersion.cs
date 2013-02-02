// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FirmwareVersion.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the FirmwareVersion type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;
    using System.Diagnostics.Contracts;

    // TODO: Read version info from the firmware file.
    public class FirmwareVersion
    {
        #region Fields

        private readonly int characterTableLength;

        private readonly int characterTableOffset;

        private readonly int imagesLength;

        private readonly int imagesOffset;

        private readonly int stringsLength;

        private readonly int stringsOffset;

        private readonly string version;

        #endregion

        #region Constructors and Destructors

        public FirmwareVersion(
            string version, 
            int imagesOffset, 
            int imagesLength, 
            int characterTableOffset, 
            int characterTableLength, 
            int stringsOffset, 
            int stringsLength)
        {
            Contract.Requires<ArgumentException>(string.IsNullOrEmpty(version) == false);
            Contract.Requires<ArgumentOutOfRangeException>(imagesOffset > 0);
            Contract.Requires<ArgumentOutOfRangeException>(imagesOffset <= 0xFFFF);
            Contract.Requires<ArgumentOutOfRangeException>(imagesLength > 0);
            Contract.Requires<ArgumentOutOfRangeException>(imagesLength + imagesOffset - 1 <= 0xFFFF);
            Contract.Requires<ArgumentOutOfRangeException>(characterTableOffset > 0);
            Contract.Requires<ArgumentOutOfRangeException>(characterTableOffset <= 0xFFFF);
            Contract.Requires<ArgumentOutOfRangeException>(characterTableLength > 0);
            Contract.Requires<ArgumentOutOfRangeException>(characterTableLength + characterTableOffset - 1 <= 0xFFFF);
            Contract.Requires<ArgumentOutOfRangeException>(stringsOffset > 0);
            Contract.Requires<ArgumentOutOfRangeException>(stringsOffset <= 0xFFFF);
            Contract.Requires<ArgumentOutOfRangeException>(stringsLength > 0);
            Contract.Requires<ArgumentOutOfRangeException>(stringsLength + stringsOffset - 1 <= 0xFFFF);

            this.version = version;
            this.imagesOffset = imagesOffset;
            this.imagesLength = imagesLength;
            this.characterTableOffset = characterTableOffset;
            this.characterTableLength = characterTableLength;
            this.stringsOffset = stringsOffset;
            this.stringsLength = stringsLength;
        }

        #endregion

        #region Public Properties

        public int CharacterTableLength
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() > 0);
                Contract.Ensures(Contract.Result<int>() + this.characterTableOffset - 1 <= 0xFFFF);

                return this.characterTableLength;
            }
        }

        public int CharacterTableOffset
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() > 0);
                Contract.Ensures(Contract.Result<int>() <= 0xFFFF);

                return this.characterTableOffset;
            }
        }

        public int ImagesLength
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() > 0);
                Contract.Ensures(Contract.Result<int>() + this.imagesOffset - 1 <= 0xFFFF);

                return this.imagesLength;
            }
        }

        public int ImagesOffset
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() > 0);
                Contract.Ensures(Contract.Result<int>() <= 0xFFFF);

                return this.imagesOffset;
            }
        }

        public int StringsLength
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() > 0);
                Contract.Ensures(Contract.Result<int>() + this.stringsOffset - 1 <= 0xFFFF);

                return this.stringsLength;
            }
        }

        public int StringsOffset
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() > 0);
                Contract.Ensures(Contract.Result<int>() <= 0xFFFF);

                return this.stringsOffset;
            }
        }

        public string Version
        {
            get
            {
                Contract.Ensures(string.IsNullOrEmpty(Contract.Result<string>()) == false);

                return this.version;
            }
        }

        #endregion

        #region Methods

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(string.IsNullOrEmpty(this.version) == false);
            Contract.Invariant(this.imagesOffset > 0);
            Contract.Invariant(this.imagesOffset <= 0xFFFF);
            Contract.Invariant(this.imagesLength > 0);
            Contract.Invariant(this.imagesLength + this.imagesOffset - 1 <= 0xFFFF);
            Contract.Invariant(this.characterTableOffset > 0);
            Contract.Invariant(this.characterTableOffset <= 0xFFFF);
            Contract.Invariant(this.characterTableLength > 0);
            Contract.Invariant(this.characterTableLength + this.characterTableOffset - 1 <= 0xFFFF);
            Contract.Invariant(this.stringsOffset > 0);
            Contract.Invariant(this.stringsOffset <= 0xFFFF);
            Contract.Invariant(this.stringsLength > 0);
            Contract.Invariant(this.stringsLength + this.stringsOffset - 1 <= 0xFFFF);
        }

        #endregion
    }
}