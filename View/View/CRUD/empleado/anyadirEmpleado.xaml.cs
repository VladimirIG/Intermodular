using Controller.Controles;
using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using System.Collections.Generic;
using System.Windows;

namespace View.CRUD.empleado
{
    /// <summary>
    /// Lógica de interacción para anyadirEmpleado.xaml
    /// </summary>
    public partial class anyadirEmpleado : Window
    {
        //--------------------------Campos de la clase
        private List<Empleado> empleados;

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
        public anyadirEmpleado()
        {
            InitializeComponent();

            empleados = EmpleadoController.listarEmpleado();
        }

        //--------------------------Botonera
        private void Bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarAsignarCampos())
            {
                if (EmpleadoController.insertarEmpleado(dni, passw, nombre, apellidos, rango, tlf, disponibilidad
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

            if (!InputChecker.comprobarFormatoDni(txt_DNI.Text))
            {
                errores += "\n-DNI: Formato incorrecto.";
                resultado = false;
            }
            else
            {
                this.dni = txt_DNI.Text;
            }

            if (txt_Password.Text.Length < 3)
            {
                errores += "\n -Contraseña: Debe tener almenos 4 caracteres.";
                resultado = false;
            }
            else
            {
                this.passw = txt_Password.Text;
            }

            if (!InputChecker.isOnlyLetters(txt_Nombre.Text) || txt_Nombre.Text.Length < 3)
            {
                errores += "\n -Nombre: Solo se admiten letras. Debe tener almenos 4 caracteres";
                resultado = false;
            }
            else
            {
                this.nombre = txt_Nombre.Text;
            }

            if (!InputChecker.isOnlyLetters(txt_Apellidos.Text) || txt_Apellidos.Text.Length < 3)
            {
                errores += "\n -Apellidos: Solo se admiten letras. Debe tener almenos 4 caracteres";
                resultado = false;
            }
            else
            {
                this.apellidos = txt_Apellidos.Text;
            }

            if (!InputChecker.isOnlyNumbers(txt_Telefono.Text) || (txt_Telefono.Text.Length < 9))
            {
                errores += "\n -Teléfono: Solo se admiten numeros. Debe tener almenos 9 caracteres";
                resultado = false;
            }
            else
            {
                this.tlf = txt_Telefono.Text;
            }

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
    }
}