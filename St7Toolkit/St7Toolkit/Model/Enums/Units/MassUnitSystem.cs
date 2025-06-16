using St7API;

namespace St7Toolkit
{
    /// <summary>
    /// Mass in units of kilograms, tonnes, grams, pounds or slugs 
    /// </summary>
    public enum MassUnitSystem
    {
        /// <summary>
        /// No length unit system is specified and model length unit system should be used.
        /// </summary>
        None = -1,

        /// <summary>
        /// Grams
        /// </summary>
        G = St7.muGRAM,

        /// <summary>
        /// Kilo-grams
        /// </summary>
        Kg = St7.muKILOGRAM,

        /// <summary>
        /// Metric tonnes
        /// </summary>
        T = St7.muTONNE,

        /// <summary>
        /// Pounds
        /// </summary>
        Lb = St7.muPOUND,

        /// <summary>
        /// Slugs
        /// </summary>
        Slug = St7.muSLUG,

        /// <summary>
        /// No unit system is set
        /// </summary>
        Unset = 255
    }
}
