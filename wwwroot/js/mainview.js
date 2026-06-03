// URLs

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
const uriStatistics = "/api/Stadistics/"

// Datos de usuario

let token;
let usr;

// Arrays de información

let cars = [];
let colors = [];
let drivers = [];
let cardrivers = [];
let makes = [];
let owners = [];
let fines = [];
let fuels = [];

// Estadisticas

let colorcount = [];
let makecount = [];
let ownercount = [];
let cardrivercount = [];
let avgprice = 0.0;
let avgkms = 0.0;

// Vistas

let currentview = "welcomeview";
let currentdriverview = "drivers";
let currentadddriver = "";

// Paginación

let from = 0;
let pages = 0;
let tablecontent = [];
let pagelimit = 6;

// Función para inicializar

function initialize() {
    token = getToken();
    usr = getUsr();
    usrname = usr.split('@')[0]
    currentdriverview = "drivers";
    document.getElementById("welcome-msg").innerText = "Cuenta activa: " + usrname;
    document.getElementById("carview").style.display = "none";
    document.getElementById("makeview").style.display = "none";
    document.getElementById("ownerview").style.display = "none";
    document.getElementById("driverview").style.display = "none";
    document.getElementById("fineview").style.display = "none";
    document.getElementById("stadisticsview").style.display = "none";
    document.getElementById("deleteconfirm").style.display = "none";

    document.getElementById("carCreate").style.display = "none";
    document.getElementById("makeCreate").style.display = "none";
    document.getElementById("ownerCreate").style.display = "none";
    document.getElementById("driverCreate").style.display = "none";
    document.getElementById("cardriverCreate").style.display = "none";
    document.getElementById("fineCreate").style.display = "none";
    document.getElementById("userAdd").style.display = "none";

    document.getElementById("drivers").style.display = "none";
    document.getElementById("cardrivers").style.display = "none";

    document.getElementById("cardetailView").style.display = "none";
    document.getElementById("cardriverdetailView").style.display = "none";
    document.getElementById("makedetailView").style.display = "none";
    document.getElementById("ownerdetailView").style.display = "none";
    document.getElementById("driverdetailView").style.display = "none";
    document.getElementById("finedetailView").style.display = "none";

    getColors();
    getFuels();
    getStatistics();
    displayTable("carsTBody")
    displayTable("cardriversTBody")
    displayTable("driversTBody")
    displayTable("makesTBody")
    displayTable("ownersTBody")
    displayTable("finesTBody")
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

function showAddDriver(id) {
    if (document.getElementById(currentadddriver) != null) { document.getElementById(currentadddriver).style.display = "none" }

    document.getElementById(id).style.display = "initial";
    currentadddriver = id;
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
    const errormsg = document.getElementById("error-msg");

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

        if ((response.ok)) {
            token = await response.text();
            saveToken(token, newemail.value.trim())
            window.location.href = "mainview.html";
        } else {
            errormsg.style.visibility = "visible"
        }
    } catch (error) {
        throw error;
    }
}

function logout() {
    token = "";
    usr = "";
    window.location.href = "index.html";
}

function saveToken(token, usr) {
    sessionStorage.setItem("token", token)
    sessionStorage.setItem("usr", usr)
}

function getToken() {
    return sessionStorage.getItem("token")
}

function getUsr() {
    return sessionStorage.getItem("usr")
}

// Para obtener los datos de ciertas entidades de la base de datos 

async function getCars() {
    try {
        cars = [];
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
        return (cars)
    } catch (error) {
        console.error('Unable to get cars: ', error);
    }
}

async function getCarDrivers() {
    try {
        cardrivers = [];
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
    } catch (error) {
        console.error('Unable to get drivers: ', error);
    }
}

async function getColors() {
    try {
        colors = [];
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
        drivers = [];
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
    } catch (error) {
        console.error('Unable to get drivers: ', error);
    }
}

async function getFines() {
    try {
        fines = [];
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
    } catch (error) {
        console.error('Unable to get drivers: ', error);
    }
}

async function getFuels() {
    try {
        fuels = [];
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
        makes = [];
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
    } catch (error) {
        console.error('Unable to get makes: ', error);
    }
}

async function getOwners() {
    try {
        owners = [];
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
    } catch (error) {
        console.error('Unable to get owners: ', error)
    }
}

