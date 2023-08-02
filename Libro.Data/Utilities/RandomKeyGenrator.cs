using System.Security.Cryptography;

public class RandomKeyGenerator : IRandomKeyGenerator
{
    public byte[] GenerateRandomKey()
    {
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            byte[] keyBytes = new byte[32];
            randomNumberGenerator.GetBytes(keyBytes);
            return keyBytes;
        }
    }
}
