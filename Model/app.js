const colors = require('colors');
const path = require('path');
const favicon = require('serve-favicon');
const morgan = require('morgan');
const methodOverride = require('method-override');
const cookieParser = require('cookie-parser');

const bodyParser = require('body-parser');
const express = require('express');

//// ROUTES ////
var empleadoRoutes = require('./routes/empleado');
var contratoRoutes = require('./routes/contrato');
var mesaRoutes = require('./routes/mesa');

var pedidoRoutes = require('./routes/pedido');
var linpedRoutes = require('./routes/linped');
var productoRoutes = require('./routes/producto');
var ticketRoutes = require('./routes/ticket');
var comandaRoutes = require('./routes/comanda');
var altatrabajadorRoutes = require('./routes/altatrabajador');
var comandacompletaRoutes = require('./routes/comandacompleta');


//// START ////
var app = express();

//// SETTINGS ////
app.set('port', /*process.env.PORT ||*/ 3000) //por si azure te da un puerto predeterminado
app.set('json spaces', 2);
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');

//// MIDDLEWARE ////
app.use(morgan('dev'));

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(require('stylus').middleware(path.join(__dirname, 'public')));
app.use(express.json());

app.use('/', empleadoRoutes);
app.use('/', contratoRoutes);
app.use('/', mesaRoutes);

app.use('/', pedidoRoutes);
app.use('/', linpedRoutes);
app.use('/', productoRoutes);
app.use('/', ticketRoutes);
app.use('/', comandaRoutes);
app.use('/', altatrabajadorRoutes);
app.use('/', comandacompletaRoutes);

app.use((req, res, next) => {
    var err = new Error('Not Found');
    err.status = 404;
    next(err);
});


app.listen(app.get('port'), () => {
    console.log('[*]SERVIDOR INICIADO en puerto', app.get('port'));
})



