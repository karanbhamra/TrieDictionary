using System;
using System.Collections.Generic;
using System.Text;

namespace Tries
{
    public class TrieNode
    {
        public char Letter { get; private set; }
        public Dictionary<char, TrieNode> Children { get; private set; }
        public bool IsWord { get; set; }

        public TrieNode(char c)
        {
            Children = new Dictionary<char, TrieNode>();
            Letter = c;
            IsWord = false;
        }
    }

    public class Trie
    {
        private TrieNode root;

        public Trie()
        {
            Clear();
        }

        public void Clear()
        {
            // $ is the start character
            root = new TrieNode('$');
        }

        public void Insert(string word)
        {
            var children = root.Children;

            for (var pos = 0; pos < word.Length; pos++)
            {
                var letter = word[pos];
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

                children = tempTrieNode.Children;

                if (pos == word.Length - 1)
                {
                    tempTrieNode.IsWord = true;
                }
            }
        }

        public bool Contains(string word)
        {
            var searchNode = SearchNode(word);

            if (searchNode != null && searchNode.IsWord)
            {
                return true;
            }

            return false;
        }

        private TrieNode SearchNode(string word)
        {
            var tempChildren = root.Children;
            TrieNode tempNode = null;

            foreach (var current in word)
            {
                if (tempChildren.ContainsKey(current))
                {
                    tempNode = tempChildren[current];
                    tempChildren = tempNode.Children;
                }
                else
                {
                    return null;
                }
            }

            return tempNode;
        }

        public List<string> GetAllMatchingPrefix(string prefix)
        {
            var allWords = new List<string>();

            var node = SearchNode(prefix);

            GetAllWords(node, allWords, prefix);

            return allWords;
        }

        private void GetAllWords(TrieNode node, List<string> allWords, string prefix)
        {
            if (node == null)
            {
                return;
            }

            foreach (var (letter, trieNode) in node.Children)
            {
                GetAllWords(trieNode, allWords, prefix + trieNode.Letter);
            }

            if (node.IsWord)
            {
                allWords.Add(prefix);
            }
        }

        public bool Remove(string prefix)
        {
            // test all the cases
            var foundNode = SearchNode(prefix);

            if (foundNode == null || prefix.Length == 0)
            {
                return false;
            }

            foundNode.IsWord = false;
            return true;

            //if (foundNode.IsLeaf && foundNode.Children.Count > 0)
            //{
            //    foundNode.IsLeaf = false;
            //    return true;
            //}
            //else //if (foundNode.IsLeaf && foundNode.Children.Count == 0)
            //{
            //    // this is a shortcut, sever the childnode from the parents children
            //    // faster than having to recursively go back up the tree

            //    var parentNode = SearchNode(prefix.Substring(0, prefix.Length - 1));

            //    parentNode.Children.Remove(prefix[^1]);

            //    return true;
            //}

           // return false;

        }
    }
}