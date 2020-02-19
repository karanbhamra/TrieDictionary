using System;
using System.Collections.Generic;
using System.Text;

namespace Tries
{
    public class TrieNode
    {
        public char letter;
        public Dictionary<char, TrieNode> children;
        public bool isLeaf;

        public TrieNode(char c)
        {
            children = new Dictionary<char, TrieNode>();
            letter = c;
            isLeaf = false;
        }
    }

    public class Trie
    {
        private TrieNode root;

        public Trie()
        {
            // $ is the start character
            root = new TrieNode('$');
        }

        public void Insert(string word)
        {
            var children = root.children;
            int pos = 0;
            foreach (var letter in word)
            {
                TrieNode tempTrieNode;

                if (children.ContainsKey(letter))
                {
                    tempTrieNode = children[letter];
                }
                else
                {
                    tempTrieNode = new TrieNode(letter);
                    children.Add(letter, tempTrieNode);
                }

                children = tempTrieNode.children;

                if (pos == word.Length - 1)
                {
                    tempTrieNode.isLeaf = true;
                }

                pos++;
            }
        }

        public bool Contains(string word)
        {
            var temp = SearchNode(word);

            if (temp != null && temp.isLeaf)
            {
                return true;
            }

            return false;
        }

        private TrieNode SearchNode(string word)
        {
            var tempChildren = root.children;

            StringBuilder stringBuilder = new StringBuilder();

            TrieNode tempNode = null;

            for (int i = 0; i < word.Length; i++)
            {
                char current = word[i];

                if (tempChildren.ContainsKey(current))
                {
                    stringBuilder.Append(current);
                    tempNode = tempChildren[current];
                    tempChildren = tempNode.children;
                }
                else
                {
                    return null;
                }
            }

            return tempNode;
        }

        public List<string> AllWords(string prefix)
        {
            List<string> allwords = new List<string>();

            var node = SearchNode(prefix);

            GetAllWords(node, allwords, prefix);

            return allwords;
        }

        private void GetAllWords(TrieNode node, List<string> allwords, string prefix)
        {
            if (node == null)
            {
                return;
            }

            foreach (var nodeChild in node.children)
            {
                GetAllWords(nodeChild.Value, allwords, prefix + nodeChild.Value.letter);
            }

            if (node.isLeaf)
            {
                allwords.Add(prefix);
            }
        }

        public bool StartsWith(string prefix)
        {
            if (SearchNode(prefix) is null)
            {
                return false;
            }

            return true;
        }
    }
}