using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStorage.Common
{
    public static class NumberToText
    {
        public static String NumberToTextVN(decimal total)
        {
            try
            {
                string rs = "";
                total = Math.Round(total, 0);
                string[] ch = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
                string[] rch = { "lẻ", "mốt", "", "", "", "lăm" };
                string[] u = { "", "mươi", "trăm", "ngàn", "", "", "triệu", "", "", "tỷ", "", "", "ngàn", "", "", "triệu" };
                string nstr = total.ToString();

                int[] n = new int[nstr.Length];
                int len = n.Length;
                for (int i = 0; i < len; i++)
                {
                    n[len - 1 - i] = Convert.ToInt32(nstr.Substring(i, 1));
                }

                for (int i = len - 1; i >= 0; i--)
                {
                    if (i % 3 == 2)
                    {
                        if (n[i] == 0 && n[i - 1] == 0 && n[i - 2] == 0) continue;
                    }
                    else if (i % 3 == 1) 
                    {
                        if (n[i] == 0)
                        {
                            if (n[i - 1] == 0) { continue; }
                            else
                            {
                                rs += " " + rch[n[i]]; continue;
                            }
                        }
                        if (n[i] == 1)
                        {
                            rs += " mười"; continue;
                        }
                    }
                    else if (i != len - 1)
                    {
                        if (n[i] == 0)
                        {
                            if (i + 2 <= len - 1 && n[i + 2] == 0 && n[i + 1] == 0) continue;
                            rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                            continue;
                        }
                        if (n[i] == 1)
                        {
                            rs += " " + ((n[i + 1] == 1 || n[i + 1] == 0) ? ch[n[i]] : rch[n[i]]);
                            rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                            continue;
                        }
                        if (n[i] == 5) 
                        {
                            if (n[i + 1] != 0)
                            {
                                rs += " " + rch[n[i]];
                                rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                                continue;
                            }
                        }
                    }

                    rs += (rs == "" ? " " : " ") + ch[n[i]];
                    rs += " " + (i % 3 == 0 ? u[i] : u[i % 3]);
                }
                if (rs[rs.Length - 1] != ' ')
                    rs += " đồng";
                else
                    rs += "đồng";

                if (rs.Length > 2)
                {
                    string rs1 = rs.Substring(0, 2);
                    rs1 = rs1.ToUpper();
                    rs = rs.Substring(2);
                    rs = rs1 + rs;
                }
                return rs.Trim().Replace("lẻ,", "lẻ").Replace("mươi,", "mươi").Replace("trăm,", "trăm").Replace("mười,", "mười");
            }
            catch
            {
                return "";
            }

        }
    }
}