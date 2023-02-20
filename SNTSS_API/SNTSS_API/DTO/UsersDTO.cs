namespace SNTSS_API.DTO
{
    public class UsersDTO
    {
        public int IdUsers { get; set; }
        public string NameUsers { get; set; } = null!;
        public int MatriculaUsers { get; set; }
        public int RolUsers { get; set; }
        public string PasswordUsers { get; set; } = null!;
        public int? DayJobUsers { get; set; }
        public string? CategoryUsers { get; set; }
        public string? CveAdscripcionUsers { get; set; }
        public string? AdscripcionUsers { get; set; }
        public string? StatusUsers { get; set; }
        public string? ShiftUsers { get; set; }
        public string? WorkerContractUsers { get; set; }
        public string? ObservationsUsers { get; set; }
        public string? PhoneUsers { get; set; }
        public string? DirectionUsers { get; set; }
        public virtual RolDTO RolUsersNavigation { get; set; } = null!;
        public virtual ICollection<EscalafonDTO> Escalafons { get; set; }
        public virtual ICollection<Message_has_usersDTO> MessageHasUsers { get; set; }

    }
}
