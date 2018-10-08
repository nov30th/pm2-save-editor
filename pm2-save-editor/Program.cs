﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;

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

            Application.Run(new Form1());
            Environment.Exit(0);

            PrincessMakerFileBuffer pm2 = new PrincessMakerFileBuffer();

            bool testLoad = pm2.LoadFile("F101.GNX");

            string newName = "Marisa";
            byte[] newNameBytes = ASCIIEncoding.ASCII.GetBytes(newName);
            //pm2.WriteAtOffset(0x74, newNameBytes.Length, newNameBytes);

            //byte[] name = pm2.ReadAtOffset(0x74, 48);

            //StringStatContainer dn = new StringStatContainer(StatInitalizationValues.statInitalizationMap[Stat.DaughtersName], pm2);

            //MessageBox.Show(dn.GetString());

            //dn.SetString("Alice");
            //dn.CommitContents();

            //MessageBox.Show(dn.GetString());

            ////MessageBox.Show(ASCIIEncoding.ASCII.GetString(name));

            //UInt16StatContainer fr = new UInt16StatContainer(StatInitalizationValues.statInitalizationMap[Stat.FightingRep], pm2);

            //var newValue = 677;
            //var result = fr.SetValue(newValue);

            //MessageBox.Show(result.ToString());

            //fr.CommitContents();

            //GNXFloatStatContainer height = new GNXFloatStatContainer(StatInitalizationValues.statInitalizationMap[Stat.Height], pm2);

            //MessageBox.Show(height.GetValue().ToString());

            //var result = height.SetValueFromFloat(100.00);

            //MessageBox.Show(result.ToString());

            //height.CommitContents();

            var height = StatFactory.BuildStat<GNXFloatStatContainer>(Stat.Height, pm2);

            var result = height.SetValue(12345);

            height.CommitContents();

            pm2.SaveFile("F109.GNX");

            //Application.Run(new Form1());



        }
    }
}
