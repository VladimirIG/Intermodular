using Controller.Logica;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Windows;

namespace Controller.Controles
{
    public class ComandaCompletaController
    {
        public static List<ComandaCompleta> listarComandaCompleta()
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/comandacompleta", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<ComandaCompleta>>(response.Content);
        }

        public static List<ComandaCompleta> getComandaCompleta(int id_pedido, int codigo_ticket, string dni_empleado)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/comandacompleta/{id_pedido}/{codigo_ticket}/{dni_empleado}", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<ComandaCompleta>>(response.Content);
        }

        public static bool deleteComanda(int id_pedido, int codigo_ticket, string dni_empleado)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/comandacompleta/{id_pedido}/{codigo_ticket}/{dni_empleado}", Method.DELETE);

            if (ComandaCompletaController.getComandaCompleta(id_pedido, codigo_ticket, dni_empleado).Count == 0)
            {
                MessageBox.Show("La Comanda no ha podido ser encontrada ", "Registro inexistente", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                if (ComandaCompletaController.getComandaCompleta(id_pedido, codigo_ticket, dni_empleado).Count == 0)
                {
                    MessageBox.Show("Comanda eliminada", "Comanda eliminada", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("La Comanda no ha podido ser eliminada ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

            }
            else
            {
                Fallos.falloConexionDB();
                return false;
            }
        }
    }
}