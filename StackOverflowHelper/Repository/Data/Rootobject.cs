namespace StackOverflowHelper.Repository.Data
{
    public class Rootobject
    {
        public User[] Items { get; set; }
        public bool HasMore { get; set; }
        public int QuotaMax { get; set; }
        public int quota_remaining { get; set; }
    }
}