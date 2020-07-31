DROP TABLE IF EXISTS comanda;
DROP TABLE IF EXISTS contrato;
DROP TABLE IF EXISTS linped;
DROP TABLE IF EXISTS pedido;
DROP TABLE IF EXISTS ticket;
DROP TABLE IF EXISTS empleado;
DROP TABLE IF EXISTS producto;
DROP TABLE IF EXISTS mesa;


CREATE TABLE mesa
(
    id serial NOT NULL,
    zona VARCHAR(15),
        CHECK(UPPER(zona) in ('TERRAZA', 'P0', 'P1', 'P2')),
    n_sillas smallint,
        CHECK(n_sillas > 0),
    activa TINYINT(1),

    CONSTRAINT PK_mesa PRIMARY KEY (id)
);

CREATE TABLE producto
(
    codigo serial NOT NULL,
    nombre VARCHAR(30),
        CHECK(nombre REGEXP '^[a-zA-Z_ ]*$'),
    categoria VARCHAR(20),
        CHECK(UPPER(categoria) in ('REFRESCO','VINO','CERVEZA','ESPIRITUOSA','ENTRANTE','PRINCIPAL','POSTRE')),
    especificaciones text,
    precio decimal(6,2),
        CHECK(precio >= 0),
    activo TINYINT(1),
    imagen VARCHAR(100),
       
    CONSTRAINT PK_producto PRIMARY KEY (codigo)
);

CREATE TABLE empleado
(
    dni VARCHAR(9),
        CHECK(dni REGEXP '^[0-9]{8,8}[A-Za-z]$' OR dni REGEXP '[XYZ][0-9]{7}[A-Z]'),
    passw VARCHAR(25),
    nombre VARCHAR(20),
        CHECK(nombre REGEXP '^[a-zA-Z_ ]*$'),
    apellidos VARCHAR(75),
        CHECK(apellidos REGEXP '^[a-zA-Z_ ]*$'),
    rango VARCHAR(20),
        CHECK(UPPER(rango) in ('CAMARERO','COCINERO','METRE','PINCHE','LAVAPLATOS','JEFECOCINA','BARMAN')),
    tlf VARCHAR(13),
    disponibilidad VARCHAR(30),
        CHECK(UPPER(disponibilidad) in ('VERANOS','SEMANASANTA','NAVIDADES','FESTIVOS','ANUAL','X')),
    isadmin TINYINT(1),
    activo TINYINT(1),
    
    CONSTRAINT PK_empleado PRIMARY KEY(dni)
);

CREATE TABLE ticket
(
    codigo serial NOT NULL,
    precio_total decimal(6,2),
        CHECK(precio_total >= 0),
    comentario text,

    CONSTRAINT PK_ticket PRIMARY KEY (codigo)
);

CREATE TABLE pedido
(
    id serial NOT NULL,
    comensales smallint,
        CHECK(comensales > 0),
    id_mesa BIGINT UNSIGNED NOT NULL,

    CONSTRAINT PK_pedido PRIMARY KEY (id),
    CONSTRAINT FK_pedido_id_mesa FOREIGN KEY (id_mesa) REFERENCES mesa(id)
);

CREATE TABLE linped 
(
    id_pedido BIGINT UNSIGNED NOT NULL,
    linea BIGINT UNSIGNED NOT NULL,
    cantidad smallint,
        CHECK(cantidad > 0),
    codigo_producto BIGINT UNSIGNED NOT NULL,
    importe decimal(6,2),

    CONSTRAINT PK_linped PRIMARY KEY (id_pedido, linea),
    CONSTRAINT FK_linped_id_pedido FOREIGN KEY (id_pedido) REFERENCES pedido (id)  ON DELETE CASCADE,
    CONSTRAINT FK_linped_codigo_producto FOREIGN KEY (codigo_producto) REFERENCES producto (codigo)
);

CREATE TABLE contrato
(
    numero serial NOT NULL,
    numero_ss VARCHAR(12),
    comienza date,
    finaliza date,
        CHECK(comienza < finaliza),
    horas_semana smallint,
            CHECK(horas_semana > 0),
    tipo VARCHAR(15),
        CHECK(UPPER(tipo) in ('INDEFINIDO','TEMPORAL','PRACTICAS','OBRASERVICIO','EVENTUAL','RELEVO')),
    dni_empleado VARCHAR(9),

    CONSTRAINT PK_contrato PRIMARY KEY (numero),
    CONSTRAINT UK_contrato_dni_empleado UNIQUE (dni_empleado),
    CONSTRAINT FK_contrato_dni_empleado FOREIGN KEY (dni_empleado) REFERENCES empleado (dni)
);

CREATE TABLE comanda
(
    id_pedido BIGINT UNSIGNED NOT NULL,
    codigo_ticket BIGINT UNSIGNED NOT NULL,
    dni_empleado VARCHAR(9) NOT NULL,
    fecha timestamp,
        CHECK(fecha > '2019-01-01'),

    CONSTRAINT PK_comanda PRIMARY KEY (id_pedido, codigo_ticket),
    CONSTRAINT UK_comanda_idpedido_codigoticket UNIQUE (id_pedido, codigo_ticket),
    CONSTRAINT UK_comanda_codigoticket_dniempleado UNIQUE (codigo_ticket, dni_empleado),

    CONSTRAINT FK_comanda_pedido FOREIGN KEY (id_pedido) REFERENCES pedido (id),
    CONSTRAINT FK_comanda_ticket FOREIGN KEY (codigo_ticket) REFERENCES ticket (codigo),
    CONSTRAINT FK_comanda_empleado FOREIGN KEY (dni_empleado) REFERENCES empleado (dni)
);
