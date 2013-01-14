// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Firmware.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the Firmware type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System.Linq;

    public class Firmware
    {
        #region Fields

        private readonly byte[] data;

        private readonly FirmwareHeader header;

        private readonly bool isChecksumValid;

        private readonly string path;

        #endregion

        #region Constructors and Destructors

        public Firmware(string path, FirmwareHeader header, byte[] data)
        {
            this.path = path;
            this.header = header;
            this.data = data;
            this.isChecksumValid = data.Sum(i => i) == this.header.Checksum;
        }

        #endregion

        #region Public Properties

        public byte[] Data
        {
            get
            {
                return this.data;
            }
        }

        public FirmwareHeader Header
        {
            get
            {
                return this.header;
            }
        }

        public bool IsChecksumValid
        {
            get
            {
                return this.isChecksumValid;
            }
        }

        public string Path
        {
            get
            {
                return this.path;
            }
        }

        #endregion
    }
}