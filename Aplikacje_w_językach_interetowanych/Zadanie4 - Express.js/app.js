//app.js
// załadowanie modułów dodatkowych i plików aplikacji
const express = require('express');
// moduł do obsługi routing
const path = require('path');
const bodyParser = require('body-parser');
const cookieParser = require('cookie-parser');
// nasz plik definiujący odpowiedzi dla ścieżek
const routes = require('./routes/index');

const mongoose = require('mongoose');
require('dotenv/config');

const app = express();


app.use(function(req, res, next) {
    res.header("Access-Control-Allow-Origin", "*"); // update to match the domain you will make the request from
    res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
    next();
  });

// app.set('views ',path.join(__dirname,'views'));
// app.set('view engine', 'pug');

// konfiguracja parserów
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended:true}));
app.use(cookieParser());

// konfiguracja routera dla wszystkich ścieżek
app.use('/', routes);

// connect do db
mongoose.connect(
    'mongodb+srv://user:userPassword@rest-hvwby.mongodb.net/test?retryWrites=true&w=majority', 
    { useNewUrlParser: true },
    ()=> console.log('connected do DB!')
);

// app.use(( req, res, next) =>{
//   res.status(404).render('404');
// });

module.exports = app;