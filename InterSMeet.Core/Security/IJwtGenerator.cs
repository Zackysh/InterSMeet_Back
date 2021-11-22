namespace InterSMeet.Core.Security
{
    public interface IPasswordGenerators
    {
        public string Hash(string password);
    }
}