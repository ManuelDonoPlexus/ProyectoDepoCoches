// La ruta de uri le dice a los metodos donde buscar el objeto que se esta buscando.
// Esa ruta aparece en el metodo controler en la definición del constructor
// ESTA RUTA TIENE QUE ESTAR BIEN
const baseurl = "http://localhost:5206"
const uri = '/api/Make';
let makes = [];
let mainview = "makeview"

// Para obtener los datos de las base de datos.

async function getMakes() {
  try {
    const response = await fetch(baseurl+uri);
    makes = await response.json();
    _displayMakes();
  } catch (error) {
    console.error('Unable to get makes.', error);
  }
}

// Representa un recuento de todos los objetos

function _displayMakeCount(itemCount) {
    const name = (itemCount === 1) ? 'marca' : 'marcas';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

// Representa la información de cada objeto sacandola de la Base de Datos

function _displayMakes() {
    const tBody = document.getElementById("makes");
    tBody.innerHTML = "";

    _displayMakeCount(makes.length);

    const bton = document.createElement("button");

    makes.forEach(make => {
        let editBton = bton.cloneNode(false);
        editBton.innerText = "Editar";
        editBton.setAttribute("onclick", `displayEditForm(${make.id})`);
        editBton.setAttribute("class", "editBton");

        let deleteBton = bton.cloneNode(false);
        deleteBton.innerText = "Borrar";
        deleteBton.setAttribute("onclick", `deleteMake(${make.id})`);
        deleteBton.setAttribute("class", "deleteBton");

        let tr = tBody.insertRow();
        tr.setAttribute("class","makeRow")

        let td1 = tr.insertCell(0);
        td1.setAttribute("class", "makeName")
        let textNode1 = document.createTextNode(make.name);
        td1.appendChild(textNode1);

        let td2 = tr.insertCell(1);
        td2.setAttribute("class", "makePrice")
        let textNode2 = document.createTextNode(make.price);
        td2.appendChild(textNode2);

        let td3 = tr.insertCell(2);
        td3.setAttribute("class", "makeHP")
        let textNode3 = document.createTextNode(make.horsePower);
        td3.appendChild(textNode3);

        let td4 = tr.insertCell(3);
        td4.appendChild(editBton);

        let td5 = tr.insertCell(4);
        td5.appendChild(deleteBton);
    });
}

// Para añadir una nueva entidad a la base de datos con los datos del formulario

function addMake() {
    const addNameTxtBox = document.getElementById("add-name");
    const addPriceTxtBox = document.getElementById("add-price");
    const addHorsePowerTxtBox = document.getElementById("add-horsePower");

    const make = {
        name: addNameTxtBox.value.trim(),
        price: addPriceTxtBox.value.trim(),
        horsePower: addHorsePowerTxtBox.value.trim()
    }

    fetch(baseurl + uri, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(make)
    })
        .then(response => response.json)
        .then(() => {
            addNameTxtBox.value = "";
            addPriceTxtBox.value = "";
            addHorsePowerTxtBox.value = "";
        })
        .catch(error => console.error("Unable to add make to database. ", error));
    _displayView(mainview);
    _hideView("addform");
}

// Para borrar una entidad de la base de datos

function deleteMake(id) {
    fetch(`${baseurl}${uri}/${id}`, {
        method: `DELETE`
    })
        .then(() => getMakes())
        .catch(error => console.error("Unable to delete make from database. ", error))
}

// Para editar los datos de una entidad de la base de datos con los datos del formulario

function editMake() {
    const makeid = document.getElementById("edit-id").value

    const make = {
        id: makeid.trim(),
        name: document.getElementById("edit-name").value.trim(),
        price: document.getElementById("edit-price").value.trim(),
        horsePower: document.getElementById("edit-horsePower").value.trim()
    }

    fetch(`${baseurl}${uri}/${makeid}`, {
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(make)
    })
        .then(() => { getMakes(); })
        .catch(error => console.error("Unable to modify make from Database. ", error))

    _displayView(mainview);
    _hideView("editform");
    return false;
}

// Para mostrar el formulario de edición

function displayEditForm(id) {
    _hideView(mainview)

    const make = makes.find(make => make.id === id)

    document.getElementById("edit-id").value = make.id;
    document.getElementById("edit-name").value = make.name;
    document.getElementById("edit-price").value = make.price;
    document.getElementById("edit-horsePower").value = make.horsePower;

    _displayView("editform")
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

function search(){
    const searchbar = document.getElementById("searchbar");
    const table = document.getElementById("makes").getElementsByTagName("tr");
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