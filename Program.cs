using System;
using System.Collections.Generic;

namespace Calc
{
    class Program
    {
        static void Main()
        {
            var steps = new[]
            {
                new Step("-5", x => x - 5),
                new Step("+5", x => x + 5),
                new Step("5", x => x * 10 + 5),
                new Step("2", x => x * 10 + 2)
            };
            FindSolution(steps, 5, 0, 210, new List<Step>());
        }

        static bool FindSolution(IEnumerable<Step> steps, int moves, int current, int target, IList<Step> crumbs)
        {
            if (moves == 0)
            {
                if (current == target)
                {
                    Console.WriteLine(string.Join(", ", crumbs));
                    return true;
                }
                
                return false;
            }

            foreach (var step in steps)
            {
                int next = step.F(current);
                crumbs.Add(step);
                var solved = FindSolution(steps, moves - 1, next, target, crumbs);
                if (solved)
                {
                    return solved;
                }

                crumbs.RemoveAt(crumbs.Count - 1);
            }

            return false;
        }

        private class Step
        {
            public Step(string name, Func<int, int> f)
            {
                Name = name;
                F = f;
            }

            public string Name { get; }

            public Func<int, int> F { get; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
