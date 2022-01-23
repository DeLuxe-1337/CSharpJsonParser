
using JsonParser.Extensions;

namespace JsonParser.Parsing.Nodes
{
    public static class NodeHelper
    {
        private static Node SearchFor(Node node, string item)
        {
            switch (node.GetType().Name)
            {
                case "AssignmentNode":
                    {
                        AssignmentNode assign = node as AssignmentNode;

                        if (assign.Name == item)
                        {
                            return assign.Value;
                        }

                        break;
                    }
                case "BlockNode":
                    {
                        BlockNode block = node as BlockNode;

                        foreach (Node n in block.nodes)
                        {
                            Node search = SearchFor(n, item);

                            if (search != null)
                            {
                                return search;
                            }
                        }
                        break;
                    }
                default:
                    return null;
            }

            return null;
        }
        public static Node Index(this Node node, string index)
        {
            index = index.MassReplace(">", " >", "> "); //To simply make it easier to split while allowing spaces in regular json words.

            string[] path = index.Split('>');

            Node current = node;
            foreach (string a in path)
            {
                current = SearchFor(current, a);
            }

            return current;
        }
        public static T Index<T>(Node node, string index)
        {
            return (T)Index(node, index);
        }
    }
}
