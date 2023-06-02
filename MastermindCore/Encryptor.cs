using System.Security.Cryptography;
using System.Text;

namespace MastermindCore;

/// <summary>
/// SRC: https://www.godo.dev/tutorials/csharp-md5/
/// </summary>
public static class Encryptor  
{  
    /// <summary>
    /// Encrypts the text.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Md5Hash(string text)  
    {  
        var md5 = MD5.Create();
        md5.ComputeHash(Encoding.ASCII.GetBytes(text));
        var result = md5.Hash;  

        var strBuilder = new StringBuilder();
        //change it into 2 hexadecimal digits for each byte
        foreach (var c in result!) 
            strBuilder.Append(c.ToString("x2"));

        return strBuilder.ToString();  
    }  
}