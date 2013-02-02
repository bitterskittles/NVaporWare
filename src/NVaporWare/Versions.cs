// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Versions.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the Versions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare
{
    using System;

    // TODO: Move these into a xml
    public static class Versions
    {
        #region Static Fields

        private static readonly Lazy<FirmwareVersion> evic11 =
            new Lazy<FirmwareVersion>(() => new FirmwareVersion("1.1", 0x2B0F, 0x11FE, 0x3D0D, 0x011F, 0xECA1, 0x0305));

        #endregion

        #region Public Properties

        public static FirmwareVersion Evic11
        {
            get
            {
                return evic11.Value;
            }
        }

        #endregion
    }
}