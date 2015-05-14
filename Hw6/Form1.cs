﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Hw6
{
    public partial class Form1 : Form
    {
        ArrayList shapeArr=new ArrayList();
        private static double[] densityArr = { 2.7, 7.87, 11.3 };
        public Form1()
        {
            InitializeComponent();
            cbox_ShapeSelect.SelectedIndex = 0;
            cbox_MaterialSelect.SelectedIndex = 0;
        }

        private void cbox_ShapeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbox_ShapeSelect.SelectedIndex)
            {
                case 0:
                    lbl_Para1.Text = "半徑";
                    lbl_Para2.Visible = false;
                    txt_Para2.Visible = false;
                    break;
                case 1:
                    lbl_Para1.Text = "邊長";
                    lbl_Para2.Visible = false;
                    txt_Para2.Visible = false;
                    break;
                case 2:
                    lbl_Para1.Text = "半徑";
                    lbl_Para2.Text = "高";
                    lbl_Para2.Visible = true;
                    txt_Para2.Visible = true;
                    break;
                case 3:
                    lbl_Para1.Text = "邊長";
                    lbl_Para2.Text = "高";
                    lbl_Para2.Visible = true;
                    txt_Para2.Visible = true;
                    break;
                default:
                    break;

            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            int amount = Shape3D.Amount;

            double density = densityArr[cbox_MaterialSelect.SelectedIndex];

            switch (cbox_ShapeSelect.SelectedIndex)
            {
                case 0:

                    Ball ball= new Ball(double.Parse(txt_Para1.Text), density);
                    shapeArr.Add(ball);
                    break;
                case 1:
                    Cube cube = new Cube(double.Parse(txt_Para1.Text), density);
                    shapeArr.Add(cube);
                    break;
                case 2:
                    Cylinder cylinder = new Cylinder(double.Parse(txt_Para1.Text), double.Parse(txt_Para2.Text), density);
                    shapeArr.Add(cylinder);
                    break;
                case 3:
                    Pyramid pyramid = new Pyramid(double.Parse(txt_Para1.Text), double.Parse(txt_Para2.Text), density);
                    shapeArr.Add(pyramid);
                    break;
                default:
                    break;
            }
            txt_Message.AppendText(((Shape3D)shapeArr[amount]).ShapeProperty()+Environment.NewLine);
            txt_AmountOfShape.Text = Shape3D.Amount.ToString();
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Shape3D.Amount;i++ )
            {
                string str = (((Shape3D)shapeArr[i]).ShowVolumeWeight() + Environment.NewLine);
                txt_Display.AppendText(str);
            }
        }
        private void btn_Apply_Click(object sender, EventArgs e)
        {
            Sort();
            txt_Display.Clear();
            for (int i = 0; i < Shape3D.Amount; i++)
            {
                string str = (((Shape3D)shapeArr[i]).ShowVolumeWeight() + Environment.NewLine);
                txt_Display.AppendText(str);
            }
        }
        private void Sort()
        {
            switch (cbox_SortSelect.SelectedIndex)
            {
                case 0:
                    if(rbtn_Ascend.Checked)
                    {
                        BubbleSort(shapeArr, CompareByVolumeAscend);
                        break;
                    }
                    else
                    {
                        BubbleSort(shapeArr, CompareByVolumeDescend);
                        break;
                    }
                case 1:
                    if (rbtn_Ascend.Checked)
                    {
                        BubbleSort(shapeArr, CompareByWeightAscend);
                        break;
                    }
                    else
                    {
                        BubbleSort(shapeArr, CompareByWeightDescend);
                        break;
                    }
                default:
                    break;
            }
        }
        private bool CompareByVolumeAscend(Shape3D a, Shape3D b)
        {
            if (a.Volume() > b.Volume())
                return true;
            else
                return false;
        }
        private bool CompareByVolumeDescend(Shape3D a, Shape3D b)
        {
            if (a.Volume() < b.Volume())
                return true;
            else
                return false;
        }
        private bool CompareByWeightAscend(Shape3D a, Shape3D b)
        {
            if (a.Weight() > b.Weight())
                return true;
            else
                return false;
        }
        private bool CompareByWeightDescend(Shape3D a, Shape3D b)
        {
            if (a.Weight() < b.Weight())
                return true;
            else
                return false;
        }

        private delegate bool CompareFunc(Shape3D a, Shape3D b);
        private void BubbleSort(ArrayList arr, CompareFunc cmp)
        {
            for (int pass = 0; pass < arr.Count; pass++)
            {
                for (int i = 0; i < arr.Count - 1; i++)
                {
                    if (cmp((Shape3D)arr[i], (Shape3D)arr[i + 1]))
                    {
                        Shape3D temp;
                        temp = (Shape3D)arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                    }
                }
            }
        }
    }
}
