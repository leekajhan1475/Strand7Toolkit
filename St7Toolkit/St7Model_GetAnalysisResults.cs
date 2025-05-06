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

namespace St7Toolkit
{
    public partial class St7Model
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loadCase"></param>
        /// <param name="system"></param>
        /// <param name="view"></param>
        /// <param name="plane"></param>
        /// <param name="digits"></param>
        /// <param name="pStress1"></param>
        /// <param name="pStress2"></param>
        /// <param name="ang1"></param>
        /// <param name="mises"></param>
        /// <param name="tresca"></param>
        /// <param name="mag"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool GetPlateStress(
            int loadCase,
            int system,
            int view,
            int plane,
            int digits,
            out double[,] pStress1,
            out double[,] pStress2,
            out double[,] ang1,
            out double[,] mises,
            out double[,] tresca,
            out double[,] mag,
            out string errorMessage
            )
        {

            string msg;

            // Select the nodes of the Strand7 model
            int iErr = St7.St7SetAllEntitySelectState(this.UId, St7.tyPLATE, St7.btTrue);
            int numPLATE = -1;
            iErr = St7.St7GetEntitySelectCount(this.UId, St7.tyPLATE, ref numPLATE);

            /***** result file perform analysis on plate stress *****/
            // Try open result file
            int[] resultCases;

            // Check if the current Strand7 model own a file path
            if (this.FileName == null)
            {
                pStress1 = new double[0, 0];
                pStress2 = new double[0, 0];
                ang1 = new double[0, 0];
                mises = new double[0, 0];
                tresca = new double[0, 0];
                mag = new double[0, 0];
                errorMessage = "The current Strand7 model does not own a file path";
                return false;
            }

            // Try open the result file
            if (!FileIO.OpenResultFile(this.UId, FileIO.St7SolverResultPath(this.FileName, this.Solver), out resultCases, out msg))
            {
                pStress1 = new double[0,0];
                pStress2 = new double[0, 0];
                ang1 = new double[0, 0];
                mises = new double[0, 0];
                tresca = new double[0, 0];
                mag = new double[0, 0];
                errorMessage = "Cannot open result file. See following error message: " + msg;
                return false;
            }

            // Declare list variables to store the plates analysis data
            // Plates principle stress 1 data
            pStress1 = new double[numPLATE, 4];
            // Plates principle stress 2 data
            pStress2 = new double[numPLATE, 4];
            // Plates angle1-x (local x) analysis data
            ang1 = new double[numPLATE, 4];
            // Plates vonMises analysis data
            mises = new double[numPLATE, 4];
            // Plates tresca - shear stress data
            tresca = new double[numPLATE, 4];
            // Plates maximum stress magnitude data
            mag = new double[numPLATE, 4];

            // Start getting each plate's stress result
            for (int i = 1; i <= numPLATE; i++)
            {
                // Check if the plate's result is available
                int[] state = new int[3];

                iErr = St7.St7GetElementResultState(
                    this.UId,
                    St7.tyPLATE,
                    i,
                    loadCase,
                    state);

                // Check if any plate result is unavailable.
                // Once found, report the plate number and return a false value for this method
                if (state[1] != St7.btTrue)
                {
                    errorMessage = $"Result of plate number: {i} is not available";
                    return false;
                }

                // Check for errors when acquiring the plate stress result
                if (iErr != St7.ERR7_NoError)
                {
                    Compute.GetErrorMessage(iErr, out msg);
                    errorMessage = "Fail to acquire result data for the plate. See following message: " + msg;
                    return false;
                }

                // Get each plate's analysis result
                int numPts = -1;
                int numCols = -1;
                double[] temp = new double[St7.kMaxPlateResult - 1];

                iErr = St7.St7GetPlateResultArray(
                  this.UId,             // Strand7 model ID
                  St7.rtPlateStress,
                  system,               // One of "Combined, LocalCoordinateSystem, GlobalXYZ"
                  i,                    // plate number
                  loadCase,             // result case
                  view,                 // location of result's location
                  plane,                // location of stress plane
                  1,                    // ignore when no ply property is assumed
                  ref numPts,
                  ref numCols,
                  temp
                  );

                // Check for errors when acquiring the plate stress result
                if (iErr != St7.ERR7_NoError)
                {
                    Compute.GetErrorMessage(iErr, out msg);
                    errorMessage = "Fail to acquire result data for the plate. See following message: " + msg;
                    return false;
                }

                // Extract result data from the temp array
                double[] plateResult = new double[numPts * numCols];
                for (int k = 0; k < numPts * numCols; k++) plateResult[k] = temp[k];

                /***** Compute output data based on input system or view *****/
                // Only compute principle stresses in combined system
                if (system == St7.stPlateCombined)
                {
                    // Iterate through each sample point
                    for (int j = 0; j < numPts; j++)
                    {
                        pStress1[i, j] = Math.Round(plateResult[(j * numCols) + St7.ipPlateCombPrincipal11], digits);
                        pStress2[i, j] = Math.Round(plateResult[(j * numCols) + St7.ipPlateCombPrincipal22], digits);
                        ang1[i, j] = Math.Round(plateResult[(j * numCols) + St7.ipPlateCombPrincipalAngle], digits);
                        mises[i, j] = Math.Round(plateResult[(j * numCols) + St7.ipPlateCombVonMises], digits);
                        tresca[i, j] = Math.Round(plateResult[(j * numCols) + St7.ipPlateCombTresca], digits);
                        mag[i, j] = Math.Round(plateResult[(j * numCols) + St7.ipPlateCombMagnitude], digits);
                    }
                }
            }
            // Close the result file
            iErr = St7.St7CloseResultFile(this.UId);

            if (iErr != St7.ERR7_NoError)
            {
                Compute.GetErrorMessage(iErr, out msg);
                errorMessage = msg;
                return false;
            }
            errorMessage = "No errors";
            return true;
        }





    }
}
