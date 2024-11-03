using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NotificationOfDictionary
{
    public NotificationOfDictionary() { }
    public void AddWord(string word, string translation)
    {
        Console.WriteLine($"Добавлено слово {word} и его перевод {translation}.");
    }
    public void RemoveWord(string word) 
    {
        Console.WriteLine($"Слово {word} удалено и все варианты его перевода.");
    }
    public void AddTranslate(string word, string translation)
    {
        Console.WriteLine($"К слову {word} добавлен перевод {translation}");
    }
    public void ReplaceWord(string word, string translation)
    {
        Console.WriteLine($"Слово '{word}' заменено на '{translation}'.");
    }
    public void ReplaceTranslation(string word, string translation)
    {
        Console.WriteLine($"Перевод в слове {word} изменен на {translation}.");
    }
    public void SearchTranslation(string word)
    {
        Console.WriteLine($"Перевод слова {word} был найден.");
    }
    public void RemoveTranslation(string word, string translation)
    {
        Console.WriteLine($"Перевод {translation} у слова {word} был удален.");
    }
    public void SaveToFileAsync()
    {
        Console.WriteLine("Файл был сохранен.");
    }
    public void LoadFromFileAsync()
    {
        Console.WriteLine("Файл был загружен.");
    }
}