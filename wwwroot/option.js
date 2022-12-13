window.onload = function () {
    getAllOptions();
}

const updateOnTypeChange = async () => {
    getAllOptions();
}


const getAllOptions = async () => {
    const response = await fetch('/Option');
    const options = await response.json();

    const insresponse = await fetch('/FinancialInstrument');
    const instruments = await insresponse.json();
    
    await populateUnderlyingDropDown();

    var optionType = document.getElementById('option-type').value;
    switch (optionType) {
        case "European":
            const typeresponse = await fetch('/European');
            const typedOptions = await typeresponse.json();
            getEuropeanOptions(typedOptions, instruments);
            getAllEuropeanInputFields()
            break;
        default:
            break;
    }

}

const getAllEuropeanInputFields = async () => {
    var html = `

    <label for="name">Name</label>
    <input type="text" class="form-control" id="option-name" placeholder="" value="">

    <label for="name">Symbol</label>
    <input type="text" class="form-control" id="option-symbol" placeholder="" value="">

    <label for="name">Volitility</label>
    <input type="text" class="form-control" id="option-volitility" placeholder="" value="">

    <label for="name">Expiration Date</label>
    <input type="text" class="form-control" id="option-expiration" placeholder="" value="">

    <label for="name">Strike</label>
    <input type="text" class="form-control" id="option-strike" placeholder="" value="">
    
    <label for="name">Is call</label>
    <input type="text" class="form-control" id="option-call" placeholder="Y/N" value="Y">
    `

    document.getElementById("input-fields").innerHTML = html;
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
        <th scope="col">Expiration Date</th> 
        <th scope="col">Strike</th> 
        <th scope="col">Is Call</th> 
        <th scope="col">Underlying</th> 
      </tr>
    </thead>
    <tbody>
    `

    console.log(options);
    instruments = getInstrumentMap(instruments);
    console.log(instruments);
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
        <td id="Name">${option.expiration_Date}</td>
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

        document.getElementById("table").innerHTML = html;
        resolve();  // <----- Resolve!
    })
}

//given a list of instruments and an id return the instrument with that id


const populateUnderlyingDropDown = async () => {
    const response = await fetch('/Underlying');
    const underlyings = await response.json(); //extract JSON from the http response
    var html = ''
    return new Promise((resolve, reject) => {
        underlyings.forEach(underlying => {
            html += `
            <a class="dropdown-item" id="${underlying.financialInstrumentID}" onclick="selectUnderlying(${underlying.financialInstrumentID})"> ${underlying.financialInstrumentID} - ${underlying.name} - ${underlying.price}</a>
            `
        });

        document.getElementById("dropdown-menu").innerHTML = html;
        resolve();  // <----- Resolve!
    })
}


const selectUnderlying = async (exchange) => {
    const response = await (await fetch(`/Underlying/${exchange}`)).json();

    document.getElementById("dropdownMenuButton").innerHTML = `${response.financialInstrumentID} - ${response.name} - ${response.price}`;
}


const getInstrumentMap = (instruments) => {
    var instrumentMap = new Map();
    instruments.forEach(instrument => {
        instrumentMap.set(instrument.financialInstrumentID, instrument);
    });
    return instrumentMap;
}

const getIMById = (instruments, id) => {
    instruments.forEach(instrument => {
        if (instrument.financialInstrumentID === id) {
            return instrument;
        }
    });
 }


 const updateOption = async () => {
    var optionType = document.getElementById('option-type').value;
    switch (optionType) {
        case "European":
            console.log("update european");
            updateEuropeanOptions();
            break;
        default:
            break;
    }
 }


const updateEuropeanOptions = async () => {
    var inputName = document.getElementById('option-name').value;
    var inputSymbol = document.getElementById('option-symbol').value;
    var inputPrice = parseFloat(document.getElementById('option-price').value);
    var inputvolitility = parseFloat(document.getElementById('option-volitility').value);
    var inputexpiration = document.getElementById('option-expiration').value;
    var inputstrike =  parseFloat(document.getElementById('option-strike').value);
    var inputcall = document.getElementById('option-call').value === "Y" ? true : false;

    var inputUnderlying = document.getElementById("dropdownMenuButton").innerHTML.split(" - ")[0];
    const udnerlying = await (await fetch(`/Underlying/${inputUnderlying}`)).json();

    const rawResponse = await fetch('/European', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name: inputName, symbol: inputSymbol, volatility:inputvolitility, expiration_Date:inputexpiration, strike:inputstrike, is_Call:inputcall, tradingMarketId: udnerlying.tradingMarketId, underlyingId: udnerlying.financialInstrumentID })
    });
    await getAllOptions();
}



const deleteAllExchanges = async () => {//delete test data
    const response = await fetch('/Exchange/delete-all', {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    });
    await getAllOptions();
}