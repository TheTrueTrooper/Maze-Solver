//*********************************************************************
//Program:     Lab 01 – MazeSolver
//Author:      Angelo Sanches
//class:       A01
//Date:        Jan sometime
//*******************************************************************
using System;
using System.Windows.Forms;
using System.IO;
using GDIDrawer;
using System.Threading;

namespace AngeloSanches_CMPE2300_Lab1
{
    /// <summary>
    /// the Basic UI for us to use
    /// IPORTANT Note : this is a partial Class 
    /// the Utility funtions incuding the constructor is in found in the MazeSolverUtil.cs
    /// </summary>
    public partial class MazeSolver
    {

        // IPORTANT Note : this is a partial Class 
        // the Utility funtions incuding the constructor is in found in the MazeSolverUtil.cs

        /// <summary>
        /// Event for load button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bu_Load_Click(object sender, EventArgs e)
        {
            Text = "Load Bitmap to solve";
            // create insh a Dilog for opening a file and set it to what we need
            OpenFileDialog FileFinder = new OpenFileDialog();
            FileFinder.InitialDirectory = Path.GetFullPath(Environment.CurrentDirectory + @"..\..\..\");
            FileFinder.Multiselect = false;
            FileFinder.Filter = "Bitmap Files|*.bmp|All Files|*.*";
            if (FileFinder.ShowDialog() == DialogResult.OK && ThreadFinished)
            {
                ThreadFinished = false;// prevent an other thread from sarting
                Bu_Solve.Enabled = false; // alow solve events
                // Create a object of BackGroundLoader withits own thread and give the thread to keep track
                // go to class BackGroundLoader for more on
                CurrentBackGroundThread = BackGroundLoader.StartNewThreadedLoader(Display, FileFinder.FileName, this, out backGroundLoader);
            }
        }
        /// <summary>
        /// Event for Solve button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bu_Solve_Click(object sender, EventArgs e)
        {
            if (ThreadFinished)// if we dont have a thread going
            {
                ThreadFinished = false;// prevent an other thread from sarting
                Bu_Solve.Enabled = false;// disable it for next time
                // Create a object of BackGroundMazeSolver withits own thread and give the thread to keep track
                // go to class BackGroundMazeSolver for more on
                CurrentBackGroundThread = BackGroundMazeSolver.StartNewThreadedSolver(Start, End, Display, Spaces, this, (int)NumUD_Speed.Value, out BackGroundSolver);
            }
        }


        /// <summary>
        /// Number Up down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumUD_Speed_ValueChanged(object sender, EventArgs e)
        {
            BackGroundSolver?.SpeedUpdateCallBack.Invoke((int)NumUD_Speed.Value);// is it null no then update the dam thing
        }
    }
}
