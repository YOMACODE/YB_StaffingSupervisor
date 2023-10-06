const express = require('express');
const myapp = express();
myapp.use(express.static('public'));
myapp.use('/ProantoV2assets/css', express.static('ProantoV2assets/css'));
myapp.use('/ProantoV2assets/images', express.static('ProantoV2assets/images'));
myapp.use('/ProantoV2assets/js', express.static('ProantoV2assets/js'));
myapp.use('/ProantoV2assets/libs', express.static('ProantoV2assets/libs'));

myapp.set('view engine', 'ejs');
myapp.get("/", function (req, res) {
    res.render('SupervisorLayout',{
        pageContent:"index"
    })

})
myapp.get("/page2", function (req, res) {
    res.render('SupervisorLayout',{
        pageContent:"index2"
    })

})
myapp.get("/page3", function (req, res) {
    res.render('SupervisorLayout',{
        pageContent:"index3"
    })

})
myapp.listen(8000);