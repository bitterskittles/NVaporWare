// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FirmwareService.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the FirmwareService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.VapeAssistant.Services
{
    using System.ComponentModel.Composition;
    using System.IO;

    using NVaporWare.VapeAssistant.ViewModels;

    [Export(typeof(IFirmwareService))]
    public class FirmwareService : IFirmwareService
    {
        #region Public Methods and Operators

        public FirmwareViewModel Load(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (var firmwareFile = new Firmware(fileStream))
            {
                var stream = firmwareFile.Stream;
            }

            return null;
        }

        public void Save(FirmwareViewModel firmware)
        {
        }

        #endregion
    }
}