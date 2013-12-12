using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BeerTech.Utility
{
    public class IDGenerator
    {
        public string GetNewID(string prefix)
        {
            var size = 20;
            var random = new Random((int)DateTime.Now.Ticks);
            var builder = new StringBuilder();
            if (!string.IsNullOrEmpty(prefix))
            {
                builder.Append(prefix);
            }
            char c;
            while (builder.Length < size)
            {
                c = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(c);
            }

            return builder.ToString();

            
        }
    }
}