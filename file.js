document.getElementById("admin").innerHTML=sessionStorage.getItem("admin");

const constants = {
    apiBasePath: 'https://localhost:44392/api'
  }
  var id=sessionStorage.getItem("id");

  var folderid=sessionStorage.getItem("folderid");
  console.log(folderid);
 
var curr=new Date();
const form = document.getElementById("file");
function createfile() {
try
{
 fetch('http://localhost:63931/api/Document', {
   body: JSON.stringify({
      "documentName": form.value,
      "createdAt": curr.toISOString(),
      "isDeleted": 0,
      "documentType": "html",
      "size": 90,
      "createdBy": id,
      "folderId": sessionStorage.getItem("folderid")
      ,
      
  }),
   method: 'POST',
   headers: {
    'Content-Type': 'application/json'
  },
 }).then((folderCreateResponse) => {
    console.log(folderCreateResponse);
listFiles();
 });
}
catch(err)
{
  console.log(err);
}
} 

  function listFiles() {
    try
    {
      var create = document.getElementById("create");
      create.innerHTML = '';
    fetch('http://localhost:63931/api/Document/'+folderid)
    .then(response => response.json())
    .then((folders) => {
      console.log(folders);
      folders.forEach(folder => {
    
      var create = document.getElementById("create");
      var art = document.createElement("article");
      console.log(folder);
      const fold = folder.documentName;
      const fid=folder.documentId;
      console.log(fold);
    
      art.innerHTML = `<i class="fa-solid fa-2x fa-file"></i>
      <a style="font-size:  20px;text-decoration: none;position: relative;left: 190px;bottom: 20px;cursor: pointer;border: 0px;"></a>
      </i> <button id="filebtn" onclick ="createfiles()"> ${fold} </button>
      
      <a style= "left:20px; bottom:20px;"></a>
      <i class="bx bx-trash" onclick="deletefolder(${fid})" style="position:relative;left: 135px;bottom: 25px;">
       </i>`;
      create.appendChild(art);
      });
   })
    
    }
    catch(err)
    {
      console.log(err);
    }
  }

  function onLoad() {
    listFiles();
    document.getElementById("admin").innerHTML=sessionStorage.getItem("admin");
  }
  
  onLoad();
  
  
  function logout() {
    window.location.href = "index.html";
    sessionStorage.clear();
  
  
  }

  function searchitem(){
    try
    {
      var search=document.getElementById("search").value;
      console.log(search);
      var create = document.getElementById("create");
      create.innerHTML = '';
    fetch(`http://localhost:63931/api/Document/${sessionStorage.getItem("id")}/${sessionStorage.getItem("folderid")}/${search}`)
    .then(response => response.json())
    .then((folders) => {
      console.log(folders);
      folders.forEach(folder => {
        // debugger;
      var create = document.getElementById("create");
      var art = document.createElement("article");
      const fold = folder.documentName;
      // fold.style.backgroundColor = "red";
      console.log(fold);
      art.innerHTML = 
      `<i class="fa-solid fa-2x fa-file"></i>
       <a style="font-size:  20px;text-decoration: none;position: relative;left: 190px;bottom: 20px;cursor: pointer;"></a>
      
     <button id="filebtn" onclick ="createfiles()"> ${fold} </button>`;
      create.appendChild(art);
      });
    })
    
    }
    catch(err)
    {
      console.log(err);
    }
  }

//   function deletefolder(folder) {
//   var raw = "";
// var requestOptions = {

//   method: 'DELETE',

//   body: raw,

//   redirect: 'follow'
// };

// let deleteurl = "http://localhost:63931/api/Folder/" + folder;

// fetch(deleteurl,requestOptions)

// .then(response=>response.text())

// .then(result => console.log(result))

//   .catch(error => console.log('error', error));

//   location.reload();  
// }

// function deletefile(folder) {
//     var raw = "";
//   var requestOptions = {
  
//     method: 'DELETE',
  
//     body: raw,
  
//     redirect: 'follow'
//   };
  
//   let deleteurl = "http://localhost:63931/api/Document/" + folder;
  
//   fetch(deleteurl,requestOptions)
  
//   .then(response=>response.text())
  
//   .then(result => console.log(result))
  
//     .catch(error => console.log('error', error));
  
//     location.reload();  
//   }


function deletefolder(folder) {
    var raw = "";
  var requestOptions = {
  
    method: 'DELETE',
  
    body: raw,
  
    redirect: 'follow'
  };
  
  let deleteurl = "http://localhost:63931/api/Document/" + folder;
  
  fetch(deleteurl,requestOptions)
  
  .then(response=>response.text())
  
  .then(result => console.log(result))
  
    .catch(error => console.log('error', error));
  
    location.reload();  
  }