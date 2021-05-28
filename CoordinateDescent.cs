using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TwoTanks
{
    public class CD
    {

        public delegate double func(double[] variables);

        static public double golden_section(func f, double[] vars, int var_index, double eps, double a,
                                                                            double b, int max_steps_count)
        {
            double res = 0.0;
            double phi = (1 + Math.Sqrt(5.0)) / 2.0;
            double A = 0, B = 0;
            double x1, x2;

            int step = 0;

            while ((b - a) > eps)
            {

                x1 = b - ((b - a) / phi);
                vars[var_index] = x1;
                A = f(vars);
                x2 = a + ((b - a) / phi);
                vars[var_index] = x2;
                B = f(vars);
                if (A > B)
                {
                    a = x1;
                }
                else
                {
                    b = x2;
                }

                step++;

                if (step > max_steps_count)
                {
                    Console.WriteLine("Over the limit");
                    break;
                }
            }

            res = (a + b) / 2;
            return res;
        }

        static public double[] descent_method(func f, double[] vars, double eps, int max_steps_count)
        {
            double B = f(vars), A = 0;
            double delta = 0.0;

            for (int i = 0; i < max_steps_count; i++)
            {
                A = B;

                for (int var_index = 0; var_index < vars.Length; var_index++)
                {
                    vars[var_index] = golden_section(f, vars, var_index, eps, 0, 500, max_steps_count);
                }

                B = f(vars);

                delta = Math.Abs(A - B);

                if (delta <= eps)
                {
                    break;
                }
            }
            return vars;
        }

    }
}
