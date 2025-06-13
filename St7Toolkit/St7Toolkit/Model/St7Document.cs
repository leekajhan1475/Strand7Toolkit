using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using St7Toolkit.Element;
using St7API;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;
using System.Data;
using Rhino;

namespace St7Toolkit
{
    /// <summary>
    /// Implements all of the Strand7 API methods.
    /// </summary>
    public partial class St7Model
    {

        /// <summary>
        /// Sets the Strand7 document's unit system according to the active Rhino document
        /// </summary>
        /// <param name="activeDoc"></param>
        /// <returns></returns>
        public bool SetSt7DocU(RhinoDoc activeDoc,
                               ForceUnitSystem forceU,
                               StressUnitSystem stressU,
                               MassUnitSystem massU,
                               TemperatureUnitSystem tempU,
                               EnergyUnitSystem energyU)
        {
            // Abort if input bogus data
            if (activeDoc.ModelUnitSystem == UnitSystem.None || activeDoc.ModelUnitSystem == UnitSystem.Unset) return false;

            int[] units = new int[St7.kLastUnit];
            if (!this.GetRhinoDocU(activeDoc, out LengthUnitSystem lengths)) return false;

            units[0] = (int)lengths;
            units[1] = (int)forceU;
            units[2] = (int)stressU;
            units[3] = (int)massU;
            units[4] = (int)tempU;
            units[5] = (int)energyU;

            // Set Strand7 document unit system
            int iErr = St7.St7SetUnits(this.UId, units);
            return (iErr == St7.ERR7_NoError);
        }

