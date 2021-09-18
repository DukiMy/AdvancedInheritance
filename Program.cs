using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace AdvancedInheritance
{
    interface IUsable
    {
        public int Damage { get; set; }
    }

    interface IEdible
    {
        public int HealAmount { get; set; }
    }

    interface IDamageable
    {
        public int Health { get; set; }
    }

    interface IGreetable
    {
        public string Reply();
    }

    class Bread : Item, IEdible
    {
        public int HealAmount { get; set; } = 50;
    }

    class Sword : Item, IUsable
    {
        public int Damage { get; set; } = 20;

        public Sword()
        {
            base.UsedSlots = 3;
        }
    }

    class Axe : Item, IUsable
    {
        public int Damage { get; set; } = 15;

        public Axe()
        {
            base.UsedSlots = 2;
        }
    }

    class Item
    {
        public int UsedSlots { get; set; }
    }

    class Mouse : Animal
    {
        public override string Reply()
        {
            return "Squeak squeak";
        }

        public Mouse()
        {
            base.Health = 30;
        }
    }

    class Horse : Animal
    {
        public override string Reply()
        {
            return "Neigh neeiiiigh";
        }

        public Horse()
        {
            base.Health = 125;
        }
    }

    class Animal : IDamageable, IGreetable
    {
        public int Health { get; set; }

        public virtual string Reply()
        {
            return "...";
        }
    }

    class Hero
    {
        public int Health { get; set; }

    }

    class Program
    {
        static List<Animal> AnimalPen = new List<Animal>();

        static List<Item> Backpack = new List<Item>();

        public static void PresentAnimals()
        {
            int i = 1;

            foreach (var animal in AnimalPen)
            {
                Type type = animal.GetType();
                string printThis = $"{type}";
                Console.WriteLine($"{i}. {printThis.Remove(0, 20)}");
                i++;
            }
        }

        public static void PresentItems()
        {
            int i = 1;

            foreach (var item in Backpack)
            {
                Type type = item.GetType();
                string printThis = $"{type}";
                Console.WriteLine($"{i}. {printThis.Remove(0,20)}");
                i++;
            }
        }

        public static Animal ChoosePet()
        {
            Animal result = null;

            PresentAnimals();
            Console.WriteLine("Choose your animal. Enter the corresponding number from the list above.");
            result = AnimalPen[Input(1, AnimalPen.Count) - 1];

            return result;
        }

        public static void Greet()
        {
            Console.WriteLine("Who do you want to greet?");

            Console.WriteLine(ChoosePet().Reply());
        }

        public static void Explore()
        {
            Random rand = new Random();
            int randomNum = rand.Next(15);

            switch (randomNum)
            {
                case 1:
                    Console.WriteLine("You stumbled across a horse. Do you want to keep it? 1. Yes. 2. No");
                    if (Input(1, 2) == 1) { AnimalPen.Add(new Horse()); }
                    break;

                case 2:
                    Console.WriteLine("You stumbled across a mouse. Do you want to keep it? 1. Yes. 2. No");
                    if (Input(1, 2) == 1) { AnimalPen.Add(new Mouse()); }
                    break;

                case 3:
                    Console.WriteLine("You found a loaf of bread. Do you want to keep it? 1. Yes. 2. No");
                    if (Input(1, 2) == 1) { Backpack.Add(new Bread()); }
                    break;

                case 4:
                    Console.WriteLine("You found a sword. Do you want to keep it? 1. Yes. 2. No");
                    if (Input(1, 2) == 1) { Backpack.Add(new Sword()); }
                    break;

                case 5:
                    Console.WriteLine("You found a axe. Do you want to keep it? 1. Yes. 2. No");
                    if (Input(1, 2) == 1) { Backpack.Add(new Axe()); }
                    break;

                default:
                    Console.WriteLine("You found nothing, keep exploring.");
                    break;
            }

        }

        public static IUsable ChooseWeapon()
        {
            var choice = new Item(); // Kan ej skriva null på implicita variabeltyper, därav "new Item()".
            Type type = null;
            bool isWeapon = false;

            do
            {
                PresentItems();
                Console.WriteLine("Choose your weapon. Enter the corresponding number from the list above.");

                choice = Backpack[Input(1, Backpack.Count) - 1];
                type = choice.GetType();

                if (choice is IUsable) { isWeapon = true; } else { Console.WriteLine("You cant hurt anyone with that."); };

            } while (!isWeapon);

            return choice as IUsable;
        }

        public static IEdible ChooseFood()
        {
            var choice = new Item(); // Kan ej skriva null på implicita variabeltyper, därav "new Item()".
            Type type = null;
            bool isFood = false;

            PresentItems();
            Console.WriteLine("Choose your food. Enter the corresponding number from the list above.");

            do
            {
                choice = Backpack[Input(1, Backpack.Count) - 1];
                type = choice.GetType();

                if (choice is IEdible) { isFood = true; } else { Console.WriteLine("Do you want to kill yourself?!?"); };

            } while (!isFood);

            return choice as IEdible;
        }

        public static void Fight()
        {
            Animal animal = ChoosePet();

            while (animal.Health > 0)
            {
                int damage = ChooseWeapon().Damage;
                animal.Health -= damage;

                Console.WriteLine($"You hit the animal with {damage}, the animal has {animal.Health} left.");
            }

            AnimalPen.Remove(animal);
            Console.WriteLine("The animal has died, it will be your slave in Valhalla.");
        }

        public static int Input(int minVal, int maxVal)
        {
            int result = 0;
            bool loop = false;

            do
            {
                bool isInt = int.TryParse(Console.ReadLine().Trim(), out result);

                if (isInt && result >= minVal && result <= maxVal)
                {
                    loop = false;
                }
                else
                {
                    Console.Beep(900, 100);
                    Thread.Sleep(70);
                    Console.Beep(400, 650);

                    loop = true;
                }
            } while (loop);

            return result;
        }

        public static void StartMeny()
        {
            string option1 = "\0";
            string option2 = "2. Explore the world";
            string option3 = "\0";
            string option4 = "\0";
            string option5 = "\0";

            int minVal = 2;
            int maxVal = 2;

            while (1+ 1 == 2)
            {
                if (AnimalPen.Count > 0 && Backpack.Count > 0)
                {
                    option5 = "5. Fight an animal.";
                    maxVal = 5;
                }

                if (AnimalPen.Count > 0)
                {
                    option3 = "3. Check your animals.";
                    option4 = "4. Greet an animal.";

                    minVal = 2;
                    maxVal = 4;
                }

                if (Backpack.Count > 0)
                {
                    option1 = "1. Present Items";

                    minVal = 1;
                    maxVal = 2;
                }

                if (Backpack.Count > 0 && AnimalPen.Count > 0)
                {
                    minVal = 1;
                    maxVal = 5;
                }


                Console.WriteLine("What do you want to do?\n" +
                    $"\n{option1}" +
                    $"\n{option2}" +
                    $"\n{option3}" +
                    $"\n{option4}" +
                    $"\n{option5}");

                switch (Input(minVal, maxVal))
                {
                    case 1:
                        PresentItems();
                        break;
                    case 2:
                        Explore();
                        break;
                    case 3:
                        PresentAnimals();
                        break;
                    case 4:
                        Greet();
                        break;
                    case 5:
                        Fight();
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            StartMeny();
        }
    }
}
