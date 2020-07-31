using Controller.Controles;
using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using System.Collections.Generic;
using System.Windows;

namespace View.CRUD.empleado
{
    /// <summary>
    /// Lógica de interacción para actualizarEmpleado.xaml
    /// </summary>
    public partial class actualizarEmpleado : Window
    {
        //--------------------------Campos de la clase
        private List<Empleado> empleados;
        private Empleado empleado;

        private string dni;
        private string passw;
        private string nombre;
        private string apellidos;
        private string rango;
        private string tlf;
        private string disponibilidad;
        private bool activo;
        private bool isadmin;

        //--------------------------Constructor
        public actualizarEmpleado()
        {
            InitializeComponent();

            empleados = EmpleadoController.listarEmpleado();
            rellenarComboBox();
        }

        //--------------------------Botonera
        private void Bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarAsignarCampos())
            {
                if (EmpleadoController.actualizarEmpleado(dni, passw, nombre, apellidos, rango, tlf, disponibilidad
                , isadmin, activo))

                {
                    this.Close();
                }
            }
        }

        private void Bnt_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Métodos principales
        private bool comprobarAsignarCampos()
        {
            string errores = "";
            bool resultado = true;

            if (txt_Password.Text.Length < 3)
            {
                errores += "\n-Contraseña: Debe tener almenos 4 caracteres.";
                resultado = false;
            }
            else
            {
                this.passw = txt_Password.Text;
            }

            if (!InputChecker.isOnlyLetters(txt_Nombre.Text) || txt_Nombre.Text.Length < 3)
            {
                errores += "\n-Nombre: Solo se admiten letras. Debe tener almenos 4 caracteres";
                resultado = false;
            }
            else
            {
                this.nombre = txt_Nombre.Text;
            }

            if (!InputChecker.isOnlyLetters(txt_Apellidos.Text) || txt_Apellidos.Text.Length < 3)
            {
                errores += "\n-Apellidos: Solo se admiten letras. Debe tener almenos 4 caracteres";
                resultado = false;
            }
            else
            {
                this.apellidos = txt_Apellidos.Text;
            }

            if (!InputChecker.isOnlyNumbers(txt_Telefono.Text) || (txt_Telefono.Text.Length < 9))
            {
                errores += "\n-Teléfono: Solo se admiten numeros. Debe tener almenos 9 caracteres";
                resultado = false;
            }
            else
            {
                this.tlf = txt_Telefono.Text;
            }

            this.dni = comb_DNI.Text;
            this.rango = comb_Rango.Text;
            this.disponibilidad = comb_Disponibilidad.Text;
            this.activo = (bool)chk_Activo.IsChecked;
            this.isadmin = (bool)chk_isAdmin.IsChecked;

            if (!resultado)
            {
                Fallos.multiFalloFormato(errores);
                return false;
            }
            return true;
        }

        //--------------------------Métodos auxiliares
        private void rellenarComboBox()
        {
            foreach (Empleado emp in empleados)
            {
                if (!comb_DNI.Items.Contains(emp.dni.ToUpper()))
                {
                    comb_DNI.Items.Add(emp.dni);
                }
            }
        }

        private void refrescarCampos()
        {
            comb_DNI.Text = empleado.dni;
            txt_Password.Text = empleado.passw;
            txt_Nombre.Text = empleado.nombre;
            txt_Apellidos.Text = empleado.apellidos;
            comb_Rango.Text = empleado.rango;
            txt_Telefono.Text = empleado.tlf;
            comb_Disponibilidad.Text = empleado.disponibilidad;
            chk_Activo.IsChecked = empleado.activo;
            chk_isAdmin.IsChecked = empleado.isadmin;
        }

        //--------------------------Eventos
        private void comb_DNI_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            empleado = EmpleadoController.getEmpleado(comb_DNI.SelectedItem.ToString())[0];
            refrescarCampos();
        }
    }
}