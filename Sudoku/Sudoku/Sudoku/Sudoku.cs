using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Sudoku : Form
    {
        TextBox[,] boxes = new TextBox[9, 9];
        List<string> good = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        public Sudoku()
        {
            InitializeComponent();

            //Bottom left
            boxes[0, 0] = textBox73;
            boxes[1, 0] = textBox74;
            boxes[2, 0] = textBox75;
            boxes[0, 1] = textBox64;
            boxes[1, 1] = textBox65;
            boxes[2, 1] = textBox66;
            boxes[0, 2] = textBox55;
            boxes[1, 2] = textBox56;
            boxes[2, 2] = textBox57;

            //Bottom middle
            boxes[3, 0] = textBox76;
            boxes[4, 0] = textBox77;
            boxes[5, 0] = textBox78;
            boxes[3, 1] = textBox67;
            boxes[4, 1] = textBox68;
            boxes[5, 1] = textBox69;
            boxes[3, 2] = textBox58;
            boxes[4, 2] = textBox59;
            boxes[5, 2] = textBox60;

            //Bottom right
            boxes[6, 0] = textBox79;
            boxes[7, 0] = textBox80;
            boxes[8, 0] = textBox81;
            boxes[6, 1] = textBox70;
            boxes[7, 1] = textBox71;
            boxes[8, 1] = textBox72;
            boxes[6, 2] = textBox61;
            boxes[7, 2] = textBox62;
            boxes[8, 2] = textBox63;

            //middle left
            boxes[0, 3] = textBox46;
            boxes[1, 3] = textBox47;
            boxes[2, 3] = textBox48;
            boxes[0, 4] = textBox37;
            boxes[1, 4] = textBox38;
            boxes[2, 4] = textBox39;
            boxes[0, 5] = textBox28;
            boxes[1, 5] = textBox29;
            boxes[2, 5] = textBox30;

            //middle middle
            boxes[3, 3] = textBox49;
            boxes[4, 3] = textBox50;
            boxes[5, 3] = textBox51;
            boxes[3, 4] = textBox40;
            boxes[4, 4] = textBox41;
            boxes[5, 4] = textBox42;
            boxes[3, 5] = textBox31;
            boxes[4, 5] = textBox32;
            boxes[5, 5] = textBox33;

            //middle right
            boxes[6, 3] = textBox52;
            boxes[7, 3] = textBox53;
            boxes[8, 3] = textBox54;
            boxes[6, 4] = textBox43;
            boxes[7, 4] = textBox44;
            boxes[8, 4] = textBox45;
            boxes[6, 5] = textBox34;
            boxes[7, 5] = textBox35;
            boxes[8, 5] = textBox36;

            //top left
            boxes[0, 6] = textBox19;
            boxes[1, 6] = textBox20;
            boxes[2, 6] = textBox21;
            boxes[0, 7] = textBox10;
            boxes[1, 7] = textBox11;
            boxes[2, 7] = textBox12;
            boxes[0, 8] = textBox1;
            boxes[1, 8] = textBox2;
            boxes[2, 8] = textBox3;

            //top middle
            boxes[3, 6] = textBox22;
            boxes[4, 6] = textBox23;
            boxes[5, 6] = textBox24;
            boxes[3, 7] = textBox13;
            boxes[4, 7] = textBox14;
            boxes[5, 7] = textBox15;
            boxes[3, 8] = textBox4;
            boxes[4, 8] = textBox5;
            boxes[5, 8] = textBox6;

            //top right
            boxes[6, 6] = textBox25;
            boxes[7, 6] = textBox26;
            boxes[8, 6] = textBox27;
            boxes[6, 7] = textBox16;
            boxes[7, 7] = textBox17;
            boxes[8, 7] = textBox18;
            boxes[6, 8] = textBox7;
            boxes[7, 8] = textBox8;
            boxes[8, 8] = textBox9;
        }

        bool? CheckFinalBoard()
        {
            string[,] board = new string[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = boxes[i, j].Text;
                }
            }
            return CheckForCompletion(board);
        }

        bool? CheckForCompletion(string[,] board)
        {
            if (CheckVerticals(board) && CheckHorizontals(board) && CheckSections(board))
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        //If the box is empty, we know we haven't won yet
                        if (board[i, j] == "")
                        {
                            return null;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        bool CheckSections(string[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    List<string> values = new List<string>();
                    for (int x = i * 3; x < i * 3 + 3; x++)
                    {
                        for (int y = j * 3; y < j * 3 + 3; y++)
                        {
                            values.Add(board[x, y]);
                        }
                    }

                    if (!CheckIsValid(values))
                    {
                        return false;
                    }

                    //values.Sort();
                    //if (!values.SequenceEqual(good))
                    //{
                    //    return false;
                    //}
                }
            }
            return true;
        }

        bool CheckVerticals(string[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                List<string> values = new List<string>();
                for (int j = 0; j < 9; j++)
                {
                    values.Add(board[i, j]);
                }

                if (!CheckIsValid(values))
                {
                    return false;
                }

                //values.Sort();
                //if (!values.SequenceEqual(good))
                //{
                //    return false;
                //}
            }
            return true;
        }

        bool CheckHorizontals(string[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                List<string> values = new List<string>();
                for (int j = 0; j < 9; j++)
                {
                    values.Add(board[j, i]);
                }

                if (!CheckIsValid(values))
                {
                    return false;
                }
                //values.Sort();
                //if (!values.SequenceEqual(good))
                //{
                //    return false;
                //}
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckFinalBoard() == null)
            {
                MessageBox.Text = "You haven't filled in all the blanks.";
            }
            else if (CheckFinalBoard() == true)
            {
                MessageBox.Text = "Congratulations, you've won!";
            }
            else
            {
                MessageBox.Text = "This is not correct";
            }
        }

        /// <summary>
        /// Starts a new game
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    boxes[i, j].Text = "";
                    boxes[i, j].Enabled = true;
                }
            }

            GenerateNewGame();
        }

        void GenerateNewGame()
        {
            RecursiveSolveSudoku(new string[9, 9] { { "", "", "", "", "", "", "", "", "" }, { "", "", "", "", "", "", "", "", "" }, { "", "", "", "", "", "", "", "", "" }, { "", "", "", "", "", "", "", "", "" }, { "", "", "", "", "", "", "", "", "" }, { "", "", "", "", "", "", "", "", "" }, { "", "", "", "", "", "", "", "", "" }, { "", "", "", "", "", "", "", "", "" }, { "", "", "", "", "", "", "", "", "" }, });

            MessageBox.Text = "Finished";

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    boxes[i, j].Text = completedBoard[i, j];
                }
            }
        }



        string[,] completedBoard = new string[9, 9];
        Random rand = new Random();
        bool RecursiveSolveSudoku(string[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i,j] == "")
                    {
                        board[i, j] = GetValidElement(board, i, j);
                        if (board[i,j] == "")
                        {
                            return false;
                        }
                        else if (CheckForCompletion(board) == null)
                        {
                            return RecursiveSolveSudoku(board.Clone() as string[,]);
                        }
                        else if (CheckForCompletion(board) == true)
                        {
                            completedBoard = board;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }

        string GetValidElement(string[,] board, int x, int y)
        {
            List<string> thingsCannotBe = new List<string>();
            for (int i = 0; i < 9; i++)
            {
                if (!thingsCannotBe.Contains(board[x,i]))
                {
                    thingsCannotBe.Add(board[x, i]);
                }
                if (!thingsCannotBe.Contains(board[i, y]))
                {
                    thingsCannotBe.Add(board[i, y]);
                }
            }
            for (int i = x - (x % 3); i < x - (x % 3) + 3; i++)
            {
                for (int j = y - (y % 3); j < y - (y % 3) + 3; j++)
                {
                    if (!thingsCannotBe.Contains(board[i, j]))
                    {
                        thingsCannotBe.Add(board[i, j]);
                    }
                }
            }

            List<string> valids = good.Where(t => !thingsCannotBe.Contains(t)).ToList();
            if (valids.Count == 0)
                return "";
            return valids[rand.Next(0, valids.Count)];
        }

        bool CheckIsValid(List<string> elements)
        {
            List<string> copy = new List<string>();
            foreach (string s in elements)
            {
                if (s == "")
                {
                    continue;
                }
                if (int.TryParse(s, out int a))
                {
                    if (!copy.Contains(s))
                        copy.Add(s);
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
