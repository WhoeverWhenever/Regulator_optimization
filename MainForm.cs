using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwoTanks
{
    public partial class MainForm : Form
    {

        private ControlSystem twoTanks { get; set; }

        private int chartLimit = 50;
        public MainForm()
        {
            InitializeComponent();
            twoTanks = new ControlSystem(1);
        }

        private void tmModeling_Tick(object sender, EventArgs e)
        {
            twoTanks.Calc();
            chPlot.Series[0].Points.AddXY(twoTanks.Time, twoTanks.Out1);
            chPlot.Series[1].Points.AddXY(twoTanks.Time, twoTanks.Out2);


            if (chPlot.Series[0].Points.Count >= chartLimit)
            {
                chPlot.Series[0].Points.RemoveAt(0);
                chPlot.Series[1].Points.RemoveAt(0);
                chPlot.ChartAreas[0].RecalculateAxesScale();
            }
            tbPIDK.Text = twoTanks.pid.K.ToString("F2");
            tbPIDTi.Text = twoTanks.pid.Ti.ToString("F2");
            tbPIDKd.Text = twoTanks.pid.Kd.ToString("F2");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            tmModeling.Start();
         
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tmModeling.Stop();
        }

        private void btnX1_Click(object sender, EventArgs e)
        {
            tmModeling.Interval = 1000;
        }

        private void btnX10_Click(object sender, EventArgs e)
        {
            tmModeling.Interval = 100;
        }

        private void btnDownIn1_Click(object sender, EventArgs e)
        {
            twoTanks.ValveIn1--;
            tbValveIn1.Text = twoTanks.ValveIn1.ToString("F2");
        }

        private void btnUpIn1_Click(object sender, EventArgs e)
        {
            twoTanks.ValveIn1++;
            tbValveIn1.Text = twoTanks.ValveIn1.ToString("F2");
        }

        private void tbValveIn1_TextChanged(object sender, EventArgs e)
        {
            double val = 0;
            if(Double.TryParse(tbValveIn1.Text, out val))
            {
                twoTanks.ValveIn1 = val;
                tbValveIn1.Text = twoTanks.ValveIn1.ToString("F2");
            }

        }

        private void tbValveIn2_TextChanged(object sender, EventArgs e)
        {
            double val = 0;
            if (Double.TryParse(tbValveIn2.Text, out val))
            {
                twoTanks.ValveIn2 = val;
                tbValveIn2.Text = twoTanks.ValveIn2.ToString("F2");
            }
        }

        private void tbValve12_TextChanged(object sender, EventArgs e)
        {
            double val = 0;
            if (Double.TryParse(tbValve12.Text, out val))
            {
                twoTanks.Valve12 = val;
                tbValve12.Text = twoTanks.Valve12.ToString("F2");
            }
        }

        private void tbValveOut1_TextChanged(object sender, EventArgs e)
        {
            double val = 0;
            if (Double.TryParse(tbValveOut1.Text, out val))
            {
                twoTanks.ValveOut1 = val;
                tbValveOut1.Text = twoTanks.ValveOut1.ToString("F2");
            }
        }

        private void btnDownIn2_Click(object sender, EventArgs e)
        {
            twoTanks.ValveIn2--;
            tbValveIn2.Text = twoTanks.ValveIn2.ToString("F2");
        }

        private void btnDown12_Click(object sender, EventArgs e)
        {
            twoTanks.Valve12--;
            tbValve12.Text = twoTanks.Valve12.ToString("F2");
        }

        private void btnDownOut1_Click(object sender, EventArgs e)
        {
            twoTanks.ValveOut1--;
            tbValveOut1.Text = twoTanks.ValveOut1.ToString("F2");
        }

        private void btnUpIn2_Click(object sender, EventArgs e)
        {
            twoTanks.ValveIn2++;
            tbValveIn2.Text = twoTanks.ValveIn2.ToString("F2");
        }

        private void btnUp12_Click(object sender, EventArgs e)
        {
            twoTanks.Valve12++;
            tbValve12.Text = twoTanks.Valve12.ToString("F2");
        }

        private void btnUpOut1_Click(object sender, EventArgs e)
        {
            twoTanks.ValveOut1++;
            tbValveOut1.Text = twoTanks.ValveOut1.ToString("F2");
        }

        private void btnSPUp_Click(object sender, EventArgs e)
        {
            twoTanks.SetPoint++;
            tbSetPoint.Text = twoTanks.SetPoint.ToString("F2");
        }

        private void btnSPDown_Click(object sender, EventArgs e)
        {
            twoTanks.SetPoint--;
            tbSetPoint.Text = twoTanks.SetPoint.ToString("F2");
        }

        private void tbSetPoint_TextChanged(object sender, EventArgs e)
        {
            double val = 0;
            if (Double.TryParse(tbSetPoint.Text, out val))
            {
                twoTanks.SetPoint = val;
                tbSetPoint.Text = twoTanks.SetPoint.ToString("F2");
            }
        }

        private void tbPIDK_TextChanged(object sender, EventArgs e)
        {
            double val = 0;
            if (Double.TryParse(tbPIDK.Text, out val))
            {
                twoTanks.pid.K = val;
                tbPIDK.Text = twoTanks.pid.K.ToString("F2");
            }
        }

        private void tbPIDTi_TextChanged(object sender, EventArgs e)
        {
            double val = 0;
            if (Double.TryParse(tbPIDTi.Text, out val))
            {
                twoTanks.pid.Ti = val;
                tbPIDTi.Text = twoTanks.pid.Ti.ToString("F2");
            }
        }

        private void tbPIDKd_TextChanged(object sender, EventArgs e)
        {
            double val = 0;
            if (Double.TryParse(tbPIDKd.Text, out val))
            {
                twoTanks.pid.Kd = val;
                tbPIDKd.Text = twoTanks.pid.Kd.ToString("F2");
            }
        }

        private void btnMode_Click(object sender, EventArgs e)
        {
            twoTanks.pid.IsManual = !twoTanks.pid.IsManual;
            btnMode.Text = twoTanks.pid.IsManual ? "Manual" : "Auto";
            btnUpIn1.Enabled = twoTanks.pid.IsManual;
            btnDownIn1.Enabled = twoTanks.pid.IsManual;
            tbValveIn1.Enabled = twoTanks.pid.IsManual;
            tbValveIn1.Text = twoTanks.ValveIn1.ToString("F2");
        }

        private void ShowOptimization(double dt, double maxtime, double setpoint, double[] variables, int series)
        {
            ControlSystem sys = new ControlSystem(dt);
            sys.pid.K = variables[0];
            sys.pid.Ti = variables[1];
            sys.pid.Kd = variables[2];
            sys.SetPoint = setpoint;
            var stepCnt = (int)(maxtime / dt);

            for (int i = 0; i < stepCnt; i++)
            {
                
                chPlot.Series[series].Points.AddXY(sys.Time, sys.Out1);
                sys.Calc();
            }
        }

        private void btnOptimization_Click(object sender, EventArgs e)
        {
            var startParam = new double[] { 1, 50, 0 };
            var I1 = Criteria.I2Criteria(startParam);
            ShowOptimization(1, 50, 6, startParam, 0);
            var optimizationResult = CD.descent_method(Criteria.I2Criteria, startParam, 0.01, 10000);
            ShowOptimization(1, 50, 6, optimizationResult, 2);
            var I2 = Criteria.I2Criteria(optimizationResult);

        }
    }
}
