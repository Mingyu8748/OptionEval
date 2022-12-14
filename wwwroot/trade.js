window.onload = function () {
    getAllInstruments();
    getAllTrades();
}


const updateOnTradeTypeChange = async () => {
    getTradeAllOptions();
}

const getTradeAllOptions = async () => {
    const response = await fetch('/Option');
    const options = await response.json();

    const insresponse = await fetch('/FinancialInstrument');
    const instruments = await insresponse.json();
    
    var optionType = document.getElementById('trade-type').value;
    switch (optionType) {
        case "All":
            getAllInstruments();
            break;
        case "European":
            const typeresponse = await fetch('/European');
            const typedOptions = await typeresponse.json();
            getEuropeanOptions(typedOptions, instruments);
            break;
        default:
            break;
    }

}

function getEuropeanOptions(options, instruments) {
    var html = `
    <thead>
      <tr>
        <th scope="col">ID</th>
        <th scope="col">Name</th>
        <th scope="col">Symbol</th>
        <th scope="col">Price</th> 
        <th scope="col">Volitility</th> 
        <th scope="col">Expire In</th> 
        <th scope="col">Strike</th> 
        <th scope="col">Is Call</th> 
        <th scope="col">Underlying</th> 
      </tr>
    </thead>
    <tbody>
    `

    instruments = getInstrumentMap(instruments);
    options.forEach(option => {
        var instrument = instruments.get(option.underlyingId);
        html += `
        <tr>
        <td id="id">${option.financialInstrumentID}</td>
        `
        html += `
        <td id="Name">${option.name}</td>
        `
        html += `
        <td id="Name">${option.symbol}</td>
        `
        html += `
        <td id="Name">${instrument.price}</td>
        `
        html += `
        <td id="Name">${option.volatility}</td>
        `
        html += `
        <td id="Name">${option.expireIn}</td>
        `
        html += `
        <td id="Name">${option.strike}</td>
        `
        html += `
        <td id="Name">${option.is_Call}</td>
        `
        html += `
        <td id="Name">${instrument.financialInstrumentID} - ${instrument.name} - ${instrument.price}</td>
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

const getInstrumentMap = (instruments) => {
    var instrumentMap = new Map();
    instruments.forEach(instrument => {
        instrumentMap.set(instrument.financialInstrumentID, instrument);
    });
    return instrumentMap;
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



const getAllTrades = async () => {
    const mresponse = await fetch('/Trade');
    const trades = await mresponse.json();

    const response = await fetch('/FinancialInstrument');
    var instruments = await response.json();

    const maresponse = await fetch('/Market');
    var markets = await maresponse.json();

    const evalresponse = await fetch('/OptionTradeEvaluation');
    var evals = await evalresponse.json();
        var html = `
        <thead>
          <tr>
            <th scope="col">ID</th>
            <th scope="col">Quantity</th>
            <th scope="col">Price</th> 
            <th scope="col">Financial Instrument</th> 

            <th scope="col">Evluation Id</th> 
            <th scope="col">Unrealized Pnl</th> 
            <th scope="col">Gamma</th> 
            <th scope="col">Vega</th> 
            <th scope="col">Rho</th> 
            <th scope="col">Theta</th> 

          </tr>
        </thead>
        <tbody>
        `

        trades.forEach(trade => {
            var instrument = getInstrumentByTrade(instruments, trade);
            
            var eval = getEvalByTrade(evals, trade);

            console.log(eval);
            html += `
            <tr>
            <td id="id">${trade.financialInstrumentId}</td>
            `
            html += `
            <td id="Quantity">${trade.quantity}</td>
            `
            html += `
            <td id="Name">${trade.trade_Price}</td>
            ` 
            html += `
            <td id="Symbol">${instrument.financialInstrumentID} - ${instrument.name} - ${instrument.price}</td>
            ` 
            html += `
            <td id="Symbol">${eval.id}</td>
            ` 
            html += `
            <td id="Symbol">${eval.unrealized_Pnl}</td>
            ` 
            html += `
            <td id="Symbol">${eval.gamma}</td>
            ` 
            html += `
            <td id="Symbol">${eval.vega}</td>
            ` 
            html += `
            <td id="Symbol">${eval.rho}</td>
            ` 
            html += `
            <td id="Symbol">${eval.theta}</td>
            </tr>
            `
        }); 
    
        html = html + `
      </tbody>
      `
    return new Promise((resolve, reject) => {

        document.getElementById("trade-history").innerHTML = html;
        resolve();  // <----- Resolve!
    })
}

const simulate = async () => {
    var inputName = document.getElementById('trade-type').value
    var inputQuantity = document.getElementById('trade-quantity').value
    var inputPrice = document.getElementById('trade-price').value
    var inputid = document.getElementById('trade-id').value

    const rawResponse = await fetch('/Simulate', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ Type: inputName, Quantity: inputQuantity, TradePrice: inputPrice, FinancialInstrumentId: inputid})
    });
    await getAllTrades();
}


const getInstrumentByTrade = (instruments, trade) => {
    let instrumentMap = null;
    instruments.forEach(instrument => {
        if (instrument.financialInstrumentID == trade.financialInstrumentId) {
            instrumentMap = instrument;
        }
    });
    return instrumentMap;
}

const getEvalByTrade = (evals, trade) => {
    let instrumentMap = null;
    evals.forEach(eval => {
        if (eval.id == trade.evaluationId) {
            instrumentMap = eval;
        }
    });
    return instrumentMap;
}