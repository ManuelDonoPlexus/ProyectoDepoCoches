const baseurl = "http://localhost:5206"
const uriCar = "/api/Car"
const uriColor = "/api/Color"
const uriDriver = "/api/Driver"
const uriCarDriver = "/api/CarDriver"
const uriMake = "/api/Make"
const uriOwner = "/api/Owner"
const uriFine = "/api/Fine"
const uriFuel = "/api/FuelType"
let cars = [];
let colors = [];
let drivers = [];
let cardrivers = [];
let makes = [];
let owners = [];
let fines = [];
let fuels = [];
let currentview = "welcomeview";
let currentdriverview = "";

// Función para inicializar

function initialize() {
    getCars();
    getCarDrivers();
    getColors();
    getDrivers();
    getFines();
    getFuels();
    getMakes();
    getOwners();
    document.getElementById("carview").style.display = "none";
    document.getElementById("makeview").style.display = "none";
    document.getElementById("ownerview").style.display = "none";
    document.getElementById("driverview").style.display = "none";
    document.getElementById("fineview").style.display = "none";

    document.getElementById("carCreate").style.display = "none";
    document.getElementById("makeCreate").style.display = "none";
    document.getElementById("ownerCreate").style.display = "none";
    document.getElementById("driverCreate").style.display = "none";
    document.getElementById("fineCreate").style.display = "none";

    document.getElementById("drivers").style.display = "none";
    document.getElementById("cardrivers").style.display = "none";

    document.getElementById("carView").style.display = "none";
    document.getElementById("makeView").style.display = "none";
    document.getElementById("ownerView").style.display = "none";
    document.getElementById("driverView").style.display = "none";
    document.getElementById("fineView").style.display = "none";

}

// Para mostrar un recuento de una entidad

function _displayCount(itemCount, id) {
    const name = (itemCount === 1) ? "item" : "items"
    document.getElementById(id).innerText = `${itemCount} ${name}`;
}

// Para mostrar una vista y esconder la anterior

function showView(id) {
    if (document.getElementById(currentview) != null) { document.getElementById(currentview).style.display = "none" }

    document.getElementById(id).style.display = "initial";
    document.getElementsByClassName("searchBar").value = "";
    currentview = id;

    document.getElementById("drivers").style.display = "none"
    document.getElementById("cardrivers").style.display = "none"
}

function showDrivers(id) {
    if (document.getElementById(currentdriverview) != null) { document.getElementById(currentdriverview).style.display = "none" }

    document.getElementById(id).style.display = "initial";
    currentdriverview = id;
}

// Para obtener los datos de ciertas entidades de la base de datos 

async function getCars() {
    try {
        const response = await fetch(baseurl + uriCar);
        cars = await response.json();
        _fillCarList();
        _displayCount(cars.length, "counterCar")
        _displayCars();
    } catch (error) {
        console.error('Unable to get cars: ', error);
    }
}

async function getCarDrivers() {
    try {
        const response = await fetch(baseurl + uriCarDriver);
        cardrivers = await response.json();
        _displayCount(cardrivers.length, "counterCarDriver");
        _displayCarDrivers();
    } catch (error) {
        console.error('Unable to get drivers: ', error);
    }
}

async function getColors() {
    try {
        const response = await fetch(baseurl + uriColor);
        colors = await response.json();
        _fillColorList();
    } catch (error) {
        console.error('Unable to get makes: ', error);
    }
}

async function getDrivers() {
    try {
        const response = await fetch(baseurl + uriDriver);
        drivers = await response.json();
        _displayCount(drivers.length, "counterDriver");
        _fillDriverList();
        _displayDrivers();
    } catch (error) {
        console.error('Unable to get drivers: ', error);
    }
}

async function getFines() {
    try {
        const response = await fetch(baseurl + uriFine);
        fines = await response.json();
        _displayCount(fines.length, "counterFine")
        _displayFines();
    } catch (error) {
        console.error('Unable to get drivers: ', error);
    }
}

