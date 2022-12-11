// let request = new XMLHttpRequest();
// request.open("POST","http://localhost:5091", true);
// request.setRequestHeader("Content-Type","application/json")

// const obj = {hello: "world"};

// request.onreadystatechange=()=>{
//     if(request.readyState = XMLHttpRequest.DONE && request.status == 201){
//         console.log("yayyy")
//     }
//     else if(request.readyState = XMLHttpRequest.DONE && request.status == 400){
//         console.log("opps")
//     }



// }
// request.send(JSON.stringify(obj));

window.onload = function () {
    getAllExchanges();
}

async function getAllExchanges() {
    const response = await fetch('/Exchange');
    const exchanges = await response.json(); //extract JSON from the http response

    var html = `
    <thead>
      <tr>
        <th scope="col">ID</th>
        <th scope="col">Name</th>
        <th scope="col">Symbol</th>
      </tr>
    </thead>
    <tbody>
    `
    exchanges.forEach(exchange => {
        html += `
        <tr>
        <td id="id">${exchange.id}</td>
        `
        html += `
        <td id="Name">${exchange.name}</td>
        `
        html += `
        <td id="Symbol">${exchange.symbol}</td>
        </tr>
        `
    });

    html = html + `
  </tbody>
  `
    document.getElementById("table").innerHTML = html;
}

async function updateExchange() {
    var inputName = document.getElementById('exchange-name').value
    var inputSymbol = document.getElementById('exchange-symbol').value

    const rawResponse = await fetch('/Exchange', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name: inputName, symbol: inputSymbol })
    }).then(getAllExchanges());

};


async function deleteAllExchanges() {
    const response = await fetch('/Exchange/delete-all', {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    }).then(window.location.reload());
}