using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

public class Dictionary
{
    public string DictionaryType { get; set; }
    private Dictionary<string, List<string>> dictionary;

    public event Action<string, string>? WordAdd;
    public event Action<string>? WordRemove;
    public event Action<string, string>? TranslateAdd;
    public event Action<string, string>? WordReplace;
    public event Action<string, string>? TranslationReplace;
    public event Action<string, string>? TranslationRemove;
    public event Action<string>? TranslationSearch;
    public event Action? SaveFile;
    public event Action? LoadFile;
    public Dictionary() { dictionary = new Dictionary<string, List<string>>(); }
    public Dictionary(string dictionaryType)
    {
        dictionary = new Dictionary<string, List<string>>();
        DictionaryType = dictionaryType;
    }
    public void AddWord(string word, string translate)
    {
        if (!dictionary.ContainsKey(word))
        {
            dictionary[word] = new List<string>();
        }
        dictionary[word].Add(translate);
        WordAdd?.Invoke(word, translate);
    }
    public void AddTranslation(string word, string translation)
    {
        if (dictionary.ContainsKey(word))
        {
            dictionary[word].Add(translation);
        }
        else
        {
            dictionary[word] = new List<string> { translation };
        }
        TranslateAdd?.Invoke(word, translation);
    }
    public void RemoveWord(string word)
    {
        if (dictionary.ContainsKey(word))
        {
            dictionary.Remove(word);
        }
        else
        {
            Console.WriteLine($"Слово '{word}' не найдено в словаре.");
        }
        WordRemove?.Invoke(word);
    }

    public void ReplaceWord(string oldWord, string newWord)
    {
        if (!dictionary.ContainsKey(oldWord))
        {
            var translations = dictionary[oldWord];
            dictionary.Remove(oldWord);
            dictionary[newWord] = translations;
        }
        WordReplace?.Invoke(oldWord, newWord);
    }

    public void ReplaceTranslation(string word, string newTranslation)
    {
        if (dictionary.ContainsKey(word))
        {
            dictionary[word] = new List<string> { newTranslation };
        }
        TranslationReplace?.Invoke(word, newTranslation);
    }
    public void RemoveTranslation(string word, string translation)
    {
        if (dictionary.ContainsKey(word))
        {
            if (dictionary[word].Count == 1)
            {
                Console.WriteLine("Перевод слова остался лишь один, его удаление невозможно.");

            }
            else
            {
                dictionary[word].Remove(translation);
                TranslationRemove?.Invoke(word, translation);
            }
        }
    }

    public List<string>? GetTranslations(string word)
    {
        if (dictionary.TryGetValue(word, out var translations))
        {
            TranslationSearch?.Invoke(word);
            return translations;
        }
        else
        {
            Console.WriteLine("Перевод не был найден.");
            return null;
        }
    }

    public async Task SaveToFileAsync(string filename = "")
    {
        if (string.IsNullOrEmpty(filename))
        {
            filename = $"{DictionaryType}_DictionarySaveFile.json";
        }

        using (StreamWriter writer = new StreamWriter(filename, append: false))
        {
            await writer.WriteLineAsync($"{DictionaryType} словарь");
            foreach (var entry in dictionary)
            {
                string line = $"{entry.Key}:{string.Join(",", entry.Value)}";
                await writer.WriteLineAsync(line);
            }
        }
        SaveFile?.Invoke();
    }

    public async Task LoadFromFileAsync(string filename)
    {
            if (File.Exists(filename))
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (line.Contains(":"))
                        {
                            string[] parts = line.Split(':');
                        if (parts.Length == 2)
                        {
                            string word = parts[0];
                            string[] translations = parts[1].Split(',');

                            if (!dictionary.ContainsKey(word))
                            {
                                dictionary[word] = new List<string>(translations);
                            }
                            else
                            {
                                foreach (var translation in translations)
                                {
                                    if (!dictionary[word].Contains(translation))
                                    {
                                        dictionary[word].Add(translation);
                                    }
                                }
                            }
                        }
                        LoadFile?.Invoke();
                    }
                }
                }
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
    }
}

