namespace Intermodular_MVC_VladimirIriarte.Modelo
{
    public class Mesa
    {
        public int id { get; set; }
        public string zona { get; set; }
        public int n_sillas { get; set; }
        public bool activa { get; set; }

        public Mesa()
        {
        }

        public Mesa(int id, string zona, int n_sillas, bool activa)
        {
            this.id = id;
            this.zona = zona;
            this.n_sillas = n_sillas;
            this.activa = activa;
        }

        public Mesa(string zona, int n_sillas, bool activa)
        {
            this.id = id;
            this.zona = zona;
            this.n_sillas = n_sillas;
            this.activa = activa;
        }
    }
}