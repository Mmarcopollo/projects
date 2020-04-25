//product.js
const mongoose = require('mongoose');

const customerSchema = mongoose.Schema({
    name: {
        type: String,
        required: true
    },
    email:{
        type: String,
        required:true
    },
    phone:{
        type: String,
        required:true
    }
});

module.exports = mongoose.model('customer', customerSchema );
