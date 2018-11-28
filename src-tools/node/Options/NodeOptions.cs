namespace node.Options
{
    using System.Collections.Generic;

    public class NodeOptions
    {
        public string Name { get; set; } = Program.Name;

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        public List<NodeServiceOptions> Services { get; set; } = new List<NodeServiceOptions>();
    }
}