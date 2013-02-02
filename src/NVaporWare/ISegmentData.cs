// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISegmentData.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the ISegmentData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(ISegmentDataContract<>))]
    public interface ISegmentData<T>
        where T : class
    {
        #region Public Properties

        T Data { get; }

        int Offset { get; }

        int Size { get; }

        #endregion
    }

    [ContractClassFor(typeof(ISegmentData<>))]
    public abstract class ISegmentDataContract<T> : ISegmentData<T>
        where T : class
    {
        #region Public Properties

        public T Data
        {
            get
            {
                Contract.Ensures(Contract.Result<T>() != null);

                throw new NotImplementedException();
            }
        }

        public int Offset
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                Contract.Ensures(Contract.Result<int>() <= 0xFFFF);

                throw new NotImplementedException();
            }
        }

        public int Size
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                Contract.Ensures(Contract.Result<int>() + this.Offset - 1 <= 0xFFFF);

                throw new NotImplementedException();
            }
        }

        #endregion
    }
}