using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Runtime;
using Rhino.Geometry;
using St7API;

namespace St7Toolkit
{
    /// <summary>
    /// Class for managing element cross section.
    /// </summary>
    public class St7CrossSection
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
        /// For a cross section setting to be valid:<br />
        /// 1. The enity number must be greater than 0. <br />
        /// 2. It must hold four nodes.
        /// </summary>
        public bool IsValid
        {
            get => (this.ID > 0);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public St7CrossSection()
        {
        }
    }
}
