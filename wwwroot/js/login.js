const baseurl = "http://localhost:5206"
const registermethod = "register" 
const loginmethod = "login" 

function login(){
    const email = document.getElementById("login-email");
    const password = document.getElementById("login-password");

    if(email.value.trim() == null && password.value.trim() == null){
        return;
    }

    const login = {
        email: email.value.trim(),
        password: password.value.trim()
    }

    fetch(baseurl + loginmethod, {
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(login)
    })
        .then(respone => respone.json())
        .then(() => location.reload())
        .catch(error => console.error("Unable to add fine to database. ", error))
}