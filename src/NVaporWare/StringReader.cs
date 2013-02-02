// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringReader.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the StringReader type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System.Diagnostics.Contracts;
    using System.IO;

    public class StringReader : IReader<string>
    {
        #region Fields

        private readonly EvicEncoding evicEncoding;

        #endregion

        #region Constructors and Destructors

        public StringReader()
        {
            this.evicEncoding = new EvicEncoding();
        }

        #endregion

        #region Public Methods and Operators

        public string Read(Stream stream, int offset)
        {
            var position = stream.Seek(offset, SeekOrigin.Begin);
            var i = 0;
            var buffer = new byte[255];
            while (i < 255 && position <= 0xFFFF)
            {
                var data = (byte)stream.ReadByte();
                if (data == 0)
                {
                    break;
                }

                buffer[i] = data;
                i++;
            }

            return this.evicEncoding.GetString(buffer, 0, i);
        }

        #endregion

        #region Methods

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.evicEncoding != null);
        }

        #endregion
    }
}