using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace Controller.Controles
{
    public class ComandaController
    {
        public static List<Comanda> listarComanda()
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/comanda", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Comanda>>(response.Content);
        }

        public static List<Comanda> getComanda(int id_pedido, int codigo_ticket, int codigo_empleado)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/comanda/{id_pedido}/{codigo_ticket}/{codigo_empleado}", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Comanda>>(response.Content);
        }

        public static void insertarComanda(int id_pedido, int codigo_ticket, string dni_empleado, string fecha)

        {
            Comanda comanda = new Comanda(id_pedido, codigo_ticket, dni_empleado, fecha);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/comanda", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(comanda);

            rest.Execute(request);
        }

        public static void actualizarComanda(int id_pedido, int codigo_ticket, string dni_empleado, string fecha)
        {
            Comanda comanda = new Comanda(id_pedido, codigo_ticket, dni_empleado, fecha);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/comanda", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(comanda);

            rest.Execute(request);
        }

        public static void deleteComanda(int id_pedido, int codigo_ticket, int codigo_empleado)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/comanda/{id_pedido}/{codigo_ticket}/{codigo_empleado}", Method.DELETE);
            var response = rest.Execute(request);
        }
    }
}