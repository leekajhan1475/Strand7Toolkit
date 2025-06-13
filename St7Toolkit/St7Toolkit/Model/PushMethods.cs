using System;
using System.Collections.Generic;
using System.Text;
using St7Toolkit.Element;
using St7API;
using Rhino.Geometry;
using Rhino.Geometry.Collections;

namespace St7Toolkit
{
    public partial class St7Model
    {
        /// <summary>
        /// Creates a new freedom case in this Strand7 document. 
        /// </summary>
        /// <param name="fc">
        /// New freedom case.
        /// </param>
        /// <param name="overrideExisting">
        /// Freedom case type (for linear analysis only).
        /// </param>
        /// <returns>
        /// Freedom case number Id (> 0).
        /// </returns>
        public bool PushNewFreedomCase(St7FreedomCase fc, out string msg, bool overrideExisting = false)
        {
            int step = 1;

            // Abort if input freedom case is invalid
            if (!fc.IsValid) 
            { 
                msg = $"Step[{step}] - Illegal value entry\n" + 
                       "Input freedom case is not a valid freedom case object\n" + 
                       "For a freedom case object to be valid, it must contain the followings:\n" +
                       "1. Case number, must be greater than 0\n" +
                       "2. Case Name, default formatt as - Freedom Case + id[1...N]"; 
                return false; 
            } else { step++; }

            // Check if input freedom case coincide any of the existing freedom cases
            // Get freedom case number (Id)
            int caseNum = -1;
            int iErr = St7.St7GetNumFreedomCase(this.UId, ref caseNum);
            if (iErr != St7.ERR7_NoError)
            {
                msg = $"Step[{step}] - Check independant freedom case\n" +
                      "Cannot get the number of freedom cases from associated Strand7 document";
                return false;
            }
            else { step++; }


            if (overrideExisting)
            {
                for (int id = 1; id < caseNum + 1; id++)
                {
                    // Check if the freedom case id coincide with the existing freedom cases
                    StringBuilder sb = new StringBuilder(St7.kMaxStrLen);
                    iErr = St7.St7GetFreedomCaseName(this.UId, id, sb, St7.kMaxStrLen);
                    if (iErr != St7.ERR7_NoError)
                    {
                        msg = $"Step[{step}.1] - Override Existing:\n" +
                               "Cannot get the name of query freedom case in this Strand7 model";
                        return false;
                    }

                    // Skip to next if name is not the same
                    if (!sb.ToString().Equals(fc.Name)) continue;
                    // If same name found, delete the existing one
                    iErr = St7.St7DeleteFreedomCase(this.UId, id);
                    if (iErr != St7.ERR7_NoError)
                    {
                        msg = $"Step[{step}.2] - Override Existing:\n" +
                              $"Fail to delete freedom case of entity id:{id} in this Strand7 model";
                        return false;
                    }
                }
                step++;
            }

            // Create a new freedom case in Strand7
            iErr = St7.St7NewFreedomCase(this.UId, fc.Name);
            if (iErr != St7.ERR7_NoError)
            {
                msg = "Step - Create Freedom Case:\n" + 
                      "Fail to create input freedom case";
                return false;
            }

            // Set freedom case degree of freedom
            iErr = St7.St7SetFreedomCaseDefaults(
                this.UId,
                fc.EntityId,
                St7FreedomCase.GetPresetDOFArray(fc.ConditionPreset)
                );

            if (iErr != St7.ERR7_NoError)
            {
                msg = "Step - Override Existing:\n" + 
                     $"Fail to set freedom case:{fc.Name} degree of freedom properties";
                return false;
            }

            // Set freedom case type
            iErr = St7.St7SetFreedomCaseType(this.UId, fc.EntityId, ((int)fc.CaseType));
            if (iErr != St7.ERR7_NoError)
            {
                msg = "Step - Create Freedom Case:\n" + 
                      "Fail to set freedom case type";
                return false;
            }

            msg = "Freedom case created and pushed to Strand7";
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityNumber">
        /// Identification number of the Node object
        /// </param>
        /// <param name="coordinates">
        /// Node's X, Y, and Z coordinates in double-precision floating numbers
        /// </param>
        /// <returns></returns>
        private bool PushNodeToStrand7(int entityNumber, double[] coordinates)
        {
            // Push node properties to Strand7 Model
            int iErr = St7.St7SetNodeXYZ(
                this.UId,
                entityNumber,
                coordinates);
            return (iErr == St7.ERR7_NoError);
        }

        /// <summary>
        /// Push Node's constraints data to Strand7.
        /// </summary>
        /// <param name="nodeNum">
        /// Node's entity number [1..N]
        /// </param>
        /// <param name="freedomCaseNum">
        /// Freedom case number.
        /// </param>
        /// <param name="ucsId">
        /// ID number of the specified User Coordinate System(UCS).<br/>
        /// Use St7Toolkit.St7Model.GlobalXYZSystem as input.
        /// <param name="Dofs">
        /// An array containing 6 [0-1] values describing the restraint conditions for the six DoF at the specified node.<br />
        /// Constraint status: Off = 0, On = 1
        /// </param>
        /// <param name="ct">
        /// A 6-element array describing the enforced displacement or rotation conditions for the six DoF at the specified node.<br />
        /// Doubles[i-1] describes the displacement of the ith DoF according to the 123456 axis
        /// convention in the specified UCS.
        /// </param>
        /// <returns></returns>
        public bool PushNodeRestraint6(int nodeNum,
                                       int freedomCaseNum,
                                       int ucsId,          // Bind to Node
                                       int[] Dofs,
                                       double[] ct,
                                       double[] cr,
                                       out string msg)
        {
            int step = 1;

            // If input Node entity number is invalid (less than 1)
            if (nodeNum < 1)
            {
                msg = $"Step[{step}] - Illegal value entry\n" + 
                      $"Input invalid Node entity number:{nodeNum}";
                return false;
            }
            else { step++; }

            // If input Freedom case number is invalid (less than 1)
            if (freedomCaseNum < 1)
            {
                msg = $"Step[{step}] - Illegal value entry\n" + 
                      $"Input invalid FreedomCase entity number:{freedomCaseNum}";
                return false;
            }
            else { step++; }

            // Gets respective node from this model
            St7Node node = this.GetNode(nodeNum);
            if (node == St7Node.Unset)
            {
                msg = $"Step[{step}] - Get Node\n" +
                      $"Fail to find Node of number:{nodeNum}";
                return false;
            }
            else if (!node.IsValid)
            {
                msg = $"Step[{step}] - Get Node\n" +
                      $"Node of number:{nodeNum} is invalid";
                return false;
            }
            else { step++; }

            // Gets freedom case from this model
            St7FreedomCase fc = this.GetFreedomCase(freedomCaseNum);
            if (fc == St7FreedomCase.Unset)
            {
                msg = $"Step[{step}] - Get Freedom Case\n" +
                      $"Fail to find Freedom Case of number:{freedomCaseNum} in St7Model:Id{this.UId}";
                return false;
            }
            else if (!fc.IsValid)
            {
                msg = $"Step[{step}] - Get Freedom Case\n" +
                      $"Freedom Case of number:{freedomCaseNum} is invalid";
                return false;
            } else { step++; }

            // Check if input freedom case's dofs preset is unset when the input
            // Degree of freedom array is null or has no value
            // if the preset is set, use it instead of the input Dofs
            if (Dofs.Length != 6 || Dofs == null)
            {
                if (fc.ConditionPreset == St7FreedomConditionPreset.Unset)
                {
                    msg = $"Step[{step}] - Acquire Freedom case Dofs preset values\n" +
                          $"Freedom Case of number:{freedomCaseNum} does not contain a degree of freedom preset";
                    return false;
                }
                // Get condition array
                Dofs = St7FreedomCase.GetPresetDOFArray(fc.ConditionPreset);
                step++;
            } else { step++; }

            // Push Node's coordinate system setting to Strand7 document
            if (!this.PushUCSSettings(node.UCSSetting, out msg))
            {
                msg = $"Step[{step}] - Push UCS settings\n" +
                      $"Fail to push coordinate system:{node.UCSSetting.Name} to Strand7";
                return false;
            } else { step++; }

            // Constrain or free the Node
            // Check how many freedoms (tX, tY...rY, rZ) are locked
            int count = 0;
            for (int i = 0; i < Dofs.Length; i++) 
            { 
                // If freedom is unlocked
                if (Dofs[i] == 0) { continue; }
                count++;
            }

            // If number of freedom of the Node is less than 1, set the constraint value of this Node to false.
            // or True otherwise
            if (count < 1) { node.Constrained = false; }
            else { node.Constrained = true; }

            // Construct enforced translation and rotation value array from
            // seperate translation stiffness array and rotation stiffness array
            double[] enforced = new double[6];
            for (int i = 0; i < 6; i++)
            {
                if (i < 3) { enforced[i] = ct[i]; continue; }
                enforced[i] = cr[i - 3];
            }

            // Push Node properties to Strand7 Model
            int iErr = St7.St7SetNodeRestraint6(
                this.UId,
                node.EntityId,
                fc.EntityId,
                ucsId,
                Dofs,
                enforced
                );

            return (iErr == St7.ERR7_NoError) ? true : false;
        }

        /// <summary>
        /// Push the Beam object to Strand7 model
        /// </summary>
        /// <param name="entityNumber"></param>
        /// <param name="propertyNumber"></param>
        /// <param name="connections"></param>
        /// <returns></returns>
        private bool PushBeamToStrand7(int entityNumber, int propertyNumber, int[] connections)
        {
            int iErr = St7.St7SetElementConnection(
                this.UId,
                St7.tyBEAM,
                entityNumber,
                propertyNumber,
                connections
                );
            return (iErr == St7.ERR7_NoError);
        }

        /// <summary>
        /// Push the Beam object's translational end release conditions to the Strand7 model.
        /// </summary>
        /// <param name="entityNumber">
        /// The beam's Id number.
        /// </param>
        /// <param name="beamEnd">
        /// The beam's end position: 1 or 2
        /// </param>
        /// <param name="status">
        /// Defines the release conditions of the beam's end Node in one of the following:<br />
        /// (the principal 1-3 axis directions of the beam).<br />
        /// 1. Released<br />
        /// 2. Fixed<br />
        /// 3. Partial<br /> 
        /// </param>
        /// <param name="stiffness">
        /// A 3-element array containing the partial stiffnesses to be<br /> 
        /// used in the case of partial end release conditions.
        /// </param>
        /// <returns></returns>
        private bool PushBeamTRelease3(int entityNumber, int beamEnd, int[] status, double[] stiffness)
        {
            if (entityNumber < 1 ||
                beamEnd < 1 || beamEnd > 2 ||
                status.Length < 3 || status.Length > 3 ||
                stiffness.Length < 3 || stiffness.Length > 3) { return false; }


            // Get the beam element
            St7Beam beam = this.Beams[entityNumber - 1];


            // Push settings to Strand7
            int iErr = St7.St7SetBeamTRelease3(
                this.UId,
                entityNumber,
                beamEnd,
                status,
                stiffness
                );
            return (iErr == St7.ERR7_NoError);
        }

        /// <summary>
        /// Add a quadrilateral plate to the Strand7 model.
        /// </summary>
        /// <param name="entityNumber">
        /// Identification number of the Plate
        /// </param>
        /// <param name="propertyNumber">
        /// Property number assigned to the Plate
        /// </param>
        /// <param name="connections">
        /// Integer array of 5 items:
        /// [0] Total number of Nodes.
        /// [1-4] Node entity number
        /// </param>
        /// <returns>
        /// Boolean flag indicating whether operation completed successfully or not.
        /// </returns>
        private bool PushPlateToStrand7(int entityNumber, int propertyNumber, int[] connections)
        {
            int iErr = St7.St7SetElementConnection(
                this.UId,
                St7.tyPLATE,
                entityNumber,
                propertyNumber,
                connections
                );
            return (iErr == St7.ERR7_NoError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyNum"></param>
        /// <param name="mesh"></param>
        /// <returns></returns>
        public bool PushRhinoMeshToStrand7(int propertyNum, Mesh mesh, out string msg)
        {
            MeshVertexList vertices_ = mesh.Vertices;
            MeshFaceList faces_ = mesh.Faces;

            // Set up Strand7 model's Node data with input Rhino mesh's vertices
            List<St7Node> nodes = new List<St7Node>();
            // Initiate node Id number
            int nodeId = 1;
            foreach (Point3d p in vertices_)
            {
                nodes.Add(new St7Node(nodeId, p));
                nodeId++;
            }

            /**** Push data to Strand7 Model ****/
            // Push Nodes to Strand7 model
            if (this.AddNodes(nodes) < 0)
            {
                msg = "Failed to push Nodes to Strand7 Model";
                return false;
            }

            // Push Plates to Strand7 model with converting 
            // Input mesh's individual face as Plate (Node connection) to the Strand7 model
            int plateId = 1;
            foreach (MeshFace face in faces_)
            {
                int a = face.A;
                int b = face.B;
                int c = face.C;
                int d = face.D;

                St7Plate plate = new St7Plate(
                    plateId,
                    this.Nodes[a],
                    this.Nodes[b],
                    this.Nodes[c],
                    this.Nodes[d]
                    );

                if (!this.AddPlate(plate))
                {
                    msg = "Failed to push Plate (Node connectivity setting) to Strand7 Model";
                    return false;
                }
                plateId++;
            }
            msg = "No errors";
            return true;
        }

        /// <summary>
        /// Sets the data for the User Coordinate System (UCS) in a Strand7 model<br />
        /// or creates a UCS if the specified UCSId does not exist.
        /// </summary>
        /// <param name="ucs">
        /// Coordinate System to write to Strand7 document.
        /// </param>
        /// <param name="msg">
        /// Report message from the method during runtime.
        /// </param>
        /// <returns>
        /// True: UCS setting were written to Strand7 document.<br />
        /// False: Otherwise.
        /// </returns>
        public bool PushUCSSettings(UCSSetting ucs, out string msg)
        {
            int step = 1;
            // Abort if:
            // 1. User coordinate system(UCS) ID is less than 1.
            // 2. UCS type is invalid.
            // 3. UCSDoublesArray is invalid, number of items is less than 10
            if (ucs.EntityId < 1 || (int)ucs.SystemType == (int)St7.ERR7_InvalidUCSType)
            {
                msg = $"Step[{step}] - Illegal parameter entries" +
                      "Invalid USCSetting input";
                return false;
            }
            else { step++; }

                // Convert setting to array representation
                ucs.ToArray(out double[] ucsArr);
            if (ucsArr[0] < 0)
            {
                msg = $"Step[{step}] - Convert to Double Array" +
                      "Fail to convert User Coordinate System to double array";
                return false;
            }
            else { step++; }

            // Push setting to Strand7
            int iErr = St7.St7SetUCS(this.UId, ucs.EntityId, (int)ucs.SystemType, ucsArr);
            if (iErr != St7.ERR7_NoError)
            {
                msg = $"Step[{step}] - Call method St7SetUCS()" +
                      "Fail to push UCS settings to Strand7";
                return false;
            }

            msg = $"Step[Final] - Method body execution completes without errors";
            return true;
        }
    }
}
