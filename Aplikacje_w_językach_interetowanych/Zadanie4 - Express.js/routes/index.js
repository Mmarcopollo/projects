const express = require('express');
const router = express.Router();

const product = require('../models/product');
const categories = require('../models/categories');
const order = require('../models/order');
const status = require('../models/orderStatus');
const customer = require('../models/customer');
const productList = require('../models/productList');

const { check, validationResult } = require('express-validator/check');

// exports.validate = [
//     check('price').isNumeric({ gt: 0.0 }).withMessage('Price have to be a positive number!'),
//     check('name').trim().isLength({ min: 1 }).withMessage('Name is required!')
// ]

////////PRODUCT METHODS/////////////////////////////
//All products
router.get('/products', async (req, res) => {
    try {
        const prod = await product.find();
        res.json(prod);
    } catch (err) {
        res.json({ message: err });
    }
});

//By ID
router.get('/products/:prodId', async (req, res) => {
    try {
        const prod = await product.findById(req.params.prodId);
        res.json(prod);
    } catch (err) {
        res.json({ message: err });
    }
});

//add product
router.post('/products', [
    check('price').isFloat({ gt: 0.0 }).withMessage('Price have to be a positive number!'),
    check('weight').isFloat({ gt: 0.0 }).withMessage('Weight have to be a positive number!'),
    check('name').trim().isLength({ min: 1 }).withMessage('Name is required!'),
    check('description').trim().isLength({ min: 1 }).withMessage('Description is required!')
], (req, res) => {
    const errors = validationResult(req)
    if (!errors.isEmpty()) {
        return res.status(422).json({ errors: errors.array() })
    }
    const prod = new product({
        name: req.body.name,
        description: req.body.description,
        price: req.body.price,
        weight: req.body.weight,
        category: req.body.category
    });
    const savedProd = prod.save();
    res.json(savedProd);
    res.json({ message: err });

});


router.patch("/products/:prodId", [
    check('prodId').isMongoId().withMessage("Product ID desn't exist"),
    check('price').isFloat({ gt: 0.0 }).withMessage('Price have to be a positive number!'),
    check('weight').isFloat({ gt: 0.0 }).withMessage('Weight have to be a positive number!'),
    check('name').trim().isLength({ min: 1 }).withMessage('Name is required!'),
    check('description').trim().isLength({ min: 1 }).withMessage('Description is required!')
], async (req, res) => {
    const errors = validationResult(req)
    if (!errors.isEmpty()) {
        return res.status(400).json({ errors: errors.array() })
    }
    try {
        const updatedProd = await product.updateOne(
            { _id: req.params.prodId },
            {
                $set: {
                    price: req.body.price,
                    description: req.body.description,
                    name: req.body.name,
                    weight: req.body.weight
                }
            }
        );
        res.json(updatedProd);
    } catch (err) {
        res.json({ message: err });
    }
})

//DELETE PRODUCT BY ID
router.delete("/products/:prodId", async (req, res) => {
    try {
        const removedProd = await product.remove({ _id: req.params.prodId });
        res.json(removedProd);
    } catch (err) {
        res.json({ message: err });
    }
});


///////////Caregory methods////////////////////
//Get all categories
router.get('/categories', async (req, res) => {
    try {
        const prod = await categories.find();
        res.json(prod);
    } catch (err) {
        res.json({ message: err });
    }
});

//////////Order methods///////////////////////////
//Get all orders list
router.get('/orders', async (req, res) => {
    try {
        const prod = await order.find();
        res.json(prod);
    } catch (err) {
        res.json({ message: err });
    }
});
//Add order
router.post('/orders', [
    check('customer').trim().isLength({ min: 1 }).withMessage('Customer have to be add!')
], (req, res) => {
    const errors = validationResult(req)
    if (!errors.isEmpty()) {
        return res.status(400).json({ errors: errors.array() })
    }
    const ord = new order({
        date: req.body.date,
        status: req.body.status,
        customer: req.body.customer
    });
    const savedOrder = ord.save();
    res.json(savedOrder);
    res.json({ message: err });

});


