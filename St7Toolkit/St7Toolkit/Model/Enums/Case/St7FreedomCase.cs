using St7API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace St7Toolkit
{
    public partial class St7FreedomCase
    {
        private string _name = string.Empty;

        private int _id = -1;

        private St7FreedomCaseType _type = St7FreedomCaseType.Normal;

        private St7FreedomConditionPreset _conditionPreset = St7FreedomConditionPreset.Default2DPlane;

        public static St7FreedomCase Unset => new St7FreedomCase();

        /// <summary>
        /// Get or set the name of this freedom case.<br/>
        /// </summary>
        public string Name
        {
            get => this._name;
            set => this._name = value;
        }

        /// <summary>
        /// Get or set the entity number (>1) of this freedom case.<br/>
        /// Warning: Id should be greater than 1. 
        /// </summary>
        public int EntityId
        {
            get => this._id;
            set => this._id = value;
        }

        /// <summary>
        /// Get or set the case type of this freedom case.<br/>
        /// NOTE: Only for LSA solver mode!!<para/>
        /// Freedom Case Types:<br/>
        /// 1. Normal: Standard boundary conditions with fixed/ free Dofs<br/>
        /// 2. Inertia Relief: FreeXYZ<br/>
        /// 3. Inertia Relief: Sym1XY<br/>
        /// 4. Inertia Relief: Sym1YZ<br/>
        /// 5. Inertia Relief: Sym1XZ<br/>
        /// 6. Inertia Relief: Sym2X<br/>
        /// 7. Inertia Relief: Sym2Y<br/>
        /// 8. Inertia Relief: Sym2Z<br/>
        /// See Strand7 freedom case type for reference.
        /// </summary>
        public St7FreedomCaseType CaseType
        {
            get => this._type;
            set => this._type = value;
        }

        /// <summary>
        /// Get or set the freedom contidion preset of this freedom case.
        /// </summary>
        public St7FreedomConditionPreset ConditionPreset
        {
            get => this._conditionPreset;
            set => this._conditionPreset = value;
        }

        /// <summary>
        /// Freedom case is valid if the case number is greater than 0 and has a name.
        /// </summary>
        public bool IsValid => (this.EntityId > 0 && this.Name != string.Empty);

        /// <summary>
        /// 
        /// </summary>
        internal St7FreedomCase() { }

        /// <summary>
        /// 
        /// </summary>
        public St7FreedomCase(string name, int id)
        {
            this._name = name;
            this._id = id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dofPreset"></param>
        /// <returns></returns>
        internal static int[] GetPresetDOFArray(St7FreedomConditionPreset dofPreset)
        {
            int[] presetArray;

            switch ((int)dofPreset)
            {
                case 0:
                    presetArray = new int[6] { 0, 0, 1, 1, 1, 1 };
                    break;
                case 1:
                    presetArray = new int[6] { 0, 0, 1, 1, 1, 0 };
                    break;
                case 2:
                    presetArray = new int[6] { 1, 0, 0, 0, 1, 1 };
                    break;
                case 3:
                    presetArray = new int[6] { 0, 1, 0, 1, 0, 1 };
                    break;
                case 4:
                    presetArray = new int[6] { 0, 0, 0, 1, 1, 1 };
                    break;
                case 5:
                    presetArray = new int[6] { 0, 0, 0, 1, 1, 1 };
                    break;
                default:
                    presetArray = new int[1] { -1 };
                    break;
            }
            return presetArray;
        }

        internal static St7FreedomConditionPreset FromSt7DocDefaults(St7Model model, int fcId)
        {
            int[] defaults = new int[6];
            int iErr = St7.St7GetFreedomCaseDefaults(model.UId, fcId, defaults);
            if (iErr != St7.ERR7_NoError)
            {
                return St7FreedomConditionPreset.Unset;
            }

            // Default2DPlane {0, 0, 1, 1, 1, 1}
            if (defaults[0] == 0 &&
                defaults[1] == 0 &&
                defaults[2] == 1 &&
                defaults[3] == 1 &&
                defaults[4] == 1 &&
                defaults[5] == 1)
            { return St7FreedomConditionPreset.Default2DPlane; }

            // BeamXYPlane {0, 0, 1, 1, 1, 0}
            if (defaults[0] == 0 &&
                defaults[1] == 0 &&
                defaults[2] == 1 &&
                defaults[3] == 1 &&
                defaults[4] == 1 &&
                defaults[5] == 0)
            { return St7FreedomConditionPreset.BeamXYPlane; }

            // BeamYZPlane {1, 0, 0, 0, 1, 1}
            if (defaults[0] == 1 &&
                defaults[1] == 0 &&
                defaults[2] == 0 &&
                defaults[3] == 0 &&
                defaults[4] == 1 &&
                defaults[5] == 1)
            { return St7FreedomConditionPreset.BeamYZPlane; }

            // BeamZXPlane {0, 1, 0, 1, 0, 1}
            if (defaults[0] == 0 &&
                defaults[1] == 1 &&
                defaults[2] == 0 &&
                defaults[3] == 1 &&
                defaults[4] == 0 &&
                defaults[5] == 1)
            { return St7FreedomConditionPreset.BeamZXPlane; }

            // Brick3D (Fixed All Rotations) {0, 0, 0, 1, 1, 1}
            if (defaults[0] == 0 &&
                defaults[1] == 0 &&
                defaults[2] == 0 &&
                defaults[3] == 1 &&
                defaults[4] == 1 &&
                defaults[5] == 1)
            { return St7FreedomConditionPreset.Brick3D; }

            // AllFree {0, 0, 0, 0, 0, 0}
            if (defaults[0] == 0 &&
                defaults[1] == 0 &&
                defaults[2] == 0 &&
                defaults[3] == 0 &&
                defaults[4] == 0 &&
                defaults[5] == 0)
            { return St7FreedomConditionPreset.AllFree; }

            return St7FreedomConditionPreset.Unset;
        }
    }
}
