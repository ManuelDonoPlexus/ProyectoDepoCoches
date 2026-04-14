// La ruta de uri le dice a los metodos donde buscar el objeto que se esta buscando.
// Esa ruta aparece en el metodo controler en la definición del constructor
// ESTA RUTA TIENE QUE ESTAR BIEN
const baseurl = "http://localhost:5206"
const uriCar = "/api/Car";
const uriOwner = "/api/Owner";
const uriMake = "/api/Make";
const uriColor = "/api/Color";
let cars = [];
let owners = [];
let makes = [];
let colors = [];
let mainview = "carview"

// Para obtener los datos de las base de datos.

async function getCars() {
  try {
    const response = await fetch(baseurl + uriCar);
    cars = await response.json();
    _displayCars();
  } catch (error) {
    console.error('Unable to get cars.', error);
  }
}

async function getOwners() {
  try {
    const response = await fetch(baseurl+uriOwner);
    owners = await response.json();
    fillOwnerNameList();    
  } catch (error) {
    console.error('Unable to get owners.', error);
  }
}

async function getMakes() {
  try {
    const response = await fetch(baseurl+uriMake);
    makes = await response.json();
    fillMakeNameList();    
  } catch (error) {
    console.error('Unable to get makes.', error);
  }
}

async function getColors() {
  try {
    const response = await fetch(baseurl+uriColor);
    colors = await response.json();
    fillColorNameList();    
  } catch (error) {
    console.error('Unable to get makes.', error);
  }
}

// Para obtener un nombre especifico de una entidad

function getSpecificOwner(search, camp) {
  let owner;
  owners.forEach( result => {
    // BUSCAR ID con NOMBRE
    if (camp == "id") {
      if (result.name == search) {
        owner = result.id;
      }
      // BUSCAR NOMBRE CON ID
    } else if (camp == "name") {
      if (result.id == search) {
        owner = result.name;
      }
    }
  })
  return owner
}

function getSpecificMake(search, camp){
  let make;
  makes.forEach( result => {
    if (camp == "id") {
      if (result.name == search) {
        make = result.id;
      }
    } else if (camp == "name") {
      if (result.id == search) {
        make = result.name;
      }
    }
  })
  return make
}

function getSpecificColor(search, camp){
  let color;
  colors.forEach( result => {
    if (camp == "id") {
      if (result.name == search) {
        color = result.id;
      }
    } else if (camp == "name") {
      if (result.id == search) {
        color = result.name;
      }
    }
  })
  return color
}

// Representa un recuento de todos los objetos

function _displayCarCount(itemCount) {
  const name = (itemCount === 1) ? "coche" : "coches";

  document.getElementById("counter").innerText = `${itemCount} ${name}`;
}

// Representa la información de cada objeto sacandola de la Base de Datos

function _displayCars() {
  const tBody = document.getElementById("cars");
  tBody.innerHTML;

  _displayCarCount(cars.length);

  const bton = document.createElement("button");

  cars.forEach(car => {
    let editBton = bton.cloneNode(false);
    editBton.innerText = "Editar";
    editBton.setAttribute("onclick", `displayEditForm(${car.id})`);
    editBton.setAttribute("class", "editBton");

    let deleteBton = bton.cloneNode(false);
    deleteBton.innerText = "Borrar";
    deleteBton.setAttribute("onclick", `deleteCar(${car.id})`);
    deleteBton.setAttribute("class", "deleteBton");

    let tr = tBody.insertRow();
    tr.setAttribute("class", "carRow")

    let td1 = tr.insertCell(0);
    td1.setAttribute("class", "carLicense")
    let textNode1 = document.createTextNode(car.license);
    td1.appendChild(textNode1);

    let td2 = tr.insertCell(1);
    td2.setAttribute("class", "carKms");
    let textNode2 = document.createTextNode(car.kms);
    td2.appendChild(textNode2);

    let td3 = tr.insertCell(2);
    td3.setAttribute("class", "carOwner");
    let ownerName = getSpecificOwner(car.ownerId, "name");
    let textNode3 = document.createTextNode(ownerName);
    td3.appendChild(textNode3);

    let td4 = tr.insertCell(3);
    td4.setAttribute("class", "carMake");
    let makeName = getSpecificMake(car.makeId, "name");
    let textNode4 = document.createTextNode(makeName);
    td4.appendChild(textNode4);

    let td5 = tr.insertCell(4);
    td5.setAttribute("class", "carColor");
    td5.setAttribute("id", car.colorId)
    let colorSpecific = getSpecificColor(car.colorId, "name");
    let textNode5= document.createTextNode(colorSpecific);
    td5.appendChild(textNode5);

    let td6 = tr.insertCell(5);
    td6.appendChild(editBton);

    let td7 = tr.insertCell(6);
    td7.appendChild(deleteBton);
  });

}