async function getStatistics() {
    try {
        colorcount = [];
        makecount = [];
        ownercount = [];
        cardrivercount = [];
        fuelcount = [];

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)

        var requestOptions = {
            method: 'GET',
            headers: myHeaders,
            redirect: 'follow'
        }

        let responseColorCount = await fetch(baseurl + uriStatistics + "colorcount", requestOptions)
            .then((response) => response.json())
        colorcount = await responseColorCount;

        let responseMakeCount = await fetch(baseurl + uriStatistics + "makecount", requestOptions)
            .then((response) => response.json());
        makecount = await responseMakeCount;

        let responseOwnerCount = await fetch(baseurl + uriStatistics + "ownercount", requestOptions)
            .then((response) => response.json());
        ownercount = await responseOwnerCount;

        let responseCarDriverCount = await fetch(baseurl + uriStatistics + "cdcarcount", requestOptions)
            .then((response) => response.json());
        cardrivercount = await responseCarDriverCount;

        let averageprice = await fetch(baseurl + uriStatistics + "averageprice", requestOptions)
            .then((response) => response.json());
        avgprice = await averageprice;

        let averagekms = await fetch(baseurl + uriStatistics + "averagekms", requestOptions)
            .then((response) => response.json());
        avgkms = await averagekms;

        _fillStatistics();
        chartify()
    } catch (error) {
        console.error('Unable to get stadistics: ', error)
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

function getSpecificCarDriver(search, filter) {
    let result;

    for (let i = 0; i < cardrivers.length; i++) {
        const cardriver = cardrivers[i];
        if (filter == "name") {
            if (cardriver.name == search) { result = cardriver; }
        } else if (filter == "id") {
            if (cardriver.id == search) { result = cardriver; }
        }
    }
    return result;
}

function getSpecificDriver(search, filter) {
    let result;
    for (let i = 0; i < drivers.length; i++) {
        const driver = drivers[i];
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

function _fillStatistics() {
    const colorX = [];
    const colorY = [];

    const makeX = [];
    const makeY = [];

    const ownerX = [];
    const ownerY = [];

    const cardriverX = [];
    const cardriverY = [];

    for (let i = 0; i < colorcount.length; i++) {
        const element = colorcount[i];
        let color = getSpecificColor(element.key, 'id');
        colorY.push(element.value)
        colorX.push(color.name)
    }

    chartify(colorX, colorY, "color-chart", "Recuento de coches por colores")

    for (let i = 0; i < makecount.length; i++) {
        const element = makecount[i];
        let make = getSpecificMake(element.key, 'id');
        makeY.push(element.value)
        makeX.push(make.name)
    }

    chartify(makeX, makeY, "make-chart", "Recuento de coches por modelo")

    for (let i = 0; i < ownercount.length; i++) {
        const element = ownercount[i];
        let owner = getSpecificOwner(element.key, 'id');
        ownerX.push(owner.name)
        ownerY.push(element.value)
    }

    chartify(ownerX, ownerY, "owner-chart", "Recuento de coches por propietario")

    for (let i = 0; i < cardrivercount.length; i++) {
        const element = cardrivercount[i];
        let car = getSpecificCar(element.key, 'id');
        cardriverX.push(car.license);
        cardriverY.push(element.value);
    }

    chartify(cardriverX, cardriverY, "cardriver-chart", "Recuento de conductores por modelo")
}

async function chartify(labelsX, dataY, chart, msg) {
    try {
        const data = [{
            x: labelsX,
            y: dataY,
            type: "bar",
            orientation: "v",
            marker: { color: "rgba(0,0,255,0.5)" }
        }];
        const layout = {
            title: msg,
            autosize: false,
            width: 400,
            height: 300,
            margin: {
                l: 100,
                r: 100,
                t: 0,
                r: 0,
                p: 4
            }
        };

        Plotly.newPlot(chart, data, layout);
    } catch (error) {
        console.log("Ha ocurido un error: " + error)
    }
}

// Mostrar todos los datos

async function displayTable(tableid) {

    switch (tableid) {
        case "carsTBody":
            await getCars()
            pages = cars.length / pagelimit;
            tablecontent = cars.slice(from, pagelimit);
            loadCarTableContent(tablecontent);
            loadPages("cars", "carTablepages", pages);
            break
        case "driversTBody":
            await getDrivers();
            pages = drivers.length / pagelimit;
            tablecontent = drivers.slice(from, pagelimit);
            loadDriverTableContent(tablecontent);
            loadPages("drivers", "driverTablepages", pages);
            break
        case "cardriversTBody":
            await getCarDrivers();
            pages = cardrivers.length / pagelimit;
            tablecontent = cardrivers.slice(from, pagelimit);
            loadCarDriverTableContent(tablecontent);
            loadPages("cardrivers", "cardriverTablepages", pages);
            break
        case "ownersTBody":
            await getOwners();
            pages = owners.length / pagelimit;
            tablecontent = owners.slice(from, pagelimit);
            loadOwnerTableContent(tablecontent);
            loadPages("owners", "ownerTablepages", pages);
            break
        case "makesTBody":
            await getMakes();
            pages = makes.length / pagelimit;
            tablecontent = makes.slice(from, pagelimit);
            loadMakesTableContent(tablecontent);
            loadPages("makes", "makeTablepages", pages);
            break
        case "finesTBody":
            await getFines();
            pages = fines.length / pagelimit;
            tablecontent = fines.slice(from, pagelimit);
            loadFinesTableContent(tablecontent);
            loadPages("fines", "fineTablepages", pages);
            break
        default:
            break
    }
}

function loadPages(tablename, tableid, pages) {
    let tablepages = document.getElementById(tableid)
    tablepages.innerHTML = "";

    for (let i = 0; i < pages; i++) {
        const element = document.createElement("li");
        const bton = `<button class="page-bton" onclick="nextPage(${tablename},${i})">${i + 1}</button>`
        element.innerHTML = bton;
        tablepages.appendChild(element);
    }
}

function nextPage(infotable, page) {
    let activePage = page + 1
    let from = pagelimit * page
    let info = [];

    if (from <= infotable.length) {
        let tablecontent = infotable.slice(from, pagelimit * activePage)

        switch (infotable) {
            case cars:
                loadCarTableContent(tablecontent)
                break;
            case drivers:
                loadDriverTableContent(tablecontent)
                break;
            case cardrivers:
                loadCarDriverTableContent(tablecontent)
                break;
            case owners:
                loadOwnerTableContent(tablecontent)
                break;
            case makes:
                loadMakesTableContent(tablecontent)
                break;
            case fines:
                loadFinesTableContent(tablecontent)
                break;
            default:
                break;
        }
    }
}

function loadCarTableContent(tablecontent) {
    const tBody = document.getElementById("carsTBody");
    tBody.innerHTML = '';

    const bton = document.createElement("button");

    for (let i = 0; i < tablecontent.length; i++) {
        const car = tablecontent[i];
        let detailsBton = bton.cloneNode(false);
        detailsBton.innerText = "Detalles";
        detailsBton.setAttribute("onclick", `showEditForm('cardetailView',${car.id})`);
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `showDeleteConfirm(${car.id}, 'car')`);
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

function loadDriverTableContent(tablecontent) {
    const tBody = document.getElementById("driversTBody");
    tBody.innerHTML = '';

    const bton = document.createElement("button");

    for (let i = 0; i < tablecontent.length; i++) {
        const driver = tablecontent[i];
        let detailsBton = bton.cloneNode(false);
        detailsBton.innerText = "Detalles";
        detailsBton.setAttribute("onclick", `showEditForm('driverdetailView',${driver.id})`);
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `showDeleteConfirm(${driver.id}, 'driver')`);
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

function loadFinesTableContent(tablecontent) {
    const tBody = document.getElementById("finesTBody");
    tBody.innerHTML = '';

    const bton = document.createElement("button");

    for (let i = 0; i < tablecontent.length; i++) {
        const fine = tablecontent[i];
        let detailsBton = bton.cloneNode(false);
        detailsBton.innerText = "Detalles";
        detailsBton.setAttribute("onclick", `showEditForm('finedetailView',${fine.id})`);
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `showDeleteConfirm(${fine.id}, 'fine')`);
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
        let txtNode3;
        if (fine.payed == true) { txtNode3 = document.createTextNode("Pagada"); }
        else { txtNode3 = document.createTextNode("No pagada"); }
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

function loadCarDriverTableContent(tablecontent) {
    const tBody = document.getElementById("cardriversTBody");
    tBody.innerHTML = '';

    const bton = document.createElement("button");

    for (let i = 0; i < tablecontent.length; i++) {
        const cardriver = tablecontent[i];
        let detailsBton = bton.cloneNode(false);
        detailsBton.innerText = "Detalles";
        detailsBton.setAttribute("onclick", `showEditForm('cardriverdetailView',${cardriver.id})`);
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `showDeleteConfirm(${cardriver.id}, 'driver')`);
        deleteBton.setAttribute("class", "deleteBton");

        let tr = tBody.insertRow();
        tr.setAttribute("class", "cardriver")

        let td0 = tr.insertCell(0);
        td0.setAttribute("class", "cardriverName");
        let txtNode0 = document.createTextNode(cardriver.driver.name);
        td0.appendChild(txtNode0);

        let td1 = tr.insertCell(1);
        td1.setAttribute("class", "cardriverDate");
        let txtNode1 = document.createTextNode(cardriver.dateDrive);
        td1.appendChild(txtNode1);

        let td2 = tr.insertCell(2);
        td2.setAttribute("class", "cardriverAssociatedCar");
        let txtNode2 = document.createTextNode(cardriver.car.license);
        td2.appendChild(txtNode2);

        let td3 = tr.insertCell(3);
        td3.appendChild(detailsBton);

        let td4 = tr.insertCell(4);
        td4.appendChild(deleteBton);
    }
}

function loadOwnerTableContent(tablecontent) {
    const tBody = document.getElementById("ownersTBody");
    tBody.innerHTML = '';

    const bton = document.createElement("button");

    for (let i = 0; i < tablecontent.length; i++) {
        const owner = tablecontent[i];
        let detailsBton = bton.cloneNode(false);
        detailsBton.innerText = "Detalles";
        detailsBton.setAttribute("onclick", `showEditForm('ownerdetailView',${owner.id})`);
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `showDeleteConfirm(${owner.id}, 'owner')`);
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

function loadMakesTableContent(tablecontent) {
    const tBody = document.getElementById("makesTBody");
    tBody.innerHTML = '';

    const bton = document.createElement("button");

    for (let i = 0; i < tablecontent.length; i++) {
        const make = tablecontent[i];
        let detailsBton = bton.cloneNode(false);
        detailsBton.innerText = "Detalles";
        detailsBton.setAttribute("onclick", `showEditForm('makedetailView',${make.id})`);
        detailsBton.setAttribute("class", "detailsBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `showDeleteConfirm(${make.id}, 'make')`);
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

// Para añadir nuevas entidades a la base de datos

function addCar() {
    try {
        const addLicense = document.getElementById("add-car-license");
        const addKms = document.getElementById("add-car-kms");
        const addColorName = document.getElementById("add-car-color");
        const addOwnerName = document.getElementById("add-car-owner");
        const addMakeName = document.getElementById("add-car-make");

        const addColor = getSpecificColor(addColorName.value.trim(), "name");
        const addOwner = getSpecificOwner(addOwnerName.value.trim(), "name");
        const addMake = getSpecificMake(addMakeName.value.trim(), "name");

        const newcar = {
            license: addLicense.value.trim(),
            kms: addKms.value.trim(),
            colorId: addColor.id,
            ownerId: addOwner.id,
            makeId: addMake.id
        }

        addLicense.value = "";
        addKms.value = "";
        addColorName.value = "";
        addOwnerName.value = "";
        addMakeName.value = "";

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(newcar)
        }

        fetch(baseurl + uriCar, requestOptions)
            .then(() => displayTable('carsTBody'))
            .then(() => showView('carview'))
    } catch (error) {
        console.error("Unable to add fine to database. ", error)
    }
}

function addCarDriver() {
    try {
        const addCDDate = document.getElementById("add-cardriver-date");
        const addCDCar = document.getElementById("add-cardriver-car");
        const addCDDriver = document.getElementById("add-cardriver-driver");

        const CDCar = getSpecificCar(addCDCar.value.trim(), "license")
        const CDDriver = getSpecificDriver(addCDDriver.value.trim(), "name")

        const newCarDriver = {
            dateDrive: addCDDate.value.trim(),
            carCDId: CDCar.id,
            driverCDId: CDDriver.id
        }

        addCDDate.value = "";
        addCDCar.value = "";
        addCDDriver.value = "";

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(newCarDriver)
        }

        fetch(baseurl + uriCarDriver, requestOptions)
            .then(() => displayTable('cardriversTBody'))
            .then(() => showView('driverview'))
    } catch (error) {
        console.error("Unable to add car driver to database. ", error)
    }
}

function addDriver() {
    try {
        const addName = document.getElementById("add-driver-name");
        const addDni = document.getElementById("add-driver-dni");
        const addEmail = document.getElementById("add-driver-email");
        const addTel = document.getElementById("add-driver-tel");
        const addOwnerName = document.getElementById("add-driver-owner");

        const addOwner = getSpecificOwner(addOwnerName.value.trim(), "name");

        const newdriver = {
            name: addName.value.trim(),
            dni: addDni.value.trim(),
            emailAddr: addEmail.value.trim(),
            phoneNumber: addTel.value.trim(),
            ownerId: addOwner.id
        }

        addName.value = "";
        addDni.value = "";
        addEmail.value = "";
        addTel.value = "";
        addOwnerName.value = "";

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(newdriver)
        }

        fetch(baseurl + uriDriver, requestOptions)
            .then(() => displayTable('driversTBody'))
            .then(() => showView('driverview'))
    } catch (error) {
        console.error("Unable to add fine to database. ", error)
    }
}

function addFine() {
    try {
        const addPrice = document.getElementById("add-fine-price");
        const addDescription = document.getElementById("add-fine-description");
        const addDate = document.getElementById("add-fine-date");
        const addCarName = document.getElementById("add-fine-car");
        const addOwnerName = document.getElementById("add-fine-owner")

        const addCar = getSpecificCar(addCarName.value.trim(), "license");
        const addOwner = getSpecificOwner(addOwnerName.value.trim(), "name");

        const newfine = {
            price: addPrice.value.trim(),
            description: addDescription.value.trim(),
            date: addDate.value.trim(),
            payed: false,
            ownerId: addOwner.id,
            carId: addCar.id,
        }

        addPrice.value = "";
        addDescription.value = "";
        addDate.value = "";
        addCarName.value = "";
        addOwnerName.value = "";

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(newfine)
        }

        fetch(baseurl + uriFine, requestOptions)
            .then(() => displayTable('finesTBody'))
            .then(() => showView('fineview'))
    } catch (error) {
        console.error("Unable to add fine to database. ", error)
    }

}

function addMake() {
    try {
        const addName = document.getElementById("add-make-name");
        const addHP = document.getElementById("add-make-horsepower");
        const addPrice = document.getElementById("add-make-price");
        const addFuelName = document.getElementById("add-make-fuel");

        const addFuel = getSpecificFuel(addFuelName.value.trim(), "name")

        const newmake = {
            name: addName.value.trim(),
            horsePower: addHP.value.trim(),
            price: addPrice.value.trim(),
            fuelTypeId: addFuel.id
        }

        addName.value = "";
        addHP.value = "";
        addPrice.value = "";
        addFuelName.value = "";

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(newmake)
        }

        fetch(baseurl + uriMake, requestOptions)
            .then(() => displayTable('makesTBody'))
            .then(() => showView('makeview'))
    } catch (error) {
        console.error("Unable to add make to database. ", error);
    }
}

function addOwner() {
    try {
        const addName = document.getElementById("add-owner-name");
        const addNif = document.getElementById("add-owner-nif");
        const addTel = document.getElementById("add-owner-tel");
        const addDate = document.getElementById("add-owner-date");
        const addEmail = document.getElementById("add-owner-email");

        const newowner = {
            name: addName.value.trim(),
            nif: addNif.value.trim(),
            phoneNumber: addTel.value.trim(),
            dateEntry: addDate.value.trim(),
            emailAddr: addEmail.value.trim()
        }

        addName.value = "";
        addNif.value = "";
        addTel.value = "";
        addDate.value = "";
        addEmail.value = "";

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(newowner)
        }

        fetch(baseurl + uriOwner, requestOptions)
            .then(() => displayTable('ownersTBody'))
            .then(() => showView('ownerview'))
    } catch (error) {
        console.error('Unable to add owner: ', error)
    }
}

function registerUser(username, email, password) {
    try {
        const newname = document.getElementById("add-user-name")
        const newemail = document.getElementById("add-user-email")
        const newpassinit = document.getElementById("add-user-password-init")
        const newpassconf = document.getElementById("add-user-password-conf")

        if (newname.value.trim() == null && newemail.value.trim() == null && newpassinit.value.trim() == null) {
            return;
        }

        if (newpassinit.value.trim() != newpassconf.value.trim()) {
            return;
        }

        const newuser = {
            name: newname.value.trim(),
            password: newpassinit.value.trim(),
            email: newemail.value.trim()
        }

        newname.value = "";
        newemail.value = "";
        newpassinit.value = "";
        newpassconf.value = "";

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(newuser)
        }

        fetch(baseurl + uriRegister, requestOptions)
            .then(respone => respone.json())
    } catch (error) {
        throw error;
    }
}

// Para borrar entidades de la base de datos

function showDeleteConfirm(id, classname) {
    showView("deleteconfirm");
    deletetitle = document.getElementById("delete-title");
    deletebtons = document.getElementById("delete-btons");
    const bton = document.createElement("button");

    if (classname == "car") {
        deletebtons.innerHTML = '';
        let car = getSpecificCar(id, "id");
        deletetitle.innerText = "¿Borrar " + car.license + "?";

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `deleteCar(${id})`);
        deleteBton.setAttribute("class", "viewDeleteBton");

        let cancelBton = bton.cloneNode(false);
        cancelBton.innerText = "Cancelar";
        cancelBton.setAttribute("onclick", `showView('carview')`);
        cancelBton.setAttribute("class", "cancelDeleteBton");

        deletebtons.appendChild(cancelBton);
        deletebtons.appendChild(deleteBton);
    }

    if (classname == "cardriver") {
        deletebtons.innerHTML = '';
        let cardriver = getSpecificCarDriver(id, "id")
        deletetitle.innerText = "¿Borrar a " + cardriver.name + "?"

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `deleteDriver(${id})`);
        deleteBton.setAttribute("class", "viewDeleteBton");

        let cancelBton = bton.cloneNode(false);
        cancelBton.innerText = "Cancelar";
        cancelBton.setAttribute("onclick", `showView('cardriverview')`);
        cancelBton.setAttribute("class", "cancelDeleteBton");

        deletebtons.appendChild(cancelBton);
        deletebtons.appendChild(deleteBton);
    }

    if (classname == "driver") {
        deletebtons.innerHTML = '';
        let driver = getSpecificDriver(id, "id")
        deletetitle.innerText = "¿Borrar a " + driver.name + "?"

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `deleteDriver(${id})`);
        deleteBton.setAttribute("class", "viewDeleteBton");

        let cancelBton = bton.cloneNode(false);
        cancelBton.innerText = "Cancelar";
        cancelBton.setAttribute("onclick", `showView('driverview')`);
        cancelBton.setAttribute("class", "cancelDeleteBton");

        deletebtons.appendChild(cancelBton);
        deletebtons.appendChild(deleteBton);
    }

    if (classname == "fine") {
        deletebtons.innerHTML = '';
        let fine = getSpecificFine(id, "id")
        deletetitle.innerText = "¿Borrar la multa del día " + fine.date + "?"

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `deleteFine(${id})`);
        deleteBton.setAttribute("class", "viewDeleteBton");

        let cancelBton = bton.cloneNode(false);
        cancelBton.innerText = "Cancelar";
        cancelBton.setAttribute("onclick", `showView('fineview')`);
        cancelBton.setAttribute("class", "cancelDeleteBton");

        deletebtons.appendChild(cancelBton);
        deletebtons.appendChild(deleteBton);
    }

    if (classname == "make") {
        deletebtons.innerHTML = '';
        let make = getSpecificMake(id, "id")
        deletetitle.innerText = "¿Borrar a " + make.name + "?"

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `deleteMake(${id})`);
        deleteBton.setAttribute("class", "viewDeleteBton");

        let cancelBton = bton.cloneNode(false);
        cancelBton.innerText = "Cancelar";
        cancelBton.setAttribute("onclick", `showView('makeview')`);
        cancelBton.setAttribute("class", "cancelDeleteBton");

        deletebtons.appendChild(cancelBton);
        deletebtons.appendChild(deleteBton);
    }

    if (classname == "owner") {
        deletebtons.innerHTML = '';
        let owner = getSpecificOwner(id, "id")
        deletetitle.innerText = "¿Borrar a " + owner.name + "?"

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `deleteOwner(${id})`);
        deleteBton.setAttribute("class", "viewDeleteBton");

        let cancelBton = bton.cloneNode(false);
        cancelBton.innerText = "Cancelar";
        cancelBton.setAttribute("onclick", `showView('ownerview')`);
        cancelBton.setAttribute("class", "cancelDeleteBton");

        deletebtons.appendChild(cancelBton);
        deletebtons.appendChild(deleteBton);
    }
}

