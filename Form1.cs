using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private double result = 0;
        private readonly double[] num = new double[] {0.0, 0.0};

        private bool absoluteValueToggle = false;
        private readonly string textFormat = "##############0.##############";

        private void ButtonNumbers_Click(object sender, EventArgs e)
        {
            EnableOperationKeys(sender, e);
            Button button = (Button)sender;

            if (MainDisplay.Text == result.ToString(textFormat) && absoluteValueToggle == false)
            {
                absoluteValueToggle = true;
            }

            if (MainDisplay.TextLength < 13 + Convert.ToInt32(MainDisplay.Text.Contains(buttonDot.Text)) + Convert.ToInt32(MainDisplay.Text.Contains("-")))
            {
                if (MainDisplay.Text.Equals("0"))
                {
                    MainDisplay.Text = button.Text;
                }
                else
                {
                    MainDisplay.Text += button.Text;
                }
            }
        }

        private void DisableOperationKeys()
        {
            buttonPlus.Enabled = false;
            buttonMinus.Enabled = false;
            buttonMult.Enabled = false;
            buttonDiv.Enabled = false;
            buttonPlusMin.Enabled = false;
            buttonDot.Enabled = false;
        }
        private void EnableOperationKeys(object sender, EventArgs e)
        {
            if (buttonPlus.Enabled)
            {
                return;
            }
            buttonPlus.Enabled = true;
            buttonMinus.Enabled = true;
            buttonMult.Enabled = true;
            buttonDiv.Enabled = true;
            buttonPlusMin.Enabled = true;
            buttonDot.Enabled = true;
            MainDisplay.Text = "0";
        }

        private void Calc_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.D0:
                case (char)Keys.NumPad0:
                    button0.PerformClick();
                    break;
                case (char)Keys.D1:
                case (char)Keys.NumPad1:
                    button1.PerformClick();
                    break;
                case (char)Keys.D2:
                case (char)Keys.NumPad2:
                    button2.PerformClick();
                    break;
                case (char)Keys.D3:
                case (char)Keys.NumPad3:
                    button3.PerformClick();
                    break;
                case (char)Keys.D4:
                case (char)Keys.NumPad4:
                    button4.PerformClick();
                    break;
                case (char)Keys.D5:
                case (char)Keys.NumPad5:
                    button5.PerformClick();
                    break;
                case (char)Keys.D6:
                case (char)Keys.NumPad6:
                    button6.PerformClick();
                    break;
                case (char)Keys.D7:
                case (char)Keys.NumPad7:
                    button7.PerformClick();
                    break;
                case (char)Keys.D8:
                case (char)Keys.NumPad8:
                    button8.PerformClick();
                    break;
                case (char)Keys.D9:
                case (char)Keys.NumPad9:
                    button9.PerformClick();
                    break;
                case (char)Keys.Back:
                    buttonBack.PerformClick();
                    break;
                default:
                    break;
            }
        }

        private void Calc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add)
            {
                buttonPlus.PerformClick();
            }
            else if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
            {
                buttonMinus.PerformClick();
            }
            else if (e.KeyCode == Keys.Multiply || (e.Shift && e.KeyCode == Keys.D8))
            {
                buttonMult.PerformClick();
            }
            else if (e.KeyCode == Keys.Divide || e.KeyCode == Keys.OemQuestion)
            {
                buttonDiv.PerformClick();
            }
            else if (e.KeyCode == Keys.OemPeriod || e.KeyCode == Keys.Decimal)
            {
                buttonDot.PerformClick();
            }
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            EnableOperationKeys(sender, e);
            if (MainDisplay.Text.Length == 1)
            {
                MainDisplay.Text = "0";
            }
            else
            {
                MainDisplay.Text = MainDisplay.Text.Remove(MainDisplay.Text.Length - 1);
            }
        }
        private void buttonDot_Click(object sender, EventArgs e)
        {
            Button btnDot = (Button)sender;
            if (MainDisplay.Text.Contains(btnDot.Text) == false)
            {
                MainDisplay.Text += btnDot.Text;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            MainDisplay.Text = "0";
            SecondaryDisplay.Clear();
        }

        private void buttonPlusMin_Click(object sender, EventArgs e)
        {
            absoluteValueToggle = false;
            if (double.Parse(MainDisplay.Text) != 0.0)
            {
                MainDisplay.Text = (Convert.ToDouble(MainDisplay.Text) * -1.0).ToString(textFormat);
            }
        }

        private int Operation()
        {
            if (SecondaryDisplay.Text.Contains("+"))
            {
                num[0] += num[1];
            }
            else if (SecondaryDisplay.Text.Contains("-"))
            {
                num[0] -= num[1];
            }
            else if (SecondaryDisplay.Text.Contains("x"))
            {
                num[0] *= num[1];
            }
            else if (SecondaryDisplay.Text.Contains("÷"))
            {
                if (num[1] == 0)
                {
                    return 1;
                }
                num[0] /= num[1];
            }
            else
            {
                num[0] = num[1];
            }
            return 0;
        }

        private void HandleInvalidInput(int code)
        {
            num[0] = num[1] = result = 0;
            DisableOperationKeys();
            if(code == 0)
            {
                MainDisplay.Text = "No Error";
            } else if(code == 1)
            {
                MainDisplay.Text = "Cannot divide by zero";
            } else if(code == 2)
            {
                MainDisplay.Text = "Invalid Input";
            }
            SecondaryDisplay.Clear();
        }

        private void ButtonOperation_Click(object sender, EventArgs e)
        {
            absoluteValueToggle = false;
            Button button = (Button)sender;
            double mainDisplayValue = Double.Parse(MainDisplay.Text);
            if (mainDisplayValue == result)
            {
                num[0] = result;
            }
            else if (SecondaryDisplay.Text != String.Empty)
            {
                num[1] = mainDisplayValue;
                if (Operation() == 1)
                {
                    HandleInvalidInput(1);
                    return;
                }
                MainDisplay.Text = num[0].ToString(textFormat);
            }
            else
            {
                num[0] = mainDisplayValue;
            }
            SecondaryDisplay.Text = num[0].ToString(textFormat) + " " + button.Text;
            MainDisplay.Text = "0";
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            EnableOperationKeys(sender, e);
            if(MainDisplay.Text == "123456789" && SecondaryDisplay.Text == "1199228833 +")
            {
                MainDisplay.Text = "jayred"; // easter egg heh.
                num[0] = num[1] = result = 0;
                DisableOperationKeys();
                SecondaryDisplay.Clear();
                return;
            }

            if (MainDisplay.Text != result.ToString(textFormat) || MainDisplay.Text == "0")
            {
                num[1] = Double.Parse(MainDisplay.Text);
                if (SecondaryDisplay.Text != String.Empty && !SecondaryDisplay.Text.Contains('='))
                {
                    int error = Operation();
                    if (error != 0)
                    {
                        HandleInvalidInput(error);
                        return;
                    }
                    SecondaryDisplay.Text += " " + num[1].ToString(textFormat) + " =";
                    result = num[0];
                    MainDisplay.Text = result.ToString(textFormat);
                }
            }
        }
    }
}
