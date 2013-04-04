// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dialogs.cs" company="The YANFOE Project">
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
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Tools.UI
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Choose the type of dialog to display
    /// </summary>
    public enum DialogType
    {
        /// <summary>
        /// Diaplays a Load dialog.
        /// </summary>
        Load,

        /// <summary>
        /// Displays a Save dialog.
        /// </summary>
        Save
    }

    /// <summary>
    /// Handles all dialog options within the YANFOE framework.
    /// </summary>
    public static class Dialogs
    {
        #region Public Methods

        /// <summary>
        /// Shows a Select a folder dialog.
        /// </summary>
        /// <param name="windowTitle">
        /// The window title.
        /// </param>
        /// <param name="defaultPath">
        /// The path to default to.
        /// </param>
        /// <returns>
        /// The select directory.
        /// </returns>
        public static string SelectDirectory(string windowTitle, string defaultPath = null)
        {
            if (defaultPath == null)
            {
                // defaultPath = MediaTypeNames.Application.StartupPath;
            }

            string returnString = String.Empty;

            if (String.IsNullOrEmpty(defaultPath))
            {
                defaultPath = Environment.SpecialFolder.Desktop.ToString();
            }

            var fileDialog = new FolderBrowserDialog
                {
                    Description = windowTitle, 
                    ShowNewFolderButton = false, 
                    RootFolder = Environment.SpecialFolder.Desktop, 
                    SelectedPath = defaultPath
                };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                returnString = fileDialog.SelectedPath;
            }

            return returnString;
        }

        /// <summary>
        /// Use a Open or Save dialog to select a file.
        /// </summary>
        /// <param name="windowTitle">
        /// The window title.
        /// </param>
        /// <param name="ext">
        /// The default file extention
        /// </param>
        /// <param name="filter">
        /// The filter
        /// </param>
        /// <param name="type">
        /// The dialog type.
        /// </param>
        /// <returns>
        /// The select file.
        /// </returns>
        public static string SelectFile(string windowTitle, string ext, string filter, DialogType type)
        {
            if (type == DialogType.Load)
            {
                var ofd = new OpenFileDialog
                    {
                       Multiselect = false, Title = windowTitle, DefaultExt = ext, Filter = filter 
                    };

                if (ofd.ShowDialog() == DialogResult.Cancel)
                {
                    return String.Empty;
                }

                return ofd.FileName;
            }
            else
            {
                var ofd = new SaveFileDialog { Title = windowTitle, DefaultExt = ext, Filter = filter };

                if (ofd.ShowDialog() == DialogResult.Cancel)
                {
                    return String.Empty;
                }

                return ofd.FileName;
            }
        }

        #endregion
    }
}