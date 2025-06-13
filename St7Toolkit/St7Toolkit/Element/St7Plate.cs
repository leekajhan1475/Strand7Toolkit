using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Rhino.Geometry;
using Rhino.Render.ChangeQueue;
using St7API;

namespace St7Toolkit.Element
{
    public class St7Plate
    {
        /// <summary>
        /// Represents the entity number of the Plate
        /// </summary>
        public int EntityId { get; set; } = -1;

        /// <summary>
        /// Represents the property number of the Plate
        /// </summary>
        public int PropertyId { get; set; } = -1;

        /// <summary>
        /// Represents the the first node of the Plate
        /// </summary>
        public St7Node A { get; set; }

        /// <summary>
        /// Represents the second node of the Plate
        /// </summary>
        public St7Node B { get; set; }

        /// <summary>
        /// Represents the third node of the Plate
        /// </summary>
        public St7Node C { get; set; }

        /// <summary>
        /// Represents the forth node of the Plate
        /// </summary>
        public St7Node D { get; set; }

        /// <summary>
        /// For a Plate object to be valid: <para/>
        /// 1. The enity number must be greater than 0 <para/>
        /// 2. It must hold four nodes
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (this.EntityId <= 0) return false;
                if(this.A == null || !this.A.IsValid ||
                    this.B == null || !this.B.IsValid ||
                    this.C == null || !this.C.IsValid ||
                    this.D == null || !this.D.IsValid) return false;
                return true;
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public St7Plate()
        {
            this.A = new St7Node();
            this.B = new St7Node();
            this.C = new St7Node();
            this.D = new St7Node();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityNumber">
        /// Enity number of the Plate that's greater than 0
        /// </param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        public St7Plate(int entityNumber, St7Node a, St7Node b, St7Node c, St7Node d)
        {
            this.EntityId = entityNumber;
            this.A = a;
            this.B = b;
            this.C = c;
            this.D = d;
        }


        /// <summary>
        /// Plate constructor
        /// </summary>
        /// <param name="entityNumber">
        /// Enity number of the Plate that's greater than 0
        /// </param>
        /// <param name="mesh"></param>
        public St7Plate(int entityNumber, Rhino.Geometry.Mesh mesh) :
            this (entityNumber,
                new St7Node(1, mesh.Vertices[0]),
                new St7Node(2, mesh.Vertices[1]),
                new St7Node(3, mesh.Vertices[2]),
                new St7Node(4, mesh.Vertices[3])
                )
        {
        }

    }
}
