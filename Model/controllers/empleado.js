var conexion = require('./conexion');

exports.list = function (req, res) {
    conexion.query("SELECT * FROM empleado", (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });

}

exports.get = function (req, res) {
    var dni = req.params.dni;
    conexion.query("SELECT * FROM empleado WHERE dni = ?", [dni], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });

}

exports.add = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));

    conexion.query("INSERT INTO empleado set ?", [json], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Empleado insertado:", json);
        }
    });
    res.end();
}

exports.update = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));
    var datos = {
        dni: json.dni,
        passw: json.passw,
        nombre: json.nombre,
        apellidos: json.apellidos,
        rango: json.rango,
        tlf: json.tlf,
        disponibilidad: json.disponibilidad,
        isadmin: json.isadmin,
        activo: json.activo
    };

    conexion.query("UPDATE empleado set ? WHERE dni = ?", [datos, datos.dni], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Empleado actualizado: ", datos);
        }
    });
    res.end();
}

exports.delete = function (req, res) {
    var dni = req.params.dni;

    conexion.query("DELETE FROM empleado WHERE dni = ?", [dni], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Empleado borrado: ", dni);
        }
    });
    res.end();
}