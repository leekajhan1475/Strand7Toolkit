using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using St7Toolkit.Element;
using St7API;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;
using static Rhino.Render.TextureGraphInfo;
using System.Runtime.InteropServices;

namespace St7Toolkit
{
    /// <summary>
    /// Implements all of the Strand7 API methods.
    /// </summary>
    public partial class St7Model
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityNumber">
        /// Node's entity number [1..N]
        /// </param>
        /// <param name="caseNumber">
        /// Freedom case number.
        /// </param>
        /// <param name="UCSId">
        /// ID number of the specified UCS. Use St7Toolkit.St7Model.GlobalXYZSystem as input.
        /// </param>
        /// <param name="DoF">
        /// An array containing 6 [0-1] values describing the restraint conditions for the six DoF at the specified node.<para/>
        /// Constraint status: Off = 0, On = 1
        /// </param>
        /// <param name="enforced">
        /// A 6-element array describing the enforced displacement or rotation conditions for the six DoF at the specified node. <para/>
        /// Doubles[i-1] describes the displacement of the ith DoF according to the 123456 axis
        /// convention in the specified UCS.
        /// </param>
        /// <returns></returns>
        public bool SetNodeRestraint6(int entityNumber, int caseNumber, int UCSId, int[] DoF, double[] enforced)
        {
            if (entityNumber < 1) return false;
            // Get the node to set constraint
            Node node = this.NodeList[entityNumber - 1];
            if (!node.IsValid) return false;
            // Push Node properties to Strand7 Model
            int iErr = St7.St7SetNodeRestraint6(
                this.UId,
                entityNumber,
                caseNumber,
                UCSId,
                DoF,
                enforced
                );

            return (iErr == St7.ERR7_NoError) ? true : false;
        }
    }
}
