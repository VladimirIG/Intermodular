using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Windows;

namespace Controller.Controles
{
    public class TicketController
    {
        public static List<Ticket> listarTicket()
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/ticket", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Ticket>>(response.Content);
        }

        public static List<Ticket> getTicket(int codigo)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/ticket/{codigo}", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Ticket>>(response.Content);
        }

        public static void insertarTicket(int codigo, decimal precio_total, string comentario)
        {
            Ticket ticket = new Ticket(codigo, precio_total, comentario);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/ticket", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(ticket);

            rest.Execute(request);
        }

        public static void insertarTicket(decimal precio_total, string comentario)
        {
            Ticket ticket = new Ticket(precio_total, comentario);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/ticket", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(ticket);

            rest.Execute(request);
        }

        public static void actualizarTicket(int codigo, decimal precio_total, string comentario)
        {
            Ticket ticket = new Ticket(codigo, precio_total, comentario);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/ticket", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(ticket);

            rest.Execute(request);
        }

        public static bool deleteTicket(int codigo)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/ticket/{codigo}", Method.DELETE);

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                if (ProductoController.getProducto(codigo).Count == 0)
                {
                    MessageBox.Show("Producto eliminado", "Producto eliminado", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("El Producto no ha podido ser eliminado ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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