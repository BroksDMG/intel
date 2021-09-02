using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel8086
{
    class CPUSimulator
    {
        List<Register> registers = new List<Register>();
        List<ParentRegister> parentRegisters = new List<ParentRegister>();

        bool run = false;

        bool error = false;

        public CPUSimulator()
        {
            AddParentRegister(new ParentRegister("A"));
            AddParentRegister(new ParentRegister("B"));
            AddParentRegister(new ParentRegister("C"));
            AddParentRegister(new ParentRegister("D"));

            Reset();
        }

        private void AddParentRegister(ParentRegister register)
        {
            registers.Add(register);
            registers.AddRange(register.childRegisters);
            parentRegisters.Add(register);
        }
     
        public void Simulate()
        {
            run = true;

            while (run)
            {
                if(!error)
                    DisplayRegisters();

                error = false;

                string[] input = Console.ReadLine().Trim().ToLower().Split(" ");

                if (input.Length == 1 && input[0] == "")
                    continue;

                try
                {
                    switch (input[0])
                    {
                        case "exit":
                            Exit();
                            break;
                        case "reset":
                            Reset();
                            break;
                        case "random":
                            Randomize();
                            break;
                        case "set":
                            Set(input);
                            break;
                        case "move":
                            Move(input);
                            break;
                        case "exchange":
                            Exchange(input);
                            break;
                        default:
                            throw new ArgumentException("Podano błędną komendę!");
                    }
                } catch (Exception ex)
                {
                    DisplayErrorMessage(ex);

                    error = true;
                }
                
            }
        }

        private void DisplayRegisters()
        {
            Console.WriteLine();

            foreach(ParentRegister register in parentRegisters)
            {
                Console.Write($"\t |  {register.Name}X {register.Value}\t");
                foreach(ChildRegister childRegister in register.childRegisters)
                {
                    Console.Write($"{childRegister.Name} {childRegister.Value}  ");
                }
                Console.WriteLine("|");
            }

            Console.WriteLine();
        }

        private void DisplayErrorMessage(Exception ex)
        {
            Console.WriteLine($"\nWystąpił błąd! ({ex.Message})");
        }

        private void Exchange(string[] input)
        {
            if (input.Length != 3)
            {
                throw new ArgumentException("Podano niewłaściwą ilość argumentów!");
            }

            Register registerA = FindRegisterByName(input[1]);

            Register registerB = FindRegisterByName(input[2]);

            if (registerA.Lenght != registerB.Lenght)
            {
                throw new ArgumentException("Podano rejestry o różnych długościach!");
            }

            string temp = registerA.Value;

            registerA.Value = registerB.Value;

            registerB.Value = temp;
        }

        private void Move(string[] input)
        {
            if (input.Length != 3)
            {
                throw new ArgumentException("Podano niewłaściwą ilość argumentów!");
            }

            Register registerA = FindRegisterByName(input[1]);

            Register registerB = FindRegisterByName(input[2]);

            if (registerA.Lenght != registerB.Lenght)
            {
                throw new ArgumentException("Podano rejestry o różnych długościach!");
            }

            registerA.Value = registerB.Value;

        }

        private void Set(string[] input)
        {
            if(input.Length != 3)
            {
                throw new ArgumentException("Podano niewłaściwą ilość argumentów!");
            }

            Register register = FindRegisterByName(input[1]);

            register.Value = input[2];
        }

        private void Randomize()
        {
            foreach(Register register in parentRegisters)
            {
                string temp = "";
                for (int i = 0; i < 4; i++)
                {
                    temp += Register.characters[new Random().Next(0, Register.characters.Length)];
                }
                register.Value = temp;
            }
        }

        private void Reset()
        {
            foreach(ParentRegister parentRegister in parentRegisters)
            {
                parentRegister.Value = "0000";
            }
        }

        private void Exit()
        {
            run = false;
        }

        private Register FindRegisterByName(string name)
        {
            Register ret = registers.Find(x =>
               x is ParentRegister && name.ToUpper() == $"{x.Name}X".ToUpper()
               || x is ChildRegister && name.ToUpper() == x.Name.ToUpper());

            if (ret == null)
            {
                throw new ArgumentException($"Nie można znaleść rejestru o nazwie: {name}");
            }

            return ret;
        }
    }
}