function deleteCar(id) {
    try {
        var toDelete = `${baseurl}${uriCar}/${id}`;

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'DELETE',
            headers: myHeaders,
            redirect: 'follow',
        }

        fetch(toDelete, requestOptions)
            .then(() => displayTable('carsTBody'))
            .then(() => showView('carview'))
    } catch (error) {
        console.error("Unable to delete car from database. ", error)
    }
}

function deleteCarDrivers(id) {
    try {
        var toDelete = `${baseurl}${uriCarDriver}/${id}`;

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'DELETE',
            headers: myHeaders,
            redirect: 'follow',
        }

        fetch(toDelete, requestOptions)
            .then(() => displayTable('driversTBody'))
            .then(() => showView('driverview'))
    } catch (error) {
        console.error("Unable to delete car from database. ", error)
    }
}

function deleteDriver(id) {
    try {
        var toDelete = `${baseurl}${uriDriver}/${id}`;

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'DELETE',
            headers: myHeaders,
            redirect: 'follow',
        }

        fetch(toDelete, requestOptions)
            .then(() => displayTable('driversTBody'))
            .then(() => showView('driverview'))
    } catch (error) {
        console.error("Unable to delete car from database. ", error)
    }
}

function deleteFine(id) {
    try {
        var toDelete = `${baseurl}${uriFine}/${id}`;

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'DELETE',
            headers: myHeaders,
            redirect: 'follow',
        }

        fetch(toDelete, requestOptions)
            .then(() => location.reload())
    } catch (error) {
        console.error("Unable to delete car from database. ", error)
    }
}

