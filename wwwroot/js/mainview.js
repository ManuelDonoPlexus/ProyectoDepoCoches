const baseurl = "http://localhost:5206"
const uriCar = "/api/Car"
const uriColor = "/api/Color"
const uriDriver = "/api/Driver"
const uriCarDriver = "/api/CarDriver"
const uriMake = "/api/Make"
const uriOwner = "/api/Owner"
const uriFine = "/api/Fine"
const uriFuel = "/api/FuelType"

const uriRegister = "/api/Auth/register";
const uriLogin = "/api/Auth/login";
let token;

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
    token = getToken();
    console.log(token)
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
    document.getElementById("userAdd").style.display = "none";

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

// Para las busquedas y para mostrar vistas de cada entidad

function search(searchbar, tableSearch, searchfield) {
    const table = document.getElementById(tableSearch).getElementsByTagName("tr");
    const filter = document.getElementById(searchbar).value.toUpperCase();

    for (let i = 0; i < table.length; i++) {
        const txt = table[i].getElementsByClassName(searchfield)[0];
        if (txt.innerText.toUpperCase().indexOf(filter) > -1) { table[i].style.display = ""; }
        else { table[i].style.display = "none"; }
    }
}

// Para el login y el registro de usuario

async function login(email, password) {
    event.preventDefault();
    const newemail = document.getElementById(email);
    const newpassword = document.getElementById(password);

    const login = {
        email: newemail.value.trim(),
        password: newpassword.value.trim()
    }

    try {
        const response = await fetch(baseurl + uriLogin, {
            method: 'POST',
            headers: {
                "Accept": "*/*",
                "Content-Type": "application/json"
            },
            body: JSON.stringify(login)
        })

        token = await response.text();
        saveToken(token)
        window.location.href = "mainview.html";
    } catch (error) {
        throw error;
    }
}

async function register(username, email, password) {
    const newname = document.getElementById(username)
    const newemail = document.getElementById(email)
    const newpass = document.getElementById(password)

    if (newname.value.trim() == null && newemail.value.trim() == null && newpass.value.trim() == null) {
        return;
    }

    const newuser = {
        name: newname.value.trim(),
        password: newpass.value.trim(),
        email: newemail.value.trim()
    }

    try {
        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)

        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow'
        }

        fetch(baseurl + uriRegister, requestOptions)
            .then(respone => respone.json())
    } catch (error) {
        throw error;
    }
}

function saveToken(token){
    sessionStorage.setItem("token", token)
}

function getToken(){
    return sessionStorage.getItem("token")
}


// Para obtener los datos de ciertas entidades de la base de datos 

async function getCars() {
    try {
        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)

        var requestOptions = {
            method: 'GET',
            headers: myHeaders,
            redirect: 'follow'
        }

        const response = await fetch(baseurl + uriCar, requestOptions)
            .then((response) => response.json());
        cars = await response.value;
        _fillCarList();
        _displayCount(cars.length, "counterCar")
        _displayCars();
    } catch (error) {
        console.error('Unable to get cars: ', error);
    }
}

async function getCarDrivers() {
    try {
        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)

        var requestOptions = {
            method: 'GET',
            headers: myHeaders,
            redirect: 'follow'
        }

        const response = await fetch(baseurl + uriCarDriver, requestOptions)
            .then((response) => response.json());
        cardrivers = await response.value;
        _displayCount(cardrivers.length, "counterCarDriver");
        _displayCarDrivers();
    } catch (error) {
        console.error('Unable to get drivers: ', error);
    }
}

async function getColors() {
    try {
        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)

        var requestOptions = {
            method: 'GET',
            headers: myHeaders,
            redirect: 'follow'
        }

        const response = await fetch(baseurl + uriColor, requestOptions)
            .then((response) => response.json());
        colors = await response.value;
        _fillColorList();
    } catch (error) {
        console.error('Unable to get makes: ', error);
    }
}

