using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Classic_Snakes_Game
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();

        int maxwidth;
        int maxheight;
        int score;
        int highscore;
        Random rand = new Random();
        bool goLeft, goRight, goDown, goUp;



        public Form1()
        {
            InitializeComponent();
            new Settings();

            // Initialize event handlers
            this.KeyDown += new KeyEventHandler(KeyIsDown);
            this.KeyUp += new KeyEventHandler(KeyIsUp);

            // Initialize timer
            GameTimer.Interval = 100; // Set the interval to 100 milliseconds
            GameTimer.Tick += new EventHandler(GameTimerEvent);
           *******************************************************************************GameTimer.Start();
        }
      
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && Settings.direction != "Right")
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.Right && Settings.direction != "Left")
            {
                goRight = true;
            }

            if (e.KeyCode == Keys.Up && Settings.direction != "Down")
            {
                goUp = true;
            }

            if (e.KeyCode == Keys.Down && Settings.direction != "Up")
            {
                goDown = true;
            }
        }
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

        private void StartGame(object sender, EventArgs e)
        {

        }

        private void TakeSnap(object sender, EventArgs e)
        {

        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            //setting the initial direction for the snake
            if (goLeft)
             Settings.direction = "left"; 
            if (goRight)
             Settings.direction = "right"; 
            if (goUp)
             Settings.direction = "up"; 
            if (goDown)
             Settings.direction = "down"; 
            //end of direction 

            //move the snake
            for (int i = Snake.Count - 1 ; i >= 0; i--)
            {
                if (i == 0)
                {
                    //move the head
                    switch (Settings.direction)

                    {
                        case "left":
                            Snake[i].X--;
                            break;
                        case "right":
                            Snake[i].X++;
                            break;
                        case "up":
                            Snake[i].Y--;
                            break;
                        case "down":
                            Snake[i].Y++;
                            break;
                    }

                    //wrap around the edges
                    if (Snake[i].X < 0)
                     Snake[i].X = maxwidth; 

                    if (Snake[i].X > maxwidth)
                     Snake[i].X = 0; 

                    if (Snake[i].Y < 0)
                     Snake[i].Y = maxheight; 

                    if (Snake[i].Y > maxheight)
                     Snake[i].Y = 0; 

                }
                else
                {
                    //move the body
                    Snake[i].X= Snake[i-1].X;
                    Snake[i].Y = Snake[i-1].Y;
                }
            }

            picCanvas.Invalidate();

        }
            

        private void UpdatePictureboxGraphics(object sender, PaintEventArgs e)
        {
                    Graphics canvas = e.Graphics;

                    Brush SnakeColor;

                    for (int i = 0; i < Snake.Count; i++)
                    {
                        if (i == 0)
                        {
                            SnakeColor = Brushes.Black;

                        }
                        else
                        { SnakeColor = Brushes.Fuchsia; }

                        canvas.FillEllipse(SnakeColor, new Rectangle
                            (
                            Snake[i].X * Settings.width,
                            Snake[i].Y * Settings.height,
                            Settings.width, Settings.height
                        ));
                    }

                    canvas.FillEllipse(Brushes.DarkRed, new Rectangle
                             (
                             food.X * Settings.width,
                             food.Y * Settings.height,
                             Settings.width, Settings.height
                         ));


        }

        private void RestartGame()
                {
                    maxwidth = picCanvas.Width / Settings.width - 1;
                    maxheight = picCanvas.Height / Settings.height - 1;

                    Snake.Clear();

                    startbtn.Enabled = false;
                    snapbtn.Enabled = false;
                    score = 0;
                    txtscore.Text = "score: " + score;

                    Circle head = new Circle { X = 10, Y = 5 };
                    Snake.Add(head);// ADDS THE HEAD PART OF THE SNAKE TO THE LIST

                    for (int i = 0; i < 10;)
                    {
                        Circle body = new Circle();
                        Snake.Add(body);
                    }

                    food = new Circle { X = rand.Next(2, maxwidth), Y = rand.Next(2, maxheight) };

                    GameTimer.Start();
        }

                private void Eatfood()
                {

                }

                private void GameOver()
                {

                }


            }
        } 
