using System.Security.Cryptography;
using System.Text;

public class AesEncryptionUtility : IAesEncryptionUtility
{
    private static readonly string EncryptionKey = "ABC123XYZ890";
    private static readonly string SaltKey = "SALTKEY123";
    private static readonly string VIKey = "@1B2c3D4e5F6g7H8";

    public string Encrypt(string plainText)
    {
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        using (
            var symmetricKey = new RijndaelManaged()
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.Zeros
            }
        )
        {
            var salt = Encoding.ASCII.GetBytes(SaltKey);
            var passwordDeriveBytes = new PasswordDeriveBytes(EncryptionKey, salt);
            var keyBytes = passwordDeriveBytes.GetBytes(256 / 8);

            symmetricKey.BlockSize = 128;
            symmetricKey.KeySize = 256;
            symmetricKey.Key = keyBytes;
            symmetricKey.IV = Encoding.ASCII.GetBytes(VIKey);

            byte[] cipherTextBytes;
            using (var encryptor = symmetricKey.CreateEncryptor())
            using (var memoryStream = new MemoryStream())
            using (
                var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
            )
            {
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                cipherTextBytes = memoryStream.ToArray();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }
    }

    public string Decrypt(string encryptedText)
    {
        byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);

        using (
            var symmetricKey = new RijndaelManaged()
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.None
            }
        )
        {
            var salt = Encoding.ASCII.GetBytes(SaltKey);
            var passwordDeriveBytes = new PasswordDeriveBytes(EncryptionKey, salt);
            var keyBytes = passwordDeriveBytes.GetBytes(256 / 8);

            symmetricKey.BlockSize = 256;
            symmetricKey.KeySize = 256;
            symmetricKey.Key = keyBytes;
            symmetricKey.IV = Encoding.ASCII.GetBytes(VIKey);

            byte[] plainTextBytes;
            int decryptedByteCount;
            using (var decryptor = symmetricKey.CreateDecryptor())
            using (var memoryStream = new MemoryStream(cipherTextBytes))
            using (
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
            )
            {
                plainTextBytes = new byte[cipherTextBytes.Length];
                decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            }
            return Encoding.UTF8
                .GetString(plainTextBytes, 0, decryptedByteCount)
                .TrimEnd("\0".ToCharArray());
        }
    }
}
