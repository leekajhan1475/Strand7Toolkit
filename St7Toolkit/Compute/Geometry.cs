using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using St7API;
using St7Toolkit.Element;

namespace St7Toolkit
{
    public static partial class Compute
    {
        public static bool SetBeamConnectivity(IEnumerable<Beam> beams, out List<Node> setNodes)
        {
            // 
            if (beams == null || beams.Count() <= 1) 
            {
                setNodes = new List<Node>();
                return false; 
            }

            // Create a list to store all of the Node positions of the input beams collection
            List<Node> allNodes = new List<Node>();

            for (int i = 0; i < beams.Count(); i++)
            {
                Beam b = beams.ElementAt(i);
                if (b == null || b.A == null || b.B == null) continue;
                allNodes.Add(b.A);
                allNodes.Add(b.B);
            }

            // Dictionary to store the original index mapped to the new index after culling duplicates
            Dictionary<int, int> originalToNewIndices = new Dictionary<int, int>();

            // Create a list to store unique Nodes' positions
            List<Node> uniqueNodes = new List<Node>();

            // HashSet to help identify unique points
            HashSet<Node> uniqueNodesSet = new HashSet<Node>();

            // Iterate through the list of points
            for (int i = 0; i < allNodes.Count; i++)
            {
                Node node = allNodes[i];
                // If point is unique, add to the unique points list
                if (uniqueNodesSet.Add(node)) { uniqueNodes.Add(node); }
                // Map the original index to the new index
                originalToNewIndices[i] = uniqueNodes.IndexOf(node);
            }

            // Output the map of original indices to new indices
            foreach (var kvp in originalToNewIndices)
            {
                int original = kvp.Key;
                int mapped = kvp.Value;
            }

            // Set up unique Node entity number
            for (int i = 0; i < uniqueNodesSet.Count; i++)
            {
                Node node = uniqueNodes[i];
                node.EntityId = i + 1;
            }

            // Set up Beam connectivity
            for (int i = 0; i < beams.Count(); i++)
            {
                Beam beam = beams.ElementAt(i);
                if (!beam.IsValid) continue;

                int indexAtStart = i * 2;
                int indexAtEnd = indexAtStart + 1;

                KeyValuePair<int,int> kvpStart = originalToNewIndices.ElementAt(indexAtStart);
                KeyValuePair<int, int> kvpEnd = originalToNewIndices.ElementAt(indexAtEnd);

                //int originalIndex_start = kvpStart.Key;
                int mappedIndex_Start = kvpStart.Value;

                //int originalIndex_End = kvpEnd.Key;
                int mappedIndex_End = kvpEnd.Value;

                Node nodeA = uniqueNodes[mappedIndex_Start];
                beam.A.EntityId = nodeA.EntityId;
                beam.A = nodeA;

                Node nodeB = uniqueNodes[mappedIndex_End];
                beam.B.EntityId = nodeB.EntityId;
                beam.B = uniqueNodes[mappedIndex_End];
            }
            setNodes = uniqueNodes;
            return true;
        }
        
    }
}
