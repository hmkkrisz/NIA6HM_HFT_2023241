
let comments = [];
let connection = null;
let commentIdToUpdate = -1;
getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:58339/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CommentCreated", (user, message) => {
        getdata();
    });

    connection.on("CommentDeleted", (user, message) => {
        getdata();
    });
    connection.on("CommentUpdated", (user, message) => {
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
    await fetch('http://localhost:58339/comment')
        .then(x => x.json())
        .then(y => {
            comments = y;
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    comments.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.commentId + "</td><td>"
            + t.text + "</td><td>" +
            `<button type="button" onclick="remove(${t.commentId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.commentId})">Update</button>`
            + "</td></tr>";
    });
}
function showupdate(id) {
    document.getElementById('commentnametoupdate').value = comments.find(t => t['commentId'] == id)['text'];
    document.getElementById('updateformdiv').style.display = 'flex';
    commentIdToUpdate = id;
}
function remove(id) {
    fetch('http://localhost:58339/comment/' + id, {
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
    let name = document.getElementById('commentname').value;
    fetch('http://localhost:58339/comment/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { text: name })
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
    let name = document.getElementById('commentnametoupdate').value;
    fetch('http://localhost:58339/comment/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { text: name, commentId: commentIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}
