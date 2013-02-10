// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuItemViewModel.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the MenuItemViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.VapeAssistant.ViewModels
{
    using System;

    using Caliburn.Micro;

    using Action = System.Action;

    public class MenuItemViewModel : PropertyChangedBase
    {
        #region Fields

        private readonly Func<bool> canExecute;

        private readonly string caption;

        private readonly Action onExecute;

        #endregion

        #region Constructors and Destructors

        public MenuItemViewModel(string caption, Action onExecute, Func<bool> canExecute = null)
        {
            this.caption = caption;
            this.onExecute = onExecute;
            this.canExecute = canExecute;
        }

        #endregion

        #region Public Properties

        public bool CanExecute
        {
            get
            {
                return this.canExecute == null || this.canExecute();
            }
        }

        public string Caption
        {
            get
            {
                return this.caption;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void CanExecuteChanged()
        {
            this.NotifyOfPropertyChange(() => this.CanExecute);
        }

        public void Execute()
        {
            this.onExecute();
        }

        #endregion
    }
}