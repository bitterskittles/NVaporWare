// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FirmwareStream.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the FirmwareStream type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;
    using System.Diagnostics.Contracts;
    using System.IO;

    public class FirmwareStream : Stream
    {
        #region Fields

        private readonly byte[] key;

        private readonly bool leaveOpen;

        private readonly int startOffset;

        private readonly Stream stream;

        private bool isDisposed;

        #endregion

        #region Constructors and Destructors

        public FirmwareStream(Stream stream, byte[] key, bool leaveOpen)
            : this(stream, key, 0, leaveOpen)
        {
            Contract.Requires<ArgumentNullException>(stream != null);
            Contract.Requires<ArgumentException>(stream.CanSeek);
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentException>(key.Length == 32);
        }

        public FirmwareStream(Stream stream, byte[] key)
            : this(stream, key, 0, true)
        {
            Contract.Requires<ArgumentNullException>(stream != null);
            Contract.Requires<ArgumentException>(stream.CanSeek);
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentException>(key.Length == 32);
        }

        public FirmwareStream(Stream stream, byte[] key, int startOffset)
            : this(stream, key, startOffset, true)
        {
            Contract.Requires<ArgumentNullException>(stream != null);
            Contract.Requires<ArgumentException>(stream.CanSeek);
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentException>(key.Length == 32);
            Contract.Requires<ArgumentOutOfRangeException>(startOffset >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(startOffset <= stream.Length);
        }

        public FirmwareStream(Stream stream, byte[] key, int startOffset, bool leaveOpen)
        {
            Contract.Requires<ArgumentNullException>(stream != null);
            Contract.Requires<ArgumentException>(stream.CanSeek);
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentException>(key.Length == 32);
            Contract.Requires<ArgumentOutOfRangeException>(startOffset >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(startOffset <= stream.Length);

            this.stream = stream;
            this.key = (byte[])key.Clone();
            this.startOffset = startOffset;
            this.leaveOpen = leaveOpen;
        }

        #endregion

        #region Public Properties

        public Stream BaseStream
        {
            get
            {
                Contract.Ensures(Contract.Result<Stream>() != null);
                return this.stream;
            }
        }

        public override bool CanRead
        {
            get
            {
                if (this.isDisposed)
                {
                    return false;
                }

                return this.stream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                if (this.isDisposed)
                {
                    return false;
                }

                return this.stream.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                if (this.isDisposed)
                {
                    return false;
                }

                return this.stream.CanWrite;
            }
        }

        public override long Length
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException("BaseStream");
                }

                var length = this.stream.Length - this.startOffset;
                if (length < 0)
                {
                    throw new InvalidOperationException();
                }

                return length;
            }
        }

        public override long Position
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException("BaseStream");
                }

                var position = this.stream.Position - this.startOffset;
                if (position < 0)
                {
                    this.stream.Seek(this.startOffset, SeekOrigin.Begin);
                    return 0;
                }

                return position;
            }

            set
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException("BaseStream");
                }

                if (this.stream.CanSeek == false)
                {
                    throw new NotSupportedException();
                }

                if (value + this.startOffset < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                this.stream.Position = value + this.startOffset;
            }
        }

        #endregion

        #region Public Methods and Operators

        public override void Flush()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException("BaseStream");
            }
        }

        public byte[] GetKey()
        {
            return (byte[])this.key.Clone();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var bytesRead = this.stream.Read(buffer, offset, count);

            var actualPosition = this.Position;
            for (var i = 0; i < bytesRead; i++)
            {
                var keyOffset = (actualPosition + i) & 0x0F;
                var value = buffer[i];
                value = (byte)(value - this.key[keyOffset + 0x10]);
                value = (byte)(value ^ this.key[keyOffset]);
                buffer[i] = value;
            }

            return bytesRead;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (this.stream.CanSeek == false)
            {
                throw new InvalidOperationException();
            }

            if (origin == SeekOrigin.End)
            {
                return this.stream.Seek(offset, origin) - this.startOffset;
            }

            return this.stream.Seek(offset + this.startOffset, origin) - this.startOffset;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var encBuffer = new byte[count];

            var actualPosition = this.Position;
            for (var i = 0; i < count; i++)
            {
                var keyOffset = (actualPosition + i) & 0x0F;
                var value = buffer[i];
                value = (byte)(value ^ this.key[keyOffset]);
                value = (byte)(value + this.key[keyOffset + 0x10]);
                encBuffer[i] = value;
            }

            Contract.Assume(count <= encBuffer.Length - offset);
            this.stream.Write(encBuffer, offset, count);
        }

        #endregion

        #region Methods

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && this.isDisposed == false)
                {
                    this.Flush();
                }
            }
            finally
            {
                try
                {
                    if (disposing && this.leaveOpen == false && this.isDisposed == false)
                    {
                        this.stream.Close();
                    }
                }
                finally
                {
                    this.isDisposed = true;
                    base.Dispose(disposing);
                }
            }
        }

        [ContractInvariantMethod]
        private new void ObjectInvariant()
        {
            Contract.Invariant(this.stream != null);
            Contract.Invariant(this.key != null);
            Contract.Invariant(this.key.Length == 32);
            Contract.Invariant(this.startOffset >= 0);
        }

        #endregion
    }
}