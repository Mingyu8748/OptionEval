window.onload = function () {
    getAllExchanges();
}



const getAllExchanges = async () => {
    const response = await fetch('/Exchange');
    const exchanges = await response.json();

    const mresponse = await fetch('/Market');
    const markets = await mresponse.json();

    return new Promise((resolve, reject) => {
        console.log("yo");
    
        var html = `
        <thead>
          <tr>
            <th scope="col">ID</th>
            <th scope="col">Name</th>
            <th scope="col">Symbol</th>
            <th scope="col">Markets</th>
          </tr>
        </thead>
        <tbody>
        `
        console.log(exchanges);
        exchanges.forEach(exchange => {
            html += `
            <tr>
            <td id="id">${exchange.id}</td>
            `
            html += `
            <td id="Name">${exchange.name}</td>
            `
            html += `
            <td id="Name">${exchange.symbol}</td>
            `
            html += `
            <td id="Symbol">${getMarketsByExchangeId(markets, exchange.id)}</td>
            </tr>
            `
        });
    
        html = html + `
      </tbody>
      `
        document.getElementById("table").innerHTML = html;
        resolve();  // <----- Resolve!
    })
}


// runction that takes a list of markets and a exchangeid and returns a list of markets name that belong to that exchange as a string seperated by -
const getMarketsByExchangeId = (markets, exchangeId) => {
    var marketNames = [];
    markets.forEach(market => {
        if (market.exchangeId === exchangeId) {
            marketNames.push(market.name);
        }
    });
    return marketNames.join(' - ');
}


const updateExchange = async () => {
    var inputName = document.getElementById('exchange-name').value
    var inputSymbol = document.getElementById('exchange-symbol').value

    const rawResponse = await fetch('/Exchange', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name: inputName, symbol: inputSymbol })
    });
    await getAllExchanges();
}



const deleteAllExchanges = async () => {//delete test data
    const response = await fetch('/Exchange/delete-all', {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    });
    await getAllExchanges();
}