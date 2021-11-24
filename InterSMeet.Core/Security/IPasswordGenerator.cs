namespace InterSMeet.Core.Security
{
    public interface IPasswordGenerator
    {
        public string Hash(string password);
        public bool CompareHash(string attemptedPassword, string hash);
    }
}