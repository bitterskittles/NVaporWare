// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISegmentEnumerator.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the ISegmentEnumerator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(ISegmentEnumeratorContract<>))]
    public interface ISegmentEnumerator<T> : IEnumerator<ISegmentData<T>>
        where T : class
    {
    }

    [ContractClassFor(typeof(ISegmentEnumerator<>))]
    public abstract class ISegmentEnumeratorContract<T> : ISegmentEnumerator<T>
        where T : class
    {
        #region Public Properties

        public ISegmentData<T> Current
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Explicit Interface Properties

        object IEnumerator.Current
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}