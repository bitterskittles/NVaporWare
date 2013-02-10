// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFactory.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the IFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.VapeAssistant.Services.Factories
{
    using System;
    using System.Diagnostics.Contracts;

    [ContractClass(typeof(IFactoryContract<>))]
    public interface IFactory<out T>
        where T : class
    {
        #region Public Methods and Operators

        T Create(object parameter);

        #endregion
    }

    [ContractClassFor(typeof(IFactory<>))]
    public abstract class IFactoryContract<T> : IFactory<T>
        where T : class
    {
        #region Public Methods and Operators

        public T Create(object parameter)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}