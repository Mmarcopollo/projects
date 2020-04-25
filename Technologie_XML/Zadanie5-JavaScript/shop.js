"use strict"

////////////////////////////////////////Loading file//////////////////////
var data;
var xhttp = new XMLHttpRequest();
xhttp.onreadystatechange = function () {
  if (this.readyState == 4 && this.status == 200) {
    loadData(this);
    updateTypesList();
    updateAlbumsList();
  }
};
xhttp.open("GET", "Albumy.xml", true);
xhttp.send();

function loadData(xml) {
  data = xml.responseXML;
}

////////////////////////////////////SHOW TYPES/////////////////////////////
function updateTypesList() {
  let typesListDiv = $('#typesList');


  //remove all elements
  while (typesListDiv.firstChild) {
    typesListDiv.removeChild(typesListDiv.firstChild);
  }

  let table = $("#typesTable").find("tbody");

  table.empty();

  var path = ""

  if (data.evaluate) {

    path = "/albumCollection/types/type/name"
    var name = data.evaluate(path, data, null, XPathResult.ANY_TYPE, null);
    path = "/albumCollection/types/type/countryoforigin"
    var country = data.evaluate(path, data, null, XPathResult.ANY_TYPE, null);
    path = "/albumCollection/types/type/time"
    var date = data.evaluate(path, data, null, XPathResult.ANY_TYPE, null);

    var nameResult = name.iterateNext();
    var countryResult = country.iterateNext();
    var dateResult = date.iterateNext();
    var id = 0;
    
    while (nameResult) {
      table.append(
        "<tr>" +
        "<td class='text-center'>" + nameResult.childNodes[0].textContent + "</td>" +
        "<td class='text-center'>" + countryResult.getAttribute('continent') + "</td>" +
        "<td class='text-center'>" + countryResult.childNodes[0].textContent + "</td>" +
        "<td class='text-center'>" + dateResult.getAttribute('age') + "</td>" +
        "<td class='text-center'>" + dateResult.childNodes[0].textContent + "</td>" +
        "<td class='text-center font-weight-bold'>" + "<button class='btn btn-primary' onclick='deleteType( " + id + ")'>Delete</button>" + "</td>" +
        "</tr>"
      );

      nameResult = name.iterateNext();
      countryResult = country.iterateNext();
      dateResult = date.iterateNext();
      id++;
    }
  }
}


//////////////////////////////////Delete TYPE//////////////////////////////////
function deleteType(index) {
  var z = data.getElementsByTagName("type")[index];

  z.parentNode.removeChild(z);
  updateTypesList();
}


///////////////////////////////////ADD TYPE//////////////////////////////////
function addType() {

  var name = $('#inputName').val();
  var country = $('#inputCountry').val();
  var date = $('#inputDate').val();
  var age = $('#inputAge').val();
  var continent = $('#inputContinent').val();
  
  var tracksCount = data.getElementsByTagName("types")[0].childElementCount;

  var newId = data.getElementsByTagName("types")[0].childNodes[(tracksCount*2)-1].getAttribute('id');

  newId = newId.replace('G','');
  newId++;
  newId = "G"+newId;

  tracksCount++;

  //get the elements in the form
  let inputName = document.getElementById("inputName");
  let inputCountry = document.getElementById("inputCountry");
  let inputDate = document.getElementById("inputDate");
  //get the values from the form
  let newName = inputName.value;
  let newCountry = inputCountry.value;
  let newDate = inputDate.value;
  //create new item
  var typeNode = data.createElement("type");
  typeNode.setAttribute("id", newId);

  var nameNode = document.createElement("name");
  nameNode.innerText = name;
  nameNode.removeAttribute("xmlns");

  var countryNode = document.createElement("countryoforigin");
  countryNode.innerText = country;
  countryNode.setAttribute("continent", continent);
  countryNode.removeAttribute("xmlns");

  var dateNode = document.createElement("time");
  dateNode.innerText = date;
  dateNode.setAttribute("age", age);
  dateNode.removeAttribute("xmlns");

  typeNode.appendChild(nameNode);
  typeNode.appendChild(countryNode);
  typeNode.appendChild(dateNode);

  data.getElementsByTagName("types")[0].appendChild(typeNode);
  
  let table = $("#typesTable").find("tbody");

  var id = tracksCount-1;
  table.append(
    "<tr>" +
    "<td class='text-center'>" + typeNode.childNodes[0].textContent + "</td>" +
    "<td class='text-center'>" + typeNode.childNodes[1].getAttribute('continent') + "</td>" +
    "<td class='text-center'>" + typeNode.childNodes[1].textContent + "</td>" +
    "<td class='text-center'>" + typeNode.childNodes[2].getAttribute('age') + "</td>" +
    "<td class='text-center'>" + typeNode.childNodes[2].textContent + "</td>" +
    "<td class='text-center font-weight-bold'>" + "<button class='btn btn-primary' onclick='deleteType( " + id + ")'>Delete</button>" + "</td>" +
    "</tr>"
  );
}

