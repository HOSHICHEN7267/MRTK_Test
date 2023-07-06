using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

class TrieNode
{
    public char Value { get; }
    public Dictionary<char, TrieNode> Children { get; }
    public bool IsEndOfWord { get; set; }

    public TrieNode(char value)
    {
        Value = value;
        Children = new Dictionary<char, TrieNode>();
        IsEndOfWord = false;
    }
}

class Trie
{
    private TrieNode root;

    public Trie()
    {
        root = new TrieNode('\0'); // 使用 null 字元作為根節點的值
    }

    public void Insert(string word)
    {
        TrieNode current = root;
        foreach (char c in word)
        {
            if (!current.Children.ContainsKey(c))
            {
                current.Children[c] = new TrieNode(c);
            }
            current = current.Children[c];
        }
        current.IsEndOfWord = true;
    }

    public bool Search(string word)
    {
        TrieNode node = StartsWith(word);
        return node != null && node.IsEndOfWord;
    }

    public TrieNode StartsWith(string prefix)
    {
        TrieNode current = root;
        foreach (char c in prefix)
        {
            if (current.Children.ContainsKey(c))
            {
                current = current.Children[c];
            }
            else
            {
                return null;
            }
        }
        return current;
    }

    public List<string> FindWords(string prefix)
    {
        TrieNode node = StartsWith(prefix);
        List<string> suggestions = new List<string>();
        
        if (node == null)
        {
            return suggestions;
        }

        CollectWords(node, prefix, suggestions);

        return suggestions;
    }

    void CollectWords(TrieNode node, string prefix, List<string> suggestions)
    {
        if (node.IsEndOfWord)
        {
            suggestions.Add(prefix);
        }

        foreach (TrieNode childNode in node.Children.Values)
        {
            CollectWords(childNode, prefix + childNode.Value, suggestions);
        }
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(root, Formatting.Indented);
    }

    public static Trie FromJson(string json)
    {
        TrieNode root = JsonConvert.DeserializeObject<TrieNode>(json);
        Trie trie = new Trie();
        trie.root = root;
        return trie;
    }
}

public class AutoComplete : MonoBehaviour
{
    Trie trie = new Trie();

    // Start is called before the first frame update
    void Start()
    {
        string jsonText = System.IO.File.ReadAllText("lexicon.json");
        trie = Trie.FromJson(jsonText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartAutoComplete(string input)
    {
        List<string> suggestions = trie.FindWords(input);

        if (suggestions.Count == 0)
        {
            Debug.Log(input + "無建議詞彙。");
        }
        else
        {
            Debug.Log(input + "建議詞彙：");
            foreach (string suggestion in suggestions)
            {
                Debug.Log(suggestion);
            }
        }
    }
}
