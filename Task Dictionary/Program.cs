class Program
{
    public static async Task Main()
    {
        var notification = new NotificationOfDictionary();
        var manager = new Dictionary();

        bool running = true;
        while (running)
        {
            Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Создать словарь.");
            Console.WriteLine("2. Добавить слово и перевод.");
            Console.WriteLine("3. Удалить слово.");
            Console.WriteLine("4. Удалить перевод.");
            Console.WriteLine("5. Найти перевод.");
            Console.WriteLine("6. Замена слова.");
            Console.WriteLine("7. Замена перевода.");
            Console.WriteLine("8. Удаление перевода.");
            Console.WriteLine("9. Добавить перевод к слову.");
            Console.WriteLine("10. Сохранить словарь в файл.");
            Console.WriteLine("11. Загрузить словарь из файла.");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите опцию: ");

            string input = Console.ReadLine();
            int choice;
            bool isValidChoice = int.TryParse(input.Trim(), out choice);

            if (!isValidChoice)
            {
                Console.WriteLine("Неверный ввод. Попробуйте снова.");
                continue;
            }

            switch (choice)
            {

                case 1:
                    Console.Write("Создание нового словаря. Введите тип словаря: ");
                    string word = Console.ReadLine();
                    manager = new Dictionary(word);
                    Console.Write("Словарь был создан.");
                    
                    break;
                case 2:
                    manager.WordAdd += notification.AddWord;
                    Console.Write("Введите слово: ");
                    word = Console.ReadLine();
                    Console.Write("Введите перевод: ");
                    string translation = Console.ReadLine();
                    manager.AddWord(word, translation);
                    break;
                case 3:
                    manager.WordRemove += notification.RemoveWord;
                    Console.Write("Введите слово для удаления: ");
                    word = Console.ReadLine();
                    manager.RemoveWord(word);
                    break;
                case 4:
                    manager.TranslationRemove += notification.RemoveTranslation;
                    Console.Write("Введите слово: ");
                    word = Console.ReadLine();
                    Console.Write("Введите перевод для удаления: ");
                    translation = Console.ReadLine();
                    manager.RemoveTranslation(word, translation);
                    break;
                case 5:
                    manager.TranslationSearch += notification.SearchTranslation;
                    Console.Write("Введите слово для поиска перевода: ");
                    word = Console.ReadLine();
                    var translations = manager.GetTranslations(word);
                    if (translations != null)
                    {
                        Console.WriteLine($"Переводы для '{word}': {string.Join(", ", translations)}");
                    }
                    break;
                case 6:
                    manager.WordReplace += notification.ReplaceWord;
                    Console.Write("Введите слово под замену: ");
                    word = Console.ReadLine();
                    Console.Write("Введите новое слово: ");
                    string newWord = Console.ReadLine();
                    manager.ReplaceWord(word, newWord);
                    break;
                case 7:
                    manager.TranslationReplace += notification.ReplaceTranslation;
                    Console.Write("Введите cлово: ");
                    word = Console.ReadLine();
                    Console.Write("Введите новый перевод: ");
                    translation = Console.ReadLine();
                    manager.ReplaceTranslation(word, translation);
                    break;
                case 8:
                    manager.TranslationRemove += notification.RemoveTranslation;
                    Console.Write("Введите cлово: ");
                    word = Console.ReadLine();
                    Console.Write("Введите перевод: ");
                    translation = Console.ReadLine();
                    manager.RemoveTranslation(word, translation);
                    break;
                case 9:
                    manager.TranslateAdd += notification.AddTranslate;
                    Console.Write("Введите cлово: ");
                    word = Console.ReadLine();
                    Console.Write("Введите перевод: ");
                    translation = Console.ReadLine();
                    manager.AddTranslation(word, translation);
                    break;
                case 10:
                    manager.SaveFile += notification.SaveToFileAsync;
                    Console.Write("Введите имя файла для сохранения: ");
                    string saveFilename = Console.ReadLine();
                    if (string.IsNullOrEmpty(saveFilename))
                    {
                        saveFilename = "DictionarySaveFile.json";
                    }
                    else if (!saveFilename.EndsWith(".json"))
                    {
                        saveFilename += ".json";
                    }
                    await manager.SaveToFileAsync(saveFilename);
                    break;
                case 11:
                    manager.LoadFile += notification.LoadFromFileAsync;
                    Console.Write("Введите имя файла для загрузки: ");
                    string loadFilename = Console.ReadLine();
                    if (string.IsNullOrEmpty(loadFilename))
                    {
                        Console.WriteLine("Имя файла не может быть пустым.");
                    }
                    else
                    {
                        if (!loadFilename.EndsWith(".json"))
                        {
                            loadFilename += ".json";
                        }
                        await manager.LoadFromFileAsync(loadFilename);
                    }
                    break;
                case 0:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }
}

