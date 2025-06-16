
using St7Toolkit.Element;

namespace St7Toolkit
{
    /// <summary>
    /// Implements all of the Strand7 API methods.
    /// </summary>
    public partial class St7Model
    {
        /// <summary>
        /// Gets a Node from St7Model given its entity number.
        /// </summary>
        /// <param name="entityNum">
        /// Node's entity number.
        /// </param>
        /// <returns>
        /// The Node of input entity number<br/>
        /// or St7Node.Empty value if failed.
        /// </returns>
        public St7Node GetNode(int entityNum)
        {
            if (!this.IsValid || entityNum < 1 || this.Nodes.Count < 1) return St7Node.Unset;

            St7Node node = St7Node.Unset;

            node = this.Nodes[entityNum - 1];

            return node;
        }

        /// <summary>
        /// Gets a Freedom Case from St7Model given its entity number.
        /// </summary>
        /// <param name="entityNum">
        /// Freedom Case's entity number, must be greater than 0.
        /// </param>
        /// <returns>
        /// The Freedom Case of input entity number<br/>
        /// or FreedomCase.Empty value if failed.
        /// </returns>
        public St7FreedomCase GetFreedomCase(int entityNum)
        {
            if (!this.IsValid || entityNum < 1 || this.FreedomCases.Count < 1) return St7FreedomCase.Unset;

            St7FreedomCase fc = St7FreedomCase.Unset;

            fc = this.FreedomCases[entityNum - 1];

            return fc;
        }
    }
}
