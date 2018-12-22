namespace AutoLazer.Core
{
    /// <summary>
    /// Block represents an object
    /// that has at least an ID. The other properties are optional
    /// </summary>
    public class Block
    {
        /// <summary>
        /// Default Constructor for a block
        /// </summary>
        /// <param name="id">The ID of the block</param>
        /// <param name="frontage">The frontage of the block</param>
        /// <param name="area">The area of the block</param>
        public Block(string id, double frontage = 0, double area = 0)
        {
            Id = id;
            Frontage = frontage;
            Area = area;
        }

        /// <summary>
        /// The Identifier of the block
        /// </summary>
        public string Id {get;}

        /// <summary>
        /// The Frontage of the block
        /// </summary>
        public double Frontage {get;}

        /// <summary>
        /// The Area of the block
        /// </summary>
        public double Area {get;}
    }
}