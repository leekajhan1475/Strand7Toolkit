using System;
using System.Collections.Generic;
using System.Text;
using St7Toolkit.Element;
using St7API;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using System.IO;
using System.Runtime.InteropServices;

namespace St7Toolkit
{
    public partial class St7Model
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Mesh PullMeshFromSt7()
        {
            Mesh mesh = new Mesh();

            // add current model's nodes as output Rhino mesh's vertices
            // select the nodes in current model
            int num_NODE = -1;
            int iErr = St7.St7GetEntitySelectCount(this.UId, St7.tyNODE, ref num_NODE);

            for (int i = 1; i < num_NODE; i++)
            {
                double[] XYZ = new double[3];
                iErr = St7.St7GetNodeXYZ(this.UId, i, XYZ);
                mesh.Vertices.Add(
                    new Point3d(
                        XYZ[0],
                        XYZ[1],
                        XYZ[2]
                        )
                    );
            }

            // add current model's plates as output Rhino mesh's faces
            // select the plates in the current model
            int num_PLATE = -1;
            iErr = St7.St7GetEntitySelectCount(this.UId, St7.tyPLATE, ref num_PLATE);
            for (int j = 1; j < num_PLATE; j++)
            {
                int[] nCONNECT = new int[5];
                iErr = St7.St7GetElementConnection(this.UId, St7.tyPLATE, j, nCONNECT);
                // Create mesh face valence!!
                mesh.Faces.AddFace(
                    new MeshFace(
                        nCONNECT[1] - 1,
                        nCONNECT[2] - 1,
                        nCONNECT[3] - 1,
                        nCONNECT[4] - 1
                        )
                    );
            }
            return mesh;
        }

