using Controller.Controles;
using Controller.Logica;
using System;
using System.Windows;

namespace View.CRUD.mesa
{
    /// <summary>
    /// Lógica de interacción para anyadirMesa.xaml
    /// </summary>
    public partial class anyadirMesa : Window
    {
        //--------------------------Campos de la clase
        private string zona;

        private int num_sillas;
        private bool activa;

        //--------------------------Constructor
        public anyadirMesa()
        {
            InitializeComponent();
        }

        //--------------------------Botonera
        private void btn_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarAsignarCampos())
            {
                if (MesaController.insertarMesa(zona, num_sillas, activa))
                {
                    this.Close();
                }
            }
        }

        private void btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Métodos auxiliares
        private bool comprobarAsignarCampos()
        {
            string errores = "";
            bool resultado = true;

            try
            {
                this.num_sillas = (Int32)updown_N_sillas.Value;
            }
            catch (Exception)
            {
                errores += "-Numero de sillas: Solo se admiten numeros.\n";
                resultado = false;
            }

            if (!resultado)
            {
                Fallos.multiFalloFormato(errores);
                return false;
            }
            this.zona = comb_Zona.Text;
            this.activa = (bool)chk_Activa.IsChecked;

            return true;
        }
    }
}