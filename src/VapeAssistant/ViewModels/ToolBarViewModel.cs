// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ToolBarViewModel.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the ToolBarViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.VapeAssistant.ViewModels
{
    using System.ComponentModel.Composition;

    using Caliburn.Micro;

    using NVaporWare.VapeAssistant.Events;

    [Export]
    public class ToolBarViewModel : Screen
    {
        #region Fields

        private readonly IEventAggregator events;

        private readonly IObservableCollection<MenuItemViewModel> menuItems;

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public ToolBarViewModel(IEventAggregator events)
        {
            this.events = events;
            this.menuItems = new BindableCollection<MenuItemViewModel>();
        }

        #endregion

        #region Public Properties

        public IObservableCollection<MenuItemViewModel> MenuItems
        {
            get
            {
                return this.menuItems;
            }
        }

        #endregion

        #region Methods

        protected override void OnInitialize()
        {
            base.OnInitialize();
            this.menuItems.Add(new MenuItemViewModel("Load", () => this.events.Publish(new LoadClickedEvent())));
            this.menuItems.Add(new MenuItemViewModel("Save", () => this.events.Publish(new SaveClickedEvent())));
            this.menuItems.Add(new MenuItemViewModel("Exit", () => this.events.Publish(new ExitClickedEvent())));
        }

        #endregion
    }
}