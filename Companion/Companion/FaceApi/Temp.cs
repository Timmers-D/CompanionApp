using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Companion.FaceApi
{
    // setup to save and recognise people, couldn't be finished due to azure subscription issues
    class Temp
    {
        private static readonly string _groupId = "myfriends";
        private static readonly string _groupName = "My Friends";

        static void Main(string[] args)
        {
            Registration();
        }

        private static void Registration()
        {
            Console.WriteLine("Initialising webcam");
            Instances.GetCapture();
            Console.WriteLine("Taking pictures, move around a little in between pictures please");
            var captures = new List<Bitmap>();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Taking picture " + i + " in 1 second");
                Thread.Sleep(1000);
                captures.Add(Instances.GetCapture());    
            }
            Console.WriteLine("Pictures taken");

            Console.WriteLine("Getting group");
            try
            {
                PersonRecognition.GetGroup(_groupId).Wait();
            }
            catch (Exception e)
            {
                if (e.HResult == -2146233088)
                {
                    Console.WriteLine("Group does not exist yet, making it");
                    PersonRecognition.CreateGroup(_groupId, _groupName).Wait();
                }
                else
                {
                    Console.WriteLine("Something went wrong:");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    return;
                }

            }

            string name;
            while (true)
            {
                Console.WriteLine("Please insert your name and press enter");
                name = Console.ReadLine();

                var exists = CheckExists(name);

                if (exists)
                {
                    Console.WriteLine("A person with this name already exists, do you want to add these pictures to the existing person?");
                    Console.WriteLine("Type Y or N and press enter");
                    var input = Console.ReadLine();
                    if (input == "Y")
                    {
                        Console.WriteLine("Adding the new images to your data!");
                        break;
                    }
                    else if (input == "N")
                    {
                        Console.WriteLine("Please try another name");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
                else
                {
                    Console.WriteLine("Name is free, adding you to the group!");
                    break;
                }
            }

            var newPerson = PersonRecognition.AddPerson(_groupId, name).Result;

            //C:\Users\dtimm\Desktop\Capture.jpg
            PersonRecognition.AddFaces(_groupId, newPerson, @"C:").Wait();
//            PersonRecognition.AddFaces(_groupId, newPerson, captures).Wait();

            Console.WriteLine("Added you to the group! Retraining the group now...");
            PersonRecognition.TrainGroup(_groupId).Wait();
            Console.WriteLine("Done training, executing a test to make sure it all went right");
            var people = PersonRecognition.Identify(_groupId, captures[0]).Result;
            if (people.Count != 0 && people[0].Name == name)
            {
                Console.WriteLine("All set!");
            }
            else
            {
                Console.WriteLine("Something went wrong.");
            }
        }

        // check if a person with this name already exists in the group of known people
        private static bool CheckExists(string name)
        {
            Console.WriteLine("Checking if this name is already in use...");
            bool exists = false;
            foreach (var person in PersonRecognition.GetAllPeople(_groupId).Result)
            {
                if (person.Name == name)
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }
    }
}
