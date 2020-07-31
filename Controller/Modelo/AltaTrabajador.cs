namespace Controller.Modelo
{
    public class AltaTrabajador
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

        public int numero { get; set; }
        public string numero_ss { get; set; }
        public string comienza { get; set; }
        public string finaliza { get; set; }
        public int horas_semana { get; set; }
        public string tipo { get; set; }
        public string dni_empleado { get; set; }

        public AltaTrabajador()
        {
        }

        public AltaTrabajador(string dni, string passw, string nombre, string apellidos
            , string rango, string tlf, string disponibilidad, bool isadmin, bool activo,
            /**/int numero, string numero_ss, string comienza, string finaliza, int horas_semana
            , string tipo)
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

            this.numero = numero;
            this.numero_ss = numero_ss;
            this.comienza = comienza;
            this.finaliza = finaliza;
            this.horas_semana = horas_semana;
            this.tipo = tipo;
            this.dni_empleado = dni;
        }

        public AltaTrabajador(string dni, string passw, string nombre, string apellidos
            , string rango, string tlf, string disponibilidad, bool isadmin, bool activo,
            /**/string numero_ss, string comienza, string finaliza, int horas_semana
            , string tipo)
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

            this.numero_ss = numero_ss;
            this.comienza = comienza;
            this.finaliza = finaliza;
            this.horas_semana = horas_semana;
            this.tipo = tipo;
            this.dni_empleado = dni;
        }
    }
}