        /// <summary>
        /// Pull all the nodes from Strand7 into this St7Model
        /// </summary>
        /// <returns></returns>
        private bool PullNodeFromStrand7()
        {
            // Clean data before import 
            if (this.Nodes.Count > 0) this.Nodes.Clear();
            if (this.Beams.Count > 0) this.Beams.Clear();
            if (this.Plates.Count > 0) this.Plates.Clear();

            // Add model's nodes to St7Model
            int num_NODE = -1;
            if (St7.St7GetEntitySelectCount(this.UId, St7.tyNODE, ref num_NODE) != St7.ERR7_NoError) { return false; }
            for (int i = 1; i <= num_NODE; i++)
            {
                double[] XYZ = new double[3];
                if (St7.St7GetNodeXYZ(this.UId, i, XYZ) != St7.ERR7_NoError) { return false; }
                this.Nodes.Add(new St7Node(i, new Point3d(XYZ[0], XYZ[1], XYZ[2])));
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fcName"></param>
        /// <returns></returns>
        public bool PullFreedomCaseName(int id, out string fcName)
        {
            StringBuilder sb = new StringBuilder(St7.kMaxStrLen);
            int iErr = St7.St7GetFreedomCaseName(this.UId, id, sb, St7.kMaxStrLen);
            if (iErr != St7.ERR7_NoError)
            {
                fcName = string.Empty;
                return false;
            }
            fcName = sb.ToString();
            return true;
        }

        /// <summary>
        /// Pull available freedom case from Strand7 document to this St7Model.
        /// </summary>
        /// <param name="removeExisting">
        /// Sets true to remove all existing freedom case in this St7Model.
        /// </param>
        /// <param name="msg">
        /// Report message for errors tracking.
        /// </param>
        /// <returns>
        /// True: Pull operation succeeded<br/>
        /// False: Otherwise.
        /// </returns>
        public bool PullAllFreedomCases(bool removeExisting, out string msg)
        {
            int caseNum = -1;
            // Get number of freedom cases in the Strand7 document
            int iErr = St7.St7GetNumFreedomCase(this.UId, ref caseNum);
            if (iErr != St7.ERR7_NoError)
            {
                msg = "Pull fails:\n" +
                      "Errors found when call method St7GetNumFreedomCase() operation";
                return false;
            } else if (caseNum == 0)
            {
                msg = "Pull fails:\n" + 
                      "Strand7 Document does not have available freedom case(s) to pull to this St7Model";
                return true;
            }

            // Pointer to store count of existing freedom cases
            int existingCount = this.FreedomCases.Count;

            if (removeExisting && existingCount > 0) 
            { 
                // Remove all items from freedom case list
                this.FreedomCases.Clear();

                // Check if all items in the list is removed
                if(this.FreedomCases.Count > 0)
                {
                    msg = "Pull fails:\n" +
                          "Cannot remove existing freedom cases in this St7Model";
                    return false;
                } 

                for (int id = 1; id < caseNum + 1; id++)
                {
                    if (!this.PullFreedomCaseName(id, out string fcName))
                    {
                        msg = "Pull fails:\n" +
                             $"Cannot find freedom case of number {id} in the associated Strand7 document";
                        return false;
                    }

                    // Gets the defaults of freedom case
                    int[] defaults = new int[6];
                    iErr = St7.St7GetFreedomCaseDefaults(this.UId, id, defaults);
                    if (iErr != St7.ERR7_NoError)
                    {
                        msg = "Pull fails:\n" + 
                             $"Cannot get freedom case:[{fcName}] Dofs setting from associated Strand7 document";
                        return false;
                    }

                    // Call for a new freedom case
                    St7FreedomCase freedomCase = new St7FreedomCase($"St7Doc Freedom Case {id}", id);
                    // Get freedoms settings
                    St7FreedomConditionPreset preset = St7FreedomCase.FromSt7DocDefaults(this, id);
                    if (preset == St7FreedomConditionPreset.Unset)
                    {
                        msg = "Pull fails:\n" + 
                             $"Cannot to set Dofs, defaults from Strand7 document \"{this.FileName}\" might not be recognized";
                        return false;
                    }

                    // Pass preset to freedom case
                    freedomCase.ConditionPreset = preset;
                    // Add to list
                    this.FreedomCases.Add(freedomCase);
                }
                msg = "Pull completes:\n"+
                     $"Operation remove existing and append {caseNum} freedom cases to list";
                return true;
            }

            // If user prompts to keep the existing freedom case, append pull ones from the document
            // to the end of this St7Model's freedom case list
            // Case number start from 1 if current freedom case list does not hold any item
            // and start from the number of existing cases + 1 when list contains items
            int appendId = (existingCount == 0) ? 1 : existingCount + 1;

            // Id as freedom case number in the "document", starting as 1
            for (int id = 1; id < caseNum + 1; id++)
            {
                if (!this.PullFreedomCaseName(id, out string fcName))
                {
                    msg = "Pull fails:\n" + 
                          $"Cannot find freedom case of number {id} in the associated Strand7 document";
                    return false;
                }

                // Gets the defaults of freedom case
                int[] defaults = new int[6];
                iErr = St7.St7GetFreedomCaseDefaults(this.UId, id, defaults);
                if (iErr != St7.ERR7_NoError)
                {
                    msg = "Pull fails:\n" + 
                         $"Cannot get freedom case:[{fcName}] Dofs setting from associated Strand7 document";
                    return false;
                }

                // Call for a new freedom case
                St7FreedomCase freedomCase = new St7FreedomCase($"St7Doc Freedom Case {appendId}", appendId);
                // Get freedoms settings
                St7FreedomConditionPreset preset = St7FreedomCase.FromSt7DocDefaults(this, id);
                if (preset == St7FreedomConditionPreset.Unset)
                {
                    msg = "Pull fails:\n" +
                         $"Cannot set Dofs, defaults from Strand7 document \"{this.FileName}\" might not be recognized";
                    return false;
                }

                // Pass preset to freedom case
                freedomCase.ConditionPreset = preset;
                // Add to list
                this.FreedomCases.Add(freedomCase);
            }

            msg = "Pull completes:\n" +
                 $"Operation append {caseNum} freedom cases to list";
            return true;
        }

        
    }
}
