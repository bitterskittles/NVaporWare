// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EvicEncoding.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the EvicEncoding type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System.Text;

    public class EvicEncoding : ASCIIEncoding
    {
        #region Public Methods and Operators

        public override string GetString(byte[] bytes, int byteIndex, int byteCount)
        {
            var clone = new byte[byteCount];
            for (var i = 0; i < byteCount; i++)
            {
                clone[i] = (byte)(bytes[byteIndex + i] + 0x2F);
            }

            return base.GetString(clone, 0, byteCount);
        }

        #endregion
    }
}