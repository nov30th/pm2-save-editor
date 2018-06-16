using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pm2_save_editor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PrincessMaker pm = new PrincessMaker();

            bool testLoad = pm.LoadFile("F101.GNX");

            //Application.Run(new Form1());

        }
    }
}
