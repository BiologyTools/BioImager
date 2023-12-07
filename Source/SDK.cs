using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AForge;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BioImager
{
    /* SDK is C# class wrapper for PriorScientificSDK DLL */
    public class SDK
    {
        [DllImport("PriorScientificSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PriorScientificSDK_Version(StringBuilder version);

        [DllImport("PriorScientificSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PriorScientificSDK_Initialise();

        [DllImport("PriorScientificSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PriorScientificSDK_OpenNewSession();

        [DllImport("PriorScientificSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PriorScientificSDK_CloseSession(int sessionID);
        
        [DllImport("PriorScientificSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int PriorScientificSDK_cmd(int session, StringBuilder tx, StringBuilder rx);
        int err;
        int sessionID = -1;


        string userRx = "";
        StringBuilder dllVersion = new StringBuilder();

        /* create a c# wrapper class for the Prior DLL */
        SDK priorSDK = new SDK();
        public SDK()
        {
             
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            /* get the version number of the dll */
            if ((err = priorSDK.GetVersion(dllVersion)) != Prior.PRIOR_OK)
            {
                MessageBox.Show("Error getting Prior SDK version (" + err.ToString() + ")");
                return;
            }
            /* SDK must be initialised before any real use
             */
            if ((err = priorSDK.Initialise()) != Prior.PRIOR_OK)
            {
                MessageBox.Show("Error initialising Prior SDK (" + err.ToString() + ")");
                return;
            }

            /* create a session in the DLL, this gives us one controller and currently an ODS and SL160 robot loader. 
             * Multiple connections allow control of multiple stage/loaders but is outside the brief for this demo
             */
            if ((sessionID = priorSDK.OpenSession()) < 0)
            {
                MessageBox.Show("Error (" + sessionID.ToString() + ") Creating session in SDK " + dllVersion);
                return;
            }

            //specify path name or PriorSDK.log is written to working directory
            //priorSDK.Cmd(sessionID, "dll.log.on",  ref userRx);


            /* my controller identifies on COM1, yours will probably be different.
             */
            int port = 1;
            int open = 0;

            /* try to connect to the ps3 */
            open = priorSDK.Cmd(sessionID, "controller.connect " + port.ToString(), ref userRx, false);


            if (open != Prior.PRIOR_OK)
            {
                MessageBox.Show("Error (" + open.ToString() + ")  connecting to stage controller ", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            systemCheck();

            /* set orientation of stage +x+y = stage left and stage forward 
             * this gives us a co-ordinate/movement system that when viewed through objectives gives positions
             * as you would see on graph paper. Just a personal preference, you can set host direction as you wish
             * default is 1 1. +ve incrementing positions moves stage physically left and forwards 
             */
            err = priorSDK.Cmd(sessionID, "controller.stage.hostdirection.set -1 1", ref userRx);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            /* disconnect the controller and close the session down */
            err = priorSDK.Cmd(sessionID, "controller.disconnect", ref userRx);
            err = priorSDK.CloseSession(sessionID);
        }

        private void systemCheck()
        {
            /* im just doing the calls here as example but its a good idea to check the devices fitted */

            err = priorSDK.Cmd(sessionID, "controller.stage.name.get", ref userRx);

            err = priorSDK.Cmd(sessionID, "controller.z.name.get", ref userRx);
        }

        public int InitialiseStage()
        {
            /* do stage initialisation, goto back right limits (could be anywhere you wish it to be in reality )
             * default positional units are in steps of 1micron
            */

            if ((err = priorSDK.Cmd(sessionID, "controller.stage.move-at-velocity " +
                                            (-10000).ToString() + " " + (-10000).ToString(), ref userRx)) != Prior.PRIOR_OK)
            {
                return 1;
            }

            waitUntilStageIdle();

            /* set temp zero pos */
            if ((err = priorSDK.Cmd(sessionID, "controller.stage.position.set 0 0", ref userRx)) != Prior.PRIOR_OK)
            {
                return 1;
            }
            waitUntilStageIdle();
            /* move off slightly */
            if ((err = priorSDK.Cmd(sessionID, "controller.stage.goto-position 1000 1000", ref userRx)) != Prior.PRIOR_OK)
            {
                return 1;
            }

            return 0;
        }
        public bool SetPosition(PointD p)
        {
            if ((err = priorSDK.Cmd(sessionID, "controller.stage.position.set "+ p.X + " " + p.Y, ref userRx)) != Prior.PRIOR_OK)
            {
                return false;
            }
            waitUntilStageIdle();
            return true;
        }
        public PointD GetPosition()
        {
            if ((err = priorSDK.Cmd(sessionID, "controller.stage.position.get", ref userRx)) != Prior.PRIOR_OK)
            {
                return new PointD(0, 0);
            }
            string[] sts = userRx.Split(' ');
            double dx = double.Parse(sts[0]);
            double yx = double.Parse(sts[1]);
            return new PointD(dx,yx);
        }
        public bool SetPosition(Point3D p)
        {
            if ((err = priorSDK.Cmd(sessionID, "controller.stage.position.set " + p.X + " " + p.Y, ref userRx)) != Prior.PRIOR_OK)
            {
                return false;
            }
            waitUntilStageIdle();
            if ((err = priorSDK.Cmd(sessionID, "controller.z.position.set " + p.Z, ref userRx)) != Prior.PRIOR_OK)
            {
                throw new Exception(err.ToString());
            }
            waitUntilStageIdle();
            return true;
        }
        public bool SetObjective(int p)
        {
            waitUntilNosePieceIdle();
            if ((err = priorSDK.Cmd(sessionID, "controller.nosepiece.goto-position " + p, ref userRx)) != Prior.PRIOR_OK)
            {
                return false;
            }
            waitUntilNosePieceIdle();
            return true;
        }
        public Point3D GetPosition3D()
        {
            if ((err = priorSDK.Cmd(sessionID, "controller.stage.position.get", ref userRx)) != Prior.PRIOR_OK)
            {
                throw new Exception(err.ToString());
            }
            string[] sts = userRx.Split(' ');
            double dx = double.Parse(sts[0]);
            double yx = double.Parse(sts[1]);
            if ((err = priorSDK.Cmd(sessionID, "controller.z.position.get", ref userRx)) != Prior.PRIOR_OK)
            {
                throw new Exception(err.ToString());
            }
            double zx = double.Parse(userRx);
            return new Point3D(dx, yx, zx);
        }
        public bool SetZ(double d)
        {
            waitUntilZIdle();
            if ((err = priorSDK.Cmd(sessionID, "controller.z.position.set " + d, ref userRx)) != Prior.PRIOR_OK)
            {
                throw new Exception(err.ToString());
            }
            waitUntilZIdle();
            return true;
        }
        public double GetZ()
        {
            if ((err = priorSDK.Cmd(sessionID, "controller.z.position.get", ref userRx)) != Prior.PRIOR_OK)
            {
                throw new Exception(err.ToString());
            }
            return double.Parse(userRx);
        }
        public bool SetNosePiece(int d)
        {
            waitUntilNosePieceIdle();
            if ((err = priorSDK.Cmd(sessionID, "controller.nosepiece.position.set " + d, ref userRx)) != Prior.PRIOR_OK)
            {
                throw new Exception(err.ToString());
            }
            waitUntilNosePieceIdle();
            return true;
        }
        public int GetNosePiece()
        {
            if ((err = priorSDK.Cmd(sessionID, "controller.nosepiece.position.get", ref userRx)) != Prior.PRIOR_OK)
            {
                throw new Exception(err.ToString());
            }
            return int.Parse(userRx);
        }
        public int GetNosePiecePositions()
        {
            if ((err = priorSDK.Cmd(sessionID, "controller.nosepiece.no-of-positions.get", ref userRx)) != Prior.PRIOR_OK)
            {
                throw new Exception(err.ToString());
            }
            return int.Parse(userRx);
        }
        public int stageBusy()
        {
            if ((err = priorSDK.Cmd(sessionID, "controller.stage.busy.get", ref userRx)) != Prior.PRIOR_OK)
            {
                return 0;
            }
            else
                return Convert.ToInt32(userRx);
        }

        public int zBusy()
        {
            if ((err = priorSDK.Cmd(sessionID, "controller.z.busy.get", ref userRx)) != Prior.PRIOR_OK)
            {
                return 0;
            }
            else
                return Convert.ToInt32(userRx);
        }
        public int nosePieceBusy()
        {
            if ((err = priorSDK.Cmd(sessionID, "controller.nosepiece.busy.get", ref userRx)) != Prior.PRIOR_OK)
            {
                return 0;
            }
            else
                return Convert.ToInt32(userRx);
        }
        public bool shutterOpen()
        {
            if ((err = priorSDK.Cmd(sessionID, "controller.shutter.open", ref userRx)) != Prior.PRIOR_OK)
            {
                return false;
            }
            else
                return true;
        }
        public bool shutterClose()
        {
            if ((err = priorSDK.Cmd(sessionID, "controller.shutter.close", ref userRx)) != Prior.PRIOR_OK)
            {
                return false;
            }
            else
                return true;
        }
        public int shutterPosition()
        {
            if ((err = priorSDK.Cmd(sessionID, "controller.shutter.state.get", ref userRx)) != Prior.PRIOR_OK)
            {
                return 0;
            }
            else
                return int.Parse(userRx);
        }
        public void waitUntilStageIdle()
        {
            do
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
            while (stageBusy() != 0);
        }
        public void waitUntilZIdle()
        {
            do
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
            while (zBusy() != 0);
        }
        public void waitUntilNosePieceIdle()
        {
            do
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
            while (nosePieceBusy() != 0);
        }

        public int GetVersion(StringBuilder version)
        {
            return PriorScientificSDK_Version(version);
        }

        public int Initialise()
        {
            return PriorScientificSDK_Initialise();
        }

        public int OpenSession()
        {
            return PriorScientificSDK_OpenNewSession();
        }

        public int CloseSession(int sessionID)
        {
            return PriorScientificSDK_CloseSession(sessionID);
        }

        public int Cmd(int session, string usertx, ref string userrx, bool displayError = true)
        {
            int ret;

            StringBuilder tx = new StringBuilder();
            StringBuilder rx = new StringBuilder();

            tx.Append(usertx);
            ret = PriorScientificSDK_cmd(session,tx,rx);

            if (ret == Prior.PRIOR_OK)
            {
                userrx = rx.ToString();
            }
            else
            {
                if (displayError)
                {
                    MessageBox.Show("Sent " + usertx + "\rSDK error: " + ret.ToString() );
                }
            }

            return ret;
        }
    }
}
