// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppBootstrapper.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the AppBootstrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.VapeAssistant
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Diagnostics.Contracts;

    using Caliburn.Micro;

    using NVaporWare.VapeAssistant.ViewModels;

    public sealed class AppBootstrapper : Bootstrapper<ShellViewModel>, IDisposable
    {
        #region Fields

        private CompositionContainer container;

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            if (this.container == null)
            {
                return;
            }

            try
            {
                this.container.Dispose();
            }
            finally
            {
                this.container = null;
            }
        }

        #endregion

        #region Methods

        protected override void BuildUp(object instance)
        {
            this.container.SatisfyImportsOnce(instance);
        }

        protected override void Configure()
        {
            Contract.Ensures(this.container != null);
            var catalog = new ApplicationCatalog();

            this.container = new CompositionContainer(catalog);

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(this.container);
            batch.AddExportedValue(catalog);

            this.container.Compose(batch);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            Contract.Assume(this.container != null);
            return this.container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            Contract.Ensures(Contract.Result<object>() != null);
            Contract.Assume(this.container != null);
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var export = this.container.GetExportedValue<object>(contract);

            if (export != null)
            {
                return export;
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        #endregion
    }
}