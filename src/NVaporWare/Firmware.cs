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
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Text;

    public class Firmware : IDisposable
    {
        #region Static Fields

        private static readonly byte[] HeaderControlBits = new byte[]
                                                               {
                                                                  0x04, 0x0A, 0x0C, 0x51, 0x08, 0x1A, 0x44, 0x09, 0x0A 
                                                               };

        private static readonly byte[] HeaderKey = InitializeHeaderKey();

        #endregion

        #region Fields

        private readonly FirmwareStream stream;

        private bool isDisposed;

        #endregion

        #region Constructors and Destructors

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", 
            Justification = "Ownership transferred to another Disposable class.")]
        public Firmware(byte[] key)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentException>(key.Length == 32);

            var memoryStream = new MemoryStream(new byte[65600]);
            memoryStream.Seek(64, SeekOrigin.Begin);
            Contract.Assume(memoryStream.CanSeek);
            Contract.Assume(memoryStream.Length >= 64);
            this.stream = new FirmwareStream(memoryStream, key, 64, false);
        }

        public Firmware(Stream stream)
            : this(stream, true)
        {
            Contract.Requires<ArgumentNullException>(stream != null);
            Contract.Requires<ArgumentException>(stream.CanSeek);
            Contract.Requires<ArgumentException>(stream.Length >= 64);
        }

        public Firmware(Stream stream, bool leaveOpen)
        {
            Contract.Requires<ArgumentNullException>(stream != null);
            Contract.Requires<ArgumentException>(stream.CanSeek);
            Contract.Requires<ArgumentException>(stream.Length >= 64);

            var key = new byte[32];
            Contract.Assume(HeaderKey.Length == 32);
            using (var headerStream = new FirmwareStream(stream, HeaderKey, 32))
            {
                headerStream.Seek(0, SeekOrigin.Begin);
                headerStream.Read(key, 0, 32);
            }

            Contract.Assume(stream.Length >= 64);
            this.stream = new FirmwareStream(stream, key, 64, leaveOpen);
        }

        #endregion

        #region Public Properties

        public Stream BaseStream
        {
            get
            {
                return this.stream.BaseStream;
            }
        }

        public int Checksum
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException("Stream");
                }

                if (this.stream.CanSeek == false)
                {
                    throw new NotSupportedException();
                }

                if (this.stream.CanRead == false)
                {
                    throw new NotSupportedException();
                }

                var position = this.stream.Position;
                try
                {
                    var checksum = 0;
                    this.stream.Seek(0, SeekOrigin.Begin);
                    var repeat = (this.stream.Length / 4096) + 1;
                    var buffer = new byte[4096];
                    for (var i = 0; i < repeat; i++)
                    {
                        var bytesRead = this.stream.Read(buffer, 0, buffer.Length);
                        for (var j = 0; j < bytesRead; j++)
                        {
                            checksum = checksum + buffer[j];
                        }
                    }

                    return checksum;
                }
                finally
                {
                    this.stream.Seek(position, SeekOrigin.Begin);
                }
            }
        }

        public Stream Stream
        {
            get
            {
                return this.stream;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void UpdateHeader()
        {
            if (this.stream != null && this.stream.CanWrite && this.stream.CanSeek)
            {
                this.BaseStream.Seek(0, SeekOrigin.Begin);
                this.BaseStream.Write(HeaderControlBits, 0, HeaderControlBits.Length);
                this.BaseStream.Write(new byte[7], 0, 7);
                var checksum = this.Checksum;
                this.BaseStream.Seek(16, SeekOrigin.Begin);
                this.BaseStream.Write(BitConverter.GetBytes(checksum), 0, 4);
                this.BaseStream.Write(new byte[12], 0, 12);

                Contract.Assume(HeaderKey.Length == 32);
                Contract.Assume(this.BaseStream.CanSeek);
                using (var headerStream = new FirmwareStream(this.BaseStream, HeaderKey))
                {
                    var key = this.stream.GetKey();
                    headerStream.Write(key, 0, key.Length);
                }
            }
        }

        #endregion

        #region Methods

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing && this.isDisposed == false)
                {
                    this.stream.Close();
                }
            }
            finally
            {
                this.isDisposed = true;
            }
        }

        private static byte[] InitializeHeaderKey()
        {
            const string HeaderKeyString = "sinowealth chenshaofan 20121101    ";
            var bytes = Encoding.ASCII.GetBytes(HeaderKeyString);
            var headerKey = new byte[32];
            Contract.Assume(32 <= bytes.GetLowerBound(0) + bytes.Length);
            Array.Copy(bytes, headerKey, 32);
            return headerKey;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.stream != null);
        }

        #endregion
    }
}