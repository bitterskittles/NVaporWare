// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Segment.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the Segment type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.IO;

    public class Segment<T> : ISegment<T>
        where T : class
    {
        #region Fields

        private readonly ISegmentAddress address;

        private readonly IReader<T> reader;

        private readonly Stream stream;

        #endregion

        #region Constructors and Destructors

        public Segment(Stream stream, ISegmentAddress address, IReader<T> reader)
        {
            Contract.Requires<ArgumentNullException>(stream != null);
            Contract.Requires<ArgumentException>(stream.CanRead);
            Contract.Requires<ArgumentException>(stream.CanSeek);
            Contract.Requires<ArgumentNullException>(address != null);
            Contract.Requires<ArgumentNullException>(reader != null);

            this.stream = stream;
            this.address = address;
            this.reader = reader;
        }

        #endregion

        #region Public Properties

        public ISegmentAddress Address
        {
            get
            {
                return this.address;
            }
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerator<ISegmentData<T>> GetEnumerator()
        {
            return new SegmentEnumerator<T>(this.stream, this.address, this.reader);
        }

        #endregion

        #region Explicit Interface Methods

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region Methods

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.stream != null);
            Contract.Invariant(this.stream.CanRead);
            Contract.Invariant(this.stream.CanSeek);
            Contract.Invariant(this.address != null);
            Contract.Invariant(this.reader != null);
        }

        #endregion
    }
}