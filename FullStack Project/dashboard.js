document.getElementById("admin").innerHTML=sessionStorage.getItem("admin");

const constants = {
    apiBasePath: 'http://localhost:63931/api'
  }
  
  let  form = document.getElementById("file");
  console.log(form);
  
  function createfolder() {
    try
    {
     fetch(`${constants.apiBasePath}/Folder`, {
       body: JSON.stringify({
        "FolderName": form.value,
        "CreatedBy": sessionStorage.getItem("id"),
        "CreatedAt": "2022-06-03T14:17:30.309Z",
        "IsDeleted": 0
      }),
       method: 'POST',
       headers: {
        'Content-Type': 'application/json'
      },
     }).then((folderCreateResponse) => {
        console.log(folderCreateResponse);
       listFolders();
     });
    }
    catch(err)
    {
      console.log(err);
    }
  }
  
  function listFolders() {
    try
    {
      var create = document.getElementById("create");
      create.innerHTML = '';
    fetch(`${constants.apiBasePath}/Folder/`+sessionStorage.getItem("id"), {
      method: 'GET'
    })
    .then(response => response.json())
    .then((folders) => {
      console.log(folders);
      folders.forEach(folder => {
        // debugger;
      var create = document.getElementById("create");
      var art = document.createElement("article");
      const fold = folder.folderName;
      const fid=folder.folderId;
      // fold.style.backgroundColor = "red";
      console.log(fid);
      art.innerHTML = 
      `<i class='foldericon fa-solid  fa-2x fa-folder'>
       <a style="font-size:20px; text-decoration:none; position:relative; left: 190px;bottom: 20px;cursor: pointer;"></a>
       </i> <button id="filebtn" onclick ="createfiles(${fid})"> ${fold} </button>
       
       <a style= "left:20px; bottom:20px;"></a>
      <i class="bx bx-trash" onclick ="deletefolder(${fid})" style="position:relative;left: 135px;bottom: 25px;">
       </i> `;
       
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
    listFolders();
  }
  
  onLoad();
  // createfile 

  function createfiles(folderid) {
    sessionStorage.setItem("folderid",folderid);
    window.location.href ="file.html"; 
  }
 function deleteSession(){
sessionStorage.clear();
window.location.href="login.html";
  }

function searchitem(){
  try
  {
    var search=document.getElementById("search").value;
    console.log(search);
    var create = document.getElementById("create");
    create.innerHTML = '';
  fetch(`http://localhost:63931/api/Folder/${search},${sessionStorage.getItem("id")}`)
  .then(response => response.json())
  .then((folders) => {
    console.log(folders);
    folders.forEach(folder => {
      // debugger;
    var create = document.getElementById("create");
    var art = document.createElement("article");
    const fold = folder.folderName;
    // fold.style.backgroundColor = "red";
    console.log(fold);
    art.innerHTML = 
    `<i class='foldericon fa-solid  fa-2x fa-folder'>
     <a style="font-size:  20px;text-decoration: none;position: relative;left: 190px;bottom: 20px;cursor: pointer;"></a>
    
     </i> <button id="filebtn" onclick ="createfiles()"> ${fold} </button>`;
    create.appendChild(art);
    });
  })
  
  }
  catch(err)
  {
    console.log(err);
  }
}




function deletefolder(folder) {
  var raw = "";
var requestOptions = {

  method: 'DELETE',

  body: raw,

  redirect: 'follow'
};

let deleteurl = "http://localhost:63931/api/Folder/" + folder;

fetch(deleteurl,requestOptions)

.then(response=>response.text())

.then(result => console.log(result))

  .catch(error => console.log('error', error));

  location.reload();  
}