using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class AES256Cipher
{
    private static RijndaelManaged aes = new RijndaelManaged();

    public static void SetKey(string key)
    {
        aes.KeySize = 256;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = Encoding.UTF8.GetBytes(key.Substring(0, 16));
    }

    public static String Encrypt(String Input)
    {
        var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
        byte[] xBuff = null;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
            {
                byte[] xXml = Encoding.UTF8.GetBytes(Input);
                cs.Write(xXml, 0, xXml.Length);
            }

            xBuff = ms.ToArray();
        }

        return BitConverter.ToString(xBuff).Replace("-", "");
    }

    public static String Decrypt(String Input)
    {
        var decrypt = aes.CreateDecryptor();
        byte[] xBuff = null;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
            {
                byte[] xXml = StringToByteArray(Input);
                cs.Write(xXml, 0, xXml.Length);
            }

            xBuff = ms.ToArray();
        }

        String Output = Encoding.UTF8.GetString(xBuff);
        return Output;
    }

    public static byte[] StringToByteArray(string hex)
    {
        //return Enumerable.Range(0, hex.Length)
        //                 .Where(x => x % 2 == 0)
        //                 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
        //                 .ToArray();

        byte[] bytes = new byte[hex.Length / 2];
        for (int i = 0; i < hex.Length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }
        return bytes;
    }



}
