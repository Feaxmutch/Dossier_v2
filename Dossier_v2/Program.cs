namespace Dossier_v2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddDossier = "1";
            const string CommandWriteDossiers = "2";
            const string CommandDeleteDossier = "3";
            const string CommandExit = "4";

            Dictionary<string, List<string>> employees = new();
            string userInput = string.Empty;
            bool isWorking = true;

            while (isWorking)
            {
                Console.Clear();
                Console.CursorVisible = false;

                Console.WriteLine($"\n{CommandAddDossier}) добавить досье" +
                                  $"\n{CommandWriteDossiers}) вывести все досье" +
                                  $"\n{CommandDeleteDossier}) удалить досье" +
                                  $"\n{CommandExit}) выход");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddDossier:
                        AddDossier(employees);
                        break;

                    case CommandWriteDossiers:
                        WriteAllDossiers(employees);
                        break;

                    case CommandDeleteDossier:
                        DeliteDossier(employees);
                        break;

                    case CommandExit:
                        isWorking = false;
                        break;
                }
            }
        }

        static void AddDossier(Dictionary<string, List<string>> employees)
        {
            string[] userInputs = new string[4];

            Console.Clear();
            Console.CursorVisible = true;

            Console.Write("Введите имя: ");
            userInputs[0] = Console.ReadLine();

            Console.Write("Введите фамилию: ");
            userInputs[1] = Console.ReadLine();

            Console.Write("Введите отчество: ");
            userInputs[2] = Console.ReadLine();

            Console.Write("Введите должность: ");
            userInputs[3] = Console.ReadLine();

            Console.CursorVisible = false;

            for (int i = 0; i < userInputs.Length; i++)
            {
                if (userInputs[i] == string.Empty)
                {
                    userInputs[i] = "Не_указано";
                }
            }

            string fullName = $"{userInputs[1]} {userInputs[0]} {userInputs[2]}";
            string post = userInputs[3];
            bool isNeedToCreateList = employees.TryGetValue(post, out List<string> names) == false
                                   || names == null;

            if (isNeedToCreateList)
            {
                employees[post] = new List<string>();
            }

            employees[post].Add(fullName);

            Console.WriteLine("Добавленно следующее досье:");
            WriteDossier(employees, post, employees[post].Count - 1);

            Console.ReadKey(true);
        }

        static void WriteDossier(Dictionary<string, List<string>> employees, string post, int dossierIndex)
        {
            Console.WriteLine($"{employees[post][dossierIndex]} - {post}");
        }

        static void WriteAllDossiers(Dictionary<string, List<string>> employees)
        {
            Console.Clear();

            foreach (var post in employees)
            {
                for (int i = 0; i < post.Value.Count; i++)
                {
                    WriteDossier(employees, post.Key, i);
                }
            }

            Console.ReadKey(true);
        }

        static void WritePosts(Dictionary<string, List<string>> employees)
        {
            foreach (var post in employees)
            {
                Console.WriteLine(post.Key);
            }
        }

        static void WriteDossiersByPost(Dictionary<string, List<string>> employees, string post)
        {
            Console.Clear();

            for (int i = 0; i < employees[post].Count; i++)
            {
                WriteDossier(employees, post, i);
            }
        }

        static void DeliteDossier(Dictionary<string, List<string>> employees)
        {
            Console.Clear();
            WritePosts(employees);

            Console.Write("\nВведите/скопируйте должность: ");
            string postFromUser = Console.ReadLine();

            if (employees.ContainsKey(postFromUser))
            {
                WriteDossiersByPost(employees, postFromUser);

                Console.Write("\nВведите/скопируйте полное ФИО, которое хотите удалить: ");
                string nameFromUser = Console.ReadLine();

                if (employees[postFromUser].Contains(nameFromUser))
                {
                    employees[postFromUser].Remove(nameFromUser);

                    if (employees[postFromUser].Count == 0)
                    {
                        employees.Remove(postFromUser);
                    }

                    Console.WriteLine("Удалено успешно");
                }
                else
                {
                    Console.WriteLine("Такое ФИО не найдено");
                }
            }
            else
            {
                Console.WriteLine("Такая должность не найдена");
            }

            Console.ReadKey();
        }
    }
}
