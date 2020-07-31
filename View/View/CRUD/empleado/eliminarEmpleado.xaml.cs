using Controller.Controles;
using Intermodular_MVC_VladimirIriarte.Modelo;
using System.Collections.Generic;
using System.Windows;

namespace View.CRUD.empleado
{
    /// <summary>
    /// Lógica de interacción para eliminarEmpleado.xaml
    /// </summary>
    public partial class eliminarEmpleado : Window
    {
        //--------------------------Campos de la clase
        private List<Empleado> empleados;

        //--------------------------Constructor
        public eliminarEmpleado()
        {
            InitializeComponent();
            empleados = EmpleadoController.listarEmpleado();
            rellenarComboBox();
        }

        //--------------------------Botonera
        private void Bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (EmpleadoController.deleteEmpleado(comb_DNI.Text))
            {
                this.Close();
            }

            this.Close();
        }

        private void Bnt_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Métodos auxiliares
        private void rellenarComboBox()
        {
            foreach (Empleado emp in empleados)
            {
                comb_DNI.Items.Add(emp.dni);
            }
        }
    }
}