using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Node     /* класс корень*/
    {
        public int value { get; set; }
        public Node left { get; set; }
        public Node right { get; set; }

        public Node(int value)  /*узел(передаем значение)*/
        {
            this.value = value;
        }
       
        public string showNode()
        {
            StringBuilder output = new StringBuilder(); /*StringBuilder-класс, который содержит метод ToString,Append,AppendLine*/
            showNode(output, 0);
            return output.ToString();   /*возвращение выходных данных*/
        }

        private void showNode(StringBuilder output, int i)    /*уровень дерева*/
        {

            if (right != null)
                right.showNode(output, i + 1);

            output.Append('\t', i);
            output.AppendLine(value.ToString());


            if (left != null)
                left.showNode(output, i + 1);

        }
        


        public void Add(int[] values)      /*добавление в дерево*/
        {
            foreach (var item in values)
            {
                Add(this, item);
            }
        }

        void Add(Node node, int value)
        {
            if (value < node.value)       /*посещаем левый узел дерева*/
            {
                if (node.left == null)            /*если пуст, то заполняем*/
                {
                    node.left = new Node(value);
                }
                else                       /*если не пуст, то посещаем*/
                    Add(node.left, value);
            }
            else                        /*посещаем правый узел*/
            {
                if (node.right == null)                 /*если пуст, то заполняем*/
                {
                    node.right = new Node(value);
                }
                else                            /*если не пуст, то посещаем*/
                    Add(node.right, value);
            }
        }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[] values = new int[] { 3, 8, 7, 9 };
            Node tree = new Node(5);
            tree.Add(values);
            Console.WriteLine("Бинарное дерево");
            Console.WriteLine(tree.showNode());

            using (StreamWriter file = new StreamWriter(@"C:\Users\User\Documents\Binary.txt", true))
            {
                file.WriteLine("Бинарное дерево");
                file.WriteLine(tree.showNode());

                file.WriteLine("Не рекурссивная");
                foreach (var n in EnumerateTree(tree))
                    file.WriteLine(n.value);
                Console.WriteLine("Не рекурссивная");
                foreach (var n in EnumerateTree(tree))
                    Console.WriteLine(n.value);
            }

            

            Console.ReadLine();
        }
        private static IEnumerable<Node> EnumerateTree(Node tree)
        {
            var stack = new Stack<Node>();
            stack.Push(tree);
            while (stack.Count > 0)
            {
                var n = stack.Pop();
                yield return n;
                if (n.left != null)
                    stack.Push(n.left);
                if (n.right != null)
                    stack.Push(n.right);
            }
        }
        
        
    }
    
}
