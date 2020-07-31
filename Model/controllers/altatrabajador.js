var conexion = require('./conexion');

exports.list = function (req, res) {
    conexion.query("SELECT * FROM empleado emp, contrato cont WHERE emp.dni = cont.dni_empleado", (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });

}

exports.get = function (req, res) {
    var dni = req.params.dni;

    conexion.query("SELECT * FROM empleado emp, contrato cont WHERE emp.dni = cont.dni_empleado AND emp.dni = ?", [dni], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });

}

exports.add = function (req, res) {

    var json = JSON.parse(JSON.stringify(req.body));
    var empleado =
    {
        dni: json.dni,
        passw: json.passw,
        nombre: json.nombre,
        apellidos: json.apellidos,
        rango: json.rango,
        tlf: json.tlf,
        disponibilidad: json.disponibilidad,
        isadmin: json.isadmin,
        activo: json.activo
    }
    var contrato =
    {
        numero_ss: json.numero_ss,
        comienza: json.comienza,
        finaliza: json.finaliza,
        horas_semana: json.horas_semana,
        tipo: json.tipo,
        dni_empleado: json.dni
    }

    conexion.query("INSERT INTO empleado set ? ", [empleado], (error, rows) => {
        if (error) {
            console.log(error);

        } else {
            console.log("Empleado insertado", empleado);
            conexion.query("INSERT INTO contrato set ? ", [contrato], (error, rows) => {
                if (error) {
                    console.log(error);
                } else {
                    console.log("Alta insertada:", contrato);
                }
            });

        }
    });

    res.end();
}

exports.update = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));

    var empleado = {
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

    var contrato = {
        numero_ss: json.numero_ss,
        comienza: json.comienza,
        finaliza: json.finaliza,
        horas_semana: json.horas_semana,
        tipo: json.tipo,
        dni_empleado: json.dni_empleado
    }

    conexion.query("UPDATE contrato set ? WHERE dni_empleado = ?", [contrato, empleado.dni], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Contrato actualizado: ", contrato);
        }
    });

    conexion.query("UPDATE empleado set ? WHERE dni = ?", [empleado, empleado.dni], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Empleado actualizado: ", empleado);
        }
    });

    res.end();
}


exports.delete = function (req, res) {

    conexion.query("DELETE FROM contrato WHERE dni_empleado = ?", [req.params.dni], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Contrato borrado:", req.params.dni);
        }
    });

    conexion.query("DELETE FROM empleado WHERE dni = ?", [req.params.dni], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Empleado borrado:", req.params.dni);
        }
    });
    res.end();
}