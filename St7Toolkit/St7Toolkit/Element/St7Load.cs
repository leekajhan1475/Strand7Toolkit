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
    public class St7Load
    {
        private int _case = -1;

        private Vector3d _dir = new Vector3d();

        private double _mag = 0;

        /// <summary>
        /// Get or set the case of this load.
        /// </summary>
        public int CaseId 
        { 
            get => _case; 
            set => _case = value; 
        }

        /// <summary>
        /// Get or set the direction of this load.
        /// </summary
        public Vector3d Direction
        {
            get => _dir;
            set => _dir = value;
        }

        /// <summary>
        /// Get or set the magnitude of this load.
        /// </summary
        public double Magnitude
        {
            get => _mag;
            set => _mag = value;
        }

        /// <summary>
        /// For a Plate object to be valid: <para/>
        /// 1. The enity number must be greater than 0 <para/>
        /// 2. It must hold four nodes
        /// </summary>
        public bool IsValid
        {
            get => CaseId > 0 && _dir.IsUnitVector;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public St7Load()
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public St7Load(double[] direction, double magnitude)
        {
            if (direction.Length != 3) throw new ArgumentException("Direction array should only contain three items");
            Direction = new Vector3d(direction[0], direction[1], direction[2]);
            if(!Direction.IsUnitVector) Direction.Unitize();
            Magnitude = magnitude;
        }

        /// <summary>
        /// Load constructor given a Rhino.Geometry.Vector3d direction and magnitude.
        /// </summary>
        /// <param name="direction">
        /// The direction of this load.
        /// </param>
        /// <param name="magnitude">
        /// The magnitude of this load.
        /// </param>
        public St7Load(Vector3d direction, double magnitude) : this(new double[] {direction.X, direction.Y, direction.Z}, magnitude)
        {
            if (!direction.IsUnitVector) direction.Unitize();
        }

        
    }
}
