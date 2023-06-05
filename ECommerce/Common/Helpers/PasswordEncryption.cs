namespace Common.Helpers
{
    public static class PasswordEncryption
    {
        public static string EncryptedPassword(string? password)
        {
            var EncryptPassword = BCrypt.Net.BCrypt.HashPassword(password);
            if (EncryptPassword != null)
            {
                return EncryptPassword;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
