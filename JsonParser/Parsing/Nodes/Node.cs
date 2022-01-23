using System.Collections.Generic;

namespace JsonParser.Parsing.Nodes
{
    public interface Node
    {
        public Node this[string index]
        {
            get
            {
                Node node = this;
                return NodeHelper.Index(node, index);
            }
        }
        public Node this[int index]
        {
            get
            {
                BlockNode node = this as BlockNode;

                return node.nodes[index];
            }
        }
    }
    public class BlockNode : Node
    {
        public List<Node> nodes;
        public BlockNode(List<Node> nodes)
        {
            this.nodes = nodes;
        }
    }
    public class ValueNode : Node
    {
        public object Value;
        public ValueNode(object value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
    public class AssignmentNode : Node
    {
        public string Name;
        public Node Value;

        public AssignmentNode(string name, Node value)
        {
            Name = name;
            Value = value;
        }
    }
}
