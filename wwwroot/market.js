

window.onload = function () {
    getAllMarkets();
    populateExchangeDropDown();
}

const getAllMarkets = async () => {
    const response = await fetch('/Market');
    const markets = await response.json(); //extract JSON from the http response

    console.log(markets);
    const exchanges = await (await fetch('/Exchange')).json();

    // turn exhanges to map with id as key
    const exchangeMap = new Map();
    exchanges.forEach(exchange => {
        exchangeMap.set(exchange.id, exchange);
    });

    var html = `
        <thead>
          <tr>
            <th scope="col">ID</th>
            <th scope="col">Name</th>
            <th scope="col">Exchange</th>
          </tr>
        </thead>
        <tbody>
        `

    markets.forEach(market => {
        html += `
            <tr>
            <td id="id">${market.id}</td>
            `
        html += `
            <td id="Name">${market.name}</td>
            `
        // html += `
        //      <td id="Symbol">${market.symbol}</td>
        //     `
        var exchange = exchangeMap.get(market.exchangeId)
        html += `
            <td id="Symbol">${exchange.id} - ${exchange.name} - ${exchange.symbol}</td>
            </tr>
            `
    });

    html = html + `
      </tbody>
      `
    return new Promise((resolve, reject) => {
        document.getElementById("table").innerHTML = html;
        resolve();  // <----- Resolve!
    })
}


const populateExchangeDropDown = async () => {
    const response = await fetch('/Exchange');
    const exchanges = await response.json(); //extract JSON from the http response
    var html = ''
    return new Promise((resolve, reject) => {
        exchanges.forEach(exchange => {
            html += `
            <a class="dropdown-item" id="${exchange.id}" onclick="selectExchange(${exchange.id})">${exchange.name} - ${exchange.symbol}</a>
            `
        });

        document.getElementById("dropdown-menu").innerHTML = html;
        resolve();  // <----- Resolve!
    })
}

const selectExchange = async (exchange) => {
    const response = await (await fetch(`/Exchange/${exchange}`)).json();

    document.getElementById("dropdownMenuButton").innerHTML = `${response.id} - ${response.name} - ${response.symbol}`;
}



const updateMarket = async () => {
    var inputName = document.getElementById('market-name').value
    var inputExchangeId = document.getElementById('dropdownMenuButton').innerHTML.split("-")[0]
    const exchange = await (await fetch(`/Exchange/${inputExchangeId}`)).json();

    const rawResponse = await fetch('/Market', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name: inputName, exchangeId: exchange.id })
    });
    await getAllMarkets();
}



const deleteAllMarket = async () => {//delete test data
    const response = await fetch('/Exchange/delete-all', {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    });
    await getAllMarket();
}
