//product.js
const mongoose = require('mongoose');

const productListSchema = mongoose.Schema({
    what: {
        type: mongoose.Schema.Types.ObjectId,
        ref: 'products'},
    order: {
        type: mongoose.Schema.Types.ObjectId,
        ref:'order'},
    number: {
        type: Number,
        required: true
    }
});

module.exports = mongoose.model('productList', productListSchema );