async function getDrivers() {
    try {
        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)

        var requestOptions = {
            method: 'GET',
            headers: myHeaders,
            redirect: 'follow'
        }

        const response = await fetch(baseurl + uriDriver, requestOptions)
            .then((response) => response.json());
        drivers = await response.value;
        _displayCount(drivers.length, "counterDriver");
        _fillDriverList();
        _displayDrivers();
    } catch (error) {
        console.error('Unable to get drivers: ', error);
    }
}

async function getFines() {
    try {
        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)

        var requestOptions = {
            method: 'GET',
            headers: myHeaders,
            redirect: 'follow'
        }

        const response = await fetch(baseurl + uriFine, requestOptions)
            .then((response) => response.json());
        fines = await response.value;
        _displayCount(fines.length, "counterFine")
        _displayFines();
    } catch (error) {
        console.error('Unable to get drivers: ', error);
    }
}

async function getFuels() {
    try {
        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)

        var requestOptions = {
            method: 'GET',
            headers: myHeaders,
            redirect: 'follow'
        }

        const response = await fetch(baseurl + uriFuel, requestOptions)
            .then((response) => response.json());
        fuels = await response.value;
        _fillFuelList();
    } catch (error) {
        console.error('Unable to get drivers: ', error);
    }
}

async function getMakes() {
    try {
        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)

        var requestOptions = {
            method: 'GET',
            headers: myHeaders,
            redirect: 'follow'
        }

        const response = await fetch(baseurl + uriMake, requestOptions)
            .then((response) => response.json());
        makes = await response.value;
        _fillMakeList();
        _displayCount(makes.length, "counterMake");
        _displayMakes();
    } catch (error) {
        console.error('Unable to get makes: ', error);
    }
}

async function getOwners() {
    try {
        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)

        var requestOptions = {
            method: 'GET',
            headers: myHeaders,
            redirect: 'follow'
        }

        const response = await fetch(baseurl + uriOwner, requestOptions)
            .then((response) => response.json());
        owners = await response.value;
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
    for (let i = 0; i < cars.length; i++) {
        const car = cars[i];
        if (filter == "license") {
            if (car.license == search) { result = car; }
        } else if (filter == "id") {
            if (car.id == search) { result = car; }
        }
    }
    return result;
}

function getSpecificDriver(search, filter) {
    let result;
    for (let i = 0; i < drivers.length; i++) {
        const driver = driver[i];
        if (filter == "id") {
            if (driver.id == search) { result = driver; }
        } else if (filter == "name") {
            if (driver.name == search) { result = driver; }
        }
    }
    return result;
}

function getSpecificColor(search, filter) {
    let result;
    for (let i = 0; i < colors.length; i++) {
        const color = colors[i];
        if (filter == "id") {
            if (color.id == search) { result = color; }
        } else if (filter == "name") {
            if (color.name == search) { result = color; }
        }
    }
    return result;
}

function getSpecificOwner(search, filter) {
    let result;
    for (let i = 0; i < owners.length; i++) {
        const owner = owners[i];
        if (filter == "id") {
            if (owner.id == search) { result = owner; }
        } else if (filter == "name") {
            if (owner.name == search) { result = owner; }
        }
    }
    return result;
}

function getSpecificMake(search, filter) {
    let result;
    for (let i = 0; i < makes.length; i++) {
        const make = makes[i];
        if (filter == "id") {
            if (make.id == search) { result = make; }
        } else if (filter == "name") {
            if (make.name == search) { result = make; }
        }
    }
    return result;
}

function getSpecificFuel(search, filter) {
    let result;
    for (let i = 0; i < fuels.length; i++) {
        const fuel = fuels[i];
        if (filter == "id") {
            if (fuel.id == search) { result = fuel; }
        } else if (filter == "name") {
            if (fuel.name == search) { result = fuel; }
        }
    }
    return result;
}

