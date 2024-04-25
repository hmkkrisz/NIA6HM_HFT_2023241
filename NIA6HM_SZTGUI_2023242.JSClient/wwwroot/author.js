
let authors = [];
let connection = null;
let authorIdToUpdate = -1;
getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:58339/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("AuthorCreated", (user, message) => {
        getdata();
    });

    connection.on("AuthorDeleted", (user, message) => {
        getdata();
    });
    connection.on("AuthorUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:58339/author')
        .then(x => x.json())
        .then(y => {
            authors = y;
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    authors.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.authorId + "</td><td>"
            + t.name + "</td><td>" +
            `<button type="button" onclick="remove(${t.authorId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.authorId})">Update</button>`
            + "</td></tr>";
    });
}
function showupdate(id) {
    document.getElementById('authornametoupdate').value = authors.find(t => t['authorId'] == id)['name'];
    document.getElementById('updateformdiv').style.display = 'flex';
    authorIdToUpdate = id;
}
function remove(id) {
    fetch('http://localhost:58339/author/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let name = document.getElementById('authorname').value;
    fetch('http://localhost:58339/author/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('authornametoupdate').value;
    fetch('http://localhost:58339/author/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, authorId: authorIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}
