const baseurl = "http://localhost:5206"
const uriCar = "/api/Car"
const uriDriver = "/api/Driver"
const uriCarDriver = "/api/CarDriver"
const uriMake = "/api/Make"
const uriOwner = "/api/Owner"
const uriFine = "/api/Fine"
let cars = [];
let drivers = [];
let cardrivers = [];
let makes = [];
let owners = [];
let fines = [];
let currentview = "welcomeview";

function initialize() {
    getCars();
    getDrivers();
    getCarDrivers();
    getMakes();
    getOwners();
    getFines();
    document.getElementById("carview").style.display = "none";
    document.getElementById("makeview").style.display = "none";
    document.getElementById("ownerview").style.display = "none";
    document.getElementById("driverview").style.display = "none";
    document.getElementById("fineview").style.display = "none";
}

// Para obtener los datos de ciertas entidades de la base de datos 

async function getCars() {
    try {
        const response = await fetch(baseurl + uriCar);
        cars = await response.json();
        _displayCars();
        _displayCount(cars.length, "counterCar")
    } catch (error) {
        console.error('Unable to get cars: ', error);
    }
}

async function getDrivers() {
    try {
        const response = await fetch(baseurl + uriDriver);
        drivers = await response.json();
        _displayDrivers();
        _displayCount(drivers.length, "counterDriver")
    } catch (error) {
        console.error('Unable to get drivers: ', error);
    }
}

async function getCarDrivers() {
    try {
        const response = await fetch(baseurl + uriCarDriver);
        drivers = await response.json();
    } catch (error) {
        console.error('Unable to get drivers: ', error);
    }
}

async function getMakes() {
    try {
        const response = await fetch(baseurl + uriMake);
        makes = await response.json();
        _displayMakes();
        _displayCount(makes.length, "counterMake")
    } catch (error) {
        console.error('Unable to get makes: ', error);
    }
}

async function getOwners() {
    try {
        const response = await fetch(baseurl + uriOwner);
        owners = await response.json();
        _displayOwners();
        _displayCount(owners.length, "counterOwner")
    } catch (error) {
        console.error('Unable to get owners: ', error)
    }
}

async function getFines() {
    try {
        const response = await fetch(baseurl + uriFine);
        drivers = await response.json();
        _displayCount(fines.length, "counterFine")
        _displayFines();
    } catch (error) {
        console.error('Unable to get drivers: ', error);
    }
}

// Mostrar todos los datos

function _displayCars() {
    const tBody = document.getElementById("carsTBody");
    tBody.innerHTML;

    const bton = document.createElement("button");

    cars.forEach(car => {
        let detailsBton = bton.cloneNode(false);
        detailsBton.innerText = "Detalles";
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("class", "deleteBton");

        let tr = tBody.insertRow();
        tr.setAttribute("class", "carRow")

        let td0 = tr.insertCell(0);
        td0.setAttribute("class", "carLicense");
        let txtNode0 = document.createTextNode(car.license);
        td0.appendChild(txtNode0);

        let td1 = tr.insertCell(1);
        td1.setAttribute("class", "carKms");
        let txtNode1 = document.createTextNode(car.kms);
        td1.appendChild(txtNode1);

        let td2 = tr.insertCell(2);
        td2.setAttribute("class", "carColor");
        let txtNode2 = document.createTextNode(car.color.name);
        td2.appendChild(txtNode2);

        let td3 = tr.insertCell(3);
        td3.setAttribute("class", "carOwner");
        let txtNode3 = document.createTextNode(car.owner.name);
        td3.appendChild(txtNode3);

        let td4 = tr.insertCell(4);
        td4.setAttribute("class", "carMake");
        let txtNode4 = document.createTextNode(car.make.name);
        td4.appendChild(txtNode4);

        let td5 = tr.insertCell(5);
        td5.appendChild(detailsBton);

        let td6 = tr.insertCell(6);
        td6.appendChild(deleteBton);
    })
}

