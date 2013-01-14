// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FirmwareHeader.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the FirmwareHeader type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;
    using System.Text;

    public class FirmwareHeader
    {
        #region Static Fields

        private static readonly byte[] HeaderKey;

        #endregion

        #region Fields

        private readonly int checksum;

        private readonly byte[] data;

        private readonly byte[] encryptionKey;

        #endregion

        #region Constructors and Destructors

        static FirmwareHeader()
        {
            const string headerKeyString = "sinowealth chenshaofan 20121101    ";
            var bytes = Encoding.ASCII.GetBytes(headerKeyString);
            HeaderKey = new byte[32];
            Array.Copy(bytes, HeaderKey, 32);
        }

        public FirmwareHeader(byte[] headerData)
        {
            if (headerData == null)
            {
                throw new ArgumentNullException("headerData");
            }

            if (headerData.Length != 0x40)
            {
                throw new ArgumentException("Header must be 64 bytes long.", "headerData");
            }

            this.data = headerData;
            this.checksum = BitConverter.ToInt32(this.data, 0x10);
            this.encryptionKey = this.ExtractEncryptionKey(this.data, HeaderKey);
        }

        #endregion

        #region Public Properties

        public int Checksum
        {
            get
            {
                return this.checksum;
            }
        }

        public byte[] Data
        {
            get
            {
                return this.data;
            }
        }

        public byte[] EncryptionKey
        {
            get
            {
                return this.encryptionKey;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Extracts data encryption key from the header of the firmware file.
        ///   Bytes 0x20-0x3f contain the 32 bytes long encryption key in encrypted format.
        /// </summary>
        /// <param name="header">
        /// First 0x40 bytes of the firmware file. 
        /// </param>
        /// <param name="headerKey">
        /// Key thats hardcoded in MVR.exe to decrypt file header. 
        /// </param>
        /// <returns>
        /// 32 bytes long encrpytion key. 
        /// </returns>
        private byte[] ExtractEncryptionKey(byte[] header, byte[] headerKey)
        {
            var dataKey = new byte[0x20];
            Array.Copy(header, 0x20, dataKey, 0, 0x20);

            // This code is 1-to-1 copy from disassembled MVR.exe, and not optimized for C#.
            // I know that al, cl, dl are bits 0..7 of eax, ecx, and edx.
            // Having separate variables doesn't change the result in the code below, though.
            byte al;
            byte cl;
            byte dl;

            uint eax;
            uint ecx;
            uint edx;

            uint v50;
            byte v5d;
            byte v5e;
            byte v5f;

            // MVR.exe v1.0, offset: 0x00404297
            for (uint v4c = 0; v4c < 0x20; v4c++)
            {
                edx = v4c;
                edx = edx & 0x0f;
                v50 = edx;
                ecx = v4c;
                al = dataKey[ecx];
                v5d = al;
                edx = v50;
                cl = headerKey[edx];
                v5e = cl;
                eax = v50;
                dl = headerKey[eax + 0x10];
                v5f = dl;
                cl = v5d;
                cl = (byte)(cl - v5f);
                cl = (byte)(cl ^ v5e);
                v5d = cl;
                eax = v4c;
                dl = v5d;
                dataKey[eax] = dl;
            }

            return dataKey;
        }

        #endregion
    }
}