using Controller.Controles;
using Controller.Logica;
using System;
using System.Windows;
using System.Windows.Controls;

namespace View.CRUD.producto
{
    /// <summary>
    /// Lógica de interacción para mostrarProducto.xaml
    /// </summary>
    public partial class mostrarProducto : Window
    {
        //--------------------------Campos de la clase
        private ListView listaVista;

        private int codigo;

        //--------------------------Constructor
        public mostrarProducto(ListView ListaVentanaPadre)
        {
            InitializeComponent();
            this.listaVista = ListaVentanaPadre;
        }

        //--------------------------Botonera
        private void bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarAsignarCampos())
            {
                this.listaVista.ItemsSource = ProductoController.getProducto(this.codigo);
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
                this.codigo = Int32.Parse(txt_Codigo.Text);
                return true;
            }
            catch (Exception)
            {
                Fallos.falloFormato("Codigo de producto");
                return false;
            }
        }
    }
}