// Para añadir una nueva entidad a la base de datos con los datos del formulario

function addCar() {
  const addLicense = document.getElementById("add-license");
  const addKms = document.getElementById("add-kms");
  const addOwner = document.getElementById("add-owner");
  const addMake = document.getElementById("add-make");
  const addColor = document.getElementById("add-color");

  let ownerId = getSpecificOwner(addOwner.value.trim(), "id");
  let makeId = getSpecificMake(addMake.value.trim(), "id");
  let colorId = getSpecificColor(addColor.value.trim(), "id");

  const car = {
    license: addLicense.value.trim(),
    kms: addKms.value.trim(),
    ownerId: ownerId,
    makeId: makeId,
    colorId: colorId,
  }

  fetch(baseurl + uriCar, {
    method: "POST",
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(car)
  })
    .then(response => response.json)
    .then(() => {
        addLicense.value = "",
        addKms.value = "",
        addOwner.value = "",
        addMake.value = "",
        addColor.value = ""
    })
    .then(() => getCars())
    .catch(error => console.error("Unable to add car to database. ", error))
  _displayView(mainview)
  _hideView("addform")
}

// Para editar los datos de una entidad de la base de datos con los datos del formulario

function editCar() {
  const carid = document.getElementById("edit-id").value;

  let editOwner = document.getElementById("edit-owner").value.trim();
  let editMake = document.getElementById("edit-make").value.trim();
  let editColor = document.getElementById("edit-color").value.trim()

  let ownerId = getSpecificOwner(editOwner, "id");
  let makeId = getSpecificMake(editMake, "id");
  let colorId = getSpecificColor(editColor, "id");

  const car = {
    id: carid.trim(),
    license: document.getElementById("edit-license").value.trim(),
    kms: document.getElementById("edit-kms").value.trim(),
    ownerId: ownerId,
    makeId: makeId,
    colorId: colorId,
  }

  fetch(`${baseurl}${uriCar}/${carid}`, {
    method: "PUT",
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(car)
  })
    .then(() => { getCars(); })
    .catch(error => console.error("Unable to modify car from Database. ", error))
  _displayView(mainview);
  _hideView("editform");
  return false;
}

// Para borrar una entidad de la base de datos

function deleteCar(id) {
  fetch(`${baseurl}${uriCar}/${id}`, {
    method: `DELETE`
  })
    .then(() => getCars())
    .catch(error => console.error("Unable to delete car from database. ", error))
}

// Para mostrar el formulario de edición

function displayEditForm(id) {
  _hideView(mainview);

  const car = cars.find(car => car.id === id);

  let ownerid = car.ownerId;
  let makeid = car.makeId;
  let colorid = car.colorId;
  const owner = owners.find(owner => owner.id === ownerid);
  const make = makes.find(make => make.id === makeid)
  const color = colors.find(color => color.id === colorid)

  document.getElementById("edit-id").value = car.id;
  document.getElementById("edit-license").value = car.license;
  document.getElementById("edit-kms").value = car.kms;
  document.getElementById("edit-owner").value = owner.name;
  document.getElementById("edit-make").value = make.name;
  document.getElementById("edit-color").value = color.name;

  _displayView("editform")

}

// Para llenar las listas de los Datalist 

function fillOwnerNameList() {
  const list = document.getElementById("ownerlist");
  const option = document.createElement("option");

  owners.forEach(owner => {
    let optionOwner = option.cloneNode(false)
    optionOwner.setAttribute("value", `${owner.name}`)
    list.appendChild(optionOwner);
  })
}

function fillMakeNameList() {
  const list = document.getElementById("makelist");
  const option = document.createElement("option");

  makes.forEach(make => {
    let optionMake = option.cloneNode(false);
    optionMake.setAttribute("value", `${make.name}`);
    list.appendChild(optionMake);
  })
}

function fillColorNameList(){
  const list = document.getElementById("colorlist");
  const option = document.createElement("option");
  
  colors.forEach(color => {
    let optionColor = option.cloneNode(false);
    optionColor.setAttribute("value", `${color.name}`)
    list.appendChild(optionColor)
  })
}

// Para esconder una vista y mostrar la vista principal, así cancelando la acción.

function cancelAction(id) {
  _displayView(mainview);
  _hideView(id);
}

// Para mostrar una vista

function _displayView(id) {
  document.getElementById(id).style.display = "initial"
}

// Para esconder una visita

function _hideView(id) {
  document.getElementById(id).style.display = "none";
}

// Para el filtrado de la busqueda

function search() {
  const searchbar = document.getElementById("searchbar");
  const table = document.getElementById("cars").getElementsByTagName("tr");
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