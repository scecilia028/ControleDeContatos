using System.Security.Cryptography;
using System.Text;

namespace ControleDeContatos.Helper
{
    public static class Criptografia
    {
        public static string GerarHash(this string valor)
        {
            //this string valor => vira uma extensão da string
            //toda vez que colocar o ponto apos a string ira aparecer
            //este metodo. Ex.: "valor".GerarHash("valor") - vai aparecer
            var hash = SHA1.Create();            
            byte[] array = new ASCIIEncoding().GetBytes(valor); //tranforma em bytes

            array = hash.ComputeHash(array);//transf em hash

            StringBuilder strHexa = new StringBuilder();

            foreach (byte item in array)
            {
                strHexa.Append(item.ToString("x2"));//"x2" significa que será convertido para o formato hexadecimal - se for 13 é 0D 
            }
            return strHexa.ToString();
        }
    }
}
