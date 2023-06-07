using System.Text;

namespace AESEncryptDecrypt;

public class Program
{

    public static async Task Main(string[] args)
    {

        while (true)
        {
            await ProcessEncryptDecrypt();
        }
    }


    public static async Task ProcessEncryptDecrypt()
    {
        int iChoice = 0;
        string strPwd = string.Empty;
        string sourceFilePath = string.Empty;
        string destinationFilePath = string.Empty;
        
        Console.WriteLine("Enter your choice:");
        Console.WriteLine("1.Encryption   2.Decryption  3.Exit ");
        Console.WriteLine("...............");
        
        iChoice = Convert.ToInt32(Console.ReadLine());

        switch (iChoice)
        {
            case 1:
                Console.WriteLine("Enter the filepath to a file containing the text to encrypt:");
                sourceFilePath = Convert.ToString(Console.ReadLine()!);

                Console.WriteLine("Enter the filepath to a target file:");
                destinationFilePath = Convert.ToString(Console.ReadLine()!);

                Console.WriteLine("Enter the Password:");
                strPwd = Convert.ToString(Console.ReadLine()!);

                await Algorithm.EncryptString(sourceFilePath, destinationFilePath, strPwd);
                break;
            case 2:
                Console.WriteLine("Enter the filepath to a file containing the text to decrypt:");
                sourceFilePath = Convert.ToString(Console.ReadLine()!);

                Console.WriteLine("Enter the filepath to a target file:");
                destinationFilePath = Convert.ToString(Console.ReadLine()!);

                Console.WriteLine("Enter the Password:");
                strPwd = Convert.ToString(Console.ReadLine()!);

                await Algorithm.DecryptString(sourceFilePath, destinationFilePath, strPwd);
                break;
            default:
                Environment.Exit(0);
                break;
        }
    }

}