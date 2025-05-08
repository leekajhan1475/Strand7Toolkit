using St7API;

namespace St7Toolkit
{
    /// <summary>
    /// Temperature in units of: <para/> 
    /// Celsius (C)<para/>
    /// Fahrenheit = C × 9/5 + 32<para/> 
    /// Kelvin = C + 273.15<para/> 
    /// Rankine = C x 9/5 + 491.67
    /// </summary>
    public enum TemperatureUnitSystem
    {
        /// <summary>
        /// No temperture unit system is specified
        /// </summary>
        None = 0,

        /// <summary>
        /// Celsius
        /// </summary>
        C,

        /// <summary>
        /// Fahrenheit
        /// </summary>
        F,

        /// <summary>
        /// Kelvin
        /// </summary>
        K,

        /// <summary>
        /// Rankine
        /// </summary>
        R,

        /// <summary>
        /// No unit system is set
        /// </summary>
        Unset = 255
    }
}
