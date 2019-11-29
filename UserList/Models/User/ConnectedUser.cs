namespace UserList.Models.User
{
    public class ConnectedUser
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public string Type { get; set; }
        public int? UserConnectedId { get; set; }
        public User UserConnected { get; set; }
    }
}