        /// <summary>
        /// Gets the Strand7 model's units system
        /// </summary>
        /// <returns></returns>
        public int GetSt7DocU(
            out LengthUnitSystem length,
            out ForceUnitSystem force,
            out StressUnitSystem stress,
            out MassUnitSystem mass,
            out TemperatureUnitSystem temp,
            out EnergyUnitSystem energy)
        {
            if (!this.IsValid)
            {
                length = LengthUnitSystem.None;
                force = ForceUnitSystem.None;
                stress = StressUnitSystem.None;
                mass = MassUnitSystem.None;
                temp = TemperatureUnitSystem.None;
                energy = EnergyUnitSystem.None;
                return -2;
            }

            int len = St7.kLastUnit;
            int[] modelUnits = new int[len];

            // Get Strand7 model working units
            int iErr = St7.St7GetUnits(this.UId, modelUnits);
            // Return None value if failed to acquire model units
            if (iErr != St7.ERR7_NoError)
            {
                length = LengthUnitSystem.None;
                force = ForceUnitSystem.None;
                stress = StressUnitSystem.None;
                mass = MassUnitSystem.None;
                temp = TemperatureUnitSystem.None;
                energy = EnergyUnitSystem.None;
                return -1;
            }

            int isLength = 1;
            int isForce = 1;
            int isStress = 1;
            int isMass = 1;
            int isTemp = 1;
            int isEnergy = 1;

            // Retrieve length units
            if (modelUnits[St7.ipLENGTHU] == St7.luMILLIMETRE)
            {
                length = LengthUnitSystem.Millimeters;      // Millimeters
            }
            else if (modelUnits[St7.ipLENGTHU] == St7.luCENTIMETRE)
            {
                length = LengthUnitSystem.Centimeters; // Centimeters
            }
            else if (modelUnits[St7.ipLENGTHU] == St7.luMETRE)
            {
                length = LengthUnitSystem.Meters;           // Meters
            }
            else if (modelUnits[St7.ipLENGTHU] == St7.luINCH)
            {
                length = LengthUnitSystem.Inches;            // Inches
            }
            else if (modelUnits[St7.ipLENGTHU] == St7.luFOOT)
            {
                length = LengthUnitSystem.Feet;              // Feet
            }
            else
            {
                length = LengthUnitSystem.Unset;                                                           // Unset the unit
                isLength = 0;
            }

            // Retrieve force units
            if (modelUnits[St7.ipFORCEU] == St7.fuNEWTON) force = ForceUnitSystem.N;                       // Newtons
            else if (modelUnits[St7.ipFORCEU] == St7.fuKILONEWTON) force = ForceUnitSystem.KN;             // Kilonewtons
            else if (modelUnits[St7.ipFORCEU] == St7.fuKILOFORCE) force = ForceUnitSystem.Kgf;             // Kilograms force
            else if (modelUnits[St7.ipFORCEU] == St7.fuPOUNDFORCE) force = ForceUnitSystem.Lbf;            // Pounds force
            else if (modelUnits[St7.ipFORCEU] == St7.fuTONNEFORCE) force = ForceUnitSystem.Tf;             // Tonnes force
            else if (modelUnits[St7.ipFORCEU] == St7.fuKIPFORCE) force = ForceUnitSystem.Kip;              // Kilopound force
            else
            {
                force = ForceUnitSystem.Unset;                                                             // Unset the unit
                isForce = 0;
            }

            // Retrieve stress units
            if (modelUnits[St7.ipSTRESSU] == St7.suPASCAL) stress = StressUnitSystem.Pa;                   // Pascals
            else if (modelUnits[St7.ipSTRESSU] == St7.suKILOPASCAL) stress = StressUnitSystem.KPa;         // Kilopascals
            else if (modelUnits[St7.ipSTRESSU] == St7.suMEGAPASCAL) stress = StressUnitSystem.MPa;         // Megapascals
            else if (modelUnits[St7.ipSTRESSU] == St7.suKSCm) stress = StressUnitSystem.Kgf_Sqcm;          // Kilogram-force/cm2
            else if (modelUnits[St7.ipSTRESSU] == St7.suPSI) stress = StressUnitSystem.Psi;                // Pound-force/inch2
            else if (modelUnits[St7.ipSTRESSU] == St7.suKSI) stress = StressUnitSystem.Ksi;                // Kilopound-force/inch2
            else if (modelUnits[St7.ipSTRESSU] == St7.suPSF) stress = StressUnitSystem.Lb_Sqft;            // Pound-force/ft2
            else
            {
                stress = StressUnitSystem.Unset;                                                           // Unset the unit
                isStress = 0;
            }

            // Retrieve mass units
            if (modelUnits[St7.ipMASSU] == St7.muGRAM) mass = MassUnitSystem.G;                            // Grams
            else if (modelUnits[St7.ipMASSU] == St7.muKILOGRAM) mass = MassUnitSystem.Kg;                  // Kilograms
            else if (modelUnits[St7.ipMASSU] == St7.muTONNE) mass = MassUnitSystem.T;                      // Tonnes
            else if (modelUnits[St7.ipMASSU] == St7.muPOUND) mass = MassUnitSystem.Lb;                     // Pounds
            else if (modelUnits[St7.ipMASSU] == St7.muSLUG) mass = MassUnitSystem.Slug;                    // Slugs
            else
            {
                mass = MassUnitSystem.Unset;                                                               // Unset the unit
                isMass = 0;
            }

            // Retrieve temperature units
            if (modelUnits[St7.ipTEMPERU] == St7.tuCELSIUS) temp = TemperatureUnitSystem.C;                // Celsius
            else if (modelUnits[St7.ipTEMPERU] == St7.tuFAHRENHEIT) temp = TemperatureUnitSystem.F;        // Fahrenheit
            else if (modelUnits[St7.ipTEMPERU] == St7.tuKELVIN) temp = TemperatureUnitSystem.K;            // Kelvin
            else if (modelUnits[St7.ipTEMPERU] == St7.tuRANKINE) temp = TemperatureUnitSystem.R;           // Rankine
            else
            {
                temp = TemperatureUnitSystem.Unset;                                                        // Unset the unit
                isTemp = 0;
            }

            // Retrieve energy units
            if (modelUnits[St7.ipENERGYU] == St7.euJOULE) energy = EnergyUnitSystem.J;                     // Joules
            else if (modelUnits[St7.ipENERGYU] == St7.euKILOJOULE) energy = EnergyUnitSystem.Kj;           // Kilojoules
            else if (modelUnits[St7.ipENERGYU] == St7.euBTU) energy = EnergyUnitSystem.Btu;                // British thermal units
            else if (modelUnits[St7.ipENERGYU] == St7.euFTLBF) energy = EnergyUnitSystem.FtLbF;            // Foot pounds-force
            else if (modelUnits[St7.ipENERGYU] == St7.euCALORIE) energy = EnergyUnitSystem.Cal;            // Calories
            else
            {
                energy = EnergyUnitSystem.Unset;                                                           // Unset the unit
                isEnergy = 0;
            }

            return (isLength + isForce + isStress + isMass + isTemp + isEnergy);
        }

    }
}
