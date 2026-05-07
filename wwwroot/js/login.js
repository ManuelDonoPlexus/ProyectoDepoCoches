const baseurl = "http://localhost:5206/api/Auth/";
const registermethod = "register"; 
const loginmethod = "login";
let token;

async function login(email, password){
    const newemail = document.getElementById(email);
    const newpassword = document.getElementById(password);

    const login = {
        email: newemail.value.trim(),
        password: newpassword.value.trim()
    }

    const response = await fetch(baseurl + loginmethod, {
        method: 'POST',
        headers: {
            "Accept": "*/*",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(login)
    })

    let token = respone.json();
}

function register(username, email, password){
    const newname = document.getElementById(username)
    const newemail = document.getElementById(email)
    const newpass = document.getElementById(password)

    if(newname.value.trim() == null && newemail.value.trim() == null && newpass.value.trim() == null){
        return;
    }

    const newuser = {
        name: newname.value.trim(),
        password: newpass.value.trim(),
        email: newemail.value.trim()
    }

    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer "+token )

    var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        redirect: 'follow'
    }

    fetch(baseurl+registermethod, requestOptions)
    .then(respone => respone.json())
}