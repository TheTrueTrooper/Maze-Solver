//*********************************************************************
//Program:     Lab 01 – MazeSolver
//Author:      Angelo Sanches
//class:       A01
//Date:        Jan sometime
//*******************************************************************
using System.Threading;
using System.Drawing;
using GDIDrawer;

namespace AngeloSanches_CMPE2300_Lab1
{/// <summary>
/// this class is for background updates between threads in fact it may have been even better if i encaplateed the functions in here for it
/// </summary>
    class BackGroundMazeSolver
    {
        //readonlys
       /// <summary>
       /// Start point as x,y
       /// </summary>
        Point Start { get; }
        /// <summary>
        /// end point as x,y
        /// </summary>
        Point End { get; }
        /// <summary>
        /// Display to draw results to in real time
        /// </summary>
        CDrawer Display { get; }
        /// <summary>
        /// The Map of spaces set out in a 2d array
        /// open - can go there
        /// closed - can't go there
        /// visited - don't go there been there done that(but open thero)
        /// </summary>
        SpaceStates[,] Spaces { get; }
        /// <summary>
        /// parent form to call back to
        /// </summary>
        MazeSolver Parent { get; }
        //regualer vars
        /// <summary>
        /// Count of the steps tried so far
        /// </summary>
        int Count;
        //corss thread stuff
        /// <summary>
        /// A speed container higher = nore wait so less speed some what unintuitive
        /// </summary>
        volatile private int _Speed;
        /// <summary>
        /// an indirect accessor For speed A speed container higher = nore wait so less speed some what unintuitive
        /// </summary>
        public int speed { get { return _Speed;  } }
        //callback stuff
        /// <summary>
        /// baseDel used in : SpeedUpdateCallBack,
        /// </summary>
        /// <param name="input">depends with me</param>
        public delegate void VoidIntUpdate(int input);
        /// <summary>
        /// a delegate for setting the speed updated takes an int
        /// </summary>
        public VoidIntUpdate SpeedUpdateCallBack;
        /// <summary>
        /// the function for the call back to adress to
        /// </summary>
        /// <param name="Send">speed to -Note: higher is slower</param>
        void SpeedUpdate(int Send)
        {
            _Speed = Send;
        }

        /// <summary>
        /// Constructor This is called indirectly by StartNewThreadedSolver 
        /// Read thet for the params as there are a lot of them and it is a mimic of this
        /// </summary>
        /// <param name="nStart">Set Start point</param>
        /// <param name="nEnd">Set End point</param>
        /// <param name="nDisplay">Set Display to update</param>
        /// <param name="nSpaces">Set Maze Map to use</param>
        /// <param name="nParent">Set Parent to update backto</param>
        /// <param name="InishSpeed">Set insihal speed to Sinc with parent at start</param>
        public BackGroundMazeSolver(Point nStart, Point nEnd, CDrawer nDisplay, SpaceStates[,] nSpaces, MazeSolver nParent,int InishSpeed)
        {
            _Speed = InishSpeed;
            SpeedUpdateCallBack = new VoidIntUpdate(SpeedUpdate);
            Start = nStart;
            End = nEnd;
            Display = nDisplay;
            Spaces = nSpaces;
            Parent = nParent;
            Count = 0;
        }

        /// <summary>
        /// a single step in a solve
        /// </summary>
        /// <param name="Vector">the point to step to or start from them</param>
        /// <returns>if the end was found or not</returns>
        bool SolveStep(Point Vector)
        {
            // if are looking  to slow it down access a back ground class that only exists for updating with a del
            if (speed != 0)
                Thread.Sleep(speed);

            if (Spaces[Vector.X, Vector.Y] == SpaceStates.open)
            {
                // we found the end we are done
                if (Vector == End)
                    return true;
                // draw the path we are on and mark were we have been
                Display.SetBBScaledPixel(Vector.X, Vector.Y, Color.Violet);
                Display.Render();
                Spaces[Vector.X, Vector.Y] = SpaceStates.visited;
                // we count the steps
                Count++;
                // check all the directions for victory recusively
                // (Order important) if we are not out of bounds (first to let us fall out before the access) & the next ones returns true we are done
                if (((Vector.X + 1) < Spaces.GetLength(0)       && SolveStep(new Point(Vector.X + 1, Vector.Y))) ||
                    ((Vector.X - 1) >= 0                        && SolveStep(new Point(Vector.X - 1, Vector.Y))) ||
                    ((Vector.Y + 1) < Spaces.GetLength(1)       && SolveStep(new Point(Vector.X, Vector.Y + 1))) ||
                    ((Vector.Y - 1) >= 0                        && SolveStep(new Point(Vector.X, Vector.Y - 1))))
                    return true;
                else // if it was not the path display as such and remove counts we'll render on next valid step or on final exit 
                {
                    //reflect the paths failed steps to the count and drawer
                    Count--;
                    Display.SetBBScaledPixel(Vector.X, Vector.Y, Color.Green);
                    Display.Render(); //<-- tried it letting it fall out to the next render and it gave a lag feel to it dont do it
                }

            }
            return false; // any time we dont find it or hit wall just return as false
        }

        /// <summary>
        /// just a step into that step also calls the notify done
        /// </summary> 
        void StartSolve()
        {
            // step into the function and use it to return if the maze was solve to the main form
            Parent.DoneSolveCallBack.Invoke(SolveStep(Start), Count);
        }

        /// <summary>
        /// an internal function for steping into the objects main perpous needed so we can fit the peram threadstarst
        /// </summary>
        /// <param name="This">This refers to this from an outsiders point of view</param>
        static void StartThread(object This)
        {
            BackGroundMazeSolver SolverWorker = (BackGroundMazeSolver)This;
            SolverWorker.StartSolve();
        }

        /// <summary>
        /// Ish this class and gives it its own thread
        /// call in place of constructor
        /// you know think this is one of the first that ive not had a public constructor
        /// </summary>
        /// <param name="nStart">Set readonly point</param>
        /// <param name="nEnd">Set readonly End point</param>
        /// <param name="nDisplay">Set readonly Display to update</param>
        /// <param name="nSpaces">Set insh Maze Map to use</param>
        /// <param name="nParent">Set readonly Parent to update backto</param>
        /// <param name="InishSpeed">Set insh speed to Sinc with parent at start</param>
        /// <param name="ObjectReturn">This class we created</param>
        /// <returns>The thread created by this if youd like to keep track of</returns>
        static public Thread StartNewThreadedSolver(Point Start, Point End, CDrawer Display, SpaceStates[,] Spaces, MazeSolver Parent, int inishSpeed, out BackGroundMazeSolver ObjectReturn)
        {
            ObjectReturn = new BackGroundMazeSolver(Start, End, Display, Spaces, Parent, inishSpeed);
            Thread CurrentThread = new Thread(new ParameterizedThreadStart(StartThread), 600000);
            CurrentThread.IsBackground = true;
            CurrentThread.Start(ObjectReturn);
            return CurrentThread;
        }
    }
}
