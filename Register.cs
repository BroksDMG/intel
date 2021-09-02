using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intel8086
{
    abstract class Register
    {
        public const string characters = "0123456789ABCDEF";

        protected string name;

        public abstract string Name { get; set; }
        public abstract string Value { get; set; }

        public abstract int Lenght { get; }


        protected void CheckValue(ref string s)
        {
            s = s.ToUpper().Trim();

            if (s.Length != Lenght)
            {
                throw new ArgumentException("Wartość rejestru ma niepoprawną ilość znaków!");
            }

            foreach (char c in s)
            {
                if (!characters.Contains(c))
                {
                    throw new ArgumentException("Użyto nieprawidłowych znaków!");
                }
            }
        }
    }

    class ParentRegister : Register
    {
        public readonly ChildRegister[] childRegisters;

        public override string Name { get => name; set => name = value; }
        public override string Value { get => GetValue(); set => SetValue(value); }
        public override int Lenght => 4;

        public ParentRegister(string name)
        {
            Name = name;
            childRegisters = new ChildRegister[] {
                new ChildRegister("H", this),
                new ChildRegister("L", this)
            };
        }

        private void SetValue(string s)
        {
            CheckValue(ref s);

            childRegisters[0].Value = s.Substring(0, 2);
            childRegisters[1].Value = s.Substring(2, 2);
        }

        private string GetValue()
        {
            string temp = "";

            for (int i = 0; i < childRegisters.Length; i++)
            {
                temp += childRegisters[i].Value;
            }

            return temp;
        }
    }

    class ChildRegister : Register
    {
        private ParentRegister parentRegister;
        private string _value;
        private string name;

        public override string Name { get => GetName(); set => name = value; }
        public override string Value { get => _value; set { CheckValue(ref value); _value = value; } }
        public override int Lenght => 2;

        public ChildRegister(string name, ParentRegister parentRegister)
        {
            Name = name;
            this.parentRegister = parentRegister;
        }

        private string GetName()
        {
            return $"{parentRegister.Name}{name}";
        }
    }
}