function deleteMake(id) {
    try {
        var toDelete = `${baseurl}${uriMake}/${id}`;

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'DELETE',
            headers: myHeaders,
            redirect: 'follow',
        }

        fetch(toDelete, requestOptions)
            .then(() => displayTable('makesTBody'))
            .then(() => showView('makeview'))
    } catch (error) {
        console.error("Unable to delete car from database. ", error)
    }
}

function deleteOwner(id) {
    try {
        var toDelete = `${baseurl}${uriOwner}/${id}`;

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'DELETE',
            headers: myHeaders,
            redirect: 'follow',
        }

        fetch(toDelete, requestOptions)
            .then(() => location.reload())
    } catch (error) {
        console.error("Unable to delete car from database. ", error)
    }
}

// Show Editform

function showEditForm(nameView, idEntity) {
    showView(nameView);

    if (nameView == "cardetailView") { carShowEdit(idEntity) }
    if (nameView == "cardriverdetailView") { cardriverShowEdit(idEntity) }
    if (nameView == "driverdetailView") { driverShowEdit(idEntity) }
    if (nameView == "makedetailView") { makeShowEdit(idEntity) }
    if (nameView == "ownerdetailView") { ownerShowEdit(idEntity) }
    if (nameView == "finedetailView") { fineShowEdit(idEntity) }
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
    enableBton.hidden = false;

    let saveBton = document.getElementById("saveEditCar");
    saveBton.setAttribute("onclick", `saveEditCar(${id})`);
    saveBton.hidden = true;
}

