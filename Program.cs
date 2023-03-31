using System;
using System.Collections.Generic;
using System.Linq;

namespace Amnesty
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Prison prison = new Prison();
            prison.Work();
        }
    }

    class Prison
    {
        private List<Prisoner> _prisoners;

        public Prison()
        {
            PrisonerCreator prisonerCreator = new PrisonerCreator();

            int prisonersCount = 10;
            _prisoners = new List<Prisoner>();
            
            for (int i = 0; i < prisonersCount; i++)
            {
                _prisoners.Add(prisonerCreator.CreatePrisoner());
            }
        }

        public void Work()
        {
            ConsoleOutputMethods.WriteRedText("Заключённые до амнистии\n");
            ShowPrisoners();

            HoldAmnesty();

            ConsoleOutputMethods.WriteRedText("Заключённые после амнистии\n");
            ShowPrisoners();
        }

        private void HoldAmnesty()
        {
            const string ForgivenCrime = "Антиправительственное";
            var remainingPrisoners = _prisoners.Where(prisoner => prisoner.Crime != ForgivenCrime);

            _prisoners = remainingPrisoners.ToList();
        }

        private void ShowPrisoners()
        {
            if (_prisoners.Count > 0)
            {
                foreach (var prisoner in _prisoners)
                {
                    Console.WriteLine($"{prisoner.FullName}\n" +
                        $"заключён за {prisoner.Crime} преступление\n");
                }
            }
            else
            {
                Console.WriteLine("В тюрьме сейчас нет заключённых");
            }
        }
    }

    class Prisoner
    {
        public Prisoner(string fullname, string crime)
        {
            FullName = fullname;
            Crime = crime;
        }

        public string FullName { get; private set; }
        public string Crime { get; private set; }
    }

    class PrisonerCreator
    {
        private Random _random = new Random();
        private List<string> _fullNames;
        private List<string> _crimes;

        public PrisonerCreator()
        {
            _fullNames = new List<string>();
            _crimes = new List<string>();

            _fullNames.Add("Пупкин Василий Викторович");
            _fullNames.Add("Сафронов Алексей Николаевич");
            _fullNames.Add("Табуретов Биба Васильевич");
            _fullNames.Add("Табуретов Боба Васильевич");
            _fullNames.Add("Котофеев Нурлан Барсикович");
            _fullNames.Add("Рыбаков Александр Юрьевич");
            _fullNames.Add("Владимиров Владимир Владимирович");

            _crimes.Add("Антиправительственное");
            _crimes.Add("Антинародное");
            _crimes.Add("Антиприродное");
            _crimes.Add("Антибожественное");
        }

        public Prisoner CreatePrisoner()
        {
            string fullName = _fullNames[_random.Next(0, _fullNames.Count)];
            string crime = _crimes[_random.Next(0, _crimes.Count)];

            return new Prisoner(fullName, crime);
        }
    }

    static class ConsoleOutputMethods
    {
        public static void WriteRedText(string text)
        {
            ConsoleColor tempColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = tempColor;
        }
    }
}