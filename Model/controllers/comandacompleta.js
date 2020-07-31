var conexion = require('./conexion');

exports.list = function (req, res) {
    var query = `SELECT com.fecha, com.id_pedido, com.codigo_ticket, com.dni_empleado, tic.precio_total,ped.comensales, emp.nombre, emp.apellidos, tic.comentario 
    FROM comanda com, ticket tic, empleado emp, pedido ped 
    WHERE com.id_pedido = ped.id 
    AND com.codigo_ticket = tic.codigo 
    AND com.dni_empleado = emp.dni`

    conexion.query(query, (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });

}

exports.get = function (req, res) {
    var id_pedido = req.params.id_pedido;
    var codigo_ticket = req.params.codigo_ticket;
    var dni_empleado = req.params.dni_empleado;
    var query = `SELECT com.fecha, com.id_pedido, com.codigo_ticket, com.dni_empleado, tic.precio_total,ped.comensales, emp.nombre, emp.apellidos, tic.comentario 
        FROM comanda com, ticket tic, empleado emp, pedido ped 
        WHERE com.id_pedido = ped.id 
        AND com.codigo_ticket = tic.codigo 
        AND com.dni_empleado = emp.dni
        AND com.id_pedido = ? 
        AND com.codigo_ticket = ? 
        AND com.dni_empleado = ?`

    conexion.query(query, [id_pedido, codigo_ticket, dni_empleado], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });

}

exports.delete = function (req, res) {
    var id_pedido = req.params.id_pedido;
    var codigo_ticket = req.params.codigo_ticket;
    var dni_empleado = req.params.dni_empleado;

    conexion.query("DELETE FROM comanda WHERE id_pedido = ? AND codigo_ticket = ? AND dni_empleado = ?", [id_pedido, codigo_ticket, dni_empleado], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Comanda borrada: ", `Pedido ${id_pedido}, Ticket ${codigo_ticket} , DNI ${dni_empleado}`);
        }
    });
/*
    conexion.query("DELETE FROM linped WHERE id_pedido = ? ", [id_pedido], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Linpeds borrados: ", `Linpeds ${id_pedido}`);
        }
    });
*/
    conexion.query("DELETE FROM pedido WHERE id = ? ", [id_pedido], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Pedido borrado: ", `Pedido ${id_pedido}`);
        }
    });

    conexion.query("DELETE FROM ticket WHERE codigo = ? ", [codigo_ticket], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Ticket borrado: ", `Ticket ${codigo_ticket}`);
        }

    });

    res.end();
}