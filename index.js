let request = new XMLHttpRequest();
request.open("POST","http://localhost:5091", true);
request.setRequestHeader("Content-Type","application/json")

const obj = {hello: "world"};

request.onreadystatechange=()=>{
    if(request.readyState = XMLHttpRequest.DONE && request.status == 201){
        console.log("yayyy")
    }
    else if(request.readyState = XMLHttpRequest.DONE && request.status == 400){
        console.log("opps")
    }



}
request.send(JSON.stringify(obj));

window.onload = function(){
    document.getElementById("id").innerHTML= 123

    document.getElementById("name").innerHTML= "my name"

    document.getElementById("symbol").innerHTML= "cool"
}



