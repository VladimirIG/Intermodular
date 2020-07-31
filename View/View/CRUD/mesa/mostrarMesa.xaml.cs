using Controller.Controles;
using Controller.Logica;
using System;
using System.Windows;
using System.Windows.Controls;

namespace View.CRUD.mesa
{
    /// <summary>
    /// Lógica de interacción para mostrarMesa.xaml
    /// </summary>
    public partial class mostrarMesa : Window
    {
        //--------------------------Campos de la clase
        private ListView listaVista;

        private int id_mesa;

        //--------------------------Constructor
        public mostrarMesa(ListView ListaVentanaPadre)
        {
            InitializeComponent();
            this.listaVista = ListaVentanaPadre;
        }

        //--------------------------Botonera
        private void btn_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarAsignarCampos())
            {
                this.listaVista.ItemsSource = MesaController.getMesa(this.id_mesa);
                this.Close();
            }
        }

        private void btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Métodos principales
        private bool comprobarAsignarCampos()
        {
            try
            {
                this.id_mesa = Int32.Parse(txt_Id.Text);
                return true;
            }
            catch (Exception)
            {
                Fallos.falloFormato("ID de la mesa");
                return false;
            }
        }
    }
}