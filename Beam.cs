using System;
using System.Collections.Generic;
using System.Text;
using St7API;
using Rhino.Geometry;

namespace St7Toolkit.Element
{
    public class Beam
    {
        /// <summary>
        /// Enity number
        /// </summary>
        public int EntityId { get; set; } = -1;

        /// <summary>
        /// Property number
        /// </summary>
        public int PropertyId { get; set; } = -1;

        /// <summary>
        /// Start Node
        /// </summary>
        public Node A { get; set; }

        /// <summary>
        /// End Node
        /// </summary>
        public Node B { get; set; }

        /// <summary>
        /// Color property
        /// </summary>
        public System.Drawing.Color Colour { get; set; }

        /// <summary>
        /// For a Beam object to be valid: <para/>
        /// 1. The enity number must be greater than 0 <para/>
        /// 2. It must have a starting node and end node
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (this.EntityId <= 0) return false;
                if (this.A == null || !this.A.IsValid ||
                    this.B == null || !this.B.IsValid) return false;
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Beam()
        {
            this.A = new Node();
            this.B = new Node();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">
        /// Entity number for the beam
        /// </param>
        public Beam(int entity)
        {
            this.EntityId = entity;
            this.A = new Node();
            this.B = new Node();
        }

        /// <summary>
        /// Default Beam constructor from Rhino.Geometry.Line
        /// </summary>
        /// <param name="id">
        /// Enity number of the beam that's greater than 0
        /// </param>
        /// <param name="line">
        /// Rhino line geometry
        /// </param>
        public Beam(int entity, Rhino.Geometry.Line line) :
            this(entity) 
        {
            this.A = new Node(-1, line.From);
            this.B = new Node(-1, line.To);
        }

        public override string ToString()
        {
            string str = $"Beam {this.EntityId.ToString()}: " +
                $"A:{this.A} | " +
                $"B:{this.B}";
            return str;
        }
    }
}
