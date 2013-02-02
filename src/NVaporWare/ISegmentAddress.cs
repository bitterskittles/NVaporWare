// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISegmentAddress.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the ISegmentAddress type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(ISegmentAddressContract))]
    public interface ISegmentAddress
    {
        #region Public Properties

        int Offset { get; }

        int Size { get; }

        #endregion
    }

    [ContractClassFor(typeof(ISegmentAddress))]
    public abstract class ISegmentAddressContract : ISegmentAddress
    {
        #region Public Properties

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