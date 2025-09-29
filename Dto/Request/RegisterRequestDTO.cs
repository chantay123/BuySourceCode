namespace WebBuySource.Dto.Request
{
    public class RegisterRequestDTO
    {
        /// <summary>
        /// Username
        /// </summary>
        /// 
        public string Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Phone { get; set; }
    }
}
