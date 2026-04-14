// La ruta de uri le dice a los metodos donde buscar el objeto que se esta buscando.
// Esa ruta aparece en el metodo controler en la definición del constructor
// ESTA RUTA TIENE QUE ESTAR BIEN
const baseurl = "http://localhost:5206"
const uri = '/api/Owner';
let owners = [];
let mainview = "ownerview"

// Para obtener los datos de las base de datos.

async function getOwners() {
  try {
    const response = await fetch(baseurl+uri);
    owners = await response.json();
    _displayOwners();    
  } catch (error) {
    console.error('Unable to get owners.', error);
  }
}

// Representa un recuento de todos los objetos

function _displayOwnerCount(itemCount) {
  const name = (itemCount === 1) ? 'propietario' : 'propietarios';

  document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

// Representa la información de cada objeto sacandola de la Base de Datos

function _displayOwners() {
  const tBody = document.getElementById("owners");
  tBody.innerHTML = "";

  _displayOwnerCount(owners.length);

  const bton = document.createElement("button");

  owners.forEach(owner => {
    let editBton = bton.cloneNode(false);
    editBton.innerText = "Editar";
    editBton.setAttribute("onclick",`displayEditForm(${owner.id})`);
    editBton.setAttribute("class","editBton");

    let deleteBton = bton.cloneNode(false);
    deleteBton.innerText = "Borrar";
    deleteBton.setAttribute("onclick", `deleteOwner(${owner.id})`);
    deleteBton.setAttribute("class","deleteBton");

    let tr = tBody.insertRow();
    tr.setAttribute("class", "ownerRow");

    let td1 = tr.insertCell(0);
    td1.setAttribute("class", "ownerName");
    let textNode1 = document.createTextNode(owner.name);
    td1.appendChild(textNode1);

    let td2 = tr.insertCell(1);
    td2.setAttribute("class", "ownerDate");
    let textNode2 = document.createTextNode(owner.dateEntry);
    td2.appendChild(textNode2);

    let td3 = tr.insertCell(2);
    td3.setAttribute("class", "ownerEmail");
    let textNode3 = document.createTextNode(owner.emailAddr);
    td3.appendChild(textNode3);

    let td4 = tr.insertCell(3);
    td4.setAttribute("class", "ownerPhone");
    let textNode4 = document.createTextNode(owner.phoneNumber);
    td4.appendChild(textNode4);

    let td5 = tr.insertCell(4);
    td5.appendChild(editBton);

    let td6 = tr.insertCell(5);
    td6.appendChild(deleteBton);
  });
  
}

// Para añadir una nueva entidad a la base de datos con los datos del formulario

function addOwner() {
  const addNameTxtBox = document.getElementById("add-name");
  const addDateTxtBox = document.getElementById("add-date");
  const addEmailTxtBox = document.getElementById("add-email");
  const addPhoneTxtBox = document.getElementById("add-phone");

  const owner = {
    name: addNameTxtBox.value.trim(),
    dateEntry: addDateTxtBox.value.trim(),
    emailAddr: addEmailTxtBox.value.trim(),
    phoneNumber: addPhoneTxtBox.value.trim(),
  }

  fetch(baseurl+uri, {
    method: "POST",
    headers:{
      "Accept": "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(owner)
  })
    .then(response => response.json)
    .then(() => {
      addNameTxtBox.value = "";
      addDateTxtBox.value = "";
      addEmailTxtBox.value = "";
      addPhoneTxtBox.value = "";
  })
  .catch(error => console.error("Unable to add owner to database. ", error))
  _displayView(mainview);
  _hideView("addform");
}

// Para borrar una entidad de la base de datos

function deleteOwner(id){
  fetch(`${baseurl}${uri}/${id}`, {
    method: `DELETE`
  })
  .then(()=> getOwners())
  .catch(error => console.error("Unable to delete owner from database. ", error))
}

// Para editar los datos de una entidad de la base de datos con los datos del formulario

function editOwner(){
  const ownerid = document.getElementById("edit-id").value;

  const owner = {
    id: ownerid.trim(),
    name: document.getElementById("edit-name").value.trim(),
    dateEntry: document.getElementById("edit-date").value.trim(),
    emailAddr: document.getElementById("edit-email").value.trim(),
    phoneNumber: document.getElementById("edit-phone").value.trim()
  }

  fetch(`${baseurl}${uri}/${ownerid}`, {
    method: "PUT",
    headers:{
      "Accept": "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(owner)
  })
    .then(()=> { getOwners(); })
    .catch(error => console.error("Unable to modify owner from Database. ", error));

    _displayView(mainview);
    _hideView("editform");
  return false;

}

// Para mostrar el formulario de edición

function displayEditForm(id) {
  _hideView(mainview);

  const owner = owners.find(owner => owner.id === id);

  document.getElementById("edit-id").value = owner.id
  document.getElementById("edit-name").value = owner.name
  document.getElementById("edit-date").value = owner.dateEntry
  document.getElementById("edit-email").value = owner.emailAddr
  document.getElementById("edit-phone").value = owner.phoneNumber

  _displayView("editform");
}

// Para esconder una vista y mostrar la vista principal, así cancelando la acción.

function cancelAction(id){
  _displayView(mainview);
  _hideView(id);
}

// Para mostrar una vista

function _displayView(id){
  document.getElementById(id).style.display= "initial"
}

// Para esconder una visita

function _hideView(id) {
  document.getElementById(id).style.display = "none";
}

// Para el filtrado de la busqueda

function search(){
    const searchbar = document.getElementById("searchbar");
    const table = document.getElementById("owners").getElementsByTagName("tr");
    const filter = searchbar.value.toUpperCase();

    for (let i = 0; i < table.length; i++) {
        txt = table[i].getElementsByClassName("ownerName")[0];
        if (txt.innerText.toUpperCase().indexOf(filter) > -1) {
            table[i].style.display = "";
        } else {
            table[i].style.display = "none";
        }
    }
}