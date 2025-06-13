using Rhino;
using System.Windows.Forms;

namespace St7Toolkit
{
    /// <summary>
    /// Implements all of the Strand7 API methods.
    /// </summary>
    public partial class St7Model
    {

        /// <summary>
        /// Sets the active Rhino document's unit system.
        /// </summary>
        /// <param name="activeDoc">
        /// </param>
        /// <returns>
        /// Integer indicating the unit system setting state:<br />
        /// -1 = Failed to change Rhino document's unit system.<br />
        /// 0 = Rhino document's unit system remains unchanged.<br />
        /// 1 = Rhino document's unit system changed according to the imported Strand7 file.<br />
        /// 2 = Rhino document and import Strand7 file has different unit system but user input "Not to cahnge" the RhinoDoc's unit.
        /// </returns>
        public int SetRhinoDocU(Rhino.RhinoDoc activeDoc)
        {
            if (this.LengthUnit == LengthUnitSystem.Unset || this.LengthUnit < 0) return -1;

            int unit = (int)activeDoc.ModelUnitSystem;
            int flag = -1;

            if (this.LengthUnit == LengthUnitSystem.Millimeters) flag = 0;
            if (this.LengthUnit == LengthUnitSystem.Centimeters) flag = 1;
            if (this.LengthUnit == LengthUnitSystem.Meters) flag = 2;
            if (this.LengthUnit == LengthUnitSystem.Inches) flag = 3;
            if (this.LengthUnit == LengthUnitSystem.Feet) flag = 4;

            // Add window to comfirm change to Rhino unit system
            if (flag > -1) 
            {
                // Show a message box with Yes and No buttons
                DialogResult result = MessageBox.Show($"The Strand7 file you're opening has a different unit system of {this.LengthUnit.ToString()} \n" +
                                                      $"to the current Rhino document's unit system of {activeDoc.ModelUnitSystem.ToString()}.\n" +
                                                      "Do you wish to continue to open the Strand7 file and modify current Rhino document's unit system?", 
                                                      "Change of Rhino Model Unit System", 
                                                      MessageBoxButtons.YesNo, 
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.No) { return 2; }
            }

            switch (flag)
            {
                case 0:
                    activeDoc.AdjustModelUnitSystem(UnitSystem.Millimeters, true);
                    break;
                case 1:
                    activeDoc.AdjustModelUnitSystem(UnitSystem.Centimeters, true);
                    break;
                case 2:
                    activeDoc.AdjustModelUnitSystem(UnitSystem.Meters, true);
                    break;
                case 3:
                    activeDoc.AdjustModelUnitSystem(UnitSystem.Inches, true);
                    break;
                case 4:
                    activeDoc.AdjustModelUnitSystem(UnitSystem.Feet, true);
                    break;
                    
            }

            if (unit == (int)activeDoc.ModelUnitSystem) { return 0; }

            return 1;
        }

        /// <summary>
        /// Sets the active Rhino document's unit system.
        /// </summary>
        /// <param name="activeDoc"></param>
        /// <returns></returns>
        public bool GetRhinoDocU(Rhino.RhinoDoc activeDoc, out LengthUnitSystem lengthU)
        {
            // Import active rhino document unit length units only
            if (activeDoc.ModelUnitSystem == UnitSystem.None || activeDoc.ModelUnitSystem == UnitSystem.Unset)
            {
                lengthU = LengthUnitSystem.Unset;
                return false;
            }
            if (activeDoc.ModelUnitSystem == UnitSystem.Millimeters) lengthU = LengthUnitSystem.Millimeters;
            else if (activeDoc.ModelUnitSystem == UnitSystem.Centimeters) lengthU = LengthUnitSystem.Centimeters;
            else if (activeDoc.ModelUnitSystem == UnitSystem.Meters) lengthU = LengthUnitSystem.Meters;
            else if (activeDoc.ModelUnitSystem == UnitSystem.Inches) lengthU = LengthUnitSystem.Inches;
            else if (activeDoc.ModelUnitSystem == UnitSystem.Feet) lengthU = LengthUnitSystem.Feet;
            else
            {
                lengthU = LengthUnitSystem.Unset;
                return false;
            }
            return true;
        }
        
    }
}
