// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkspaceViewModel.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the WorkspaceViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.VapeAssistant.ViewModels
{
    using System.ComponentModel.Composition;
    using System.Diagnostics.Contracts;
    using System.IO;

    using Caliburn.Micro;

    using NVaporWare.VapeAssistant.Events;
    using NVaporWare.VapeAssistant.Services;

    [Export]
    public class WorkspaceViewModel : Screen, IHandle<LoadClickedEvent>, IHandle<SaveClickedEvent>
    {
        #region Fields

        private readonly IEventAggregator events;

        private readonly IFirmwareService firmwareService;

        private readonly IMessageBoxService messageBoxService;

        private readonly IOpenFileDialogService openFileDialog;

        private FirmwareViewModel firmware;

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public WorkspaceViewModel(
            IEventAggregator events, 
            IFirmwareService firmwareService, 
            IOpenFileDialogService openFileDialog, 
            IMessageBoxService messageBoxService)
        {
            this.events = events;
            this.firmwareService = firmwareService;
            this.openFileDialog = openFileDialog;
            this.messageBoxService = messageBoxService;
        }

        #endregion

        #region Public Properties

        public FirmwareViewModel Firmware
        {
            get
            {
                return this.firmware;
            }

            set
            {
                if (Equals(value, this.firmware))
                {
                    return;
                }

                this.firmware = value;
                this.NotifyOfPropertyChange();
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Load(string path)
        {
            if (File.Exists(path) == false)
            {
                return;
            }

            try
            {
                this.Firmware = this.firmwareService.Load(path);
            }
            catch
            {
                this.messageBoxService.Show(string.Format("Could not load\n\"{0}\"", path), "Firmware Editor");
            }
        }

        public void Save()
        {
            Contract.Requires(this.Firmware != null);
        }

        #endregion

        #region Explicit Interface Methods

        void IHandle<LoadClickedEvent>.Handle(LoadClickedEvent message)
        {
            var result = this.openFileDialog.ShowDialog();
            if (result == false)
            {
                return;
            }

            this.Load(this.openFileDialog.FileName);
        }

        void IHandle<SaveClickedEvent>.Handle(SaveClickedEvent message)
        {
            this.Save();
        }

        #endregion

        #region Methods

        protected override void OnInitialize()
        {
            base.OnInitialize();
            this.events.Subscribe(this);
        }

        #endregion
    }
}