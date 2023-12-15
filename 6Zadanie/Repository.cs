using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _6Zadanie
{
    internal class Repository
    {


        private string fileName;

        public Repository(string fileName)
        {
            this.fileName = fileName;
        }

        public Worker[] GetAllWorkers()
        {
            string[] lines = File.ReadAllLines(fileName);

            return lines.Select(line => ParseWorker(line)).ToArray();
        }

        public Worker GetWorkerById(int id)
        {
            string[] lines = File.ReadAllLines(fileName);

            return lines
                .Select(line => ParseWorker(line))
                .FirstOrDefault(worker => worker.Id == id);
        }

        public void DeleteWorker(int id)
        {
            string[] lines = File.ReadAllLines(fileName);

            var updatedWorkers = lines
                .Select(line => ParseWorker(line))
                .Where(worker => worker.Id != id)
                .Select(worker => $"{worker.Id}#{worker.Timestamp}#{worker.FIO}#{worker.Age}#{worker.Height}#{worker.DateOfBirth}#{worker.PlaceOfBirth}");

            File.WriteAllLines(fileName, updatedWorkers);
        }

        public void AddWorker(Worker worker)
        {
            worker.Id = GenerateUniqueId();
            worker.Timestamp = DateTime.Now;

            string newWorkerLine = $"{worker.Id}#{worker.Timestamp}#{worker.FIO}#{worker.Age}#{worker.Height}#{worker.DateOfBirth}#{worker.PlaceOfBirth}";

            File.AppendAllLines(fileName, new[] { newWorkerLine });
        }

        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            string[] lines = File.ReadAllLines(fileName);

            return lines
                .Select(line => ParseWorker(line))
                .Where(worker => worker.Timestamp >= dateFrom && worker.Timestamp <= dateTo)
                .ToArray();
        }

        private Worker ParseWorker(string line)
        {
            string[] parts = line.Split('#');

            return new Worker
            {
                Id = int.Parse(parts[0]),
                Timestamp = DateTime.Parse(parts[1]),
                FIO = parts[2],
                Age = int.Parse(parts[3]),
                Height = int.Parse(parts[4]),
                DateOfBirth = DateTime.Parse(parts[5]),
                PlaceOfBirth = parts[6]
            };
        }

        private int GenerateUniqueId()
        {
            string[] lines = File.ReadAllLines(fileName);

            if (lines.Length == 0)
                return 1;

            int lastId = lines
                .Select(line => int.Parse(line.Split('#')[0]))
                .Max();

            return lastId + 1;
        }
    }
}
