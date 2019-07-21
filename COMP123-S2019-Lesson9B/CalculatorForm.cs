using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP123_S2019_Lesson9B
{
    public partial class CalculatorForm : Form
    {
        // Class Properties
        public string OutputString { get; set; }
        public float OutputValue { get; set; }
        public bool DecimalExists { get; set; }
        public Label ActiveLabel { get; set; }


        /// <summary>
        /// This is the constructor for the Calculator Form
        /// </summary>
        public CalculatorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is the event handler for the form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorForm_Load(object sender, EventArgs e)
        {
            ClearNumericKeyBoard();
            CalculatorButtonTableLayoutPanel.Visible = false;
            ActiveLabel = null;
        }

        /// <summary>
        /// This is the event handler for the CalculatorForm Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorForm_Click(object sender, EventArgs e)
        {
            ClearNumericKeyBoard();
            if (ActiveLabel != null)
            {
                ActiveLabel.BackColor = Color.White;
            }

            CalculatorButtonTableLayoutPanel.Visible = false;
            ActiveLabel = null;
        }

        /// <summary>
        /// This is a shared event handler for the CalculatorButton click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorButton_Click(object sender, EventArgs e)
        {
            var TheButton = sender as Button;
            var tag = TheButton.Tag.ToString();
            int numericValue = 0;

            bool numericRusult = int.TryParse(tag, out numericValue);

            if  (numericRusult)
            {
                int maxSize = (DecimalExists) ? 5 : 3;

                if (OutputString == "0")
                {
                    OutputString = tag;
                }
                else
                {
                    if (OutputString.Length < maxSize)
                    {
                        OutputString += tag; 
                    }
                }
                ResultLabel.Text = OutputString;
            }
            else
            {
                switch (tag)
                {
                    case "back":
                        RemoveLastCharacterFromResultLabel();
                        break;
                    case "done":
                        FinalizeOutput();
                        break;
                    case "clear":
                        ClearNumericKeyBoard();
                        break;
                    case "decimal":
                        AddDecimalToResultLabel();
                        break;
                }
            }
            //int ButtonValue;
            //bool Result = int.TryParse(TheButton.Text, out ButtonValue);

            //if (Result)
            //{
            //    ResultLabel.Text = TheButton.Text;
            //}
            //else
            //{
            //    ResultLabel.Text = "Not a Number (NAN)"; 
            //}
        }

        /// <summary>
        /// This method adds a decimal point to the ResultLabel
        /// </summary>
        private void AddDecimalToResultLabel()
        {
            if (!DecimalExists)
            {
                OutputString += ".";
                DecimalExists = true;
            }
        }

        /// <summary>
        /// This method finalizes and converts the outputString to a floating point value
        /// </summary>
        private void FinalizeOutput()
        {
            OutputValue = float.Parse(OutputString);

            OutputValue = (float)Math.Round(OutputValue, 1);
            if (OutputValue < 0.1f)
            {
                OutputValue = 0.1f;
            }
            ActiveLabel.Text = OutputValue.ToString();
            ClearNumericKeyBoard();
            CalculatorButtonTableLayoutPanel.Visible = false;
            ActiveLabel.BackColor = Color.White;
            ActiveLabel = null;
        }

        /// <summary>
        /// This method removes the last character from the ResultLabel
        /// </summary>
        private void RemoveLastCharacterFromResultLabel()
        {
            var lastChar = OutputString.Substring(OutputString.Length - 1);
            if (lastChar == ".")
            {
                DecimalExists = false;
            }
            OutputString = OutputString.Remove(OutputString.Length - 1);

            if (OutputString.Length == 0)
            {
                OutputString = "0";
            }
            ResultLabel.Text = OutputString;
        }

        /// <summary>
        /// This method resets the numeric keyboard and related variables
        /// </summary>
        private void ClearNumericKeyBoard()
        {
            ResultLabel.Text = "0";
            OutputString = "0";
            OutputValue = 0.0f;
            DecimalExists = false;
        }

      
        /// <summary>
        /// This is the event handler for the HeightLabel click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveLabel_Click(object sender, EventArgs e)
        {
            if (ActiveLabel != null)
            {
                ActiveLabel.BackColor = Color.White;
                ActiveLabel = null;
            }

            ActiveLabel = sender as Label;
            ActiveLabel.BackColor = Color.LightBlue;
            CalculatorButtonTableLayoutPanel.Visible = true;
            if (ActiveLabel.Text != "0")
            {
                ResultLabel.Text = ActiveLabel.Text;
                OutputString = ActiveLabel.Text;
            }
        }

   
    }
}
