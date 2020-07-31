namespace Intermodular_MVC_VladimirIriarte.Modelo
{
    public class Contrato
    {
        public int numero { get; set; }
        public string numero_ss { get; set; }
        public string comienza { get; set; }
        public string finaliza { get; set; }
        public int horas_semana { get; set; }
        public string tipo { get; set; }
        public string dni_empleado { get; set; }

        public Contrato()
        {
        }

        public Contrato(int numero, string numero_ss, string comienza, string finaliza, int horas_semana
            , string tipo, string dni_empleado)
        {
            this.numero = numero;
            this.numero_ss = numero_ss;
            this.comienza = comienza;
            this.finaliza = finaliza;
            this.horas_semana = horas_semana;
            this.tipo = tipo;
            this.dni_empleado = dni_empleado;
        }

        public Contrato(string numero_ss, string comienza, string finaliza, int horas_semana
    , string tipo, string dni_empleado)
        {
            this.numero_ss = numero_ss;
            this.comienza = comienza;
            this.finaliza = finaliza;
            this.horas_semana = horas_semana;
            this.tipo = tipo;
            this.dni_empleado = dni_empleado;
        }
    }
}