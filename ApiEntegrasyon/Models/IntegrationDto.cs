namespace ApiEntegrasyon.Models
{
    public class IntegrationRequest
    {
        public string Req { get; set; }
        public DateTime EntryDateReq { get; set; }
    }
    public class IntegrationDto
    {
        public int UserCode { get; set; }
        public int? Password { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string MobilePhone { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public decimal Balance { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string Address { get; set; }
        public List<string> Roles { get; set; }
    }

}
