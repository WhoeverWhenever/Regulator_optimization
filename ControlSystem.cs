using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoTanks
{

  
    public class ControlSystem
    {

        private double dt;

        private ClampBlock clampInput;
        private SpeedClampBlock spClampInput;
        private ClampBlock clampoutput;
        private DelayBlock delay;

        public PIDBlock pid { get; set; }

        public double Time;
        public ComplexBlock Object { get; set; }

        private double valveIn1;
        private double valveIn2;
        private double valve12;
        private double valveOut1;
        public double ValveIn1 { get { return valveIn1; } set { 
                                valveIn1 = clampInput.Calc(value);
                pid.Umanual = value;
            } }
        public double ValveIn2 { get { return valveIn2; } set { valveIn2 = clampInput.Calc(value); } }
        public double Valve12 { get { return valve12; } set { valve12 = clampInput.Calc(value); } }
        public double ValveOut1 { get { return valveOut1; } set { valveOut1 = clampInput.Calc(value); } }

        public double SetPoint { get; set; }
        public double E { get; set; }

        private APBlock Tank1;
        private APBlock Tank2;

        private double out1;
        private double out2;

        public double Out1 { get { return out1; } }
        public double Out2 { get { return out2; } }

        public ControlSystem(double dt)
        {
            this.dt = dt;
            clampInput = new ClampBlock(0, 100);
            spClampInput = new SpeedClampBlock(dt, 10);


            pid = new PIDBlock(1, 0.02, 0, dt);
           


            Tank1 = new APBlock(dt, 54, 50);
            Tank2 = new APBlock(dt, 42, 50);

        }

        public void Calc ()
        {
         
            Time += dt;
            E = SetPoint - out1;

            if (!pid.IsManual)
            {
                ValveIn1 = pid.Calc(E);
            }
            double input1 = 100.25 * ValveIn1 + 92.3 * ValveIn2 - 152.26 * Valve12 + 0.96 * Out2;
            out1 = Tank1.Calc(input1);


         
            double input2 = 53 * Valve12 + 0.334 * Out1 - 194.06 * ValveOut1;
            out2 = Tank2.Calc(input2);


        }
    }

}
