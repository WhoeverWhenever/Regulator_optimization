using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoTanks
{
    public class Criteria
    {
        public static double dt = 1;
        public static double maxTime = 10000;
        public static double eps = 0.1;

        public static double I2Criteria(double[] variables)
        {
            double sum = 0;
            double prevE = 0;
            ControlSystem sys = new ControlSystem(dt);
            sys.pid.K = variables[0];
            sys.pid.Ti = variables[1];
            sys.pid.Kd = variables[2];
            sys.SetPoint = 5;
            var stepCnt = (int)(maxTime / dt);

            for(int i = 0; i < stepCnt; i++)
            {
                sys.Calc();
                sum += sys.E * sys.E * dt;

                if (Math.Abs(prevE - sys.E) < eps)
                {
                    break;
                }
                prevE = sys.E;
            }

            return sum;
        }
    }
}
