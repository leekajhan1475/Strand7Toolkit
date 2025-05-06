using System.Collections.Generic;
using St7Toolkit.Element;

namespace St7Toolkit
{
    /// <summary>
    /// Implements all of the Strand7 API methods.
    /// </summary>
    public partial class St7Model
    {
        /// <summary>
        /// Adds a Node object to this Strand7 model file.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool AddNode(Node node)
        {
            // Check if the input node is a valid node object
            if(!node.IsValid || node == null) { return false; }
            // Append node to locally stored array
            this.NodeList.Add(node);
            // Get Rhino active document unit
            Rhino.UnitSystem unitSystem = Rhino.RhinoDoc.ActiveDoc.ModelUnitSystem;
            // 

            double[] coordinates = new double[] { node.Origin.X, node.Origin.Y, node.Origin.Z };
            return (this.PushNodeToStrand7(node.EntityId, coordinates));
        }

        /// <summary>
        /// Adds a collection of nodes to this Strand7 model
        /// </summary>
        /// <param name="nodes">
        /// Collection of St7Toolkit.Element.Node
        /// </param>
        /// <returns>
        /// -1 if all of the nodes were pushed to Strand7 model <para/>
        /// or index [0...N-1] of the node that cannot be pushed.
        /// </returns>
        public int AddNodes(IEnumerable<Node> nodes)
        {
            int id = 0;
            foreach (Node n in nodes)
            {
                if (!this.AddNode(n)) { return id; }
                id++;
            }
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="beam"></param>
        /// <returns></returns>
        public bool AddBeam(St7Toolkit.Element.Beam beam)
        {
            if (!beam.IsValid || beam == null) { return false; }
            int[] connections = new int[] { 2, beam.A.EntityId, beam.B.EntityId };
            return (this.PushBeamToStrand7(beam.EntityId, beam.PropertyId, connections));
        }

        /// <summary>
        /// Adds a collection of St7Toolkit.Element.Beam objects to the Strand7 model
        /// </summary>
        /// <param name="beams">
        /// A collection of St7Toolkit.Element.Beam objects to push to Strand7 model
        /// </param>
        /// <returns>
        /// -1 if all of the beams were pushed to Strand7 model <para/>
        /// or index [0...N-1] of the beam that cannot be pushed.
        /// </returns>
        public int AddBeams(IEnumerable<St7Toolkit.Element.Beam> beams)
        {
            int id = 0;
            foreach (Beam b in beams)
            {
                if (!this.AddBeam(b)) { return id; }
                id++;
            }
            return -1;
        }

        /// <summary>
        /// Add a quadrilateral plate to the Strand7 model.
        /// </summary>
        /// <param name="plate">
        /// St7Toolkit.Element.Plate object
        /// </param>
        /// <returns>
        /// Boolean flag indicating whether operation completed successfully or not.
        /// </returns>
        
        public bool AddPlate(St7Toolkit.Element.Plate plate)
        {
            // Set array item with number of Nodes and respective entity numbers
            int[] connections = new int[5]
            {
                4,          // Four Nodes
                plate.A.EntityId, // Entity number of first node
                plate.B.EntityId, // Entity number of second node
                plate.C.EntityId, // Entity number of third node
                plate.D.EntityId  // Entity number of fourth node
            };
            return (this.PushPlateToStrand7(plate.EntityId, plate.PropertyId, connections));
        }

        /// <summary>
        /// Add plates to this Strand7 model
        /// </summary>
        /// <param name="plates"></param>
        /// <returns>
        /// -1 if all of the plates were pushed to Strand7 model <para/>
        /// or index [0...N-1] of the plate that cannot be pushed.
        /// </returns>
        public int AddPlates(IEnumerable<St7Toolkit.Element.Plate> plates)
        {
            int id = 0;
            foreach (Plate plate in plates)
            {
                if (!this.AddPlate(plate)) { return id; }
                id++;
            }
            return -1;
        }
    }
}
