window.onload = function () {
    getAllUnderlyings();
    populateMarketDropDown();
}



const getAllUnderlyings = async () => {
    const response = await fetch('/Underlying');
    const underlyings = await response.json();

    const mresponse = await fetch('/Market');
    const markets = await mresponse.json();

        var html = `
        <thead>
          <tr>
            <th scope="col">ID</th>
            <th scope="col">Name</th>
            <th scope="col">Symbol</th>
            <th scope="col">Price</th>
            <th scope="col">Markets</th>
          </tr>
        </thead>
        <tbody>
        `
        const insresponse = await fetch('/FinancialInstrument');
        var instruments = await insresponse.json();
        underlyings.forEach(underlying => {
            html += `
            <tr>
            <td id="id">${underlying.financialInstrumentID}</td>
            `
            html += `
            <td id="Name">${underlying.name}</td>
            `
            html += `
            <td id="Name">${underlying.symbol}</td>
            `
            html += `
            <td id="Name">${underlying.price}</td>
            `
            html += `
            <td id="Symbol">${getMarketsByUnderlyingId(markets, underlying.tradingMarketId)}</td>
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


const populateMarketDropDown = async () => {
    const response = await fetch('/Market');
    const markets = await response.json(); //extract JSON from the http response
    var html = ''
    return new Promise((resolve, reject) => {
        markets.forEach(market => {
            html += `
            <a class="dropdown-item" id="${market.id}" onclick="selectMarket(${market.id})">${market.name}</a>
            `
        });

        document.getElementById("dropdown-menu").innerHTML = html;
        resolve();  // <----- Resolve!
    })
}

const selectMarket = async (exchange) => {
    const response = await (await fetch(`/Market/${exchange}`)).json();

    document.getElementById("dropdownMenuButton").innerHTML = `${response.id} - ${response.name}`;
}


const getMarketsByUnderlyingId = (markets, underlyingId) => {  
    var marketNames = [];
    markets.forEach(market => {
        if (market.id === underlyingId) {
            marketNames.push(market.name);
        }
    });
    return marketNames.join(" - ");
}


const updateUnderlying = async () => {
    var inputName = document.getElementById('underlying-name').value
    var inputSymbol = document.getElementById('underlying-symbol').value
    var inputPrice = document.getElementById('underlying-price').value
    var inputmarketId = document.getElementById('dropdownMenuButton').innerHTML.split("-")[0]
    const market = await (await fetch(`/Market/${inputmarketId}`)).json();

    const rawResponse = await fetch('/Underlying', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name: inputName, symbol: inputSymbol, price: inputPrice, tradingMarketId: market.id})
    });
    await getAllUnderlyings();
}



const deleteAllUnderlying = async () => {//delete test data
    const response = await fetch('/Underlying/delete-all', {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    });
    await getAllUnderlyings();
}