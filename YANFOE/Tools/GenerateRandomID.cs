// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenerateRandomIDcs.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   Defines the GenerateRandomID type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Tools
{
    using System;
    using System.Linq;

    public static class GenerateRandomID
    {
        public static string Generate()
        {
            long i = Guid.NewGuid().ToByteArray().Aggregate<byte, long>(1, (current, b) => current * (b + 1));
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
    }
}
