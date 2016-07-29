//*********************************************************************
//Program:     Lab 01 – MazeSolver
//Author:      Angelo Sanches
//class:       A01
//Date:        Jan sometime
//*******************************************************************
using System;
using System.Drawing;
using System.Windows.Forms;
using GDIDrawer;
using System.IO;
using System.Threading;

namespace AngeloSanches_CMPE2300_Lab1
{
    /// <summary>
    ///  an enum for us to use in our stuff as space info
    /// </summary>
    public enum SpaceStates
    {
        open,
        closed,
        visited
    }

    /// <summary>
    /// the UI Utilities for us to use
    /// IPORTANT Note : this is a partial Class 
    /// the UI spesific funtions incuding the Button del is in found in the MazeSolverUI.cs
    /// </summary>
    public partial class MazeSolver : Form
    {
        // IPORTANT Note : this is a partial Class 
        // the UI spesific funtions incuding the Button del is in found in the MazeSolverUI.cs

        /// <summary>
        /// the thread we stared dont do any thing with it might depeceate it
        /// </summary>
        Thread CurrentBackGroundThread;

        /// <summary>
        /// s threaded back ground solver lasts near as long as the thread in question
        /// </summary>
        BackGroundMazeSolver BackGroundSolver;
        /// <summary>
        /// s threaded back ground loader lasts near as long as the thread in question
        /// </summary>
        BackGroundLoader backGroundLoader;

        /// <summary>
        /// gona only allow one thread to ever start to run at a time simplifys
        /// </summary>
        volatile bool ThreadFinished;
        /// <summary>e drawer to 
        /// thrender to
        /// </summary>
        volatile private CDrawer Display;
        //the array for the states of pixles
        volatile private SpaceStates[,] Spaces;

        /// <summary>
        /// start point
        /// </summary>
        private Point Start;
        /// <summary>
        /// End point they need to put public:,protected:,private: in like C++ where you say it like once and blanket 
        /// </summary>
        private Point End;

        // call back for the theads
        /// <summary>
        /// a call back for notifying the form of a finished load process and give the drawer back so we can pass it again
        /// </summary>
        /// <param name="back">Return CDrawer Display not for yourr use</param>
        /// <param name="Start">Return Start</param>
        /// <param name="End">Return End</param>
        /// <param name="nSpaces">Return Map of spaces</param>
        public delegate void voidCDrawerPointPointSpaceStatesCallback(CDrawer back, Point Start, Point End, SpaceStates[,] nSpaces);
        /// <summary>
        /// Call back for Soved maze or failed too
        /// </summary>
        /// <param name="Solved">Was it solved</param>
        /// <param name="Steps">Steps it took (irelvant if a fail solve? make it nullable na)</param>
        public delegate void voidBoolStringCallback(bool Solved, int Steps);
        /// <summary>
        /// a call back for notifying the form of a finished load process and give the drawer, start,End, Map  back so we can pass it again
        /// </summary>
        public voidCDrawerPointPointSpaceStatesCallback DoneLoadCallBack { get; }
        /// <summary>
        ///  a call back for notifying the form of a finished solve process and give the drawer back so we can pass it again
        /// </summary>
        public voidBoolStringCallback DoneSolveCallBack { get; }
        
        /// <summary>
        /// The Defualt Constructor
        /// </summary>
        public MazeSolver()
        {
            InitializeComponent();
            ThreadFinished = true;// needs to start true
            // setup the call backs
            DoneLoadCallBack = new voidCDrawerPointPointSpaceStatesCallback(LoadDoneUpdate);
            DoneSolveCallBack = new voidBoolStringCallback(SolveDone);
        }
        /// <summary>
        /// The the Solvers finish callback
        /// </summary>
        /// <param name="Solved">Did you sove it</param>
        /// <param name="Steps">The number of steps it took</param>
        void SolveDone(bool Solved, int Steps)
        {
            // set the background soolver to null so we dont try to change its speed
            BackGroundSolver = null;
            // write out a message or two
            if (Solved)
                Invoke(new Action(() =>  { Lb_StepsReturn.Text = "The maze was solved in " + Steps + " Steps"; }));
            else
                Invoke(new Action(() => { Lb_StepsReturn.Text = "The maze has no solution"; }));
            ThreadFinished = true; // let us create threads again
        }
        /// <summary>
        /// Loads don call back
        /// </summary>
        /// <param name="GiveItBack">CDrawer ro return</param>
        /// <param name="nStart">The point at the start to return</param>
        /// <param name="nEnd">The point at the end to return</param>
        /// <param name="nSpaces">The Map of apaces to return</param>
        void LoadDoneUpdate(CDrawer GiveItBack, Point nStart, Point nEnd, SpaceStates[,] nSpaces)
        {
            // return all the valvues
            Display = GiveItBack;
            Start = new Point(nStart.X, nStart.Y);
            End = new Point(nEnd.X, nEnd.Y);
            Spaces = nSpaces;
            ThreadFinished = true; // free up thread creation
            Invoke(new Action(() => { Bu_Solve.Enabled = true; }));// open the button
        }


    }
}

