using System.Security.Cryptography;
using System.Text;

namespace AESEncryptDecrypt;

public class Algorithm
{

    public static async Task EncryptString(string sourceFilePath, string destinationFilePath, string strPwd)
    {
        
        try
        {
            await using (FileStream fileStream = new(destinationFilePath, FileMode.OpenOrCreate))
            {
                using Aes aes = Aes.Create();
                byte[] key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(strPwd));
                aes.Key = key;

                byte[] iv = aes.IV;
                fileStream.Write(iv, 0, iv.Length);

                await using CryptoStream cryptoStream = new(
                           fileStream,
                           aes.CreateEncryptor(),
                           CryptoStreamMode.Write);
                await using StreamWriter encryptWriter = new(cryptoStream);
                await encryptWriter.WriteLineAsync(await File.ReadAllTextAsync(sourceFilePath, Encoding.UTF8));
            }

            Console.WriteLine("The file was encrypted.");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"The encryption failed.\n\n {exception}");
        }
    }

    public static async Task DecryptString(string sourceFilePath, string destinationFilePath, string strPwd)
    {
        try
        {
            await using FileStream fileStream = new(sourceFilePath, FileMode.Open);
            using Aes aes = Aes.Create();
            byte[] iv = new byte[aes.IV.Length];
            int numBytesToRead = aes.IV.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                int n = fileStream.Read(iv, numBytesRead, numBytesToRead);
                if (n == 0) break;

                numBytesRead += n;
                numBytesToRead -= n;
            }

            byte[] key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(strPwd));

            await using CryptoStream cryptoStream = new(
                       fileStream,
                       aes.CreateDecryptor(key, iv),
                       CryptoStreamMode.Read);
            using StreamReader decryptReader = new(cryptoStream);
            string decryptedMessage = await decryptReader.ReadToEndAsync();
            Console.WriteLine($"The decrypted file text:\n\n {decryptedMessage}");
            await File.WriteAllTextAsync(destinationFilePath, decryptedMessage, Encoding.UTF8);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"The decryption failed.\n\n {exception}");
        }
    }

}