//////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////ALBUM//////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////
function updateAlbumsList() {
  let typesListDiv = $('#albumsList');

  //remove all elements
  while (typesListDiv.firstChild) {
    typesListDiv.removeChild(typesListDiv.firstChild);
  }

  let table = $("#albumsTable").find("tbody");

  table.empty();

  var path = ""

  if (data.evaluate) {

    path = "/albumCollection/albums/album"
    var album = data.evaluate(path, data, null, XPathResult.ANY_TYPE, null);
    var albumResult = album.iterateNext();

    path = "/albumCollection/albums/album/author"
    var author = data.evaluate(path, data, null, XPathResult.ANY_TYPE, null);
    path = "/albumCollection/albums/album/title"
    var title = data.evaluate(path, data, null, XPathResult.ANY_TYPE, null);
    path = "/albumCollection/albums/album/releasedate"
    var releasedate = data.evaluate(path, data, null, XPathResult.ANY_TYPE, null);
    path = "/albumCollection/albums/album/price"
    var price = data.evaluate(path, data, null, XPathResult.ANY_TYPE, null);

    var authorResult = author.iterateNext();
    var titleResult = title.iterateNext();
    var releaseResult = releasedate.iterateNext();
    var priceResult = price.iterateNext();
    var id = 0;
    var albumID;
    while (authorResult) {

      console.log(id);
      albumID = albumResult.getAttribute('id');
      table.append(
        "<tr id='albumTable'>" +
        "<td class='text-center'>" + authorResult.childNodes[0].textContent + "</td>" +
        "<td class='text-center'>" + titleResult.childNodes[0].textContent + "</td>" +
        "<td class='text-center'>" + releaseResult.childNodes[0].textContent + "</td>" +
        "<td class='text-center'>" + priceResult.childNodes[0].textContent + 'zł' + "</td>" +
        "<td class='text-center'>" + "<button class='btn btn-primary' onclick='deleteAlbum( " + id + ")'>Delete</button>" + "</td>" +
        "<td class='text-center'>" + "<button class='btn btn-primary' onclick='showAlbum(" + id + ")'>Show</button>" + "</td>" +
        "</tr>"
      );


      albumResult = album.iterateNext();
      authorResult = author.iterateNext();
      titleResult = title.iterateNext();
      releaseResult = releasedate.iterateNext();
      priceResult = price.iterateNext();
      id++;
    }
  }
}

///////////////////////////Delete album///////////////////////////////////////
function deleteAlbum(index) {
  var z = data.getElementsByTagName("album")[index];

  console.log(index);
  z.parentNode.removeChild(z);
  updateAlbumsList();
}

