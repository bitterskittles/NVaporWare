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

        private readonly ISegmentAddress characterMap;

        private readonly ISegmentAddress images;

        private readonly ISegmentAddress strings;

        private readonly string version;

        #endregion

        #region Constructors and Destructors

        public FirmwareVersion(
            string version, ISegmentAddress images, ISegmentAddress characterMap, ISegmentAddress strings)
        {
            Contract.Requires<ArgumentException>(string.IsNullOrEmpty(version) == false);
            Contract.Requires<ArgumentNullException>(images != null);
            Contract.Requires<ArgumentNullException>(characterMap != null);
            Contract.Requires<ArgumentNullException>(strings != null);

            this.version = version;
            this.images = images;
            this.characterMap = characterMap;
            this.strings = strings;
        }

        #endregion

        #region Public Properties

        public ISegmentAddress CharacterMap
        {
            get
            {
                return this.characterMap;
            }
        }

        public ISegmentAddress Images
        {
            get
            {
                return this.images;
            }
        }

        public ISegmentAddress Strings
        {
            get
            {
                return this.strings;
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
            Contract.Invariant(this.images != null);
            Contract.Invariant(this.characterMap != null);
            Contract.Invariant(this.strings != null);
        }

        #endregion
    }
}