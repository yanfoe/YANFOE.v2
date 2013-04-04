// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="ModelBase.cs">
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
//   The Model Base Class
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Models
{
    #region Required Namespaces

    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    using YANFOE.Factories.Internal;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   The Model Base Class
    /// </summary>
    [Serializable]
    public abstract class ModelBase : INotifyPropertyChanged, IDisposable
    {
        #region Fields

        /// <summary>
        ///   The disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        ///   The handle.
        /// </summary>
        private IntPtr handle;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="ModelBase" /> class.
        /// </summary>
        public ModelBase()
        {
            this.ChangedValues = new ThreadedBindingList<string>();
        }

        #endregion

        #region Public Events

        /// <summary>
        ///   Occurs when a property value changes.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the changed values.
        /// </summary>
        /// <value> The changed values. </value>
        public ThreadedBindingList<string> ChangedValues { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   The clear changes.
        /// </summary>
        public void ClearChanges()
        {
            this.ChangedValues.Clear();
        }

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing. 
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                CloseHandle(this.handle);
                this.handle = IntPtr.Zero;

                this.disposed = true;
            }
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="name">
        /// The name of the changed variable 
        /// </param>
        /// <param name="databaseDirty">
        /// The database Dirty.
        /// </param>
        protected void OnPropertyChanged(string name, bool databaseDirty = false)
        {
            if (DatabaseIOFactory.AppLoading)
            {
                return;
            }

            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (databaseDirty)
            {
                DatabaseIOFactory.DatabaseDirty = true;
            }

            if (handler != null)
            {
                try
                {
                    handler(this, new PropertyChangedEventArgs(name));
                }
                catch (Exception ex)
                {
                    Debug.Write(ex);
                }
            }
        }

        /// <summary>
        /// The close handle.
        /// </summary>
        /// <param name="handle">
        /// The handle. 
        /// </param>
        /// <returns>
        /// bool value 
        /// </returns>
        [DllImport("Kernel32")]
        private static extern bool CloseHandle(IntPtr handle);

        #endregion
    }
}