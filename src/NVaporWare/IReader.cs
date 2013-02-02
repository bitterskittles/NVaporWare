// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReader.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the IReader type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;
    using System.Diagnostics.Contracts;
    using System.IO;

    [ContractClass(typeof(IReaderContract<>))]
    public interface IReader<out T>
    {
        #region Public Methods and Operators

        T Read(Stream stream, int offset);

        #endregion
    }

    [ContractClassFor(typeof(IReader<>))]
    public abstract class IReaderContract<T> : IReader<T>
    {
        #region Public Methods and Operators

        public T Read(Stream stream, int offset)
        {
            Contract.Requires<ArgumentNullException>(stream != null);
            Contract.Requires<ArgumentException>(stream.CanRead);
            Contract.Requires<ArgumentException>(stream.CanSeek);
            Contract.Requires<ArgumentOutOfRangeException>(offset >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(offset <= 0xFFFF);

            throw new NotImplementedException();
        }

        #endregion
    }
}