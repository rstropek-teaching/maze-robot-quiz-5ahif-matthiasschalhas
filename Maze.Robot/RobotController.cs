using Maze.Library;
using System.Collections.Generic;
using System.Drawing;

namespace Maze.Solver
{
    /// <summary>
    /// Moves a robot from its current position towards the exit of the maze
    /// </summary>
    public class RobotController
    {
        private IRobot robot;
        private bool reachedEnd;
        private List<Point> listofPoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="RobotController"/> class
        /// </summary>
        /// <param name="robot">Robot that is controlled</param>
        public RobotController(IRobot robot)
        {
            // Store robot for later use
            this.robot = robot;
            this.reachedEnd = false;
            this.listofPoints = new List<Point>();
        }

        /// <summary>
        /// Moves the robot to the exit
        /// </summary>
        /// <remarks>
        /// This function uses methods of the robot that was passed into this class'
        /// constructor. It has to move the robot until the robot's event
        /// <see cref="IRobot.ReachedExit"/> is fired. If the algorithm finds out that
        /// the exit is not reachable, it has to call <see cref="IRobot.HaltAndCatchFire"/>
        /// and exit.
        /// </remarks>
        public void MoveRobotToExit()
        {
            // Here you have to add your code
            robot.ReachedExit += (_, __) => reachedEnd = true;
            // Trivial sample algorithm that can just move right
            directionCheck(0,0);

            if(reachedEnd == false)
            {
                robot.HaltAndCatchFire();
            }
            
            // Tip: Avoid multiple empty lines. This is generally considered bad
            // coding style.

           
        }

        // Tip: In C#, member names should start with an uppercase letter.
        public void directionCheck(int x, int y)
        {
            // Prüft, ob der Punkt noch nicht vorgekommen ist und ob das Ende noch nicht erreicht wurde
            if(this.listofPoints.Contains(new Point(x,y))== false && this.reachedEnd == false)
            {
                //Aufnahme der Punktes in die Liste
                this.listofPoints.Add(new Point(x,y));
                
                //Prüft, ob das Ende noch nicht erreicht wird und ob es möglich ist nach rechts zu fahren
                if (this.reachedEnd == false && this.robot.TryMove(Direction.Right)==true)
                {
                    directionCheck(x+1,y);
                    if(this.reachedEnd == false)
                    {
                        robot.Move(Direction.Left);
                    }
                }

                //Prüft, ob das Ende noch nicht erreicht wird und ob es möglich ist nach unten zu fahren
                if (this.reachedEnd == false && this.robot.TryMove(Direction.Down) == true)
                {
                    directionCheck(x,y+1);
                    if (this.reachedEnd == false)
                    {
                        robot.Move(Direction.Up);
                    }
                }

                //Prüft, ob das Ende noch nicht erreicht wird und ob es möglich ist nach links zu fahren
                if (this.reachedEnd == false && this.robot.TryMove(Direction.Left) == true)
                {
                    directionCheck(x-1,y);
                    if (this.reachedEnd == false)
                    {
                        robot.Move(Direction.Right);
                    }
                }

                //Prüft, ob das Ende noch nicht erreicht wird und ob es möglich ist nach oben zu fahren
                if (this.reachedEnd == false && this.robot.TryMove(Direction.Up) == true)
                {
                    directionCheck(x,y-1);
                    if (this.reachedEnd == false)
                    {
                        robot.Move(Direction.Down);
                    }
                }


            }
        }
    }
}
