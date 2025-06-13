using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Runtime;
using Rhino.Geometry;
using St7API;

namespace St7Toolkit.Element
{
    /// <summary>
    /// Class for managing materials.
    /// </summary>
    public class St7Material
    {
        private int _id = -1;

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            get => this._id;
            set => this._id = value;
        }

        /// <summary>
        /// For a Plate object to be valid: <para/>
        /// 1. The enity number must be greater than 0 <para/>
        /// 2. It must hold four nodes
        /// </summary>
        public bool IsValid
        {
            get => (this.ID > 0);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public St7Material()
        {
        }
    }
}
