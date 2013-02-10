// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FirmwareViewModel.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the FirmwareViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.VapeAssistant.ViewModels
{
    using Caliburn.Micro;

    public class FirmwareViewModel : PropertyChangedBase
    {
        #region Fields

        private readonly IObservableCollection<ImageViewModel> images;

        #endregion

        #region Constructors and Destructors

        public FirmwareViewModel()
        {
            this.images = new BindableCollection<ImageViewModel>();
        }

        #endregion

        #region Public Properties

        public IObservableCollection<ImageViewModel> Images
        {
            get
            {
                return this.images;
            }
        }

        #endregion
    }
}