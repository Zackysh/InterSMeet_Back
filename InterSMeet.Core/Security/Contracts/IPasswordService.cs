namespace InterSMeet.Core.Security
{
    public interface IPasswordService
    {
        public string Hash(string password);
        public bool CompareHash(string attemptedPassword, string hash);
    }
}