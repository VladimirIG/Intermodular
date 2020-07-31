INSERT INTO mesa
    (zona, n_sillas, activa)

VALUES
    ('TERRAZA', 4, 0),
    ('TERRAZA', 4, 0),
    ('TERRAZA', 4, 1),
    ('TERRAZA', 4, 1),

    ('P0', 8, 1),
    ('P0', 8, 1),
    
    ('P1', 6, 1),
    ('P1', 6, 1),
    ('P1', 4, 1),
    ('P1', 4, 1),

    ('P2', 4, 0),
    ('P2', 6, 0)
;

INSERT INTO producto
    (nombre, categoria, especificaciones, precio, activo, imagen)

VALUES
    ('TAQUITOS' ,                    	'ENTRANTE',		    'Aqui van los ingredientes',    8.60,   1,  'IMG/entrante/taquitos.jpg'),
    ('GUACAMOLE',                    	'ENTRANTE',		    'Aqui van los ingredientes',    6.90,   1,  'IMG/entrante/guacamole.jpg'),
    ('NACHOS',                   		'ENTRANTE',	        'Aqui van los ingredientes',    5.40,  1,  'IMG/entrante/nachos.jpg'),
    ('PATE OLIVADA',                    'ENTRANTE',	        'Aqui van los ingredientes',    6.00,   1,  'IMG/entrante/olivada.jpg'),

    ('ENCHILADA',                       'PRINCIPAL',        'Aqui van los ingredientes',    12.90,  1,  'IMG/principal/enchilada.jpg'),
    ('QUESADILLAS',                     'PRINCIPAL',        'Aqui van los ingredientes',    10.40,  1,  'IMG/principal/quesadilla.jpg'),

    ('COCA COLA',                       'REFRESCO',	    	'Aqui van los ingredientes',    1.50,   1,  'IMG/bebida/coca.png'),
    ('FANTA NARANJA',                   'REFRESCO',	        'Aqui van los ingredientes',    1.50,   1,  'IMG/bebida/naranja.png'),
    ('FANTA LIMON',                     'REFRESCO',	    	'Aqui van los ingredientes',    1.50,   1,  'IMG/bebida/limon.png'),
    ('AGUA SOLAN',                      'REFRESCO',	    	'Aqui van los ingredientes',    1.50,   1,  'IMG/bebida/solan.png'),

    ('PASTEL FRESAS',                   'POSTRE',		    'Aqui van los ingredientes',    3.50,   1,  'IMG/postre/pastelfresas.jpg'),
    ('PASTEL ZANAHORIA',                'POSTRE',	        'Aqui van los ingredientes',    3.50,   1,  'IMG/postre/pastelzanahoria.jpg'),
    ('TIRAMISU',                        'POSTRE',			'Aqui van los ingredientes',    4.00,   1,  'IMG/postre/tiramisu.jpg'),
    ('HELADO CHOCOLATE',                'POSTRE',	        'Aqui van los ingredientes',    2.50,   1,  'IMG/postre/heladochoco.jpg'),
    ('CAFE SOLO',                       'POSTRE',			'Aqui van los ingredientes',    2.50,   1,  'IMG/postre/cafesolo.jpg'),

    ('JACK DANIELS',                    'ESPIRITUOSA',	    'Aqui van los ingredientes',    4.50,   1,  'IMG/espirituosa/jackdaniels.png'),
    ('JOSE CUERVO',                     'ESPIRITUOSA',	    'Aqui van los ingredientes',    4.00,   1,  'IMG/espirituosa/josecuervo.png'),

    ('ESTRELLA GALICIA',                'CERVEZA',	        'Aqui van los ingredientes',    1.50,   1,  'IMG/cerveza/estrellagalicia.jpg'),
    ('BUDWEISER',                       'CERVEZA',  		'Aqui van los ingredientes',    1.50,   0,  'IMG/cerveza/budweiser.png'),
    ('HEINIKEN',                        'CERVEZA',			'Aqui van los ingredientes',    1.80,   0,  'IMG/cerveza/heineken.jpg'),
    ('PAULANER',                        'CERVEZA',			'Aqui van los ingredientes',    2.70,   1,  'IMG/cerveza/paulaner.jpg'),

    ('CAMPO VIEJO',                     'VINO',  		    'Aqui van los ingredientes',    2.50,   1,  'IMG/vino/campoviejorioja.jpg'),
    ('EMPIRE',                          'VINO',			    'Aqui van los ingredientes',    3.00,   1,  'IMG/vino/empire.png'),
    ('TORRE ORIA',                      'VINO',			    'Aqui van los ingredientes',    2.70,   1,  'IMG/vino/torreoria.jpg')