function getSpecificFine(search, filter) {
    let result;
    for (let i = 0; i < fines.length; i++) {
        const fine = fines[i];
        if (filter == "id") {
            if (fine.id == search) { result = fine; }
        } else if (filter == "name") {
            if (fine.name == search) { result = fine; }
        }
    }
    return result;
}

// Para llenar DataLists, necesarias para formar las listas de datos

function _fillCarList() {
    const list = document.getElementById("carList");
    const option = document.createElement("option");

    for (let i = 0; i < cars.length; i++) {
        const car = cars[i];
        let optionCar = option.cloneNode(false);
        optionCar.setAttribute("value", `${car.license}`)
        list.appendChild(optionCar)
    }
}

function _fillDriverList() {
    const list = document.getElementById("driverList");
    const option = document.createElement("option");

    for (let i = 0; i < drivers.length; i++) {
        const driver = drivers[i];
        let optionDriver = option.cloneNode(false);
        optionDriver.setAttribute("value", `${driver.name}`)
        list.appendChild(optionDriver)
    }
}

function _fillColorList() {
    const list = document.getElementById("colorList");
    const option = document.createElement("option");

    for (let i = 0; i < colors.length; i++) {
        const color = colors[i];
        let optionColor = option.cloneNode(false);
        optionColor.setAttribute("value", `${color.name}`)
        list.appendChild(optionColor)
    }
}

function _fillFuelList() {
    const list = document.getElementById("fuelList");
    const option = document.createElement("option");

    for (let i = 0; i < fuels.length; i++) {
        const fuel = fuels[i];
        let optionFuel = option.cloneNode(false);
        optionFuel.setAttribute("value", `${fuel.name}`)
        list.appendChild(optionFuel)
    }
}

function _fillMakeList() {
    const list = document.getElementById("makeList");
    const option = document.createElement("option");

    for (let i = 0; i < makes.length; i++) {
        const make = makes[i];
        let optionMake = option.cloneNode(false);
        optionMake.setAttribute("value", `${make.name}`)
        list.appendChild(optionMake)
    }
}

function _fillOwnerList() {
    const list = document.getElementById("ownerList");
    const option = document.createElement("option");

    for (let i = 0; i < owners.length; i++) {
        const owner = owners[i];
        let optionOwner = option.cloneNode(false);
        optionOwner.setAttribute("value", `${owner.name}`);
        list.appendChild(optionOwner);
    }
}

// Mostrar todos los datos

function _displayCars() {
    const tBody = document.getElementById("carsTBody");
    tBody.innerHTML;

    const bton = document.createElement("button");

    for (let i = 0; i < cars.length; i++) {
        const car = cars[i];
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
    }
}

function _displayDrivers() {
    const tBody = document.getElementById("driversTBody");
    tBody.innerHTML;

    const bton = document.createElement("button");

    for (let i = 0; i < drivers.length; i++) {
        const driver = drivers[i];
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
    }
}

function _displayCarDrivers() {
    const tBody = document.getElementById("cardriversTBody");
    tBody.innerHTML;

    const bton = document.createElement("button");

    for (let i = 0; i < cardrivers.length; i++) {
        const driver = cardrivers[i];
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
    }
}

function _displayMakes() {
    const tBody = document.getElementById("makesTBody");
    tBody.innerHTML;

    const bton = document.createElement("button");

    for (let i = 0; i < makes.length; i++) {
        const make = makes[i];
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
    }
}

function _displayOwners() {
    const tBody = document.getElementById("ownersTBody");
    tBody.innerHTML;

    const bton = document.createElement("button");

    for (let i = 0; i < owners.length; i++) {
        const owner = owners[i];
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
    }
}

