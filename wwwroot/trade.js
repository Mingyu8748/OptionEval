window.onload = function () {
    getAllInstruments();
}


const getAllInstruments= async () => {

    const mresponse = await fetch('/FinancialInstrument');
    const trades = await mresponse.json();

    const response = await fetch('/Market');
    const markets = await response.json();

        var html = `
        <thead>
          <tr>
            <th scope="col">ID</th>
            <th scope="col">Name</th>
            <th scope="col">Symbol</th>
            <th scope="col">Price</th> 
            <th scope="col">Market</th> 
          </tr>
        </thead>
        <tbody>
        `

        console.log(trades);
        console.log(markets);
        trades.forEach(trade => {
            html += `
            <tr>
            <td id="id">${trade.financialInstrumentID}</td>
            `
            html += `
            <td id="Name">${trade.name}</td>
            `
            html += `
            <td id="Name">${trade.symbol}</td>
            `
            html += `
            <td id="Name">${trade.price}</td>
            ` 
            html += `
            <td id="Symbol">${getMarketsByMarketId(markets, trade.tradingMarketId)}</td>
            </tr>
            `
        }); 
    
        html = html + `
      </tbody>
      `
    return new Promise((resolve, reject) => {

        document.getElementById("table-instrument").innerHTML = html;
        resolve();  // <----- Resolve!
    })
}


const getAllTrades= async () => {


    const mresponse = await fetch('/Trade');
    const trades = await mresponse.json();

        var html = `
        <thead>
          <tr>
            <th scope="col">ID</th>
            <th scope="col">Quantity</th>
            <th scope="col">Trade Price</th> 
            <th scope="col">FinancialInstrumentId</th> 
          </tr>
        </thead>
        <tbody>
        `
        trades.forEach(trade => {
            html += `
            <tr>
            <td id="id">${trade.id}</td>
            `
            html += `
            <td id="Name">${trade.quantity}</td>
            `
            html += `
            <td id="Name">${trade.price}</td>
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

        document.getElementById("table-instrument").innerHTML = html;
        resolve();  // <----- Resolve!
    })
}




const getMarketsByMarketId = (markets, underlyingId) => {  
    var marketNames = [];
    markets.forEach(market => {
        if (market.id === underlyingId) {
            marketNames.push(market.name);
        }
    });
    return marketNames.join(" - ");
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