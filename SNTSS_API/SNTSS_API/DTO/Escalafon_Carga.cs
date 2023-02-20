namespace SNTSS_API.DTO
{
    public class Escalafon_Carga
    {
        public int No_Prog { get; set; }
        public string Nombre { get; set; }
        public int Matricula { get; set; }
        public DateTime Fecha_de_Registro { get; set; }
        public int Grupo { get; set; }
        public float Calificacion { get; set; }
        public int Tipo_de_Contratacion {get; set;}
        public int Dias_Laborados { get; set; }
        public string Estatus { get; set; }
        public string Observaciones { get; set; }
    }
}
