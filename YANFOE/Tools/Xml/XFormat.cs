namespace YANFOE.Tools.Xml
{
    using System;
    using System.Windows.Forms;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;

    public static class XFormat
    {
        public static void AddColouredText(string strTextToAdd, RichTextBox richTextBox)
        {
            try
            {
                // Use the RichTextBox to create the initial RTF code
                richTextBox.Clear();

                if (string.IsNullOrEmpty(strTextToAdd))
                {
                    return;
                }

                richTextBox.AppendText(strTextToAdd);
                string strRtf = richTextBox.Rtf;
                richTextBox.Clear();

                /* 
                 * ADD COLOUR TABLE TO THE HEADER FIRST 
                 * */

                // Search for colour table info, if it exists (which it shouldn't)
                // remove it and replace with our one
                int tableStart = strRtf.IndexOf("colortbl;");

                if (tableStart != -1)
                {
                    // find end of colortbl tab by searching
                    // forward from the colortbl tab itself
                    int tableEnd = strRtf.IndexOf('}', tableStart);
                    strRtf = strRtf.Remove(tableStart, tableEnd - tableStart);

                    // now insert new colour table at index of old colortbl tag
                    strRtf = strRtf.Insert(
                        tableStart,
                        "colortbl ;\\red255\\green0\\blue0;\\red0\\green128\\blue0;\\red0\\green0\\blue255;}");
                }
                else
                {
                    // find index of start of header
                    int startIndex = strRtf.IndexOf("\\rtf");

                    // get index of where we'll insert the colour table
                    // try finding opening bracket of first property of header first                
                    int insertLoc = strRtf.IndexOf('{', startIndex);

                    // if there is no property, we'll insert colour table
                    // just before the end bracket of the header
                    if (insertLoc == -1)
                    {
                        insertLoc = strRtf.IndexOf('}', startIndex) - 1;
                    }

                    // insert the colour table at our chosen location                
                    strRtf = strRtf.Insert(
                        insertLoc,
                        "{\\colortbl ;\\red128\\green0\\blue0;\\red0\\green128\\blue0;\\red0\\green0\\blue255;}");
                }

                for (int i = 0; i < strRtf.Length; i++)
                {
                    if (strRtf[i] == '<')
                    {
                        // add RTF tags after symbol 
                        // Check for comments tags 
                        strRtf = strRtf[i + 1] == '!' ? strRtf.Insert(i + 4, "\\cf2 ") : strRtf.Insert(i + 1, "\\cf1 ");

                        // add RTF before symbol
                        strRtf = strRtf.Insert(i, "\\cf3 ");

                        // skip forward past the characters we've just added
                        // to avoid getting trapped in the loop
                        i += 6;
                    }
                    else if (strRtf[i] == '>')
                    {
                        // add RTF tags after character
                        strRtf = strRtf.Insert(i + 1, "\\cf0 ");

                        // Check for comments tags
                        if (strRtf[i - 1] == '-')
                        {
                            strRtf = strRtf.Insert(i - 2, "\\cf3 ");

                            // skip forward past the 6 characters we've just added
                            i += 8;
                        }
                        else
                        {
                            strRtf = strRtf.Insert(i, "\\cf3 ");

                            // skip forward past the 6 characters we've just added
                            i += 6;
                        }
                    }
                }

                richTextBox.Rtf = strRtf;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, 0, ex.Message, ex.StackTrace);
            }
        }
    }
}
