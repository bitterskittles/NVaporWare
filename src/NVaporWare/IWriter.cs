// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWriter.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the IWriter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;
    using System.Diagnostics.Contracts;
    using System.IO;

    [ContractClass(typeof(IWriterContract<>))]
    public interface IWriter<in T>
    {
        #region Public Methods and Operators

        void Write(Stream stream, int offset, T bitmap);

        #endregion
    }

    [ContractClassFor(typeof(IWriter<>))]
    public abstract class IWriterContract<T> : IWriter<T>
    {
        #region Public Methods and Operators

        public void Write(Stream stream, int offset, T bitmap)
        {
            Contract.Requires<ArgumentNullException>(stream != null);
            Contract.Requires<ArgumentException>(stream.CanSeek);
            Contract.Requires<ArgumentException>(stream.CanWrite);
            Contract.Requires<ArgumentOutOfRangeException>(offset >= 0);
            Contract.Requires<ArgumentOutOfRangeException>(offset <= 0xFFFF);

            throw new NotImplementedException();
        }

        #endregion
    }
}