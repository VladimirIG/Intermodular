const express = require('express');
var router = express.Router();
const mesa = require('../controllers/mesa');

router.get("/mesa", mesa.list);
router.get("/mesa/:id", mesa.get);
router.post("/mesa", mesa.add);
router.put("/mesa", mesa.update);
router.delete("/mesa/:id", mesa.delete);

module.exports = router;