function cardriverShowEdit(id) {
    let cardriver = getSpecificCarDriver(id, "id");
    var nameCDinput = document.getElementById("nameCarDriver");
    var licenseCDinput = document.getElementById("licenseCarDriver");
    var dateCDinput = document.getElementById("dateCarDriver");

    nameCDinput.value = cardriver.driver.name;
    nameCDinput.disabled = true;
    licenseCDinput.value = cardriver.car.license;
    licenseCDinput.disabled = true;
    dateCDinput.value = cardriver.dateDrive;
    dateCDinput.disabled = true;

    let delBton = document.getElementById("deleteEditCarDriver");
    delBton.setAttribute("onclick", `deleteCarDriver(${id})`);

    let enableBton = document.getElementById("enableEditCarDriver");
    enableBton.setAttribute("onclick", `enableEditCarDriver(${id})`);
    enableBton.hidden = false;

    let saveBton = document.getElementById("saveEditCarDriver");
    saveBton.setAttribute("onclick", `saveEditCarDriver(${id})`);
    saveBton.hidden = true;
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
    enableBton.hidden = false;

    let saveBton = document.getElementById("saveEditDriver");
    saveBton.setAttribute("onclick", `saveEditDriver(${id})`);
    saveBton.hidden = true;
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
    enableBton.hidden = false;

    let saveBton = document.getElementById("saveEditFine");
    saveBton.setAttribute("onclick", `saveEditFine(${id})`);
    saveBton.hidden = true;
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
    enableBton.hidden = false;

    let saveBton = document.getElementById("saveEditMake");
    saveBton.setAttribute("onclick", `saveEditMake(${id})`);
    saveBton.hidden = true;
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
    enableBton.hidden = false;

    let saveBton = document.getElementById("saveEditOwner");
    saveBton.setAttribute("onclick", `saveEditOwner(${id})`);
    saveBton.hidden = true;
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
    saveBton.setAttribute("onclick", `saveEditCar(${id})`);
    saveBton.hidden = false;
}

