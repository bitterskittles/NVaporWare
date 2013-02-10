// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CaptionButtons.xaml.cs" company="bitterskittles">
//   Copyright © 2013 bitterskittles.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the CaptionButtons type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NVaporWare.VapeAssistant.Controls
{
    using System.Diagnostics.Contracts;
    using System.Windows;

    public partial class CaptionButtons
    {
        #region Static Fields

        public static DependencyProperty MarginButtonProperty = DependencyProperty.Register(
            "MarginButton", typeof(Thickness), typeof(CaptionButtons));

        public static DependencyProperty TypeProperty = DependencyProperty.Register(
            "Type", typeof(CaptionType), typeof(CaptionButtons), new PropertyMetadata(CaptionType.Full));

        #endregion

        #region Fields

        private Window parent;

        #endregion

        #region Constructors and Destructors

        public CaptionButtons()
        {
            this.InitializeComponent();
            this.Loaded += this.CaptionButtonsLoaded;
        }

        #endregion

        #region Enums

        public enum CaptionType
        {
            Full, 

            Close, 

            ReduceClose
        }

        #endregion

        #region Public Properties

        public Thickness MarginButton
        {
            get
            {
                var o = this.GetValue(MarginButtonProperty);
                Contract.Assume(o != null);
                return (Thickness)o;
            }

            set
            {
                this.SetValue(MarginButtonProperty, value);
            }
        }

        public CaptionType Type
        {
            get
            {
                var o = this.GetValue(TypeProperty);
                Contract.Assume(o != null);
                return (CaptionType)o;
            }

            set
            {
                this.SetValue(TypeProperty, value);
            }
        }

        #endregion

        #region Methods

        private void CaptionButtonsLoaded(object sender, RoutedEventArgs e)
        {
            this.parent = this.GetTopParent();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Contract.Requires(this.parent != null);
            this.parent.Close();
        }

        private Window GetTopParent()
        {
            return Window.GetWindow(this);
        }

        private void MinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            Contract.Requires(this.parent != null);
            this.parent.WindowState = WindowState.Minimized;
        }

        private void RestoreButtonClick(object sender, RoutedEventArgs e)
        {
            Contract.Requires(this.parent != null);
            this.parent.WindowState = this.parent.WindowState == WindowState.Maximized
                                          ? WindowState.Normal
                                          : WindowState.Maximized;
        }

        #endregion
    }
}