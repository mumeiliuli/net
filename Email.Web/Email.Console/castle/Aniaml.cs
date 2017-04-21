using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.ConsoleTest.castle
{
    public class Dog : IAnimal
    {
        public string Name
        {
            get
            {
                return "Dog";
            }
        }
    }

    public interface IAnimal
    {
        string Name { get; }
    }

    public class AnimalManager
    {
        public IAnimal _animal;
        public int Age { get; set; }
        public AnimalManager(IAnimal animal)
        {
            _animal = animal;
        }
        
        public void GetName()
        {
            System.Console.WriteLine(_animal.Name);
        }
    }
}
