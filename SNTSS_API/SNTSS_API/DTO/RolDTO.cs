namespace SNTSS_API.DTO
{
    public class RolDTO
    {
        public int IdRol { get; set; }
        public string NameRol { get; set; } = null!;
        public virtual ICollection<UsersDTO> Users { get; set; }
    }
}
