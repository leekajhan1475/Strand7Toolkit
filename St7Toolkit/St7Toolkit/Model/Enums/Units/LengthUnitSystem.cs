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
        None = -1,

        /// <summary>
        /// mm
        /// </summary>
        Millimeters = (int)St7.luMILLIMETRE,

        /// <summary>
        /// cm
        /// </summary>
        Centimeters = (int)St7.luCENTIMETRE,

        /// <summary>
        /// m
        /// </summary>
        Meters = (int)St7.luMETRE,

        /// <summary>
        /// Inch
        /// </summary>
        Inches = (int)St7.luINCH,

        /// <summary>
        /// Ft
        /// </summary>
        Feet = (int)St7.luFOOT,

        /// <summary>
        /// No unit system is set
        /// </summary>
        Unset = 255
    }
}
