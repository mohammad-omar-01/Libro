public interface IAesEncryptionUtility
{
    string Decrypt(string encryptedText);
    string Encrypt(string plainText);
}