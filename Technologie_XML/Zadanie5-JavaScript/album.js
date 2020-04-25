"use strict"

var url_string = this.window.location.href
var url = new URL(url_string);
let ID = url.searchParams.get("a");


var data;
var xhttp = new XMLHttpRequest();
xhttp.onreadystatechange = function () {
  if (this.readyState == 4 && this.status == 200) {
    loadData(this);
    findAlbum();
  }
};
xhttp.open("GET", "Albumy.xml", true);
xhttp.send();

function loadData(xml) {
  data = xml.responseXML;
}

var albumObject;

function findAlbum() {

  var path = "/albumCollection/albums/album";
  var album = data.evaluate(path, data, null, XPathResult.ANY_TYPE, null);
  var albumResult = album.iterateNext();
  console.log(albumResult.getAttribute('id') + 'start');

  var isFind = false;
  var albumID;
  albumID = albumResult.getAttribute('id');
  while (albumResult && isFind == false) {
    if (albumID == 'A' + ID) {
      albumObject = albumResult;
      isFind = true;
    }
    else if (isFind == false) {
      console.log(albumID + 'id');
      console.log(albumResult.getAttribute('id'));
      albumResult = album.iterateNext();
      albumID = albumResult.getAttribute('id');
    }
  }
  updateAlbumData();
  updateTopSong();
  updateTrackList();
}

function updateAlbumData() {

  var path = "/albumCollection/types/type";
  var type = data.evaluate(path, data, null, XPathResult.ANY_TYPE, null);
  var typeResult = type.iterateNext();

  var isFind = false;
  var albumID = albumObject.getAttribute('typeId');
  var name;

  while (typeResult && isFind == false) {
    if (albumID == typeResult.getAttribute('id')) {
      name = typeResult.getElementsByTagName("name")[0].textContent;
      isFind = true;
    }
    typeResult = type.iterateNext();
  }

  let table = $("#albumTable").find("tbody");

  table.empty();
  table.append(
    "<tr id='albumTable'>" +
    "<td class='text-center font-weight-bold'>" + "AUTHOR" + "</td>" +
    "<td class='text-center '>" + albumObject.getElementsByTagName("author")[0].textContent + "</td>" +
    "</tr>",
    "<tr id='albumTable'>" +
    "<td class='text-center font-weight-bold'>" + "TITLE" + "</td>" +
    "<td class='text-center'>" + albumObject.getElementsByTagName("title")[0].textContent + "</td>" +
    "</tr>",
    "<tr id='albumTable'>" +
    "<td class='text-center font-weight-bold'>" + "TYPE" + "</td>" +
    "<td class='text-center '>" + name + "</td>" +
    "</tr>",
    "<tr id='albumTable'>" +
    "<td class='text-center font-weight-bold'>" + "RELEASE DATE" + "</td>" +
    "<td class='text-center'>" + albumObject.getElementsByTagName("releasedate")[0].textContent + "</td>" +
    "</tr>",
    "<tr id='albumTable'>" +
    "<td class='text-center font-weight-bold'>" + "PRICE" + "</td>" +
    "<td class='text-center'>" + albumObject.getElementsByTagName("price")[0].textContent + 'zł' + "</td>" +
    "</tr>"
  );
}

function updateTopSong() {

  let table = $("#topSongTable").find("tbody");

  table.empty();
  table.append(
    "<tr id='topSongTable'>" +
    "<td class='text-center font-weight-bold'>" + "TITLE" + "</td>" +
    "<td class='text-center '>" + albumObject.getElementsByTagName("topsong")[0].childNodes[1].textContent + "</td>" +
    "</tr>",
    "<tr id='topSongTable'>" +
    "<td class=' text-center font-weight-bold'>" + "LENGHT" + "</td>" +
    "<td class='text-center'>" + albumObject.getElementsByTagName("topsong")[0].childNodes[3].textContent + "</td>" +
    "</tr>",
    "<tr id='topSongTable'>" +
    "<td class='text-center font-weight-bold'>" + "PRODUCER" + "</td>" +
    "<td class='text-center'>" + albumObject.getElementsByTagName("topsong")[0].childNodes[5].textContent + "</td>" +
    "</tr>",
    "<tr id='topSongTable'>" +
    "<td class='text-center font-weight-bold'>" + "TRACK NUMBER" + "</td>" +
    "<td class='text-center'>" + albumObject.getElementsByTagName("topsong")[0].childNodes[7].textContent + 'zł' + "</td>" +
    "</tr>"
  );
}


function updateTrackList() {

  var tracksCount = albumObject.getElementsByTagName("tracks")[0].childElementCount;

  let table = $("#trackTable").find("tbody");

  var trackID = 3;
  var infoId = 1;
  for (var i = 0; i < tracksCount - 1; i++) {
    table.append(
      "<tr id='albumTable'>" +
      "<td class='text-center font-weight-bold'>" + albumObject.getElementsByTagName("tracks")[0].childNodes[trackID].childNodes[infoId].textContent + "</td>" +
      "<td>" + albumObject.getElementsByTagName("tracks")[0].childNodes[trackID].childNodes[infoId + 2].textContent + "</td>" +
      "<td>" + albumObject.getElementsByTagName("tracks")[0].childNodes[trackID].childNodes[infoId + 4].textContent + "</td>" +
      "</tr>"
    );
    trackID += 2;
  }

}


function back() {
  window.location = 'index.html';
}