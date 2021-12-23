using System;

namespace Garage.UIConsole
{
    class Program
    {
        /// <summary>
        /// The main method, vill handle the menues for the program
        /// </summary>
        /// <param name="args"></param>
        static void Main()
        {
            do
            {
                // Huvudmeny
                ShowMainMenu();
                GetUserInput();
            } while (true);
        }
        private static void ShowMainMenu()
        {
            UI.Clear();
            UI.AddMessage("Vänligen navigera genom menyn genom att ange numret \n(1, 2, 3 ,4, 5, 0) som du väljer med hjälp av numeriska tangenter"
                    + "\n1.Instansieringen av ett nytt garage"
                    + "\n2.Instansieringen av ett nytt garage med ett antal fordon"
                    + "\n3.Lista samtliga parkerade fordon"
                    + "\n4.Lista fordonstyper och hur många av varje som står i garaget"
                    + "\n5.Lägga till fordon garaget"
                    + "\n6.Ta bort fordon ur garaget"
                    + "\n7.Hitta ett specifikt fordon"
                    + "\n8.Filtrera fordon"
                    + "\n0.Stänga av applikationen");
        }

        private static void GetUserInput()
        {
            var keyPressed = UI.GetKey();//Creates the Key input to be used with the switch-case below.

            var actionMeny = new Dictionary<ConsoleKey, Action>()
                {
                    {ConsoleKey.NumPad1,CreateGarage },
                    {ConsoleKey.NumPad2,CreateGarageWithVehicles },
                    {ConsoleKey.NumPad3,ListOfVehicles },
                    {ConsoleKey.NumPad4,ListOfVehicleTypes},
                    {ConsoleKey.NumPad5,AddVehicle },
                    {ConsoleKey.NumPad6,RemoveVehicle},
                    {ConsoleKey.NumPad7,FindVehicle},
                    {ConsoleKey.NumPad8,FilterVehicles },
                    {ConsoleKey.NumPad0,ClosePrograme },
                };

            if (actionMeny.ContainsKey(keyPressed))
                actionMeny[keyPressed]?.Invoke();

        }


        static void CreateGarage()
        {
            UI.Clear();
            UI.AddMessage("Instansieringen av ett nytt garage");
            throw new NotImplementedException();
        }

        static void CreateGarageWithVehicles()
        {
            UI.Clear();
            UI.AddMessage("Instansieringen av ett nytt garage med ett antal fordon");
            throw new NotImplementedException();
        }

        static void ListOfVehicles()
        {
            UI.Clear();
            UI.AddMessage("Lista samtliga parkerade fordon");
            throw new NotImplementedException();
        }

        static void ListOfVehicleTypes()
        {
            UI.Clear();
            UI.AddMessage("Lista fordonstyper och hur många av varje som står i garaget");
            throw new NotImplementedException();
        }

        static void AddVehicle()
        {
            UI.Clear();
            UI.AddMessage("Lägga till fordon garaget");
            throw new NotImplementedException();
        }

        static void RemoveVehicle()
        {
            UI.Clear();
            UI.AddMessage("Ta bort fordon ur garaget");
            throw new NotImplementedException();
        }

        static void FindVehicle()
        {
            UI.Clear();
            UI.AddMessage("Hitta ett specifikt fordon");
            throw new NotImplementedException();
        }

        static void FilterVehicles()
        {
            UI.Clear();
            UI.AddMessage("Filtrera fordon");
            throw new NotImplementedException();
        }

        static void ClosePrograme()
        {
            UI.Clear();
            UI.AddMessage("Stängs");
            Environment.Exit(0);
        }
    }
}

