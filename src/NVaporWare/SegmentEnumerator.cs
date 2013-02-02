// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SegmentEnumerator.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the SegmentEnumerator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;
    using System.Collections;
    using System.Diagnostics.Contracts;
    using System.IO;

    public class SegmentEnumerator<T> : ISegmentEnumerator<T>
        where T : class
    {
        #region Fields

        private readonly int lastPosition;

        private readonly IReader<T> reader;

        private readonly ISegmentAddress segment;

        private readonly Stream stream;

        private int position;

        #endregion

        #region Constructors and Destructors

        public SegmentEnumerator(Stream stream, ISegmentAddress segment, IReader<T> reader)
        {
            Contract.Requires<ArgumentNullException>(stream != null);
            Contract.Requires<ArgumentException>(stream.CanRead);
            Contract.Requires<ArgumentException>(stream.CanSeek);
            Contract.Requires<ArgumentNullException>(segment != null);
            Contract.Requires<ArgumentNullException>(reader != null);

            this.stream = stream;
            this.segment = segment;
            this.reader = reader;
            this.position = this.segment.Offset;
            this.lastPosition = this.segment.Offset + this.segment.Size;
        }

        #endregion

        #region Public Properties

        public ISegmentData<T> Current { get; private set; }

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
            if (this.position >= this.lastPosition)
            {
                this.Current = null;
                return false;
            }

            try
            {
                var data = this.reader.Read(this.stream, this.position);
                if (data == null)
                {
                    this.Current = null;
                    return false;
                }

                var dataSize = this.stream.Position - this.position;
                if (dataSize < 0)
                {
                    this.Current = null;
                    return false;
                }

                this.Current = new SegmentData<T>(data, this.position, (int)dataSize);
                this.position = (int)this.stream.Position;
                return true;
            }
            catch (Exception ex)
            {
                this.Current = null;
                throw new ApplicationException("Corrupt data", ex);
            }
        }

        public void Reset()
        {
            this.Current = null;
            this.position = this.segment.Offset;
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
            Contract.Invariant(this.stream.CanRead);
            Contract.Invariant(this.stream.CanSeek);
            Contract.Invariant(this.segment != null);
            Contract.Invariant(this.reader != null);
        }

        #endregion
    }
}