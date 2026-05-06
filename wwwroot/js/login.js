const baseurl = "http://localhost:5206"
const registermethod = "register" 
const loginmethod = "login" 

function login(email, password){
    const newemail = document.getElementById(email);
    const newpassword = document.getElementById(password);

    if(email.value.trim() == null && password.value.trim() == null){
        return;
    }

    const login = {
        email: newemail.value.trim(),
        password: newpassword.value.trim()
    }
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
}