async function getFuels() {
    try {
        const response = await fetch(baseurl + uriFuel);
        fuels = await response.json();
        _fillFuelList();
    } catch (error) {
        console.error('Unable to get drivers: ', error);
    }
}

async function getMakes() {
    try {
        const response = await fetch(baseurl + uriMake);
        makes = await response.json();
        _fillMakeList();
        _displayCount(makes.length, "counterMake");
        _displayMakes();
    } catch (error) {
        console.error('Unable to get makes: ', error);
    }
}

async function getOwners() {
    try {
        const response = await fetch(baseurl + uriOwner);        
        owners = await response.json();
        _fillOwnerList();
        _displayCount(owners.length, "counterOwner");
        _displayOwners();
    } catch (error) {
        console.error('Unable to get owners: ', error)
    }
}

// Para buscar entidades especificas

function getSpecificCar(search, filter) {
    let result;
    cars.forEach(car => {
        if (filter == "license") {
            if (car.license == search) { result = car; }
        } else if (filter == "id") {
            if (car.id == search) { result = car; }
        }
    })
    return result;
}

function getSpecificDriver(search, filter) {
    let result;
    drivers.forEach(driver => {
        if (filter == "id") {
            if (driver.id == search) { result = driver; }
        } else if (filter == "name") {
            if (driver.name == search) { result = driver; }
        }
    })
    return result;
}

function getSpecificColor(search, filter) {
    let result;
    colors.forEach(color => {
        if (filter == "id") {
            if (color.id == search) { result = color; }
        } else if (filter == "name") {
            if (color.name == search) { result = color; }
        }
    })
    return result;
}

function getSpecificOwner(search, filter) {
    let result;
    owners.forEach(owner => {
        if (filter == "id") {
            if (owner.id == search) { result = owner; }
        } else if (filter == "name") {
            if (owner.name == search) { result = owner; }
        }
    })
    return result;
}

function getSpecificMake(search, filter) {
    let result;
    makes.forEach(make => {
        if (filter == "id") {
            if (make.id == search) { result = make; }
        } else if (filter == "name") {
            if (make.name == search) { result = make; }
        }
    })
    return result;
}

function getSpecificFuel(search, filter) {
    let result;
    fuels.forEach(fuel => {
        if (filter == "id") {
            if (fuel.id == search) { result = fuel; }
        } else if (filter == "name") {
            if (fuel.name == search) { result = fuel; }
        }
    })
    return result;
}

function getSpecificFine(search, filter) {
    let result;
    fines.forEach(fine => {
        if (filter == "id") {
            if (fine.id == search) { result = fine; }
        } else if (filter == "name") {
            if (fine.name == search) { result = fine; }
        }
    })
    return result;
}

// Para llenar DataLists, necesarias para formar las listas de datos

function _fillCarList() {
    const list = document.getElementById("carList");
    const option = document.createElement("option");

    cars.forEach(car => {
        let optionCar = option.cloneNode(false);
        optionCar.setAttribute("value", `${car.license}`)
        list.appendChild(optionCar)
    })

}

function _fillDriverList() {
    const list = document.getElementById("driverList");
    const option = document.createElement("option");

    drivers.forEach(driver => {
        let optionDriver = option.cloneNode(false);
        optionDriver.setAttribute("value", `${driver.name}`)
        list.appendChild(optionDriver)
    })

}

function _fillColorList() {
    const list = document.getElementById("colorList");
    const option = document.createElement("option");

    colors.forEach(color => {
        let optionColor = option.cloneNode(false);
        optionColor.setAttribute("value", `${color.name}`)
        list.appendChild(optionColor)
    })
}

function _fillFuelList() {
    const list = document.getElementById("fuelList");
    const option = document.createElement("option");

    fuels.forEach(fuel => {
        let optionFuel = option.cloneNode(false);
        optionFuel.setAttribute("value", `${fuel.name}`)
        list.appendChild(optionFuel)
    })
}

function _fillMakeList() {
    const list = document.getElementById("makeList");
    const option = document.createElement("option");

    makes.forEach(make => {
        let optionMake = option.cloneNode(false);
        optionMake.setAttribute("value", `${make.name}`)
        list.appendChild(optionMake)
    })
}