function enableEditCarDriver(id) {
    var name = document.getElementById("nameCarDriver");
    var date = document.getElementById("dateCarDriver");
    var license = document.getElementById("licenseCarDriver");

    name.disabled = false;
    date.disabled = false;
    license.disabled = false;

    let enableBton = document.getElementById("enableEditCarDriver");
    enableBton.hidden = true;

    let saveBton = document.getElementById("saveEditCarDriver");
    saveBton.setAttribute("onclick", `saveEditCarDriver(${id})`);
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
    saveBton.setAttribute("onclick", `saveEditMake(${id})`);
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
    saveBton.setAttribute("onclick", `saveEditOwner(${id})`);
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
    saveBton.setAttribute("onclick", `saveEditDriver(${id})`);
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
    saveBton.setAttribute("onclick", `saveEditFine(${id})`);
    saveBton.hidden = false;
}

// Save changes from Edit

function saveEditCar(id) {
    try {
        event.preventDefault();
        var editLicense = document.getElementById("licenseCar").value.trim();
        var editKms = document.getElementById("kmsCar").value.trim();

        var editColorName = document.getElementById("colorCar").value.trim();
        var editOwnerName = document.getElementById("ownerCar").value.trim();
        var editMakeName = document.getElementById("makeCar").value.trim();

        var editColor = getSpecificColor(editColorName, "name");
        var editOwner = getSpecificOwner(editOwnerName, "name");
        var editMake = getSpecificMake(editMakeName, "name");

        const newcar = {
            license: editLicense,
            kms: editKms,
            colorId: editColor.id,
            ownerId: editOwner.id,
            makeId: editMake.id,
        }

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'PUT',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(newcar)
        }

        fetch(baseurl + uriCar + '/' + id, requestOptions)
            .then(() => displayTable('carsTBody'))
            .then(() => showView('carview'))
    } catch (error) {
        console.error("Unable to add fine to database. ", error)
    }
}

