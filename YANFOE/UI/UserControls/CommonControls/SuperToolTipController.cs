// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="SuperToolTipController.cs">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <license>
//   This software is licensed under a Creative Commons License
//   Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0)
//   http://creativecommons.org/licenses/by-nc-sa/3.0/
//   See this page: http://www.yanfoe.com/license
//   For any reuse or distribution, you must make clear to others the
//   license terms of this work.
// </license>
// <summary>
//   The super tool tip controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.UI.UserControls.CommonControls
{
    #region Required Namespaces

    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    #endregion

    /// <summary>
    /// The super tool tip controller.
    /// </summary>
    public class SuperToolTipController : UserControl
    {
        #region Fields

        /// <summary>
        /// The tooltip window.
        /// </summary>
        private readonly Popup tooltipWindow;

        /// <summary>
        /// The parent element.
        /// </summary>
        private FrameworkElement parentElement;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SuperToolTipController"/> class.
        /// </summary>
        public SuperToolTipController()
        {
            this.tooltipWindow = new Popup();
            this.tooltipWindow.AllowsTransparency = false;

            // tooltipWindow.Background = new SolidColorBrush(Colors.Red);
            this.tooltipWindow.HorizontalAlignment = HorizontalAlignment.Left;
            this.tooltipWindow.VerticalAlignment = VerticalAlignment.Top;
            this.tooltipWindow.Placement = PlacementMode.AbsolutePoint;
            this.tooltipWindow.Width = 50;
            this.tooltipWindow.Height = 50;

            // tooltipWindow.IsOpen = true;
            this.Initialized += this.SuperToolTipController_Initialized;

            // parentElement.MouseMove += new System.Windows.Input.MouseEventHandler(parentElement_MouseMove);
        }

        #endregion

        #region Delegates

        /// <summary>
        /// The get tool tip info handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public delegate void GetToolTipInfoHandler(object sender, GetToolTipInfoHandlerArgs args);

        #endregion

        #region Public Events

        /// <summary>
        /// The get tooltip info.
        /// </summary>
        public event GetToolTipInfoHandler GetTooltipInfo;

        #endregion

        #region Methods

        /// <summary>
        /// The parent element on mouse enter.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="mouseEventArgs">
        /// The mouse event args.
        /// </param>
        private void ParentElementOnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
        }

        /// <summary>
        /// The parent element on mouse leave.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="mouseEventArgs">
        /// The mouse event args.
        /// </param>
        private void ParentElementOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            // tooltipWindow.IsOpen = false;
        }

        /// <summary>
        /// The parent element on mouse move.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ParentElementOnMouseMove(object sender, MouseEventArgs e)
        {
            UIElement hitElement = (UIElement)this.parentElement.InputHitTest(e.GetPosition(this.parentElement));
            var controlMousePosition = e.GetPosition(hitElement);

            if (this.GetTooltipInfo != null)
            {
                var args = new GetToolTipInfoHandlerArgs(controlMousePosition);
                args.Element = hitElement;
                args.MousePosition = e;
                this.GetTooltipInfo(this, args);

                if (args.Info != null)
                {
                    this.tooltipWindow.PlacementRectangle =
                        new Rect(
                            this.PointToScreen(Mouse.GetPosition(this.parentElement)).X + 10, 
                            this.PointToScreen(Mouse.GetPosition(this.parentElement)).Y + 10, 
                            50, 
                            50);
                }

                // tooltipWindow.Margin = new Thickness(, 0, 0);
                // tooltipWindow.Margin = new Thickness(10, 10, 0, 0);
            }
        }

        /// <summary>
        /// The super tool tip controller_ initialized.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SuperToolTipController_Initialized(object sender, EventArgs e)
        {
            this.parentElement = this.Parent as FrameworkElement;
            if (this.parentElement != null)
            {
                this.parentElement.MouseEnter += this.ParentElementOnMouseEnter;
                this.parentElement.MouseLeave += this.ParentElementOnMouseLeave;
                this.parentElement.MouseMove += this.ParentElementOnMouseMove;
            }
        }

        #endregion
    }

    /// <summary>
    /// The get tool tip info handler args.
    /// </summary>
    public class GetToolTipInfoHandlerArgs
    {
        #region Fields

        /// <summary>
        /// The control mouse position.
        /// </summary>
        public Point ControlMousePosition;

        /// <summary>
        /// The element.
        /// </summary>
        public UIElement Element;

        /// <summary>
        /// The info.
        /// </summary>
        public SuperToolTipControlInfo Info;

        /// <summary>
        /// The mouse position.
        /// </summary>
        public MouseEventArgs MousePosition;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetToolTipInfoHandlerArgs"/> class.
        /// </summary>
        /// <param name="controlMousePosition">
        /// The control mouse position.
        /// </param>
        public GetToolTipInfoHandlerArgs(Point controlMousePosition)
        {
            this.ControlMousePosition = controlMousePosition;
        }

        #endregion
    }

    /// <summary>
    /// The super tool tip control info.
    /// </summary>
    public class SuperToolTipControlInfo
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SuperToolTipControlInfo"/> class.
        /// </summary>
        /// <param name="identifier">
        /// The identifier.
        /// </param>
        public SuperToolTipControlInfo(string identifier)
        {
            this.Identifier = identifier;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the super tool tip.
        /// </summary>
        public SuperToolTip SuperToolTip { get; set; }

        #endregion
    }
}