﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculette_Agile
{
    public partial class Calculator : Form
    {

        enum customOperator
        {
            addition,
            soustraction,
            multiplication,
            division,
            equals
        }

        customOperator selectedOperator = customOperator.addition;
        string operand1 = "0";
        string operand2 = "0";
        string result = "0";
        string lastResultValue = "0";
        bool lastIsOperand = false;
        Calculate calcul = new Calculate();




        public Calculator()
        {
            InitializeComponent();
            setOperatorsEnabled(false);
            EqualButton.Enabled = false;
        }

        private void OnNumericButtonClick(object sender, EventArgs e)
        {
            setOperatorsEnabled(true);
            EqualButton.Enabled = true;
            string myNumber = ((Button)sender).Tag.ToString();
            if (lastIsOperand)
            {
                DisplayTextBox.Text = myNumber;
                if (selectedOperator.Equals(customOperator.equals))
                {
                    result = DisplayTextBox.Text.ToString();
                }
            }
            else
            {
                if (DisplayTextBox.Text.Length < 10)
                {
                    // Clear all left zeros
                DisplayTextBox.Text = DisplayTextBox.Text.TrimStart('0');
                DisplayTextBox.Text += myNumber.ToString();
                }
            }
            lastIsOperand = false;
        }

       
        private void OnOperationButtonClick(object sender, EventArgs e)
        {
            setOperatorsEnabled(false);

            EqualButton.Enabled = false;
            string tagValue = ((Button)sender).Tag.ToString();
            string displayValue = DisplayTextBox.Text;
            DisplayTextBox.Text = "";
            lastIsOperand = true;
            SignTextBox.Text = tagValue;
            /*
            if (displayValue == "0" || displayValue == "")
            {
                DisplayTextBox.Text = "-";
                lastIsOperand = false;
            }
            else
            {
             * */

            if (result.Equals("Err /0"))
            {
                result = displayValue;
                selectedOperator = customOperator.equals;
                DisplayTextBox.Text = displayValue;
                lastOperand(tagValue);
            }
            else
            {
                switch (selectedOperator)
                {
                    case customOperator.addition:
                        result = calcul.Addition(result, displayValue);
                        break;
                    case customOperator.soustraction:
                        result = calcul.Soustraction(result, displayValue);
                        break;
                    case customOperator.multiplication:
                        result = calcul.Multiplication(result, displayValue);
                        break;
                    case customOperator.division:
                        result = calcul.Division(result, displayValue);
                        break;
                    default:
                        break;
                }
                DisplayTextBox.Text = result;
                lastOperand(tagValue);
                if (result.Equals("Err /0"))
                {
                    setOperatorsEnabled(false);
                }
            }

            //}
        }

        private void OnEqualButtonClick(object sender, EventArgs e)
        {
            string displayValue = DisplayTextBox.Text;
            DisplayTextBox.Text = "";
            lastIsOperand = true;
            SignTextBox.Text = "=";
            if (result.Equals("Err /0"))
            {
                result = displayValue;
                selectedOperator = customOperator.equals;
                lastOperand("=");
                DisplayTextBox.Text = displayValue;
            }
            else
            {
                switch (selectedOperator)
                {
                    case customOperator.addition:
                        result = calcul.Addition(result, displayValue);
                        break;
                    case customOperator.soustraction:
                        result = calcul.Soustraction(result, displayValue);
                        break;
                    case customOperator.multiplication:
                        result = calcul.Multiplication(result, displayValue);
                        break;
                    case customOperator.division:
                        result = calcul.Division(result, displayValue);
                        break;
                    case customOperator.equals:
                        // result = "0";
                        // operand1 = "0";
                        break;

                    default:
                        break;
                }
            
                DisplayTextBox.Text = result;
                lastOperand("=");
                EqualButton.Enabled = false;
                if (result.Equals("Err /0"))
                {
                    setOperatorsEnabled(false);
                }
            }
        }

        public void lastOperand (string tag)
        {
            switch (tag)
            {
                case "+":
                    selectedOperator = customOperator.addition;
                    break;
                case "-":
                    selectedOperator = customOperator.soustraction;
                    break;
                case "=":
                    selectedOperator = customOperator.equals;
                    break;
                case "x":
                    selectedOperator = customOperator.multiplication;
                    break;
                case "/":
                    selectedOperator = customOperator.division;
                    break;
                default:
                    break;
            }
        }

        private void OnClearButtonClick(object sender, EventArgs e)
        {
            string tagValue = ((Button)sender).Tag.ToString();
            switch (tagValue)
            {
                case "CA":
                    result = calcul.ClearAll();
                    DisplayTextBox.Text = result;
                    SignTextBox.Text = "";
                    setOperatorsEnabled(true);
                    break;

                case "Back":
                    if (!lastIsOperand)
                    {
                        DisplayTextBox.Text = calcul.ClearLastDigit(DisplayTextBox.Text);
                    }
                    break;

                case "CE":
                    DisplayTextBox.Text = "0";
                    setOperatorsEnabled(false);
                    EqualButton.Enabled = false;
                    setOperatorsEnabled(true);
                    break;

                default:
                    break;
            }
        }

        private void OnChangeSignButtonClick(object sender, EventArgs e)
        {
            if (lastIsOperand)
            {
                DisplayTextBox.Text = "-";
            }
            else
            {
                string displayValue = DisplayTextBox.Text;

                if (displayValue.Contains('-'))
                {
                    DisplayTextBox.Text = displayValue.Replace("-", string.Empty);
                }
                else
                {
                    DisplayTextBox.Text = "-" + displayValue;
                }
            }
            lastIsOperand = false;
            
        }

        private void setOperatorsEnabled(bool isEnabled)
        {
            DivideButton.Enabled = isEnabled;
            TimesButton.Enabled = isEnabled;
            PlusButton.Enabled = isEnabled;
            MinusButton.Enabled = isEnabled;
        }

    }
    
}
