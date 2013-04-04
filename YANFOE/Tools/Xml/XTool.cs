// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="XTool.cs">
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
//   The x tool.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Xml
{
    #region Required Namespaces

    using System;
    using System.Linq;
    using System.Text;
    using System.Xml;

    #endregion

    /// <summary>
    ///   The x tool.
    /// </summary>
    public static class XTool
    {
        #region Public Methods and Operators

        /// <summary>
        /// Whether a given character is allowed by XML 1.0.
        /// </summary>
        /// <param name="character">
        /// The character. 
        /// </param>
        /// <returns>
        /// The is legal xml char. 
        /// </returns>
        public static bool IsLegalXmlChar(int character)
        {
            return character == 0x9 || character == 0xA || character == 0xD
                   || (character >= 0x20 && character <= 0xD7FF) || (character >= 0xE000 && character <= 0xFFFD)
                   || (character >= 0x10000 && character <= 0x10FFFF);
        }

        /// <summary>
        /// The is valid xml.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsValidXML(string value)
        {
            try
            {
                // Check we actually have a value
                if (string.IsNullOrEmpty(value) == false)
                {
                    // Try to load the value into a document
                    XmlDocument xmlDoc = new XmlDocument();

                    xmlDoc.LoadXml(value);

                    // If we managed with no exception then this is valid XML!
                    return true;
                }
                else
                {
                    // A blank value is not valid xml
                    return false;
                }
            }
            catch (XmlException)
            {
                return false;
            }
        }

        /// <summary>
        /// Remove illegal XML characters from a string.
        /// </summary>
        /// <param name="xml">
        /// The xml. 
        /// </param>
        /// <returns>
        /// The sanitize xml string. 
        /// </returns>
        public static string SanitizeXmlString(string xml)
        {
            if (xml == null)
            {
                throw new ArgumentNullException("xml");
            }

            var buffer = new StringBuilder(xml.Length);

            foreach (char c in xml.Where(c => IsLegalXmlChar(c)))
            {
                buffer.Append(c);
            }

            return buffer.ToString();
        }

        #endregion
    }
}