function _displayDrivers() {
    const tBody = document.getElementById("driversTBody");
    tBody.innerHTML;

    const bton = document.createElement("button");

    drivers.forEach(driver => {
        let detailsBton = bton.cloneNode(false);
        detailsBton.innerText = "Detalles";
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("class", "deleteBton");

        let tr = tBody.insertRow();
        tr.setAttribute("class", "driver")

        let td0 = tr.insertCell(0);
        td0.setAttribute("class", "driverName");
        let txtNode0 = document.createTextNode(driver.name);
        td0.appendChild(txtNode0);

        let td1 = tr.insertCell(1);
        td1.setAttribute("class", "driverDNI");
        let txtNode1 = document.createTextNode(driver.dni);
        td1.appendChild(txtNode1);

        let td2 = tr.insertCell(2);
        td2.setAttribute("class", "driverPhone");
        if (driver.phoneNumber === null) { td2.innerText = "-" }
        else { td2.appendChild(document.createTextNode(driver.phoneNumber)) }

        let td3 = tr.insertCell(3);
        td3.setAttribute("class", "driverEmail");        
        if (driver.emailAddr == null) { td3.innerText = "-" }
        else { td3.appendChild(document.createTextNode(driver.emailAddr)) }

        let td4 = tr.insertCell(4);
        td4.setAttribute("class", "driverAssociatedOwner");
        let txtNode4 = document.createTextNode(driver.owner.name);
        td4.appendChild(txtNode4);

        let td5 = tr.insertCell(5);
        td5.appendChild(detailsBton);

        let td6 = tr.insertCell(6);
        td6.appendChild(deleteBton);
    })
}

function _displayMakes() {
    const tBody = document.getElementById("makesTBody");
    tBody.innerHTML;

    const bton = document.createElement("button");

    makes.forEach(make => {
        let detailsBton = bton.cloneNode(false);
        detailsBton.innerText = "Detalles";
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("class", "deleteBton");

        let tr = tBody.insertRow();
        tr.setAttribute("class", "makeRow")

        let td0 = tr.insertCell(0);
        td0.setAttribute("class", "makeName");
        let txtNode0 = document.createTextNode(make.name);
        td0.appendChild(txtNode0)

        let td1 = tr.insertCell(1);
        td1.setAttribute("class", "makeHorsePower");
        let txtNode1 = document.createTextNode(make.horsePower);
        td1.appendChild(txtNode1);

        let td2 = tr.insertCell(2);
        td2.setAttribute("class", "makePrice");
        let txtNode2 = document.createTextNode(make.price);
        td2.appendChild(txtNode2);

        let td3 = tr.insertCell(3);
        td3.setAttribute("class", "makeFuelType")
        let txtNode3 = document.createTextNode(make.fuelType.name)
        td3.appendChild(txtNode3)

        let td4 = tr.insertCell(4);
        td4.appendChild(detailsBton);

        let td5 = tr.insertCell(5);
        td5.appendChild(deleteBton);
    })
}

function _displayOwners() {
    const tBody = document.getElementById("ownersTBody");
    tBody.innerHTML;

    const bton = document.createElement("button");

    owners.forEach(owner => {
        let detailsBton = bton.cloneNode(false);
        detailsBton.innerText = "Detalles";
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("class", "deleteBton");

        let tr = tBody.insertRow();
        tr.setAttribute("class", "ownerRow");

        let td0 = tr.insertCell(0);
        td0.setAttribute("class", "ownerName");
        let txtNode0 = document.createTextNode(owner.name);
        td0.appendChild(txtNode0);

        let td1 = tr.insertCell(1);
        td1.setAttribute("class", "ownerNif");
        let txtNode1 = document.createTextNode(owner.nif);
        td1.appendChild(txtNode1);

        let td2 = tr.insertCell(2);
        td2.setAttribute("class", "ownerDate");
        let txtNode2 = document.createTextNode(owner.dateEntry);
        td2.appendChild(txtNode2);

        let td3 = tr.insertCell(3);
        td3.setAttribute("class", "ownerPhone");
        if (owner.phoneNumber == null) { td3.innerText = "-" }
        else { td3.appendChild(document.createTextNode(owner.phoneNumber)) }

        let td4 = tr.insertCell(4);
        td4.setAttribute("class", "ownerEmail");
        if (owner.emailAddr == null) { td4.innerText = "-" }
        else { td4.appendChild(document.createTextNode(owner.emailAddr)) }

        let td5 = tr.insertCell(5);
        td5.appendChild(detailsBton);

        let td6 = tr.insertCell(6);
        td6.appendChild(deleteBton);

    })
}

