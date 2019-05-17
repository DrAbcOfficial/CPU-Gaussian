using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GaussianOut
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //封装列表
        static string szFilePath = "";
        static string word1 = "Frequencies --";
        static string word2 = "Thermal correction to Enthalpy=";
        static string word3 = "Thermal correction to Gibbs Free Energy=";
        static string word4 = "Sum of electronic and thermal Enthalpies=";
        static string word5 = "Sum of electronic and thermal Free Energies=";

        //打开文件
        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            szFilePath = comboBox1.Text = ofd.FileName;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string[] aryFre = { };
            string[] aryNum = { };
            string[] aryNeg = { };
            int j = 0;
            if (Form1.szFilePath == "")
                label1.Text = "没有输入文件，请检查文件路径是否正确!";
            else if (Form1.szFilePath.Split('.')[1] != "out")
                label1.Text = "输入文件格式不正确!";
            else
            {
                System.IO.StreamReader sr = System.IO.File.OpenText(Form1.szFilePath);
                string s = sr.ReadToEnd();
                sr.Close();
                string[] temp = s.Split('\n');
                for(int i = 0; i< temp.Length; i++)
                {
                    if(temp[i].IndexOf(Form1.word1) != -1)
                        aryFre = aryFre.Concat(temp[i].Replace(Form1.word1, "").Split(' ')).ToArray();  //去除头部后按空格拆分
                    else if (temp[i].IndexOf(Form1.word2) != -1)
                        aryNum = Form1.AryAddBuffer(aryNum, temp[i].Replace(Form1.word2, "").Replace(" ", ""), 0);  //去除文件头部后去除所有空格
                    else if (temp[i].IndexOf(Form1.word3) != -1)
                        aryNum = Form1.AryAddBuffer(aryNum, temp[i].Replace(Form1.word3, "").Replace(" ", ""), 1);
                    else if (temp[i].IndexOf(Form1.word4) != -1)
                        aryNum = Form1.AryAddBuffer(aryNum, temp[i].Replace(Form1.word4, "").Replace(" ", ""), 2);
                    else if (temp[i].IndexOf(Form1.word5) != -1)
                        aryNum = Form1.AryAddBuffer(aryNum, temp[i].Replace(Form1.word5, "").Replace(" ", ""), 3);
                }
                //删除Frq无用的空白元素
                List<string> list = new List<string>();
                aryFre.ToList().ForEach(
                    (character) =>
                        {
                            if (!string.IsNullOrEmpty(character))
                            {
                                list.Add(character);
                            }
                        }
                    );
                aryFre = list.ToArray();
                //寻找负数项
                for (int i = 0; i < aryFre.Length; i++)
                {
                    if(aryFre[i].Contains("-"))
                    {
                        j++;
                        aryNeg = Form1.AryAddBuffer(aryNeg, aryFre[i], 0);
                    }
                }
                //输出了
                label1.Text = "频率有: " + j + " 个负数， " + (aryFre.Length - j) + " 个正数.\n" +
                              "负数为: " + Form1.szOutNegBuffer(aryNeg) + "\n" +
                              "H(Hartree) = " + aryNum[0] + "\n" +
                              "G(Hartree) = " + aryNum[1] + "\n" +
                              "H = " + aryNum[2] + "\n" +
                              "G = " + aryNum[3] + "\n";
            }
        }
        //按照格式列出负数
        static string szOutNegBuffer(string[] a)
        {
            string cache = "";
            for (int i = 0; i < a.Length; i++)
            {
                cache = cache + " | " + a[i];
            }
            return cache == "" ? "无" : cache;
        }
        //为数组添加文件
        static string[] AryAddBuffer( string[] a ,string val ,int index)
        {
            List<string> temArray = new List<string>(a);
            temArray.Insert(index,val);
            return temArray.ToArray();
        }
    }
}
