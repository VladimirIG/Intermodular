using System.Windows;

namespace Controller.Logica
{
    public abstract class Fallos
    {
        public static void falloConexionDB()
        {
            MessageBox.Show("No se ha podido conectar con la base de datos, por favor reinicie la aplicación, o inicie la base de datos manualmente.", "Fallo de conexión", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void falloFormato(string campo)
        {
            MessageBox.Show($"El campo \'{campo}\' no tiene el formato adecuado.", "Error en el formato", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void falloPK(string campo)
        {
            MessageBox.Show($"El empleado con DNI/NIE \'{campo}\' ya existe en la base de datos.", "Error de duplicidad", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void multiFalloFormato(string campo)
        {
            MessageBox.Show($"Los siguientes campos poseen errores y no pueden ser validados:" +
                $"\n{campo}.", "Errores en los formatos", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}