// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageBoxService.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the MessageBoxService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.VapeAssistant.Services
{
    using System.ComponentModel.Composition;
    using System.Windows;

    [Export(typeof(IMessageBoxService))]
    public class MessageBoxService : IMessageBoxService
    {
        #region Public Methods and Operators

        public void Show(string messageBoxText, string caption)
        {
            MessageBox.Show(messageBoxText, caption);
        }

        #endregion
    }
}