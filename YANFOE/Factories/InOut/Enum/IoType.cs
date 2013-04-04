// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoType.cs" company="The YANFOE Project">
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

namespace YANFOE.Factories.InOut.Enum
{
    /// <summary>
    /// The movie io type.
    /// </summary>
    public enum MovieIOType
    {
        /// <summary>
        /// Movie IO Type : All
        /// </summary>
        All, 

        /// <summary>
        /// Movie IO Type : NFO
        /// </summary>
        Nfo, 

        /// <summary>
        /// Movie IO Type : Poster
        /// </summary>
        Poster, 

        /// <summary>
        /// Movie IO Type : Fanart
        /// </summary>
        Fanart,

        /// <summary>
        /// Movie IO Type : Trailer
        /// </summary>
        Trailer, 

        /// <summary>
        /// Movie IO Type : Images
        /// </summary>
        Images
    }

    /// <summary>
    /// The tv io type.
    /// </summary>
    public enum TVIoType
    {
        /// <summary>
        /// TV IO Type : All
        /// </summary>
        All, 

        /// <summary>
        /// TV IO Type : Series
        /// </summary>
        Series, 

        /// <summary>
        /// TV IO Type : Season
        /// </summary>
        Season, 

        /// <summary>
        /// TV IO Type : Episode
        /// </summary>
        Episode
    }

    /// <summary>
    /// The series io type.
    /// </summary>
    public enum SeriesIOType
    {
        /// <summary>
        /// Series IO Type : All
        /// </summary>
        All, 

        /// <summary>
        /// Series IO Type : NFO
        /// </summary>
        Nfo, 

        /// <summary>
        /// Series IO Type : Poster
        /// </summary>
        Poster, 

        /// <summary>
        /// Series IO Type : Fanart
        /// </summary>
        Fanart, 

        /// <summary>
        /// Series IO Type : Banner
        /// </summary>
        Banner, 

        /// <summary>
        /// Series IO Type : Images
        /// </summary>
        Images, 

        /// <summary>
        /// Series IO Type : None
        /// </summary>
        None
    }

    /// <summary>
    /// The season io type.
    /// </summary>
    public enum SeasonIOType
    {
        /// <summary>
        /// Season IO Type : All
        /// </summary>
        All, 

        /// <summary>
        /// Season IO Type : Poster
        /// </summary>
        Poster, 

        /// <summary>
        /// Season IO Type : Fanart
        /// </summary>
        Fanart, 

        /// <summary>
        /// Season IO Type : Banner
        /// </summary>
        Banner, 

        /// <summary>
        /// Season IO Type : None
        /// </summary>
        None
    }

    /// <summary>
    /// The episode io type.
    /// </summary>
    public enum EpisodeIOType
    {
        /// <summary>
        /// Episode IO Type : All
        /// </summary>
        All, 

        /// <summary>
        /// Episode IO Type : NFO
        /// </summary>
        Nfo, 

        /// <summary>
        /// Episode IO Type : Screenshot
        /// </summary>
        Screenshot, 

        /// <summary>
        /// Episode IO Type : None
        /// </summary>
        None
    }
}