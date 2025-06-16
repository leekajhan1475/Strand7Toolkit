using System;
using System.Collections.Generic;
using System.Text;
using St7Toolkit.Element;
using St7API;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using System.IO;

namespace St7Toolkit
{
    /// <summary>
    /// Angle in units of:<para/>
    /// Turns (1 turn = 360 arc degrees)<para/>
    /// Radians (1pi radian = 180 arc degrees)<para/>
    /// Degrees <para/>
    /// Minutes (60 arc minutes = 1 arc degree)<para/>
    /// Seconds (3600 arc seconds = 1 arc degree)<para/>
    /// Gradians (400 gradians = 2pi radians = 360 arc degree)<para/>
    /// </summary>
    public enum AngleUnitSystem
    {
        None = -1,     // None indicates no angle unit system is specified and model angle unit system should be used.
        Turns = 1,
        Radians = 2,
        Degree = 3,
        Minutes = 4,
        Seconds = 5,
        Gradians = 6,

        /// <summary>
        /// No unit system is set
        /// </summary>
        Unset = 255
    }
}
