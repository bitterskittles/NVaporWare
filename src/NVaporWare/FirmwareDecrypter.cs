// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FirmwareDecrypter.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the FirmwareDecrypter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;
    using System.IO;
    using System.Text;

    public class FirmwareDecrypter
    {
        #region Static Fields

        private static readonly byte[] HeaderKey;

        #endregion

        #region Constructors and Destructors

        static FirmwareDecrypter()
        {
            const string headerKeyString = "sinowealth chenshaofan 20121101    ";
            var bytes = Encoding.ASCII.GetBytes(headerKeyString);
            HeaderKey = new byte[32];
            Array.Copy(bytes, HeaderKey, 32);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Decrypts Joyetech eVic firmware file and returns its contents.
        /// </summary>
        /// <param name="path">
        /// A relative or absolute path for the file that will be decrypted. 
        /// </param>
        /// <returns>
        /// Decrypted contents of the firwmware file. 
        /// </returns>
        public Firmware Decrypt(string path)
        {
            if (File.Exists(path) == false)
            {
                throw new ArgumentException("File does not exists.", "path");
            }

            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                var fileHeader = new byte[0x40];
                fileStream.Read(fileHeader, 0, fileHeader.Length);

                var header = new FirmwareHeader(fileHeader);

                var data = new byte[0x10000];
                fileStream.Read(data, 0, data.Length);
                var contents = this.DecryptBinFileData(data, header.EncryptionKey);
                var firmwave = new Firmware(path, header, contents);
                return firmwave;
            }
            catch (Exception ex)
            {
                throw new Exception("Error decrypting file.", ex);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Decrypts .bin file data section.
        /// </summary>
        /// <param name="data">
        /// Data section of the .bin file (starts from file offset: 0x40) 
        /// </param>
        /// <param name="encryptionKey">
        /// 32 bytes long encryption key. 
        /// </param>
        /// <returns>
        /// Decrypted data section. 
        /// </returns>
        private byte[] DecryptBinFileData(byte[] data, byte[] encryptionKey)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            if (encryptionKey == null)
            {
                throw new ArgumentNullException("encryptionKey");
            }

            if (encryptionKey.Length != 0x20)
            {
                throw new ArgumentException("encryptionKey must be 32 bytes long.", "encryptionKey");
            }

            var decrypted = new byte[data.Length];

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

            // MVR.exe v1.0, offset: 0x00404333
            for (uint v4c = 0; v4c < data.Length; v4c++)
            {
                eax = v4c;
                eax = eax & 0x0f;
                v50 = eax;
                edx = v4c;
                cl = data[edx];
                v5d = cl;
                eax = v50;
                dl = encryptionKey[eax];
                v5e = dl;
                ecx = v50;
                al = encryptionKey[ecx + 0x10];
                v5f = al;
                dl = v5d;
                dl = (byte)(dl - v5f);
                v5e = (byte)(v5e ^ dl);
                ecx = v4c;
                al = v5e;
                decrypted[ecx] = al;
            }

            return decrypted;
        }

        #endregion
    }
}