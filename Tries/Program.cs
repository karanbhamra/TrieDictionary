﻿using System;
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

            string json = File.ReadAllText("fulldictionary.json");
            
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            
            foreach (var word in dictionary.Keys)
            {
                trie.Insert(word.ToLower());
            }
            
            // trie.Insert("hey");
            // trie.Insert("hell");
            // trie.Insert("hello");
            // trie.Insert("baby");
            // trie.Insert("babe");
            // trie.Insert("havana");
            // trie.Insert("heaven");

            do
            {
                Console.WriteLine("Search for words starting with");
                string input = Console.ReadLine().ToLower();

                if (input == "x")
                {
                    break;
                }

                var words = trie.AllWords(input);

                if (words.Count == 0)
                {
                    Console.WriteLine($"No words found starting with {input}");
                }
                else if (words.Count == 1)
                {
                    Console.WriteLine($"Word: {input} ------ {dictionary[input]}");
                }
                else
                {
                    foreach (var word in words)
                    {
                        if (word == input)
                        {
                            Console.WriteLine($"Word: {input} ------ {dictionary[input]}");
                        }
                        else
                        {
                            Console.WriteLine(word);
                        }
                    }
                }


            } while (true);


        }
    }
}