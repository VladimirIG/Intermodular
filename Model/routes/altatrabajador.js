const express = require('express');
var router = express.Router();
const altatrabajador = require('../controllers/altatrabajador');

router.get("/altatrabajador", altatrabajador.list);
router.get("/altatrabajador/:dni", altatrabajador.get);
router.post("/altatrabajador", altatrabajador.add);
router.put("/altatrabajador", altatrabajador.update);
router.delete("/altatrabajador/:dni", altatrabajador.delete);

module.exports = router;
