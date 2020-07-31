const express = require('express');
var router = express.Router();
const login = require('../controllers/login');

router.get("/login/:dni/passw", login.list);

module.exports = router;