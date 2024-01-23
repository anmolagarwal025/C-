using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Snake_Game
{
    public partial class Form1 : Form
    {
        private List<Circle>Snake = new List<Circle>();
        private Circle food = new Circle();

        public Form1()
        {
            InitializeComponent();

            new Settings();

            // Setting game speed and timer..
            timer1.Interval = 1000 / Settings.Speed;
            timer1.Tick += UpdateScreen;
            timer1.Start();

            //strart new game
            StartGame(); 
            
        }

        private void StartGame()
        {
            label3.Visible = false;

            new Settings();

            Snake.Clear();

            // Created a object for a new Snake when game starts it will look like this...
            Circle head = new Circle();
            head.X = 10;
            head.Y = 5;
            Snake.Add(head);
            
            label2.Text = Settings.Score.ToString();
            GenerateFood();
        }

        // Will place a food at random places on screen or playing area;
        private void GenerateFood()
        {
            int maxXpos = pictureBox1.Size.Width / Settings.Width;
            int maxYpos = pictureBox1.Size.Height / Settings.Height;

            Random random = new Random();
            food = new Circle();

            food.X =  random.Next(0, maxXpos);
            food.Y =  random.Next(0, maxYpos); 
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            if (Settings.Gameover == true)
            {
                if (Inputs.KeyPressed(Keys.Enter))
                //if (e.KeyChar == (char)ConsoleKey.Enter)
                {
                    StartGame();
                }
            }
            else
            {
                // We are applying the condition in a way that snake will kill itself means it can't able to turn into itself
                // so if the directions of snake will not able turn exact into himself.

                if (Inputs.KeyPressed(Keys.Right) && Settings.directions != Direction.Left)
                //if (e.KeyChar == (char)Keys.Right && Settings.directions != Direction.Left)
                    Settings.directions = Direction.Right;
                else if (Inputs.KeyPressed(Keys.Left) && Settings.directions != Direction.Right)
                //else if (e.KeyChar == (char)Keys.Left && Settings.directions != Direction.Right)
                    Settings.directions = Direction.Right;
                else if (Inputs.KeyPressed(Keys.Up) && Settings.directions != Direction.Down)
                //else if (e.KeyChar == (char)Keys.Up && Settings.directions != Direction.Down)
                    Settings.directions = Direction.Right;
                else if (Inputs.KeyPressed(Keys.Down) && Settings.directions != Direction.Up)
                //else if (e.KeyChar == (char)Keys.Down && Settings.directions != Direction.Up)
                    Settings.directions = Direction.Right;

                MovePlayer();
            }

            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (!Settings.Gameover)
            {
                Brush SnakeColour;

                for (int i = 0; i < Snake.Count; i++)
                {
                    if (i == 0)
                    {
                        SnakeColour = Brushes.Black;
                    }
                    else
                    {
                        SnakeColour = Brushes.Green;
                    }

                    // Drawing Snake
                    canvas.FillEllipse(SnakeColour,
                        new Rectangle(Snake[i].X * Settings.Width,
                                      Snake[i].Y * Settings.Height,
                                      Settings.Width, Settings.Height));

                    // Drawing Food
                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.X * Settings.Width,
                        food.Y * Settings.Height,
                        Settings.Width, Settings.Height));
                }
            }
            else
            {
                string gameover = $"Game Over \nYour Final Score is {Settings.Score}\nPress Enter to try again.";
                label3.Text = gameover;
                label3.Visible = true;
            }
        }

        private void MovePlayer()
        {
            for (int i = Snake.Count - 1; i >=0; i--)
            {
                if (i == 0)
                {
                    switch (Settings.directions)
                    {
                        case Direction.Right:
                            Snake[i].X++;
                            break;
                        case Direction.Left:
                            Snake[i].X--;
                            break;
                        case Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Direction.Down:
                            Snake[i].Y++;
                            break;
                    }

                    // Direct collision with game world
                    int maxXpos = pictureBox1.Width/Settings.Width;
                    int maxYpos = pictureBox1.Height/Settings.Height;

                    if (Snake[i].X < 0 ||
                        Snake[i].Y < 0 ||
                        Snake[i].X >= maxXpos ||
                        Snake[i].Y >= maxYpos)
                    {
                        Die();
                    }

                    // Direct collision with own body
                    for (int j = 0; j<Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X 
                            && Snake[i].Y == Snake[j].X)
                        {
                            Die();
                        }
                    }

                    // Direct collision with the food
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y) 
                    {
                        Eat();
                    }
                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        private void Die()
        {
            Settings.Gameover = true;
        }

        private void Eat()
        {
            Circle food = new Circle();

            food.X = Snake[Snake.Count - 1].X;
            food.Y = Snake[Snake.Count - 1].Y;

            Snake.Add(food);

            //Update SCore
            Settings.Score += Settings.Points;
            label2.Text = Settings.Score.ToString();

            GenerateFood();
        }

/*        private void label1_Click(object sender, EventArgs e)
        {
            //This is wrong function declare, as there is no use of it...
        }*/

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Inputs.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Inputs.ChangeState(e.KeyCode, false);
        }
    }
}
