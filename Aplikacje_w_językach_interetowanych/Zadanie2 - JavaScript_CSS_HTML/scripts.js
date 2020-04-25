"use strict"
let todoList = [];

$.ajax({
  // copy Your bin identifier here. It can be obtained in the dashboard
  url: 'https://api.jsonbin.io/b/5dad5c9abecada6d1ddf45b8/latest',
  type: 'GET',
  headers: { //Required only if you are trying to access a private bin
    'secret-key': "$2b$10$srm5Q4PYvndj8lF6CZu/2enttyeciy17UTkDuwC7jI8MdEXpsn5.K"
  },
  success: (data) => {
    //console.log(data);
    todoList = data;
  },
  error: (err) => {
    console.log(err.responseJSON);
  }
});


let updateJSONbin = function () {
  $.ajax({
    url: 'https://api.jsonbin.io/b/5dad5c9abecada6d1ddf45b8',
    type: 'PUT',
    headers: { //Required only if you are trying to access a private bin
      'secret-key': "$2b$10$srm5Q4PYvndj8lF6CZu/2enttyeciy17UTkDuwC7jI8MdEXpsn5.K"
    },
    contentType: 'application/json',
    data: JSON.stringify(todoList),
    success: (data) => {
      console.log(data);
    },
    error: (err) => {
      console.log(err.responseJSON);
    }
  });
}

//initList();

let updateTodoList = function () {
  // let todoListDiv = document.getElementById("todoListView");
  let todoListDiv = $('#todoListView');

  //remove all elements
  while (todoListDiv.firstChild) {
    todoListDiv.removeChild(todoListDiv.firstChild);
  }

  //add all elements
  let filterInput = document.getElementById("inputSearch");
  let dateFrom = document.getElementById("dateFrom").value;
  let dateTo = document.getElementById("dateTo").value;
  let table = $("#todoTable").find("tbody");

  table.empty();

  for (let todo in todoList) {
    if (
      (filterInput.value == "") ||
      (todoList[todo].title.includes(filterInput.value)) ||
      (todoList[todo].description.includes(filterInput.value))
    ) {

      if (todoList[todo].dueDate >= dateFrom && todoList[todo].dueDate <= dateTo) {
        let newElement = document.createElement("p");
        let newContent = document.createTextNode(todoList[todo].title + " " +
          todoList[todo].description);

        //table
        table.append(
          "<tr>" +
          "<td>" + todoList[todo].title + "</td>" +
          "<td>" + todoList[todo].description + "</td>" +
          "<td>" + todoList[todo].place + "</td>" +
          "<td>" + todoList[todo].dueDate + "</td>" +
          "<td>" + "<button onclick='deleteTodo(" + todo + ")'>Delete</button>" + "</td>" +
          "</tr>"
        );
      }

    }
  }
}

setInterval(updateTodoList, 1000);

let deleteTodo = function (index) {
  todoList.splice(index, 1);
  updateJSONbin();
  updateTodoList();
}

let addTodo = function () {
  //get the elements in the form
  let inputTitle = document.getElementById("inputTitle");
  let inputDescription = document.getElementById("inputDescription");
  let inputPlace = document.getElementById("inputPlace");
  let inputDate = document.getElementById("inputDate");
  //get the values from the form
  let newTitle = inputTitle.value;
  let newDescription = inputDescription.value;
  let newPlace = inputPlace.value;
  let newDate = new Date(inputDate.value);
  //create new item
  let newTodo = {
    title: newTitle,
    description: newDescription,
    place: newPlace,
    dueDate: newDate
  };
  //add item to the list
  todoList.push(newTodo);
  updateJSONbin();
  updateTodoList();

}