function saveEditCarDriver(id) {
    try {
        event.preventDefault();
        var nameDriverCarDriver = document.getElementById("nameCarDriver").value.trim();
        var licenseCarDriver = document.getElementById("licenseCarDriver").value.trim();
        var dateCarDriver = document.getElementById("dateCarDriver").value.trim();

        var driverCD = getSpecificDriver(nameDriverCarDriver, "name");
        var carCD = getSpecificCar(licenseCarDriver, "license");

        const newcardriver = {
            carCDId: carCD.id,
            driverCDId: driverCD.id,
            dateDrive: dateCarDriver
        }

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'PUT',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(newcardriver)
        }

        fetch(baseurl + uriCarDriver + '/' + id, requestOptions)
            .then(() => displayTable('cardriversTBody'))
            .then(() => showView('driverview'))
    } catch (error) {
        console.error("Unable to add fine to database. ", error)
    }
}

function saveEditDriver(id) {
    try {
        event.preventDefault();
        var editName = document.getElementById("nameDriver").value.trim();
        var editDni = document.getElementById("dniDriver").value.trim();
        var editEmail = document.getElementById("emailaddrDriver").value.trim();
        var editPhone = document.getElementById("phonenumberDriver").value.trim();

        var editOwnerName = document.getElementById("ownerDriver").value.trim();
        var editOwner = getSpecificOwner(editOwnerName, "name")

        const newdriver = {
            name: editName,
            dni: editDni,
            emailAddr: editEmail,
            phoneNumber: editPhone,
            ownerId: editOwner.id,
        }

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'PUT',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(newdriver)
        }

        fetch(baseurl + uriDriver + '/' + id, requestOptions)
            .then(() => displayTable('driversTBody'))
            .then(() => showView('driverview'))
    } catch (error) {
        console.error("Unable to add fine to database. ", error)
    }

}

function saveEditMake(id) {
    try {
        event.preventDefault();
        var editName = document.getElementById("nameMake").value.trim();
        var editHorsePower = document.getElementById("horsepowerMake").value.trim();
        var editPrice = document.getElementById("priceMake").value.trim();
        var editFuelName = document.getElementById("fueltypeMake").value.trim();
        var editFuel = getSpecificFuel(editFuelName, "name")

        const newmake = {
            name: editName,
            horsePower: editHorsePower,
            price: editPrice,
            fuelTypeId: editFuel.id,
        }

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'PUT',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(newmake)
        }

        fetch(baseurl + uriMake + '/' + id, requestOptions)
            .then(() => displayTable('makesTBody'))
            .then(() => showView('makeview'))
    } catch (error) {
        console.error("Unable to add fine to database. ", error)
    }
}