function _displayFines() {
    const tBody = document.getElementById("finesTBody");
    tBody.innerHTML;

    const bton = document.createElement("button");

    for (let i = 0; i < fines.length; i++) {
        const fine = fines[i];
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

// Show Editform

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
    var license = document.getElementById("licenseCar");
    var kms = document.getElementById("kmsCar");
    var color = document.getElementById("colorCar");
    var owner = document.getElementById("ownerCar");
    var make = document.getElementById("makeCar");

    license.value = car.license;
    license.disabled = true
    kms.value = car.kms;
    kms.disabled = true;
    color.value = car.color.name;
    color.disabled = true;
    owner.value = car.owner.name;
    owner.disabled = true;
    make.value = car.make.name;
    make.disabled = true;

    let delBton = document.getElementById("deleteEditCar");
    delBton.setAttribute("onclick", `deleteCar(${id})`);

    let enableBton = document.getElementById("enableEditCar");
    enableBton.setAttribute("onclick", `enableEditCar(${id})`);

    let saveBton = document.getElementById("saveEditCar");
    saveBton.setAttribute("onclick", `saveEditCar(${id})`);
}

function makeShowEdit(id) {
    let make = getSpecificMake(id, "id");
    var name = document.getElementById("nameMake");
    var horsePower = document.getElementById("horsepowerMake");
    var price = document.getElementById("priceMake");
    var fuel = document.getElementById("fueltypeMake");

    name.value = make.name;
    name.disabled = true;
    horsePower.value = make.horsePower;
    horsePower.disabled = true;
    price.value = make.price;
    price.disabled = true;
    fuel.value = make.fuelType.name;
    fuel.disabled = true;

    let delBton = document.getElementById("deleteEditMake");
    delBton.setAttribute("onclick", `deleteMake(${id})`);

    let enableBton = document.getElementById("enableEditMake");
    enableBton.setAttribute("onclick", `enableEditMake(${id})`);

    let saveBton = document.getElementById("saveEditMake");
    saveBton.setAttribute("onclick", `saveEditMake(${id})`);
}

function driverShowEdit(id) {
    let driver = getSpecificDriver(id, "id");
    var name = document.getElementById("nameDriver");
    var dni = document.getElementById("dniDriver");
    var email = document.getElementById("emailaddrDriver");
    var phone = document.getElementById("phonenumberDriver");
    var owner = document.getElementById("ownerDriver");

    name.value = driver.name;
    name.disabled = true;
    dni.value = driver.dni;
    dni.disabled = true;
    email.value = driver.emailAddr;
    email.disabled = true;
    phone.value = driver.phoneNumber;
    phone.disabled = true;
    owner.value = driver.owner.name;
    owner.disabled = true;

    let delBton = document.getElementById("deleteDriver");
    delBton.setAttribute("onclick", `deleteDriver(${id})`);

    let enableBton = document.getElementById("enableEditDriver");
    enableBton.setAttribute("onclick", `enableEditDriver(${id})`);

    let saveBton = document.getElementById("saveEditDriver");
    saveBton.setAttribute("onclick", `saveEditDriver(${id})`);
}

function ownerShowEdit(id) {
    let owner = getSpecificOwner(id, "id");
    var name = document.getElementById("nameOwner");
    var nif = document.getElementById("nifOwner");
    var phone = document.getElementById("phonenumberOwner");
    var date = document.getElementById("datentryOwner");
    var email = document.getElementById("emailaddrOwner");

    name.value = owner.name;
    name.disabled = true;
    nif.value = owner.nif;
    nif.disabled = true;
    phone.value = owner.phoneNumber;
    phone.disabled = true;
    date.value = owner.dateEntry;
    date.disabled = true;
    email.value = owner.emailAddr;
    email.disabled = true;

    let delBton = document.getElementById("deleteOwner");
    delBton.setAttribute("onclick", `deleteOwner(${id})`);

    let enableBton = document.getElementById("enableEditOwner");
    enableBton.setAttribute("onclick", `enableEditOwner(${id})`);

    let saveBton = document.getElementById("saveEditOwner");
    saveBton.setAttribute("onclick", `saveEditOwner(${id})`);
}

function fineShowEdit(id) {
    let fine = getSpecificFine(id, "id");
    var price = document.getElementById("priceFine");
    var payed = document.getElementById("payedFine");
    var description = document.getElementById("descriptionFine");
    var date = document.getElementById("dateFine");
    var owner = document.getElementById("ownerFine");
    var car = document.getElementById("carFine");

    price.value = fine.price;
    price.disabled = true;
    if (fine.payed == true) { payed.checked = true }
    else { payed.checked = false }
    payed.disabled = true;
    description.value = fine.description;
    description.disabled = true;
    date.value = fine.date;
    date.disabled = true;
    owner.value = fine.owner.name;
    owner.disabled = true;
    car.value = fine.car.name;
    car.disabled = true;

    let delBton = document.getElementById("deleteFine");
    delBton.setAttribute("onclick", `deleteFine(${id})`);

    let enableBton = document.getElementById("enableEditFine");
    enableBton.setAttribute("onclick", `enableEditFine(${id})`);

    let saveBton = document.getElementById("saveEditFine");
    saveBton.setAttribute("onclick", `saveEditFine(${id})`);
}

// Enable Editform

function enableEditCar(id) {
    var license = document.getElementById("licenseCar");
    var kms = document.getElementById("kmsCar");
    var color = document.getElementById("colorCar");
    var owner = document.getElementById("ownerCar");
    var make = document.getElementById("makeCar");

    license.disabled = false;
    kms.disabled = false;
    color.disabled = false;
    owner.disabled = false;
    make.disabled = false;

    let enableBton = document.getElementById("enableEditCar");
    enableBton.hidden = true;

    let saveBton = document.getElementById("saveEditCar");
    saveBton.hidden = false;
}

function enableEditMake(id) {
    var name = document.getElementById("nameMake");
    var horsePower = document.getElementById("horsepowerMake");
    var price = document.getElementById("priceMake");
    var fuel = document.getElementById("fueltypeMake");

    name.disabled = false;
    horsePower.disabled = false;
    price.disabled = false;
    fuel.disabled = false;

    let enableBton = document.getElementById("enableEditMake");
    enableBton.hidden = true;

    let saveBton = document.getElementById("saveEditMake");
    saveBton.hidden = false;
}

function enableEditDriver(id) {
    var name = document.getElementById("nameDriver");
    var dni = document.getElementById("dniDriver");
    var email = document.getElementById("emailaddrDriver");
    var phone = document.getElementById("phonenumberDriver");
    var owner = document.getElementById("ownerDriver");

    name.disabled = false;
    dni.disabled = false;
    email.disabled = false;
    phone.disabled = false;
    owner.disabled = false;

    let enableBton = document.getElementById("enableEditDriver");
    enableBton.hidden = true;

    let saveBton = document.getElementById("saveEditDriver");
    saveBton.hidden = false;
}

function enableEditOwner(id) {
    var name = document.getElementById("nameOwner");
    var nif = document.getElementById("nifOwner");
    var phone = document.getElementById("phonenumberOwner");
    var date = document.getElementById("datentryOwner");
    var email = document.getElementById("emailaddrOwner");

    name.disabled = false;
    nif.disabled = false;
    phone.disabled = false;
    date.disabled = false;
    email.disabled = false;

    let enableBton = document.getElementById("enableEditOwner");
    enableBton.hidden = true;

    let saveBton = document.getElementById("saveEditOwner");
    saveBton.hidden = false;
}

function enableEditFine(id) {
    var price = document.getElementById("priceFine");
    var payed = document.getElementById("payedFine");
    var description = document.getElementById("descriptionFine");
    var date = document.getElementById("dateFine");
    var owner = document.getElementById("ownerFine");
    var car = document.getElementById("carFine");

    price.disabled = false;
    payed.disabled = false;
    description.disabled = false;
    date.disabled = false;
    owner.disabled = false;
    car.disabled = false;

    let enableBton = document.getElementById("enableEditFine");
    enableBton.hidden = true;

    let saveBton = document.getElementById("saveEditFine");
    saveBton.hidden = false;
}

// Save changes from Edit

function saveEditCar() {
    var editLicense = document.getElementById("licenseCar");
    var editKms = document.getElementById("kmsCar");

    var editColorName = document.getElementById("colorCar").value.trim();
    var editOwnerName = document.getElementById("ownerCar").value.trim();
    var editMakeName = document.getElementById("makeCar").value.trim();
    editColor = getSpecificColor(editColorName, "name");
    editOwner = getSpecificOwner(editOwnerName, "name");
    editMake = getSpecificMake(editMakeName, "name");

    const car = {
        license: editLicense.value.trim(),
        kms: editKms.value.trim(),
        colorId: editColor.id,
        ownerId: editOwner.id,
        makeId: editMake.id
    }
    fetch(baseurl + uriCar, {
        method: 'PUT',
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

function saveEditMake() {
    var editName = document.getElementById("nameMake");
    var editHorsePower = document.getElementById("horsepowerMake");
    var editPrice = document.getElementById("priceMake");

    var editFuelName = document.getElementById("fueltypeMake").value.trim();
    editFuel = getSpecificFuel(editFuelName, "name")


    const make = {
        name: editName.value.trim(),
        horsePower: editHorsePower.value.trim(),
        price: editPrice.value.trim(),
        fuelTypeId: editFuel.id
    }
    fetch(baseurl + uriMake, {
        method: 'PUT',
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

function saveEditDriver() {
    var editName = document.getElementById("nameDriver");
    var editDni = document.getElementById("dniDriver");
    var editEmail = document.getElementById("emailaddrDriver");
    var editPhone = document.getElementById("phonenumberDriver");

    var editOwnerName = document.getElementById("ownerDriver").value.trim();
    editOwner = getSpecificOwner(editOwnerName)

    const driver = {
        name: editName.value.trim(),
        dni: editDni.value.trim(),
        emailAddr: editEmail.value.trim(),
        phoneNumber: editPhone.value.trim(),
        ownerId: editOwner.id
    }
    fetch(baseurl + uriDriver, {
        method: 'PUT',
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

function saveEditOwner() {
    var editName = document.getElementById("nameOwner");
    var editNif = document.getElementById("nifOwner");
    var editPhone = document.getElementById("phonenumberOwner");
    var editDate = document.getElementById("datentryOwner");
    var editEmail = document.getElementById("emailaddrOwner");

    const owner = {
        name: editName.value.trim(),
        nif: editNif.value.trim(),
        phoneNumber: editPhone.value.trim(),
        dateEntry: editDate.value.trim(),
        emailAddr: editEmail.value.trim()
    }
    fetch(baseurl + uriOwner, {
        method: 'PUT',
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

function saveEditFine() {
    var editPrice = document.getElementById("priceFine");
    var editPayed = document.getElementById("payedFine");
    var editDescription = document.getElementById("descriptionFine");
    var editDate = document.getElementById("dateFine");

    var editOwnerName = document.getElementById("ownerFine").value.trim();
    var editCarName = document.getElementById("carFine").value.trim();
    editOwner = getSpecificOwner(editOwnerName, "name");
    editCar = getSpecificCar(editCarName, "name")

    var payed;
    if (editPayed.checked == true) { payed = true }
    else { payed = false }

    const fine = {
        price: editPrice.value.trim(),
        date: editDate.value.trim(),
        description: editDescription.value.trim(),
        payed: payed,
        ownerId: editOwner.id,
        carId: editCar.id,

    }
    fetch(baseurl + uriFine, {
        method: 'PUT',
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(fine)
    })
        .then(() => location.reload())
        .catch(error => console.error("Unable to add fine to database. ", error))
}



