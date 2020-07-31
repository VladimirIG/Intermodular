const express = require('express');
var router = express.Router();
const producto = require('../controllers/producto');

router.get("/producto", producto.list);
router.get("/producto/:codigo", producto.get);
router.post("/producto", producto.add);
router.put("/producto", producto.update);
router.delete("/producto/:codigo", producto.delete);

module.exports = router;