function saveEditOwner(id) {
    try {
        event.preventDefault();
        var editName = document.getElementById("nameOwner").value.trim();
        var editNif = document.getElementById("nifOwner").value.trim();
        var editPhone = document.getElementById("phonenumberOwner").value.trim();
        var editDate = document.getElementById("datentryOwner").value.trim();
        var editEmail = document.getElementById("emailaddrOwner").value.trim();

        const newowner = {
            name: editName,
            nif: editNif,
            phoneNumber: editPhone,
            dateEntry: editDate,
            emailAddr: editEmail
        }

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'PUT',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(newowner)
        }

        fetch(baseurl + uriOwner + '/' + id, requestOptions)
            .then(() => displayTable('ownersTBody'))
            .then(() => showView('ownerview'))
    } catch (error) {
        console.error("Unable to add fine to database. ", error)
    }
}

function saveEditFine(id) {
    try {
        event.preventDefault();
        var editPrice = document.getElementById("priceFine").value.trim();
        var editPayed = document.getElementById("payedFine").value.trim();
        var editDescription = document.getElementById("descriptionFine").value.trim();
        var editDate = document.getElementById("dateFine").value.trim();

        var editOwnerName = document.getElementById("ownerFine").value.trim();
        var editCarName = document.getElementById("carFine").value.trim();
        var editOwner = getSpecificOwner(editOwnerName, "name");
        var editCar = getSpecificCar(editCarName, "name")

        var payed;
        if (editPayed.checked == true) { payed = true }
        else { payed = false }

        const newfine = {
            price: editPrice,
            date: editDate,
            description: editDescription,
            payed: payed,
            ownerId: editOwner.id,
            carId: editCar.id,
        }

        var myHeaders = new Headers();
        myHeaders.append("Authorization", "Bearer " + token)
        myHeaders.append("Accept", "application/json")
        myHeaders.append("Content-Type", "application/json")

        var requestOptions = {
            method: 'PUT',
            headers: myHeaders,
            redirect: 'follow',
            body: JSON.stringify(newfine)
        }

        fetch(baseurl + uriFine + '/' + id, requestOptions)
            .then(() => displayTable('finesTBody'))
            .then(() => showView('fineview'))
    } catch (error) {
        console.error("Unable to add fine to database. ", error)
    }
}

function exportToExcel(data) {
    switch (data) {
        case 'cars':
            convertCarsToCsv(cars);
            break;
        case 'makes':
            convertMakesToCsv(makes);
            break;
        case 'owners':
            convertOwnersToCsv(owners);
            break;
        case 'drivers':
            convertDriversToCsv(drivers)
            break;
        case 'cardrivers':
            convertCarDriversToCsv(cardrivers)
            break;
        case 'fines':
            convertFinesToCsv(fines)
            break;
        default:
            break;
    }

    //window.open('data:application/vnd.ms-excel,' + encodeURIComponent(CsvString));
    console.log(data)
}

function convertCarsToCsv(table){
    var csvString = "";
    csvString += "Matricula,Kilometros,Color,Propietario,Modelo,\r\n"
    table.forEach(item => {
        csvString += item.license+','+item.kms+','+item.color.name+','+item.owner.name+','+item.make.name+','+"\r\n";
    });
    window.open("data:application/vnd.ms-excel," + encodeURIComponent(csvString));
}

function convertMakesToCsv(table){
    var csvString = "";
    csvString += "Nombre,Poder de caballo,Precio,Combustible,\r\n"
    table.forEach(item => {
        csvString += item.name+','+item.horsePower+','+item.price+','+item.fuelType.name+','+"\r\n";
    });
    window.open("data:application/vnd.ms-excel," + encodeURIComponent(csvString));
}

function convertOwnersToCsv(table){
    var csvString = "";
    csvString += "Nombre,NIF,Fecha de entrada,Telefono,Dirección de correo,\r\n"
    table.forEach(item => {
        csvString += item.name+','+item.nif+','+item.dateEntry+','+item.phoneNumber+','+item.emailAddr+','+"\r\n";
    });
    window.open("data:application/vnd.ms-excel," + encodeURIComponent(csvString));
}

function convertDriversToCsv(table){
    var csvString = "";
    csvString += "Nombre,DNI,Telefono,Dirección de correo,Propietario asociado,\r\n"
    table.forEach(item => {
        csvString += item.name+','+item.dni+','+item.phoneNumber+','+item.emailAddr+','+item.owner.name+','+"\r\n";
    });
    window.open("data:application/vnd.ms-excel," + encodeURIComponent(csvString));
}

function convertCarDriversToCsv(table){
    var csvString = "";
    csvString += "Nombre del conductor,Fecha de conducción,Vehiculo asociado,\r\n"
    table.forEach(item => {
        csvString += item.driver.name+','+item.dateDrive+','+item.car.license+','+"\r\n";
    });
    window.open("data:application/vnd.ms-excel," + encodeURIComponent(csvString));
}

function convertFinesToCsv(table){
    var csvString = "";
    csvString += "Fecha,Importe,Descripción,Estado,Propietario asociado,Coche asociado,\r\n"
    table.forEach(item => {
        csvString += item.date+','+item.price+','+item.description+','+item.payed+','+item.owner.name+','+item.car.license+"\r\n";
    });
    window.open("data:application/vnd.ms-excel," + encodeURIComponent(csvString));
}