;

INSERT INTO empleado
    (dni, passw, nombre, apellidos, rango, tlf, disponibilidad, isadmin, activo)

VALUES
    ('00000000A','admin', 'root', 'root', 'JEFECOCINA', '000000000', 'X',  1,  0),
    ('12345678y', 'pass', 'VLADIMIR', 'IRIARTE', 'METRE' , '694381436', 'ANUAL', 1, 1),
    ('29999419o', 'pass29999419o', 'VIOLA', 'MCDANIEL COX', 'CAMARERO', '601623988', 'ANUAL', 0, 1),
    ('48508709o', 'pass48508709o', 'FRANKLIN', 'WADE HUBBARD', 'BARMAN', '608559381', 'ANUAL', 0, 1),
    ('X4091445P', 'passX4091445p', 'CECELIA', 'STEWART STRICKLAND', 'PINCHE' , '691950513', 'ANUAL', 0, 0)
;

INSERT INTO ticket
    (precio_total,  comentario)

VALUES
    (145.88, null ),
    (209.20, null ),
    (263.08, null ),
    (294.57, null ),
    (189.99, 'La reina Leticia y sus escoltas han cenado aquí' ),
    (258.37, 'No pudimos ofrecer TACOS por falta de ingredientes'),
    (169.75, null ),
    (79.36,  null ),
    (169.83, null ),
    (200.51,  null ),
    (193.74, null ),
    (58.34,  null )
;

INSERT INTO pedido
    (comensales, id_mesa)

VALUES
    (2, 1),
    (2, 2),
    (4, 3),
    (4, 4),
    (2, 5),
    (4, 6),
    (8, 7),
    (8, 8),
    (4, 9),
    (4, 10),
    (4, 11),
    (4, 12),
    (2, 1),
    (2, 2)
;

INSERT INTO linped
    ( id_pedido, linea,  cantidad, importe, codigo_producto)

VALUES
    (1, 1,  2,    4.5,   2),
    (1, 2,  1,    2.5,   3),
    (2, 1,  1,    2,     1),
    (2, 2,  3,    3,     8),
    (2, 3,  1,    3,     7)
;

INSERT INTO contrato
    (numero_ss, comienza, finaliza, horas_semana, tipo, dni_empleado)

VALUES
    ('34475', '2010-1-2', '2014-1-2', 21, 'TEMPORAL', '12345678y'),
    ('51582', '2013-9-2','2014-9-2', 21, 'TEMPORAL', '29999419o'),
    ('46319', '2011-9-11','2014-9-11', 21, 'TEMPORAL', '48508709o'),
    ('27534', '2016-11-19', '2017-11-1', 21, 'INDEFINIDO', 'X4091445P')
;

INSERT INTO comanda
    (id_pedido, codigo_ticket, dni_empleado, fecha)

VALUES
    (1, 1, '12345678y', '2019-05-13 13:15:31'),
    (2, 2, '29999419o', '2019-05-13 14:00:31' ),
    (3, 3, '48508709o', now() ),
    (4, 4, 'X4091445P', '2019-05-13 17:15:31.123456789' ),
    (5, 5, '12345678y', '2019-05-13 18:05:31.123456789' )        
;

--------------------- USUARIO ROOT PARA EVITAR QUE EL ENCARGADO SE BORRE ASÏ MISMO

INSERT INTO empleado
    (dni, passw, nombre, apellidos, rango, tlf, disponibilidad, isadmin, activo)

VALUES
    ('00000000A','admin', 'root', 'root', 'JEFECOCINA', '000000000', 'X',  1,  0);