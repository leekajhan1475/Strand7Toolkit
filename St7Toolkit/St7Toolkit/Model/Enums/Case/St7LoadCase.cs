using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace St7Toolkit
{
    public class St7LoadCase
    {
        private string _name = string.Empty;

        private int _id = -1;

        private St7FreedomCaseType _type = St7FreedomCaseType.Normal;

        private St7FreedomConditionPreset _conditionPreset = St7FreedomConditionPreset.Default2DPlane;

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
        /// Freedom case is valid if the case number is greater than 0 and has a name.
        /// </summary>
        public bool IsValid => (this.EntityId > 0 && this.Name != string.Empty);

        /// <summary>
        /// 
        /// </summary>
        internal St7LoadCase() { }

        /// <summary>
        /// 
        /// </summary>
        public St7LoadCase(string name, int id)
        {
            this._name = name;
            this._id = id;
        }
    }
}
