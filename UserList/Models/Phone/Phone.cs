namespace UserList.Models.Phone
{
    public class Phone
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }

        public int UserId { get; set; }
        public User.User User { get; set; }
    }
}