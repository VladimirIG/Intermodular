const mysql = require('mysql');

var conexion = mysql.createConnection({
    user: "root",
    password: "admin",
    host: "localhost",
    port: "3306",
    database: "intermodular"
});

module.exports = conexion;