//Update order status
router.patch("/orders/:id", [
    check('id').isMongoId().withMessage("Order doesn't exist")
], async (req, res) => {
    const errors = validationResult(req)
    if (!errors.isEmpty()) {
        return res.status(400).json({ errors: errors.array() })
    }

    var bodyNumber;
    if(req.body.status == "5e2d6ca699165b424c3ac310")//not aproved
    {
        bodyNumber = 1
    }
    if(req.body.status == "5e2c9e9ff6deb7347c562c5f")// approved
    {
        bodyNumber = 2;
    }
    if(req.body.status == "5e253e903c8db40860674f8b")// completed
    {
        bodyNumber = 3;
    }
    if( req.body.status == "5e2d6c9399165b424c3ac30f")// canceled
    {
        bodyNumber = 4;
    }

    var baseStatus = await order.findById(req.params.id);
    var baseNumber;
    if(baseStatus.status == "5e2d6ca699165b424c3ac310")//not aproved
    {
        baseNumber = 1
    }
    if(baseStatus.status == "5e2c9e9ff6deb7347c562c5f")// approved
    {
        baseNumber = 2;
    }
    if(baseStatus.status == "5e253e903c8db40860674f8b")// completed
    {
        baseNumber = 3;
    }
    if( baseStatus.status == "5e2d6c9399165b424c3ac30f")// canceled
    {
        baseNumber = 4;
    }

    //res.json({message: req.body.status});
    if (baseNumber > bodyNumber) {
        res.json({ message: "Status can't be set backwords!" });
    }
    else {
        try {
            const updatedStatus = await order.updateOne(
                { _id: req.params.id },
                { $set: { status: req.body.status } }
            );
            res.json(updatedStatus);
        } catch (err) {
            res.json({ message: "err" });
        }
    }


});

//Order list by status
router.get('/orders/status/:id', async (req, res) => {
    try {
        const prod = await order.find({status: req.params.id});
        res.json(prod);
    } catch (err) {
        res.json({ message: err });
    }
});

//////////////////Status Methods/////////////////////////////////
//Get all status
router.get('/status', async (req, res) => {
    try {
        const prod = await status.find();
        res.json(prod);
    } catch (err) {
        res.json({ message: err });
    }
});




///////////////EXTRA METHODS
router.post('/customers', [
    check('name').trim().isLength({ min: 1 }).withMessage('Customer have to have name!'),
    check('email').isEmail().withMessage('Wrong email adress!'),
    check('phone').isNumeric().withMessage('Wrong phone number!')
], (req, res) => {
    const errors = validationResult(req)
    if (!errors.isEmpty()) {
        return res.status(400).json({ errors: errors.array() })
    }
    const prod = new customer({
        name: req.body.name,
        email: req.body.email,
        phone: req.body.phone
    });

    try {
        const savedProd = prod.save();
        res.json(savedProd);
    } catch (err) {
        res.json({ message: err });
    }
});

router.get('/customers', async (req, res) => {
    try {
        const cust = await customer.find();
        res.json(cust);
    } catch (err) {
        res.json({ message: err });
    }
});

router.post('/categories', async (req, res) => {
    const prod = new categories({
        name: req.body.name
    });

    try {
        const savedProd = await prod.save();
        res.json(savedProd);
    } catch (err) {
        res.json({ message: err });
    }
});

router.post('/status', async (req, res) => {
    const stat = new status({
        name: req.body.name
    });

    try {
        const savedStatus = await stat.save();
        res.json(savedStatus);
    } catch (err) {
        res.json({ message: err });
    }
});

router.post('/productList', [
    check('what').isMongoId().withMessage("Product doesn't exists!"),
    check('order').isMongoId().withMessage("Order doesn't exists!"),
    check('number').isNumeric().isLength({ min: 1 }).withMessage("Number of products have to be a number!"),
    check('number').isInt({ gt: 0 }).withMessage("Number of products have to be a number!")
], (req, res) => {
    const errors = validationResult(req)
    if (!errors.isEmpty()) {
        return res.status(400).json({ errors: errors.array() })
    }
    const list = new productList({
        what: req.body.what,
        order: req.body.order,
        number: req.body.number
    });

    try {
        const savedList = list.save();
        res.json(savedList);
    } catch (err) {
        res.json({ message: err });
    }
});

router.get('/productList', async (req, res) => {
    try {
        const prod = await productList.find();
        res.json(prod);
    } catch (err) {
        res.json({ message: err });
    }
});


module.exports = router;