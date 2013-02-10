// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpenFileDialogService.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the OpenFileDialogService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.VapeAssistant.Services
{
    using System;
    using System.ComponentModel.Composition;

    using Microsoft.Win32;

    [Export(typeof(IOpenFileDialogService))]
    public class OpenFileDialogService : IOpenFileDialogService
    {
        #region Fields

        private readonly Lazy<OpenFileDialog> openFileDialog;

        #endregion

        #region Constructors and Destructors

        public OpenFileDialogService()
        {
            this.openFileDialog = new Lazy<OpenFileDialog>(() => new OpenFileDialog());
        }

        #endregion

        #region Public Properties

        public string FileName
        {
            get
            {
                return this.OpenFileDialog.FileName;
            }
        }

        #endregion

        #region Properties

        protected OpenFileDialog OpenFileDialog
        {
            get
            {
                return this.openFileDialog.Value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public bool ShowDialog()
        {
            return this.OpenFileDialog.ShowDialog().GetValueOrDefault(false);
        }

        #endregion
    }
}