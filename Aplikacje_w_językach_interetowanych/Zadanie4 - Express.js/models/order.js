//product.js
const mongoose = require('mongoose');

const orderSchema = mongoose.Schema({
    date: {
        type: Date,
        required: false
    },
    status: {
        type: mongoose.Schema.Types.ObjectId,
        ref: 'orderStatus'
    },
    customer: {
        type: mongoose.Schema.Types.ObjectId,
        ref: 'customer'
    }
});

module.exports = mongoose.model('order', orderSchema);
