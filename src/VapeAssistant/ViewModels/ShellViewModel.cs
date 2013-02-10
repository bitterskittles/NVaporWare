// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShellViewModel.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the ShellViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.VapeAssistant.ViewModels
{
    using System.ComponentModel.Composition;

    using Caliburn.Micro;

    using NVaporWare.VapeAssistant.Events;

    [Export]
    public class ShellViewModel : Conductor<object>.Collection.AllActive, IHandle<ExitClickedEvent>
    {
        #region Fields

        private readonly IEventAggregator events;

        private readonly ToolBarViewModel toolBar;

        private readonly WorkspaceViewModel workspace;

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public ShellViewModel(IEventAggregator events, ToolBarViewModel toolBar, WorkspaceViewModel workspace)
        {
            this.events = events;
            this.toolBar = toolBar;
            this.workspace = workspace;

            this.Items.Add(this.toolBar);
            this.Items.Add(this.workspace);
        }

        #endregion

        #region Public Properties

        public ToolBarViewModel ToolBar
        {
            get
            {
                return this.toolBar;
            }
        }

        public WorkspaceViewModel Workspace
        {
            get
            {
                return this.workspace;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Handle(ExitClickedEvent message)
        {
            this.TryClose();
        }

        #endregion

        #region Methods

        protected override void OnInitialize()
        {
            base.OnInitialize();
            this.DisplayName = "Device Emulator";
            this.events.Subscribe(this);
        }

        #endregion
    }
}