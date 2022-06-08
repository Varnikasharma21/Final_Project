
function checkUser() {
    let user=document.getElementById("users").value;
    let password=document.getElementById("password").value;
    var myHeaders = new Headers();
myHeaders.append("Content-Type", "application/json");

var raw = JSON.stringify({
  "Username": user,
  "UserPassword": password
});

var requestOptions = {
  method: 'POST',
  headers: myHeaders,
  body: raw,
  redirect: 'follow'
};

fetch("http://localhost:63931/api/Login", requestOptions)
  .then(response => response.json())
  .then(result => showstorage(result))
  .catch(error => console.log('error', error));
   
}
function showstorage(data)
{
if(data.token!=null && data.token!=undefined && data.token!="")

{  
    console.log(data);
    sessionStorage.setItem("token",data.token);
    sessionStorage.setItem("id",data.userid);
    sessionStorage.setItem("admin",data.name);
}
loc();
}
function loc()
{
if(sessionStorage.getItem("token")!=null)
{
window.location.href="dashboard.html";
}
else
{
alert("Login Credentials are wrong");
}
}
 