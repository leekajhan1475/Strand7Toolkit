using St7API;


namespace St7Toolkit
{
    /// <summary>
    /// Defines the XYZ axis system type
    /// </summary>
    public enum UCSType
    {
        /// <summary>
        /// Cartesian system
        /// </summary>
        Cartesian = St7.csCartesian,
    
        /// <summary>
        /// Cylindrical system
        /// </summary>
        Cylindrical = St7.csCylindrical,

        /// <summary>
        /// Spherical system
        /// </summary>
        Spherical = St7.csSpherical,

        /// <summary>
        /// Toroidal system
        /// </summary>
        Toroidal = St7.csToroidal
    }
}
