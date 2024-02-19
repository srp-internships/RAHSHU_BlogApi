namespace RAHSHU_BlogApi.Dtos.SeedDto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public CompanyDto Company { get; set; }
        public AddressDto Address { get; set; }
    }

    public class CompanyDto
    {
        public string Name { get; set; } = null!;
    }

    public class AddressDto
    {
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
    }
}