function _fillOwnerList() {
    const list = document.getElementById("ownerList");
    const option = document.createElement("option");

    owners.forEach(owner => {
        let optionOwner = option.cloneNode(false);
        optionOwner.setAttribute("value", `${owner.name}`);
        list.appendChild(optionOwner);
    })
}

// Mostrar todos los datos

function _displayCars() {
    const tBody = document.getElementById("carsTBody");
    tBody.innerHTML;

    const bton = document.createElement("button");

    cars.forEach(car => {
        let detailsBton = bton.cloneNode(false);
        detailsBton.innerText = "Detalles";
        detailsBton.setAttribute("onclick", `showEditForm('carView',${car.id})`);
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `deleteCar(${car.id})`);
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
        detailsBton.setAttribute("onclick", `showEditForm('driverView',${driver.id})`);
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `deleteDriver(${driver.id})`);
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

function _displayCarDrivers() {
    const tBody = document.getElementById("cardriversTBody");
    tBody.innerHTML;

    const bton = document.createElement("button");

    cardrivers.forEach(driver => {
        let detailsBton = bton.cloneNode(false);
        detailsBton.innerText = "Detalles";
        detailsBton.setAttribute("onclick", `showEditForm('driverView',${driver.id})`);
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `deleteDriver(${driver.id})`);
        deleteBton.setAttribute("class", "deleteBton");

        let tr = tBody.insertRow();
        tr.setAttribute("class", "cardriver")

        let td0 = tr.insertCell(0);
        td0.setAttribute("class", "cardriverName");
        let txtNode0 = document.createTextNode(driver.driver.name);
        td0.appendChild(txtNode0);

        let td1 = tr.insertCell(1);
        td1.setAttribute("class", "cardriverDate");
        let txtNode1 = document.createTextNode(driver.dateDrive);
        td1.appendChild(txtNode1);

        let td2 = tr.insertCell(2);
        td2.setAttribute("class", "cardriverAssociatedCar");
        let txtNode2 = document.createTextNode(driver.car.license);
        td2.appendChild(txtNode2);

        let td3 = tr.insertCell(3);
        td3.appendChild(detailsBton);

        let td4 = tr.insertCell(4);
        td4.appendChild(deleteBton);
    })
}

function _displayMakes() {
    const tBody = document.getElementById("makesTBody");
    tBody.innerHTML;

    const bton = document.createElement("button");

    makes.forEach(make => {
        let detailsBton = bton.cloneNode(false);
        detailsBton.innerText = "Detalles";
        detailsBton.setAttribute("onclick", `showEditForm('makeView',${make.id})`);
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `deleteMake(${make.id})`);
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
        detailsBton.setAttribute("onclick", `showEditForm('ownerView',${owner.id})`);
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `deleteOwner(${owner.id})`);
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
        detailsBton.setAttribute("onclick", `showEditForm('fineView',${fine.id})`);
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `deleteFine(${fine.id})`);
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
        td5.setAttribute("class", "fineCarAssociate");
        let txtNode5 = document.createTextNode(fine.car.license);
        td5.appendChild(txtNode5);

        let td6 = tr.insertCell(6);
        td6.appendChild(detailsBton);

        let td7 = tr.insertCell(7);
        td7.appendChild(deleteBton);
    })
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

