// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFirmwareService.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the IFirmwareService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.VapeAssistant.Services
{
    using System;
    using System.Diagnostics.Contracts;

    using NVaporWare.VapeAssistant.ViewModels;

    [ContractClass(typeof(FirmwareServiceContract))]
    public interface IFirmwareService
    {
        #region Public Methods and Operators

        FirmwareViewModel Load(string path);

        void Save(FirmwareViewModel firmware);

        #endregion
    }

    [ContractClassFor(typeof(IFirmwareService))]
    public abstract class FirmwareServiceContract : IFirmwareService
    {
        #region Public Methods and Operators

        public FirmwareViewModel Load(string path)
        {
            throw new NotImplementedException();
        }

        public void Save(FirmwareViewModel firmware)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}