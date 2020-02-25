using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Tries
{
    class Program
    {
        static void Main(string[] args)
        {
            Trie trie = new Trie();

            trie.Insert("hey");
            trie.Insert("he");
            trie.Insert("hell");
            trie.Insert("hello");
            trie.Insert("baby");
            trie.Insert("babe");
            trie.Insert("havana");
            trie.Insert("heaven");

            while(true)
            {
                Console.WriteLine("do what? remove, list");
                string input = Console.ReadLine();

                if (input.Contains("r"))
                {
                    Console.WriteLine("remove what?");
                    string str = Console.ReadLine();

                    trie.Remove(str);
                }
                else if (input.Contains("l"))
                {
                    Console.WriteLine("list prefix");
                    string str = Console.ReadLine();
                    foreach (var item in trie.GetAllMatchingPrefix(str))
                    {
                        Console.WriteLine(item);
                    }
                }
                else if (input.Contains('x'))
                {
                    return;
                }
            }




        }
    }
}