function searchCarDriver() {
    const searchbar = document.getElementById("cardriverSearchbar");
    const table = document.getElementById("cardriversTBody").getElementsByTagName("tr");
    const filter = searchbar.value.toUpperCase()

    for (let i = 0; i < table.length; i++) {
        txt = table[i].getElementsByClassName("cardriverName")[0];
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

// Para añadir nuevas entidades a la base de datos

function addOwner() {
    const addName = document.getElementById("add-owner-name");
    const addNif = document.getElementById("add-owner-nif");
    const addTel = document.getElementById("add-owner-tel");
    const addDate = document.getElementById("add-owner-date");
    const addEmail = document.getElementById("add-owner-email");

    const owner = {
        name: addName.value.trim(),
        nif: addNif.value.trim(),
        phoneNumber: addTel.value.trim(),
        dateEntry: addDate.value.trim(),
        emailAddr: addEmail.value.trim()
    }

    fetch(baseurl + uriOwner, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(owner)
    })
        .then(response => response.json)
        .then(() => location.reload())
        .catch(error => console.error("Unable to add owner to database. ", error))
}

function addMake() {
    const addName = document.getElementById("add-make-name");
    const addHP = document.getElementById("add-make-horsepower");
    const addPrice = document.getElementById("add-make-price");
    const addFuelName = document.getElementById("add-make-fuel");

    const addFuel = getSpecificFuel(addFuelName.value.trim(), "name")


    const make = {
        name: addName.value.trim(),
        horsePower: addHP.value.trim(),
        price: addPrice.value.trim(),
        fuelTypeId: addFuel.id
    }

    fetch(baseurl + uriMake, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(make)
    })
        .then(response => response.json)
        .then(() => location.reload())
        .catch(error => console.error("Unable to add make to database. ", error))
}

function addFine() {
    const addPrice = document.getElementById("add-fine-price");
    const addDescription = document.getElementById("add-fine-description");
    const addDate = document.getElementById("add-fine-date")

    const addCarName = document.getElementById("add-fine-car");
    const addOwnerName = document.getElementById("add-fine-owner")

    const addCar = getSpecificCar(addCarName.value.trim(), "license");
    const addOwner = getSpecificOwner(addOwnerName.value.trim(), "name");

    const fine = {
        price: addPrice.value.trim(),
        description: addDescription.value.trim(),
        date: addDate.value.trim(),
        payed: false,
        ownerId: addOwner.id,
        carId: addCar.id,

    }

    fetch(baseurl + uriFine, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(fine)
    })
        .then(() => location.reload())
        .catch(error => console.error("Unable to add fine to database. ", error))

}

function addCar() {
    const addLicense = document.getElementById("add-car-license");
    const addKms = document.getElementById("add-car-kms");
    const addColorName = document.getElementById("add-car-color");
    const addOwnerName = document.getElementById("add-car-owner");
    const addMakeName = document.getElementById("add-car-make");

    const addColor = getSpecificColor(addColorName.value.trim(), "name");
    const addOwner = getSpecificOwner(addOwnerName.value.trim(), "name");
    const addMake = getSpecificMake(addMakeName.value.trim(), "name");

    const car = {
        license: addLicense.value.trim(),
        kms: addKms.value.trim(),
        colorId: addColor.id,
        ownerId: addOwner.id,
        makeId: addMake.id
    }

    fetch(baseurl + uriCar, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(car)
    })
        .then(respone => respone.json())
        .then(() => location.reload())
        .catch(error => console.error("Unable to add fine to database. ", error))
}

function addDriver() {
    const addName = document.getElementById("add-driver-name");
    const addDni = document.getElementById("add-driver-dni");
    const addEmail = document.getElementById("add-driver-email");
    const addTel = document.getElementById("add-driver-tel");
    const addOwnerName = document.getElementById("add-driver-owner");

    const addOwner = getSpecificOwner(addOwnerName.value.trim(), "name");

    const driver = {
        name: addName.value.trim(),
        dni: addDni.value.trim(),
        emailAddr: addEmail.value.trim(),
        phoneNumber: addTel.value.trim(),
        ownerId: addOwner.id
    }

    fetch(baseurl + uriDriver, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(driver)
    })
        .then(respone => respone.json())
        .then(() => location.reload())
        .catch(error => console.error("Unable to add fine to database. ", error))
}

function addCarDriver() { }

// Para borrar entidades de la base de datos

function deleteCar(id) {
    fetch(`${baseurl}${uriCar}/${id}`, {
        method: `DELETE`
    })
        .then(() => location.reload())
        .catch(error => console.error("Unable to delete car from database. ", error))
}

function deleteCarDrivers(id) {
    fetch(`${baseurl}${uriCarDriver}/${id}`, {
        method: `DELETE`
    })
        .then(() => location.reload())
        .catch(error => console.error("Unable to delete car from database. ", error))
}