function _displayFines() {
    const tBody = document.getElementById("finesTBody");
    tBody.innerHTML;

    const bton = document.createElement("button");

    fines.forEach(fine => {
        let detailsBton = bton.cloneNode(false);
        detailsBton.innerText = "Detalles";
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("class", "deleteBton");

        let tr = tBody.insertRow();
        tr.setAttribute("class", "fine");

        let td0 = tr.insertCell(0);
        td0.setAttribute("class", "fineDate");
        let txtNode0 = document.createTextNode(fine.date);
        td0.appendChild(txtNode0);

        let td1 = tr.insertCell(1);
        td1.setAttribute("class", "fineImport");
        let txtNode1 = document.createTextNode(fine.price);
        td1.appendChild(txtNode1);

        let td2 = tr.insertCell(2);
        td2.setAttribute("class", "fineDescription");
        let txtNode2 = document.createTextNode(fine.description);
        td2.appendChild(txtNode2);

        let td3 = tr.insertCell(3);
        td3.setAttribute("class", "finePayed");
        let txtNode3 = document.createTextNode(fine.payed);
        td3.appendChild(txtNode3);

        let td4 = tr.insertCell(4);
        td4.setAttribute("class", "fineOwnerAssociate");
        let txtNode4 = document.createTextNode(fine.owner.name);
        td4.appendChild(txtNode4);

        let td5 = tr.insertCell(5);
        td5, setAttribute("class", "fineCarAssociate");
        let txtNode5 = document.createTextNode(fine.car.license)

        let td6 = tr.insertCell(6);
        td6.appendChild(detailsBton);

        let td7 = tr.insertCell(7);
        td7.appendChild(deleteBton)
    })
}

// Para mostrar un recuento de una entidad

function _displayCount(itemCount, id) {
    const name = (itemCount === 1) ? "item" : "items"
    document.getElementById(id).innerText = `${itemCount} ${name}`;
}

// Para las busquedas y para mostrar vistas de cada entidad

function searchCar() {
    const searchbar = document.getElementById("carSearchbar");
    const table = document.getElementById("carsTBody").getElementsByTagName("tr");
    const filter = searchbar.value.toUpperCase()

    for (let i = 0; i < table.length; i++) {
        txt = table[i].getElementsByClassName("carLicense")[0];
        if (txt.innerText.toUpperCase().indexOf(filter) > -1) {
            table[i].style.display = "";
        } else {
            table[i].style.display = "none";
        }
    }
}

function searchMake() {
    const searchbar = document.getElementById("makeSearchBar");
    const table = document.getElementById("makesTBody").getElementsByTagName("tr");
    const filter = searchbar.value.toUpperCase()

    for (let i = 0; i < table.length; i++) {
        txt = table[i].getElementsByClassName("makeName")[0];
        if (txt.innerText.toUpperCase().indexOf(filter) > -1) {
            table[i].style.display = "";
        } else {
            table[i].style.display = "none";
        }
    }
}

function searchOwner() {
    const searchbar = document.getElementById("ownerSearchbar");
    const table = document.getElementById("ownersTBody").getElementsByTagName("tr");
    const filter = searchbar.value.toUpperCase()

    for (let i = 0; i < table.length; i++) {
        txt = table[i].getElementsByClassName("ownerName")[0];
        if (txt.innerText.toUpperCase().indexOf(filter) > -1) {
            table[i].style.display = "";
        } else {
            table[i].style.display = "none";
        }
    }
}

function searchDriver() {
    const searchbar = document.getElementById("driverSearchbar");
    const table = document.getElementById("driversTBody").getElementsByTagName("tr");
    const filter = searchbar.value.toUpperCase()

    for (let i = 0; i < table.length; i++) {
        txt = table[i].getElementsByClassName("driverName")[0];
        if (txt.innerText.toUpperCase().indexOf(filter) > -1) {
            table[i].style.display = "";
        } else {
            table[i].style.display = "none";
        }
    }
}

function searchFine() {
    const searchbar = document.getElementById("fineSearchbar");
    const table = document.getElementById("fineTBody").getElementsByTagName("tr");
    const filter = searchbar.value.toUpperCase()

    for (let i = 0; i < table.length; i++) {
        txt = table[i].getElementsByClassName("fineDate")[0];
        if (txt.innerText.toUpperCase().indexOf(filter) > -1) {
            table[i].style.display = "";
        } else {
            table[i].style.display = "none";
        }
    }
}

function showView(id) {
    if (document.getElementById(currentview) != null) { document.getElementById(currentview).style.display = "none" }

    document.getElementById(id).style.display = "initial";
    document.getElementsByClassName("searchBar").value = "";
    currentview = id;
}