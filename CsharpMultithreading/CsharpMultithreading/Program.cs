using System;
using System.Threading;

/* This program demonstrates basic multithreading in C#, creating and running multiple 
 * threads concurrently. Each thread simulates work by sleeping for a different number 
 * of seconds. The Thread.Sleep method is used to introduce delays, and the console output
 * reflects the activity of each thread.
 */

namespace CsharpMultithreading
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Get the main thread
            Thread mainThread = Thread.CurrentThread;

            // Print information about the main thread
            Console.WriteLine("Main Thread.");

            // Create and start five additional threads
            for (int i = 1; i <= 5; i++)
            {
                int threadNumber = i;

                // Create a new thread using a lambda expression
                Thread newThread = new Thread(() =>
                {
                    // Print a message indicating that the thread is running
                    Console.WriteLine($"Thread {threadNumber} is running.");

                    // Call the Sleep method with the thread number
                    Sleep(threadNumber);
                });

                // Start the new thread
                newThread.Start();
            }

            // Wait for user input before exiting the program
            Console.ReadLine();
        }

        // Method that simulates work by sleeping for a specified number of seconds
        public static void Sleep(int seconds)
        {
            // Sleep for the specified number of seconds
            Thread.Sleep(seconds * 1000);

            // Print a message indicating the number of seconds slept
            Console.WriteLine($"Slept {seconds} seconds");
        }
    }
}