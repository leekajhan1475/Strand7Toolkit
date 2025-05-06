using System;
using System.Collections.Generic;
using System.Text;
using Rhino;
using Rhino.Geometry;
using St7API;

namespace St7Toolkit.Element
{
    public class Node
    {
        
        /// <summary>
        /// Represents the entity number of the Node
        /// </summary>
        public int EntityId { get; set; }  = -1;

        /// <summary>
        /// 
        /// </summary>
        public Point3d Origin { get; set; }

        /// <summary>
        /// For a Node object to be valid: <para/>
        /// 1. The enity number must be greater than 0 <para/>
        /// 2. It must have X, Y, and Z coordinates
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (this.EntityId <= 0) return false;
                if (!this.Origin.IsValid) return false;
                return true;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Node()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private Node(double x, double y, double z)
        {
            this.Origin = new Point3d(x,y,z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityNumber"></param>
        /// <param name="origin"></param>
        public Node(int entityNumber, Point3d origin) : 
            this(origin.X, origin.Y, origin.Z)
        {
            this.EntityId = entityNumber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal double[] GetOriginAsArray()
        {
            return new double[] { this.Origin.X, this.Origin.Y, this.Origin.Z };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = $"Node {this.EntityId.ToString()}: {{" + 
                $"{this.Origin.X.ToString()}," +
                $"{this.Origin.Y.ToString()}," +
                $"{this.Origin.Z.ToString()}}}";
            return str;
        }

    }
}
