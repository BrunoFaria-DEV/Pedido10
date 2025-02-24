using System.Text.RegularExpressions;

namespace Pedido10.Application.Formater
{
    public static class Helpers
    {
        public static string OnlyNumbers(string exp) 
        {
            return Regex.Replace(exp, "[^0-9]", "");
        }
    }
}
