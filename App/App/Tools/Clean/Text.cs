// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Text.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools.Clean
{
    using System;
    using System.Text.RegularExpressions;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.InternalApps.Logs.Enums;

    /// <summary>
    /// Text Cleaning methods.
    /// </summary>
    public static class Text
    {
        /// <summary>
        /// HTML to UTF8 conversion
        /// </summary>
        private static readonly string[,] MatrixConversion = 
        {
            { "&Agrave;", "À" }, 
            { "&Aacute;", "Á" },
            { "&Acirc;", "Â" }, 
            { "&Atilde;", "Ã" },
            { "&Auml;", "Ä" },
            { "&Aring;", "Å" },
            { "&AElig;", "Æ" }, 
            { "&Ccedil;", "Ç" },
            { "&Egrave;", "È" }, 
            { "&Eacute;", "É" },
            { "&Ecirc;", "È" }, 
            { "&Euml;", "Ë" },
            { "&Igrave;", "Ì" }, 
            { "&Igrave;", "Ì" },
            { "&Iacute;", "Í" }, 
            { "&Icirc;", "Î" },
            { "&Iuml;", "Ï" }, 
            { "&ETH;", "Ð" },
            { "&Ntilde;", "Ñ" }, 
            { "&Ograve;", "Ò" },
            { "&Oacute;", "Ó" }, 
            { "&Ocirc;", "Ô" },
            { "&Otilde;", "Õ" }, 
            { "&Ouml;", "Ö" },
            { "&Oslash;", "Ø" }, 
            { "&Ugrave;", "Ù" },
            { "&Uacute;", "Ú" }, 
            { "&Ucirc;", "Û" },
            { "&Uuml;", "Ü" }, 
            { "&Yacute;", "Ý" },
            { "&THORN;", "Þ" }, 
            { "&szlig;", "ß" },
            { "&agrave;", "à" }, 
            { "&aacute;", "á" },
            { "&acirc;", "â" }, 
            { "&atilde;", "ã" },
            { "&auml;", "ä" }, 
            { "&aring;", "å" },
            { "&aelig;", "æ" }, 
            { "&ccedil;", "ç" },
            { "&egrave;", "è" }, 
            { "&eacute;", "é" },
            { "&ecirc;", "ê" }, 
            { "&euml;", "ë" },
            { "&igrave;", "ì" }, 
            { "&iacute;", "í" },
            { "&icirc;", "î" }, 
            { "&iuml;", "ï" }, 
            { "&eth;", "ð" },
            { "&ntilde;", "ñ" }, 
            { "&ograve;", "ò" },
            { "&oacute;", "ó" }, 
            { "&ocirc;", "ô" },
            { "&otilde;", "õ" }, 
            { "&ouml;", "ö" },
            { "&oslash;", "ø" }, 
            { "&ugrave;", "ù" },
            { "&uacute;", "ú" }, 
            { "&ucirc;", "û" },
            { "&uuml;", "ü" }, 
            { "&yacute;", "ý" },
            { "&thorn;", "þ" }, 
            { "&yuml;", "ÿ" },
            { "&quot;", @"""" }, 
            { "\n", String.Empty },
            { "\r", String.Empty },
            { "&nbsp;", " " },
            { "|", " " }, { "%20", " " }, 
            { "&#xB0;", "°" }, 
            { "&#x26;", "&" }, 
            { "&#xB7;", "-" },
            { "&#xB3;", "³" },
            { "&#xC0;", "À" }, 
            { "&#x22;", " " }, 
            { "&#x27;", "'" }, 
            { "&#xE0;", "à" }, 
            { "&#xC1;", "Á" },
            { "&#xE1;", "á" },
            { "&#xC2;", "Â" },
            { "&#xE2;", "â" },
            { "&#xC3;", "Ã" },
            { "&#xE3;", "ã" },
            { "&#xC4;", "Ä" },
            { "&#xE4;", "ä" },
            { "&#xC5;", "Å" },
            { "&#xE5;", "å" },
            { "&#xE6;", "æ" },
            { "&#x1E02;", "Ḃ" },
            { "&#x1E03;", "ḃ" },
            { "&#xC7;", "Ç" },
            { "&#xE7;", "ç" },
            { "&#xD0;", "Ð" },
            { "&#xF0;", "ð" },
            { "&#xC8;", "È" },
            { "&#xE8;", "è" },
            { "&#xC9;", "É" },
            { "&#xE9;", "é" },
            { "&#xCA;", "Ê" },
            { "&#xEA;", "ê" },
            { "&#xCB;", "Ë" },
            { "&#xEB;", "ë" },
            { "&#xCC;", "Ì" },
            { "&#xEC;", "ì" },
            { "&#xCD;", "Í" },
            { "&#xED;", "í" },
            { "&#xCE;", "Î" },
            { "&#xEE;", "î" },
            { "&#xCF;", "Ï" },
            { "&#xEF;", "ï" },
            { "&#xD1;", "Ñ" },
            { "&#xF1;", "ñ" },
            { "&#xD2;", "Ò" },
            { "&#xF2;", "ò" },
            { "&#xD3;", "Ó" },
            { "&#xF3;", "ó" },
            { "&#xD4;", "Ô" },
            { "&#xF4;", "ô" },
            { "&#xD5;", "Õ" },
            { "&#xF5;", "õ" },
            { "&#xD6;", "Ö" },
            { "&#xF6;", "ö" },
            { "&#xD8;", "Ø" },
            { "&#xF8;", "ø" },
            { "&#x152;", "Œ" },
            { "&#x153;", "œ" }, 
            { "&#xDF;", "ß" }, 
            { "&#xDE;", "Þ" },
            { "&#xFE;", "þ" },
            { "&#xD9;", "Ù" },
            { "&#xF9;", "ù" },
            { "&#xDA;", "Ú" },
            { "&#xFA;", "ú" },
            { "&#xDB;", "Û" },
            { "&#xFB;", "û" },
            { "&#xDC;", "Ü" },
            { "&#xFC;", "ü" },
            { "&#xDD;", "Ý" },
            { "&#xFD;", "ý" },
            { "&#x9F;", "Ÿ" },
            { "&#xFF;", "ÿ" },
            { "&#xFC;", "ü" },
            { "&#xE5;", "å" },
            { "&#xF6;", "ö" },
            { "&#xC6;", "Æ" },
            { "&#x27;", "\'" },
            { "&#34;", "\"" },
            { "&#38;", "&" },
            { "&#133;", "…" },
            { "&#145;", "‘" },
            { "&#150;", "–" },
            { "&#160;", " " },
            { "&#161;", "¡" },
            { "&#162;", "¢" },
            { "&#163;", "£" },
            { "&#164;", "¤" },
            { "&#165;", "¥" },
            { "&#166;", "¦" },
            { "&#167;", "§" },
            { "&#168;", "¨" },
            { "&#169;", "©" },
            { "&#170;", "ª" },
            { "&#171;", "«" },
            { "&#172;", "¬" },
            { "&#173;", "­" },
            { "&#174;", "®" },
            { "&#175;", "¯" },
            { "&#176;", "°" },
            { "&#177;", "±" },
            { "&#178;", "²" },
            { "&#179;", "³" },
            { "&#180;", "´" },
            { "&#181;", "µ" },
            { "&#182;", "¶" },
            { "&#183;", "·" },
            { "&#184;", "¸" },
            { "&#185;", "¹" },
            { "&#186;", "º" },
            { "&#187;", "»" },
            { "&#188;", "¼" },
            { "&#189;", "½" },
            { "&#190;", "¾" },
            { "&#191;", "¿" },
            { "&#192;", "À" },
            { "&#193;", "Á" },
            { "&#194;", "Â" },
            { "&#195;", "Ã" },
            { "&#196;", "Ä" },
            { "&#197;", "Å" },
            { "&#198;", "Æ" },
            { "&#199;", "Ç" },
            { "&#200;", "È" },
            { "&#201;", "É" },
            { "&#202;", "Ê" },
            { "&#203;", "Ë" },
            { "&#204;", "Ì" },
            { "&#205;", "Í" },
            { "&#206;", "Î" },
            { "&#207;", "Ï" },
            { "&#208;", "Ð" },
            { "&#209;", "Ñ" },
            { "&#210;", "Ò" },
            { "&#211;", "Ó" },
            { "&#212;", "Ô" },
            { "&#213;", "Õ" },
            { "&#214;", "Ö" },
            { "&#215;", "×" },
            { "&#216;", "Ø" },
            { "&#217;", "Ù" },
            { "&#218;", "Ú" },
            { "&#219;", "Û" },
            { "&#220;", "Ü" },
            { "&#221;", "Ý" },
            { "&#222;", "Þ" },
            { "&#223;", "ß" },
            { "&#224;", "à" },
            { "&#225;", "á" },
            { "&#226;", "â" },
            { "&#227;", "ã" },
            { "&#228;", "ä" },
            { "&#229;", "å" },
            { "&#230;", "æ" },
            { "&#231;", "ç" },
            { "&#232;", "è" },
            { "&#233;", "é" },
            { "&#234;", "ê" },
            { "&#235;", "ë" },
            { "&#236;", "ì" },
            { "&#237;", "í" },
            { "&#238;", "î" },
            { "&#239;", "ï" },
            { "&#240;", "ð" },
            { "&#241;", "ñ" },
            { "&#242;", "ò" },
            { "&#243;", "ó" },
            { "&#244;", "ô" },
            { "&#245;", "õ" },
            { "&#246;", "ö" },
            { "&#247;", "÷" },
            { "&#248;", "ø" },
            { "&#249;", "ù" },
            { "&#250;", "ú" },
            { "&#251;", "û" },
            { "&#252;", "ü" },
            { "&#253;", "ý" },
            { "&#254;", "þ" },
            { "&#255;", "ÿ" },
        };

        /// <summary>
        /// Applies both RemoveHtml and ValidizeResult on a string, along with removing all \r\n new line codes
        /// </summary>
        /// <param name="text">Input string.</param>
        /// <returns>Returns fully cleaned string</returns>
        public static string FullClean(string text)
        {
            const string LogCatagory = "Clean > Text > FullClean";

            try
            {
                text = ValidizeResult(text);
                text = RemoveHtml(text);
                text = text.Replace("\r", String.Empty);
                text = text.Replace("\n", String.Empty);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, LogCatagory, ex.Message);
            }

            return text;
        }

        /// <summary>
        /// Replace HTML special character codes with UTF characters.
        /// </summary>
        /// <param name="text">String for which replacements will be made.</param>
        /// <returns>Returns processed string</returns>
        public static string ValidizeResult(string text)
        {
            const string LogCatagory = "Clean > Text > ValidizeResult";

            try
            {
                for (var i = 0; i < MatrixConversion.GetLength(0); i++)
                {
                    text = text.Replace(MatrixConversion[i, 0], MatrixConversion[i, 1]);
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, LogCatagory, ex.Message);
            }

            return text;
        }

        /// <summary>
        /// Removes all html tags from within a string and all double (or more) spaces.
        /// </summary>
        /// <param name="text">The string for which all all HTML tags will be removed.</param>
        /// <returns>String with all HTML removed</returns>
        public static string RemoveHtml(string text)
        {
            const string LogCatagory = "Clean > Text > RemoveHtml";

            try
            {
                var output = Regex.Replace(text, @"<(.|\n)*?>|\s{2,}", String.Empty);
                return output.Trim();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, LogCatagory, ex.Message);
                return null;
            }
        }
    }
}
