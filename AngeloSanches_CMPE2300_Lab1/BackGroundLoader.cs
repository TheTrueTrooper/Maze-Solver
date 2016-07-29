//*********************************************************************
//Program:     Lab 01 – MazeSolver
//Author:      Angelo Sanches
//class:       A01
//Date:        Jan sometime
//*******************************************************************
using System;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using GDIDrawer;

namespace AngeloSanches_CMPE2300_Lab1
{
    /// <summary>
    /// A class that Loads the image from a background thread might template this need to think about that though
    /// </summary>
    class BackGroundLoader
    {
        /// <summary>
        /// The Drawer to close/ return sized
        /// </summary>
        private CDrawer Display { set; get; }
        /// <summary>
        /// The Path to load from
        /// </summary>
        private string Path { set; get; }
        /// <summary>
        /// The Parent to return(call back to)
        /// </summary>
        private MazeSolver Maze { set; get; }

        /// <summary>
        /// The private constructor that is only to be called indirectly when creating a new thread so this is 
        /// kind of vage ref StartNewThreadedLoader for more information as it is the public indirect caller
        /// </summary>
        /// <param name="nDisplay">the display</param>
        /// <param name="nPath">the image Path</param>
        /// <param name="nMaze">The parent Form might template this for future use</param>
        BackGroundLoader(CDrawer nDisplay, string nPath,  MazeSolver nMaze)
        {
            Display = nDisplay;
            Path = nPath;
            Maze = nMaze;
        }
        /// <summary>
        /// the classes main worker function more or less it loads up the form
        /// </summary>
        void Load()
        {
            try
            {
                // load in a bit map
                Bitmap TempBitMap = (Bitmap)Bitmap.FromFile(Path);
                //---------------------------------------------------------------------------
                // the manditory check remove for any size
                if (TempBitMap.Width > 190 || TempBitMap.Height > 100)
                    throw new Exception("We are sorry but the image is too big. Please keep the images size to 190px by 100px as a larger object could result in a stack overflow. ");
                //----------------------------------------------------------------------------------
                // if the file loads close the old one if it is not Null and create a display box insished to what we need based on the bit map
                Display?.Close();
                Display = new CDrawer(TempBitMap.Width * 10, TempBitMap.Height * 10, false);
                Display.Scale = 10;
                // create a spaces state identifyer as a 2D rep of it
                SpaceStates[,] Spaces = new SpaceStates[TempBitMap.Width, TempBitMap.Height];
                // Set up a End and Start
                Point End = new Point();
                Point Start = new Point();
                // now we can begin a case by case pixle checking/ set them in the box
                for (int x = 0; x < TempBitMap.Width; x++)
                {
                    for (int y = 0; y < TempBitMap.Height; y++)
                    {
                        Display.SetBBScaledPixel(x, y, TempBitMap.GetPixel(x, y));
                        // if it is black it is a wall
                        if (TempBitMap.GetPixel(x, y) == Color.FromArgb(0, 0, 0))
                        {
                            Spaces[x, y] = SpaceStates.closed;
                        }
                        // else its open but that is the default state so checking and writing will just slow us down
                        // so now we check for start(green) and end(red) that fall into the open side and store them acordingly
                        else if (TempBitMap.GetPixel(x, y) == Color.FromArgb(255, 0, 0))// red end
                        {
                            End = new Point(x, y);
                        }
                        else if (TempBitMap.GetPixel(x, y) == Color.FromArgb(0, 255, 0))// green start
                        {
                            Start = new Point(x, y);
                        }
                    }

                }
                if(End.X == 0 && End.Y == 0 && Start.X == 0 && Start.Y ==0)
                {
                    throw new Exception("odd the maze seems to be missing start or end or we some how put them on top of one another"); 
                }
                // only at the end render them
                Display.Render();
                Maze.DoneLoadCallBack.Invoke(Display, Start, End, Spaces);
            }
            //throw out some message boxes for failed attempts
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// an internal function for steping into the objects main perpous needed so we can fit the peram threadstarst
        /// </summary>
        /// <param name="This">This refers to this from an outsiders point of view</param>
        static void StartThread(object This)
        {
            //
            BackGroundLoader LoaderWorker = (BackGroundLoader)This;
            LoaderWorker.Load();
        }

        /// <summary>
        /// Inishizes a new instance of this class form the private constructor and then starts 
        /// a new background thread for it to run in.
        /// </summary>
        /// <param name="nDisplay">The CDrawer display box we use to render</param>
        /// <param name="nPath">The path we will want to load an image from</param>
        /// <param name="nMaze">The parent form</param>
        /// <param name="ObjectReturn">The a out for the parent so you can call back to this object if necassary</param>
        /// <returns>The thread started for if you would like to keep track of it</returns>
        static public Thread StartNewThreadedLoader(CDrawer nDisplay, string nPath, MazeSolver nMaze, out BackGroundLoader ObjectReturn)
        {
            ObjectReturn = new BackGroundLoader(nDisplay, nPath, nMaze);
            Thread CurrentThread = new Thread(new ParameterizedThreadStart(StartThread));
            CurrentThread.IsBackground = true;
            CurrentThread.Start(ObjectReturn);
            return CurrentThread;
        }

    }
}
