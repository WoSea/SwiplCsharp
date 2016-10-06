using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SbsSW.SwiPlCs; //thu vien ket noi Prolog voi C#
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Load_Result
{
   public class  LoadResult
    {
        public void LoadFile(string s)
        {
            s = "consult('" + s + "')";
            string query = s.Replace("\\", "//"); //Xu ly duong dan
            string[] p = { "-q", "-f", query };
            PlEngine.Initialize(p);
            try
            {
                PlQuery q = new PlQuery(query);
                Assert.IsTrue(q.NextSolution());
            }
            catch (SbsSW.SwiPlCs.Exceptions.PlException e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString(), "Error");
            }
        }


        public string Result(string s)
        {
            s.Trim();
            Regex r = new Regex(@"[A-Z_][a-zA-Z_]*");
            MatchCollection matches = r.Matches(s);
            string result = "";
            try
            {
                PlQuery q = new PlQuery(s);
                bool HasSolution = false;
                foreach (PlQueryVariables v in q.SolutionVariables)
                {
                    HasSolution = true;
                    foreach (Match match in matches)
                    {
                        result += match.ToString() + " = " + v[match.ToString()].ToString() + ";";
                    }
                }
                if (matches.Count == 0)
                    return HasSolution ? "true" : "false";
                return result;
            }
            catch (SbsSW.SwiPlCs.Exceptions.PlException ex)
            {
                return "Error query: " + ex.Message;
            }
        }

    }
}
