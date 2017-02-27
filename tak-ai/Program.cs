using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TakAI.Services;

namespace TakAI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main()
        {
         
            //TODO validate program input
            Console.WriteLine("(i)nteractive or (f)ile game?");
            string gameType = Console.ReadLine();

            //setting this up to read a file
            switch (gameType)
            {
                case "i":
                    break;
                case "f":
                    TurnService.GetTurnFile();
                    break;
            }
            Console.ReadLine();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }

}
