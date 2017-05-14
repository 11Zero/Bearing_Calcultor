using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PcrCalcultor
{
    public class Calculator
    {
        static public double pi = 3.1415926;
        /// <summary>
        /// 计算弹簧刚度k
        /// </summary>
        /// <param name="l">立杆间距</param>
        /// <param name="h">步距</param>
        /// <param name="Q">下碗扣抗剪强度设计值，取60</param>
        /// <param name="EI">弯曲刚度</param>
        /// <returns>弹簧刚度值</returns>
        static double Solvek(double l, double h, double Q,double EI)
        {
            double k1 = 12 * EI / (l * l * l + h * l * l);
            double k2 = Q / l;
            return Math.Min(k1,k2);
        }

        /// <summary>
        /// 计算屈曲时使Pcr达到最小值的正整数的半波数n
        /// </summary>
        /// <param name="L">架体高度</param>
        /// <param name="k">弹簧刚度值</param>
        /// <param name="h">步距</param>
        /// <param name="EI">弯曲刚度</param>
        /// <returns>半波数</returns>
        static double Solven(double L, double k, double h, double EI)
        {
            return Math.Round(Math.Pow(L/pi,4)*Math.Sqrt(k/h/EI));
        }

        /// <summary>
        /// 计算临界荷载Pcr
        /// </summary>
        /// <param name="n">半波数n</param>
        /// <param name="L">架体高度</param>
        /// <param name="beta_alpha">扫地杆高度与悬臂长度修正系数</param>
        /// <param name="beta_L">架体高度修正系数</param>
        /// <param name="beta_J">剪刀撑修正系数</param>
        /// <param name="EI">弯曲刚度</param>
        /// <returns>临界荷载</returns>
        static double SolvePcr(double n, double L,double beta_alpha, double beta_L, double beta_J, double EI)
        {
            return 2*n*n*pi*pi*EI*beta_J/Math.Pow(beta_alpha*beta_L*L,2);
        }

        /// <summary>
        /// 计算betaalpha
        /// </summary>
        /// <param name="sdg_h">扫地杆高度</param>
        /// <param name="xbg_h">悬臂杆高度</param>
        /// <param name="h">步距</param>
        /// <param name="nx">受力薄弱区间支架跨数</param>
        /// <returns>betaalpha</returns>
        static double Solvealpha(double sdg_h, double xbg_h, double h, int nx)
        {
            double step1 = 0.0;-+

            double step2 = 0.0;
            double step3 = 0.0;
            switch (nx)
            {
                case 3: 
                    {
                        step1 = 1.0;
                        step2 = 1.036;
                        step3 = 1.144;
                    } break;
                case 4:
                    {
                        step1 = 1.0;
                        step2 = 1.030;
                        step3 = 1.111;
                    } break;
                case 5:
                    {
                        step1 = 1.0;
                        step2 = 1.028;
                        step3 = 1.101;
                    } break;
                case 6:
                    {
                        step1 = 1.0;
                        step2 = 1.026;
                        step3 = 1.096;
                    } break;
                default:
                    {
                        step1 = 1.0;
                        step2 = 1.036;
                        step3 = 1.144;
                    }break;
            }
            double alpha = Math.Max(sdg_h, xbg_h) / h;
            double result = 0.0;
            if (alpha <= 0.2)
                result = step1;
            else if(alpha>0.2 && alpha<=0.4)
            {
                result = (step2 - step1) * (alpha - 0.2) / 0.2 + step1;
            }
            else if (alpha > 0.4 && alpha <= 0.6)
            {
                result = (step3 - step2) * (alpha - 0.4) / 0.2 + step2;
            }
            else
            {
                result = step3;
            }
            return result;
        }
    }
}
