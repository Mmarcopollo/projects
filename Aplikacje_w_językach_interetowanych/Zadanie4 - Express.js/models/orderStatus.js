//product.js
const mongoose = require('mongoose');

const orderStatusSchema = mongoose.Schema({
    name: {
        type: String,
        enum: ['NOT APPROVED','APPROVED','CANCELED','COMPLETED'],
        required: true
    }    
});

module.exports = mongoose.model('orderStatus', orderStatusSchema );
