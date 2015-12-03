using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace devurl
{
    class Program
    {
        public static void WriteHalt(string s)

        {

            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(s);

            Console.ForegroundColor = oldColor;

        }

        public static void WriteNormal(string s)

        {

            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(s);

            Console.ForegroundColor = oldColor;

        }


        public static void WriteScan(string s)

        {

            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(s);

            Console.ForegroundColor = oldColor;

        }

        public static void WriteIntro(string s)

        {

            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine(s);

            Console.ForegroundColor = oldColor;
      
        }

        static void Main(string[] args)

        {
            Console.Title = "devurl 0.1a";

            {

                Fiddler.FiddlerApplication.BeforeRequest += delegate (Fiddler.Session oS) {

                    //WriteScan(" Scanning");

                    oS.bBufferResponse = true;

                };

                Fiddler.FiddlerApplication.BeforeRequest += delegate (Fiddler.Session oS)

                {
                    if (oS.uriContains("vizplay.org"))
                    {
                        Thread.Sleep(500);
                        Console.WriteLine("");
                        WriteScan("[debug]: Found linkage...");
                        if (oS.uriContains(".mp4"))
                        {
                            Thread.Sleep(500);
                            WriteScan("[debug]: Found mp4 location...");
                            if (oS.uriContains("st="))
                            {
                                Thread.Sleep(500);
                                WriteScan("[debug]: Found secret key...");
                                if (oS.uriContains("&hash="))
                                {
                                    Thread.Sleep(500);
                                    Console.Beep();
                                    string path = @"c:\url.txt";
                                    Console.WriteLine();
                                    WriteScan("possible url: " + oS.fullUrl);
                                    Console.WriteLine();
                                    string file = oS.fullUrl;
                                    File.AppendAllText(path, file);
                                    File.AppendAllText(path, Environment.NewLine);
                                }
                            }
                   
                        }
                    }

                };
                }


            Fiddler.CONFIG.IgnoreServerCertErrors = true;

            Fiddler.FiddlerApplication.Startup(49211, true, true);


            Object forever = new Object();

            lock (forever)

            {

                System.Threading.Monitor.Wait(forever);

            }

        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)

        {

            Console.Clear();

            WriteHalt(" Shutting Down..");

            Fiddler.FiddlerApplication.Shutdown();

            System.Threading.Thread.Sleep(750);

        }

    }

}