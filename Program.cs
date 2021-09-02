using System;

namespace Intel8086
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===================================");
            Console.WriteLine("  Symulator procesora Intel 8086!  ");
            Console.WriteLine("===================================");
            Console.WriteLine();
            Console.WriteLine("Komendy: ");
            Console.WriteLine("- Exit                           - wyjdź z programu");
            Console.WriteLine("- Reset                          - resetuje wszystkie wartości wszystkich rejestrów");
            Console.WriteLine("- Random                         - losuje wartości wszystkich rejestrów");
            Console.WriteLine("- Set <rejestr> <wartość>        - ustawia wartość rejestru na podaną");
            Console.WriteLine("- Move <rejestr> <register>      - kopiuje wartość rejestru drugiego do pierwszego");
            Console.WriteLine("- Exchange <rejestr> <rejestr>   - zamienia wartości podanych rejestrów");

            new CPUSimulator().Simulate();
        }
    }
}
