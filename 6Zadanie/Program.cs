using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using _6Zadanie;

namespace _6Zadanie
{
    internal class Program
    {
        static void Main()
        {
            string fileName = @"D:\Workers.txt";
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Файл {fileName} не найден. Создаем новый файл.");
                File.WriteAllText(fileName, string.Empty);
            }

            Repository repository = new Repository(fileName);

            while (true)
            {
                Console.WriteLine("1. Просмотр всех записей");
                Console.WriteLine("2. Просмотр одной записи по ID");
                Console.WriteLine("3. Создание записи");
                Console.WriteLine("4. Удаление записи");
                Console.WriteLine("5. Загрузка записей в выбранном диапазоне дат");
                Console.WriteLine("6. Выход");
                Console.Write("Выберите действие (1-6): ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            DisplayWorkers(repository.GetAllWorkers());
                            break;
                        case 2:
                            Console.Write("Введите ID записи: ");
                            if (int.TryParse(Console.ReadLine(), out int id))
                            {
                                Worker worker = repository.GetWorkerById(id);
                                DisplayWorker(worker);
                            }
                            break;
                        case 3:
                            Console.Write("Введите Ф.И.О.: ");
                            string fio = Console.ReadLine();
                            Console.Write("Введите возраст: ");
                            if (int.TryParse(Console.ReadLine(), out int age))
                            {
                                Console.Write("Введите рост: ");
                                if (int.TryParse(Console.ReadLine(), out int height))
                                {
                                    Console.Write("Введите дату рождения (в формате дд.мм.гггг): ");
                                    if (DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
                                    {
                                        Console.Write("Введите место рождения: ");
                                        string placeOfBirth = Console.ReadLine();

                                        Worker newWorker = new Worker
                                        {
                                            FIO = fio,
                                            Age = age,
                                            Height = height,
                                            DateOfBirth = dateOfBirth,
                                            PlaceOfBirth = placeOfBirth
                                        };

                                        repository.AddWorker(newWorker);
                                        Console.WriteLine("Запись успешно добавлена.");
                                    }
                                }
                            }
                            break;
                        case 4:
                            Console.Write("Введите ID записи для удаления: ");
                            if (int.TryParse(Console.ReadLine(), out int deleteId))
                            {
                                repository.DeleteWorker(deleteId);
                                Console.WriteLine("Запись успешно удалена.");
                            }
                            break;
                        case 5:
                            Console.Write("Введите дату начала (в формате дд.мм.гггг): ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime dateFrom))
                            {
                                Console.Write("Введите дату окончания (в формате дд.мм.гггг): ");
                                if (DateTime.TryParse(Console.ReadLine(), out DateTime dateTo))
                                {
                                    Worker[] workersInRange = repository.GetWorkersBetweenTwoDates(dateFrom, dateTo);
                                    DisplayWorkers(workersInRange);
                                }
                            }
                            break;
                        case 6:
                            return;
                        default:
                            Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                }

                Console.WriteLine();
            }
        }

        static void DisplayWorkers(Worker[] workers)
        {
            Console.WriteLine("ID\tФ.И.О.\t\t\tВозраст\tРост\tДата рождения\tМесто рождения");
            foreach (var worker in workers)
            {
                Console.WriteLine($"{worker.Id}\t{worker.FIO}\t\t{worker.Age}\t{worker.Height}\t{worker.DateOfBirth:d}\t\t{worker.PlaceOfBirth}");
            }
        }

        static void DisplayWorker(Worker worker)
        {

            Console.WriteLine($"ID: {worker.Id}");
            Console.WriteLine($"Ф.И.О.: {worker.FIO}");
            Console.WriteLine($"Возраст: {worker.Age}");
            Console.WriteLine($"Рост: {worker.Height}");
            Console.WriteLine($"Дата рождения: {worker.DateOfBirth:d}");
            Console.WriteLine($"Место рождения: {worker.PlaceOfBirth}");


        }
    }
}
