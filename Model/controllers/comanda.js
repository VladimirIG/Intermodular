var conexion = require('./conexion');

exports.list = function (req, res) {
    debugger;
    conexion.query("SELECT * FROM comanda", (error, rows) => {
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

    conexion.query("SELECT * FROM comanda WHERE id_pedido = ? AND codigo_ticket = ? AND dni_empleado = ?", [id_pedido, codigo_ticket, dni_empleado], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });
}

exports.add = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));

    conexion.query("INSERT INTO comanda set ?", [json], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Comanda insertada: ", `Pedido ${json.id_pedido}, Ticket ${json.codigo_ticket} , DNI ${json.dni_empleado}`);
        }
    });
    res.end();
}

exports.update = function (req, res) {

    var json = JSON.parse(JSON.stringify(req.body));
    var datos = {
        id_pedid: json.id_pedid,
        codigo_ticket: json.codigo_ticket,
        dni_empleado: json.dni_empleado,
        fecha: json.fecha
    };

    conexion.query("UPDATE comanda set ? WHERE id_pedido = ? AND codigo_ticket = ? AND dni_empleado = ?", [datos, datos.id_pedido, datos.codigo_ticket, datos.dni_empleado], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Comanda actualizada: ", datos);
        }
    });
    res.end();
}

exports.delete = function (req, res) {
    var id_pedido = req.params.id_pedido;
    var codigo_ticket = req.params.codigo_ticket;
    var dni_empleado = req.params.dni_empleado;

    conexion.query("DELETE FROM comanda WHERE id_pedido = ? AND codigo_ticket = ? AND dni_empleado = ?", [id_pedido, codigo_ticket, dni_empleado], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Comanda borrada:", `Pedido ${id_pedido}, Ticket ${codigo_ticket} , DNI ${dni_empleado}`);
        }
    });
    res.end();
}