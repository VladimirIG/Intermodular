using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Windows;

namespace Controller.Controles
{
    public class EmpleadoController
    {
        public static List<Empleado> getEmpleado(string dni)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/empleado/{dni}", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Empleado>>(response.Content);
        }

        public static List<Empleado> listarEmpleado()
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/empleado/", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Empleado>>(response.Content);
        }

        public static bool insertarEmpleado(string dni, string passw, string nombre, string apellidos
            , string rango, string tlf, string disponibilidad, bool isadmin, bool activo)
        {
            if (EmpleadoController.getEmpleado(dni)[0] != null)
            {
                Fallos.falloPK(dni);
                return false;
            }

            Empleado empleado = new Empleado(dni, passw, nombre, apellidos
            , rango, tlf, disponibilidad, isadmin, activo);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/empleado", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(empleado);
            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                MessageBox.Show("Empleado insertado", "Empleado insertado", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                Fallos.falloConexionDB();
                return false;
            }
        }

        public static bool actualizarEmpleado(string dni, string passw, string nombre, string apellidos
            , string rango, string tlf, string disponibilidad, bool isadmin, bool activo)
        {
            if (!dni.Equals("00000000A"))
            {
                Empleado empleado = new Empleado(dni, passw, nombre, apellidos
            , rango, tlf, disponibilidad, isadmin, activo);

                var rest = new RestClient(Connection.LINK);
                var request = new RestRequest("/empleado", Method.PUT);

                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(empleado);
                var resultado = rest.Execute(request);

                if (resultado.IsSuccessful)
                {
                    MessageBox.Show("Empleado Actualizado", "Empleado Actualizado", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    Fallos.falloConexionDB();
                    return false;
                }
            }
            MessageBox.Show("Este usuario no puede ser actualizado", "Excepción de usuario root", MessageBoxButton.OK, MessageBoxImage.Hand);
            return false;
        }

        public static bool deleteEmpleado(string dni)
        {
            if (!dni.Equals("00000000A"))
            {
                var rest = new RestClient(Connection.LINK);
                var request = new RestRequest($"/empleado/{dni}", Method.DELETE);
                var resultado = rest.Execute(request);

                if (resultado.IsSuccessful)
                {
                    if (EmpleadoController.getEmpleado(dni).Count == 0)
                    {
                        MessageBox.Show("Empleado eliminado", "Empleado eliminado", MessageBoxButton.OK, MessageBoxImage.Information);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("El Empleado no ha podido ser eliminado ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    Fallos.falloConexionDB();
                    return false;
                }
            }

            MessageBox.Show("Este usuario no puede ser eliminado", "Excepción de usuario root", MessageBoxButton.OK, MessageBoxImage.Hand);
            return false;
        }
    }
}