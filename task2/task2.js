const fs = require('fs'), sha3_256 = require('js-sha3').sha3_256;
var files = fs.readdirSync(process.cwd());
for (let file of files) {
    let data = fs.readFileSync(file);
    let hash = sha3_256.create().update(data);
    console.log(file + ' ' + hash)
}