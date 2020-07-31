using Controller.Controles;
using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using System;
using System.Collections.Generic;
using System.Windows;

namespace View.CRUD.mesa
{
    public partial class actualizarMesa : Window
    {
        //--------------------------Campos de la clase
        private List<Mesa> mesas;

        private Mesa mesa;
        private int id;
        private string zona;
        private int num_sillas;
        private bool activa;

        //--------------------------Constructor
        public actualizarMesa()
        {
            InitializeComponent();

            mesas = MesaController.listarMesa();
            rellenarComboBox();
        }

        //--------------------------Botonera
        private void Btn_Aceptar_Click_1(object sender, RoutedEventArgs e)
        {
            if (comprobarAsignarCampos())
            {
                if (MesaController.actualizarMesa(id, zona, num_sillas, activa))
                {
                    this.Close();
                }
            }
        }

        private void Btn_Salir_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Métodos principales
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

            this.id = Int32.Parse(comb_Id.Text);
            this.zona = comb_Zona.Text;
            this.activa = (bool)chk_Activa.IsChecked;

            return true;
        }

        //--------------------------Métodos auxiliares
        private void rellenarComboBox()
        {
            foreach (Mesa mesa in mesas)
            {
                comb_Id.Items.Add(mesa.id);
            }
        }

        private void refrescarCampos()
        {
            comb_Id.Text = mesa.id.ToString();
            comb_Zona.Text = mesa.zona;
            updown_N_sillas.Value = mesa.n_sillas;
            chk_Activa.IsChecked = mesa.activa;
        }

        //--------------------------Eventos
        private void comb_Id_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int id = Int32.Parse(comb_Id.SelectedItem.ToString());

            mesa = MesaController.getMesa(id)[0];
            refrescarCampos();
        }
    }
}