function addAlbum()
{
  var author = $('#inputAuthor').val();
  var title = $('#inputTitle').val();
  var release = $('#inputRelease').val();
  var price = $('#inputPrice').val();
  
  var albumsCount = data.getElementsByTagName("albums")[0].childElementCount;

  var newId = data.getElementsByTagName("albums")[0].childNodes[(albumsCount*2)-1].getAttribute('id');

  newId = newId.replace('A','');
  newId++;
  newId = "A"+newId;

  albumsCount++;
  //create new item
  var albumNode = data.createElement("album");
  albumNode.setAttribute("id", newId);

  var authorNode = document.createElement("author");
  authorNode.innerText = author;
 
  var titleNode = document.createElement("title");
  titleNode.innerText = title;

  var topsongNode = document.createElement("topsong");

  var topSongTitleNode = document.createElement("title");
  var lenghteNode = document.createElement("lenght");
  var producerNode = document.createElement("producer");
  var trackNumbereNode = document.createElement("trackNumber");

  topsongNode.appendChild(topSongTitleNode);
  topsongNode.appendChild(lenghteNode);
  topsongNode.appendChild(producerNode);
  topsongNode.appendChild(trackNumbereNode);

  var releaseNode = document.createElement("releasedate");
  releaseNode.innerText = release;

  var priceNode = document.createElement("price");
  priceNode.innerText = price;
  priceNode.setAttribute("currency", 'zł');

  
  var tracksNode = document.createElement("tracks");



  albumNode.appendChild(authorNode);
  albumNode.appendChild(titleNode);
  albumNode.appendChild(topsongNode);
  albumNode.appendChild(releaseNode);
  albumNode.appendChild(priceNode);
  albumNode.appendChild(tracksNode);

  data.getElementsByTagName("albums")[0].appendChild(albumNode);
  
  let table = $("#albumsTable").find("tbody");

  var id = albumsCount-1;
  console.log(id+'dodany')
  table.append(
    "<tr>" +
    "<td class='text-center'>" + albumNode.childNodes[0].textContent + "</td>" +
    "<td class='text-center'>" + albumNode.childNodes[1].textContent + "</td>" +
    "<td class='text-center'>" + albumNode.childNodes[3].textContent + "</td>" +
    "<td class='text-center'>" + albumNode.childNodes[4].textContent+'zł' + "</td>" +
    "<td class='text-center font-weight-bold'>" + "<button class='btn btn-primary' onclick='deleteAlbum( " + id + ")'>Delete</button>" + "</td>" +
    "<td class='text-center font-weight-bold'>" + "<button class='btn btn-primary' onclick='showAlbum(" + id + ")'>Show</button>" + "</td>" +

    "</tr>"
  );
}

/////////////////////////Show album//////////////////////////////////////////////
function showAlbum(id) {
  id++;
  window.location = 'album.html?a=' + id;
}



/////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////SAVE//////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
function saveFile() {

  var xmlObject = new XMLSerializer().serializeToString(data.documentElement);
  var header = '<?xml version="1.0" encoding="UTF-8" standalone="no"?>' + '\n';
  var text = header.concat(xmlObject);

  while (text.search('xmlns="http://www.w3.org/1999/xhtml"') > 0) {
    text = text.replace(' xmlns="http://www.w3.org/1999/xhtml"', '');

  }
  var blob = new Blob([text], { type: "text/xml" });
  saveAs(blob, "test.xml");
}



//////////////////////////////////////////////////////////////////
/////////////////////////PDF//////////////////////////////////////
//////////////////////////////////////////////////////////////////
function saveToPDF() {
  var pdf = new jsPDF('p', 'pt', 'letter');
  var source = $('#typesList')[0];

  var specialElementHandlers = {
      '#bypassme': function (element, renderer) {
          return true
      }
  };
  var margins = {
      top: 20,
      bottom: 0,
      left: 80,
      rigt: 0,
      width: 800
  };
  pdf.fromHTML(
  source, // HTML string or DOM elem ref.
  margins.left, // x coord
  margins.top, { // y coord
      'width': margins.width, // max width of content on PDF
      'elementHandlers': specialElementHandlers
  },

  function (dispose) {
      pdf.save('Test.pdf');
  }, margins);
}