using St7API;

namespace St7Toolkit
{
    /// <summary>
    /// Length units in meters, centimeters, millimeters, feet or inches
    /// </summary>
    public enum LengthUnitSystem
    {
        /// <summary>
        /// No length unit system is specified
        /// </summary>
        None = 0,

        /// <summary>
        /// mm
        /// </summary>
        Millimeters = St7.luMILLIMETRE,

        /// <summary>
        /// cm
        /// </summary>
        Centimeters = St7.luCENTIMETRE,

        /// <summary>
        /// m
        /// </summary>
        Meters = St7.luMETRE,

        /// <summary>
        /// Inch
        /// </summary>
        Inches = St7.luINCH,

        /// <summary>
        /// Ft
        /// </summary>
        Feet = St7.luFOOT,

        /// <summary>
        /// No unit system is set
        /// </summary>
        Unset = 255
    }
}
