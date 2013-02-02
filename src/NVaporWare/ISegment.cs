// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISegment.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the ISegment type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(ISegmentContract<>))]
    public interface ISegment<T> : IEnumerable<ISegmentData<T>>
        where T : class
    {
        #region Public Properties

        ISegmentAddress Address { get; }

        #endregion
    }

    [ContractClassFor(typeof(ISegment<>))]
    public abstract class ISegmentContract<T> : ISegment<T>
        where T : class
    {
        #region Public Properties

        public ISegmentAddress Address
        {
            get
            {
                Contract.Ensures(Contract.Result<ISegmentAddress>() != null);

                throw new NotImplementedException();
            }
        }

        #endregion

        #region Public Methods and Operators

        public IEnumerator<ISegmentData<T>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Explicit Interface Methods

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}