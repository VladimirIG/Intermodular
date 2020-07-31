using Controller.Controles;
using Controller.Logica;
using System;
using System.Windows;
using System.Windows.Controls;

namespace View.CRUD.contrato
{
    /// <summary>
    /// Lógica de interacción para mostrarContrato.xaml
    /// </summary>
    public partial class mostrarContrato : Window
    {
        //--------------------------Campos de la clase
        private ListView listaVista;

        private int num_contrato;

        //--------------------------Constructor
        public mostrarContrato(ListView ListaVentanaPadre)
        {
            InitializeComponent();
            this.listaVista = ListaVentanaPadre;
        }

        //--------------------------Botonera
        private void bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarAsignarCampos())
            {
                this.listaVista.ItemsSource = ContratoController.getContrato(this.num_contrato);
                this.Close();
            }
        }

        private void bnt_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Métodos principales
        private bool comprobarAsignarCampos()
        {
            try
            {
                this.num_contrato = Int32.Parse(txt_Numero.Text);
                return true;
            }
            catch (Exception)
            {
                Fallos.falloFormato("Número del contrato");
                return false;
            }
        }
    }
}