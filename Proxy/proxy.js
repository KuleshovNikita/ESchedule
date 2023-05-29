let express = require('express');
var bodyParser = require('body-parser');

const serverUrl = 'https://localhost:20000/api';
const attendanceEndpoint = '/attendance';

const jsonParser = bodyParser.json();
let app = express();

app.post(attendanceEndpoint, jsonParser, async function (req, res) {

    process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = 0;

    const url = serverUrl + '/lesson' + attendanceEndpoint + `/${req.body.pupilId}`;
    console.log(url);
    await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        }
    })
    .then(async (r) =>  {
        const data = await r.json();
        console.log(data);
        res.send(data);
    })
    .catch(err => {
        console.log(err);
        res.send(err);
    });
});

app.listen(3001, '0.0.0.0', () => {
    console.log("Application started and Listening on port 3001");
});