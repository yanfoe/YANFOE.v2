// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="BindableRichTextBox.cs">
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
//   The bindable rich text box.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.UI
{
    #region Required Namespaces

    using System.IO;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;

    #endregion

    // http://social.msdn.microsoft.com/forums/en-US/wpf/thread/a4f44623-3ace-4087-b887-ce8a8c6b5070

    /// <summary>
    /// The bindable rich text box.
    /// </summary>
    public class BindableRichTextBox : RichTextBox
    {
        // See MS TextBox._isInsideTextContentChange (via Reflector)
        #region Static Fields

        /// <summary>
        /// The text property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", 
            typeof(string), 
            typeof(BindableRichTextBox), 
            new FrameworkPropertyMetadata(
                string.Empty, 
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal, 
                _onTextPropertyChanged, 
                _coerceText, 
                true, 
                UpdateSourceTrigger.LostFocus));

        #endregion

        #region Fields

        /// <summary>
        /// The _is inside text content change.
        /// </summary>
        private bool _isInsideTextContentChange;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableRichTextBox"/> class.
        /// </summary>
        public BindableRichTextBox()
        {
            this.TextChanged += this._onTextChanged;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return (string)this.GetValue(TextProperty);
            }

            set
            {
                this.SetValue(TextProperty, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The _coerce text.
        /// </summary>
        /// <param name="d">
        /// The d.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        private static object _coerceText(DependencyObject d, object value)
        {
            return value ?? string.Empty;
        }

        /// <summary>
        /// The _on text property changed.
        /// </summary>
        /// <param name="d">
        /// The d.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void _onTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BindableRichTextBox o = (BindableRichTextBox)d;
            string v = (string)e.NewValue;

            if (o._isInsideTextContentChange)
            {
                return;
            }

            o._isInsideTextContentChange = true;
            o._setText(v);
            o._isInsideTextContentChange = false;

            o._clearUndo();
        }

        /// <summary>
        /// The _clear undo.
        /// </summary>
        private void _clearUndo()
        {
            var limit = this.UndoLimit;
            this.UndoLimit = 0;
            this.UndoLimit = limit;
        }

        /// <summary>
        /// The _get text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string _getText()
        {
            return new TextRange(this.Document.ContentStart, this.Document.ContentEnd).Text;
        }

        /// <summary>
        /// The _on text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _onTextChanged(object sender, TextChangedEventArgs e)
        {
            if (this._isInsideTextContentChange)
            {
                return;
            }

            this._isInsideTextContentChange = true;
            this.Text = this._getText();
            this._isInsideTextContentChange = false;
        }

        /// <summary>
        /// The _set text.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        private void _setText(string text)
        {
            // new TextRange(Document.ContentStart, Document.ContentEnd).Text = text;
            this.SelectAll();
            MemoryStream memStream = new MemoryStream(Encoding.Default.GetBytes(text));
            this.Selection.Load(memStream, DataFormats.Rtf);
        }

        #endregion
    }
}