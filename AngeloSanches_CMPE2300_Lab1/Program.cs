//*********************************************************************
//Program:     Lab 01 – MazeSolver
//Author:      Angelo Sanches (soda)
//class:       A01
//Date:        Jan sometime
//*******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AngeloSanches_CMPE2300_Lab1
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
            Application.Run(new MazeSolver());
        }
    }
}
