var conexion = require('../controllers/conexion');

exports.list = function (req, res) {
    conexion.query("SELECT * FROM contrato", (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });

}

exports.get = function (req, res) {
    var numero = req.params.numero;
    conexion.query("SELECT * FROM contrato WHERE numero =?", [numero], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });

}

exports.add = function (req, res) {
    var datos = JSON.parse(JSON.stringify(req.body));
    conexion.query("INSERT INTO contrato set ?", datos, (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Contrato insertado: ", datos);
        }
    });
    res.end();
}

exports.update = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));
    var datos = {
        numero: json.numero,
        numero_ss: json.numero_ss,
        comienza: json.comienza,
        finaliza: json.finaliza,
        horas_semana: json.horas_semana,
        tipo: json.tipo,
        dni_empleado: json.dni_empleado
    };

    conexion.query("UPDATE contrato set ? WHERE numero = ?", [datos, datos.numero], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Contrato actualizado: ", datos);
        }
    });
    res.end();
}

exports.delete = function (req, res) {
    var numero = req.params.numero;
    conexion.query("DELETE FROM contrato WHERE numero = ?", [numero], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Contrato borrado: ", numero);
        }
    });
    res.end();
} 