function deleteDriver(id) {
    fetch(`${baseurl}${uriDriver}/${id}`, {
        method: `DELETE`
    })
        .then(() => location.reload())
        .catch(error => console.error("Unable to delete car from database. ", error))
}

function deleteFine(id) {
    fetch(`${baseurl}${uriFine}/${id}`, {
        method: `DELETE`
    })
        .then(() => location.reload())
        .catch(error => console.error("Unable to delete car from database. ", error))
}

function deleteMake(id) {
    fetch(`${baseurl}${uriMake}/${id}`, {
        method: `DELETE`
    })
        .then(() => location.reload())
        .catch(error => console.error("Unable to delete car from database. ", error))
}

function deleteOwner(id) {
    fetch(`${baseurl}${uriOwner}/${id}`, {
        method: `DELETE`
    })
        .then(() => location.reload())
        .catch(error => console.error("Unable to delete car from database. ", error))
}

// Editform

function showEditForm(nameView, idEntity) {
    showView(nameView);

    if (nameView == "carView") { carShowEdit(idEntity) }
    if (nameView == "driverView") { driverShowEdit(idEntity) }
    if (nameView == "makeView") { makeShowEdit(idEntity) }
    if (nameView == "ownerView") { ownerShowEdit(idEntity) }
    if (nameView == "fineView") { fineShowEdit(idEntity) }
}

function carShowEdit(id) {
    let car = getSpecificCar(id, "id");
    document.getElementById("licenseCar").value = car.license;
    document.getElementById("kmsCar").value = car.kms;
    document.getElementById("colorCar").value = car.color;
    document.getElementById("ownerCar").value = car.owner.name;
    document.getElementById("makeCar").value = car.make.name;

    let delBton = document.getElementById("deleteCar");
    delBton.setAttribute("onclick", `deleteCar(${id})`);
}

function driverShowEdit(id) {
    let driver = getSpecificDriver(id, "id");
    document.getElementById("nameDriver").value = driver.name;
    document.getElementById("dniDriver").value = driver.dni;
    document.getElementById("emailaddrDriver").value = driver.emailAddr;
    document.getElementById("phonenumberDriver").value = driver.phoneNumber;
    document.getElementById("ownerDriver").value = driver.owner.name;

    let delBton = document.getElementById("deleteDriver");
    delBton.setAttribute("onclick", `deleteDriver(${id})`);
}

function makeShowEdit(id) {
    let make = getSpecificMake(id, "id");
    document.getElementById("nameMake").value = make.name;
    document.getElementById("horsepowerMake").value = make.horsePower;
    document.getElementById("priceMake").value = make.price;
    document.getElementById("fueltypeMake").value = make.fuelType.name;

    let delBton = document.getElementById("deleteMake");
    delBton.setAttribute("onclick", `deleteMake(${id})`);
}

function ownerShowEdit(id) {
    let owner = getSpecificOwner(id, "id");
    document.getElementById("nameOwner").value = owner.name;
    document.getElementById("nifOwner").value = owner.nif;
    document.getElementById("phonenumberOwner").value = owner.phoneNumber;
    document.getElementById("datentryOwner").value = owner.dateEntry;
    document.getElementById("emailaddrOwner").value = owner.emailAddr;

    let delBton = document.getElementById("deleteOwner");
    delBton.setAttribute("onclick", `deleteOwner(${id})`);
}

function fineShowEdit(id) {
    let fine = getSpecificFine(id, "id");
    document.getElementById("priceFine").value = fine.price;
    if (fine.payed == true) { document.getElementById("payedFine").checked = true }
    else { document.getElementById("payedFine").checked = false }
    document.getElementById("descriptionFine").value = fine.description;
    document.getElementById("dateFine").value = fine.date;
    document.getElementById("ownerFine").value = fine.owner.name;
    document.getElementById("carFine").value = fine.car.name;

    let delBton = document.getElementById("deleteFine");
    delBton.setAttribute("onclick", `deleteFine(${id})`);
}