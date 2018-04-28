//Damien Sudol
//Program.cs
//04/11/2018
//v1.0


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create driver object            
            P1Driver driverObj = new P1Driver();

            //Print Driver Welcome Message
            driverObj.welcomeMessage();

            //Simulate creation of new EncryptWord object and guessing game
            driverObj.encryptWord("1    a A b B z Z y Y");

            //Simulate the reset of existing object followed by guessing game
            driverObj.printObjReset("1   A a B b Z z Y y");

            //Simulate reset of existing object with string composed of special characters
            driverObj.printObjReset("1   !@#$%^&*()_+");
            
            //Simulate creating a second EncryptWord object
            driverObj.encryptWord("2   ZZZZZaaaaa");

            //Simulate resetting the secondly created EncryptWord object
            driverObj.printObjReset("2   Testing a test");

            //Simulate creating a third distinct EncryptWord object
            driverObj.encryptWord("3   ladsfja;slfjadsfa !");

            //Simulate resetting a third distinct EncryptWord object
            driverObj.printObjReset("3   kdkdkdkdkdkdkdk");
            
            //Print all created EncryptWord object's statistical state
            //to ensure all objects retain an individual identity
            driverObj.printAllObjCreated();

            Console.ReadLine();

        }
    }
}
