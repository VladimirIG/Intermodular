namespace Intermodular_MVC_VladimirIriarte.Modelo
{
    public class Empleado
    {
        public string dni { get; set; }
        public string passw { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string rango { get; set; }
        public string tlf { get; set; }
        public string disponibilidad { get; set; }
        public bool isadmin { get; set; }
        public bool activo { get; set; }

        public Empleado(string dni, string passw, string nombre, string apellidos
            , string rango, string tlf, string disponibilidad, bool isadmin, bool activo)
        {
            this.dni = dni;
            this.passw = passw;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.rango = rango;
            this.tlf = tlf;
            this.disponibilidad = disponibilidad;
            this.isadmin = isadmin;
            this.activo = activo;
        }
    }
}